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

        public RabbitMQClient(string _hostName = "localhost", string _username = null, string _password = null, int _port = -1)
        {
            var factory = new ConnectionFactory() { HostName = _hostName };

            if (_username != null)
                factory.UserName = _username;

            if (_password != null)
                factory.Password = _password;

            if (_port != -1)
                factory.Port = _port;

            m_connection = factory.CreateConnection();
            m_channel = m_connection.CreateModel();
            m_replyQueueName = m_channel.QueueDeclare().QueueName;
            m_consumer = new QueueingBasicConsumer(m_channel);
            m_channel.BasicConsume(queue: m_replyQueueName, noAck: true, consumer: m_consumer);
        }

        protected byte[] SendRequest(byte[] _request, int _timeout = 10000)
        {
            var corrId = Guid.NewGuid().ToString();
            var props = m_channel.CreateBasicProperties();
            props.ReplyTo = m_replyQueueName;
            props.CorrelationId = corrId;

            m_channel.BasicPublish(exchange: "", routingKey: "rpc_queue", basicProperties: props, body: _request);

            var timeStart = DateTime.Now;

            while (true)
            {
                var eventArgs = m_consumer.Queue.Dequeue();
                if (eventArgs.BasicProperties.CorrelationId.Equals(corrId))
                    return eventArgs.Body;

                Thread.Sleep(100);

                if (((DateTime.Now) - timeStart).Milliseconds > _timeout)
                    return null;
            }
        }

        public RabbitMQTaskResult GetResponce(RabbitMQTask _task, int _timeout = 10000)
        {
            var taskSerializer = new BytesSerializer<RabbitMQTask>();
            var requestBytes = taskSerializer.ObjectToByteArray(_task);

            var responce = SendRequest(requestBytes, _timeout);

            var resultSerializer = new BytesSerializer<RabbitMQTaskResult>();
            return resultSerializer.ByteArrayToObject(responce);
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
