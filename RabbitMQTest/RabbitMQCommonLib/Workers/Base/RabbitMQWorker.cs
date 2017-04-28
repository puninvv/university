using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib;

namespace RabbitMQCommonLib.Workers
{
    public class RabbitMQWorker : IDisposable
    {
        private IConnection m_connection;
        private IModel m_channel;
        private EventingBasicConsumer m_consumer;

        private BytesSerializer<RabbitMQMessage> m_messageSerializer = new BytesSerializer<RabbitMQMessage>();

        public RabbitMQWorker(string _hostName = null, string _username = null, string _password = null, int _port = -1, string _queueName = null)
        {
            var factory = new ConnectionFactory();

            factory.HostName = string.IsNullOrEmpty(_hostName) ? "localhost" : _hostName;

            if (!string.IsNullOrEmpty(_username))
                factory.UserName = _username;

            if (!string.IsNullOrEmpty(_password))
                factory.Password = _password;

            if (_port != -1)
                factory.Port = _port;

            m_connection = factory.CreateConnection();
            m_channel = m_connection.CreateModel();

            if (string.IsNullOrEmpty(_queueName))
                _queueName = Properties.Settings.Default.DefaultQueueName;

            m_channel.QueueDeclare(queue: _queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            foreach (var item in Enum.GetValues(typeof(RabbitMQTaskType)))
                m_channel.QueueDeclare(queue: item.ToString(), durable: false, exclusive: false, autoDelete: false, arguments: null);

            m_channel.BasicQos(0, 1, false);

            m_consumer = new EventingBasicConsumer(m_channel);
            m_consumer.Received += OnReceived;

            m_channel.BasicConsume(queue: _queueName, noAck: false, consumer: m_consumer);
            foreach (var item in Enum.GetValues(typeof(RabbitMQTaskType)))
                m_channel.BasicConsume(queue: item.ToString(), noAck: false, consumer: m_consumer);
        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body;
            var props = e.BasicProperties;
            var replyProps = m_channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;
            replyProps.ReplyTo = props.ReplyTo;

            var resultBytes = e.Body;
            var resultType = RabbitMQTaskResultType.Undefined;

            RabbitMQTask task = null;
            RabbitMQMessage message = null;

            try
            {
                message = m_messageSerializer.ByteArrayToObject(body);

                if (message.Tasks.Count != 0)
                {
                    task = message.Tasks.Dequeue();

                    var worker = RabbitMQWorkersFactory.GetWorker(task.TaskType);
                    resultBytes = worker.ProcessTask(message.Data);
                }

                resultType = RabbitMQTaskResultType.Success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(" [.] " + ex.Message);
                resultType = RabbitMQTaskResultType.Failed;
            }
            finally
            {
                task.ResultType = resultType;
                var result = new RabbitMQMessage(resultBytes, task);

                m_channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: m_messageSerializer.ObjectToByteArray(result));
                m_channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);

                if (message.Tasks.Count != 0)
                {
                    var nextTask = new RabbitMQMessage(resultBytes, message.Tasks);
                    var nextTaskType = message.Tasks.Peek().TaskType;

                    m_channel.BasicPublish(exchange: "", routingKey: nextTaskType.ToString(), basicProperties: replyProps, body: m_messageSerializer.ObjectToByteArray(nextTask));
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    m_consumer.Received -= OnReceived;
                    m_channel.Dispose();
                    m_connection.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
