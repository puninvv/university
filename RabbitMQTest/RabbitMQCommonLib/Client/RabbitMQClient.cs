using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Client
{
    public class RabbitMQClient : IDisposable
    {
        private IConnection m_connection;
        private IModel m_channel;
        private string m_replyQueueName;
        private QueueingBasicConsumer m_consumer;
        private string m_queueName;

        public class RabbitMQClientEventArgs : EventArgs
        {
            public double Progress
            {
                get;
                private set;
            }

            public RabbitMQTask CompletedTask
            {
                get;
                private set;
            }

            public RabbitMQClientEventArgs(double _progress, RabbitMQTask _completedTask)
            {
                Progress = _progress;
                CompletedTask = _completedTask;
            }

            public override string ToString()
            {
                return string.Format("{3}%\t[task: Id={0} Type={1} Result={2}]", CompletedTask.Id, CompletedTask.TaskType, CompletedTask.ResultType, Progress);
            }
        }

        public event EventHandler<RabbitMQClientEventArgs> OnTaskProcessed;

        public RabbitMQClient(string _hostName = null, string _username = null, string _password = null, int _port = -1, string _queueName = null)
        {
            var factory = new ConnectionFactory();

            factory.HostName = string.IsNullOrEmpty(_hostName) ? "localhost" : _hostName;
            
            if (!string.IsNullOrEmpty(_username))
                factory.UserName = _username;

            if (!string.IsNullOrEmpty(_password))
                factory.Password = _password;

            if (_port != -1)
                factory.Port = _port;

            m_queueName = _queueName ?? Properties.Settings.Default.DefaultQueueName;

            m_connection = factory.CreateConnection();
            m_channel = m_connection.CreateModel();

            m_replyQueueName = m_channel.QueueDeclare().QueueName;
            m_consumer = new QueueingBasicConsumer(m_channel);
            m_channel.BasicConsume(queue: m_replyQueueName, noAck: true, consumer: m_consumer);
        }

        protected void SendRequest(byte[] _request, string _corrId)
        {
            var props = m_channel.CreateBasicProperties();
            props.ReplyTo = m_replyQueueName;
            props.CorrelationId = _corrId;

            m_channel.BasicPublish(exchange: "", routingKey: m_queueName, basicProperties: props, body: _request);
        }

        protected byte[] WaitForNextRequest(string _corrId, int _timeout = 10000)
        {
            var timeStart = DateTime.Now;

            while (true)
            {
                var eventArgs = m_consumer.Queue.Dequeue();
                if (eventArgs.BasicProperties.CorrelationId.Equals(_corrId))
                    return eventArgs.Body;

                Thread.Sleep(1000);

                if (((DateTime.Now) - timeStart).Milliseconds > _timeout)
                    return null;
            }
        }


        public RabbitMQMessage GetResponce(RabbitMQMessage _tasks, Guid _corrId, int _timeout = 10000)
        {
            var tasksCount = _tasks.Tasks.Count;
            var completedTasksCount = 0;

            var taskSerializer = new BytesSerializer<RabbitMQMessage>();
            var requestBytes = taskSerializer.ObjectToByteArray(_tasks);

            var startTime = DateTime.Now;

            SendRequest(requestBytes, _corrId.ToString());

            while ((DateTime.Now - startTime).Milliseconds < _timeout)
            {
                var responce = WaitForNextRequest(_corrId.ToString(), _timeout);
                var resultSerializer = new BytesSerializer<RabbitMQMessage>();
                var taskResult = resultSerializer.ByteArrayToObject(responce);

                completedTasksCount++;

                OnTaskProcessed?.Invoke(this, new RabbitMQClientEventArgs(100.0 * completedTasksCount / tasksCount, taskResult.Tasks.Dequeue()));

                if (completedTasksCount == tasksCount)
                    return taskResult;
            }

            return null;
        }

        #region IDisposable Support
        private bool disposedValue = false; // Для определения избыточных вызовов

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_connection.Dispose();
                    m_channel.Dispose();
                }

                m_replyQueueName = null;
                m_consumer = null;

                disposedValue = true;
            }
        }

        ~RabbitMQClient()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
