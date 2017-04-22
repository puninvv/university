using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib.Workers.UserRabbitMQWorker;

namespace RabbitMQCommonLib.Client.UserRabbitMQClient
{
    public class UserRabbitMQClient : RabbitMQClientBase, IClient<User>
    {
        private BytesSerializer<User> m_serializer = new BytesSerializer<User>();

        public UserRabbitMQClient(string _hostName = "localhost", string _username = null, string _password = null, int _port = -1)
            : base(_hostName, _username, _password, _port) { }

        public User GetResponce(User _data, int _timeout = 10000)
        {
            var requestBytes = m_serializer.ObjectToByteArray(_data);

            var responce = base.SendRequest(requestBytes, _timeout);

            return m_serializer.ByteArrayToObject(responce);
        }
    }
}
