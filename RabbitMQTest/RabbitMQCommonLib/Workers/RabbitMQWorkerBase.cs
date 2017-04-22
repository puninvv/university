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
    public abstract class RabbitMQWorkerBase : IDisposable
    {
        private IConnection m_connection;
        private IModel m_channel;
        private EventingBasicConsumer m_consumer;

        public RabbitMQWorkerBase(string _hostName = "localhost", string _username = null, string _password = null, int _port = -1)
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


            m_channel.QueueDeclare(queue: "rpc_queue", durable: false, exclusive: false, autoDelete: false, arguments: null);
            m_channel.BasicQos(0, 1, false);

            m_consumer = new EventingBasicConsumer(m_channel);
            m_consumer.Received += OnReceived;

            m_channel.BasicConsume(queue: "rpc_queue", noAck: false, consumer: m_consumer);
        }

        private void OnReceived(object sender, BasicDeliverEventArgs e)
        {
            byte[] responseBytes = null;

            var body = e.Body;
            var props = e.BasicProperties;
            var replyProps = m_channel.CreateBasicProperties();
            replyProps.CorrelationId = props.CorrelationId;

            try
            {
                responseBytes = ProcessRequest(body);
            }
            catch (Exception ex)
            {
                Console.WriteLine(" [.] " + ex.Message);
                responseBytes = new byte[0];
            }
            finally
            {
                m_channel.BasicPublish(exchange: "", routingKey: props.ReplyTo, basicProperties: replyProps, body: responseBytes);
                m_channel.BasicAck(deliveryTag: e.DeliveryTag, multiple: false);
            }
        }

        protected abstract byte[] ProcessRequest(byte[] _message);

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
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
