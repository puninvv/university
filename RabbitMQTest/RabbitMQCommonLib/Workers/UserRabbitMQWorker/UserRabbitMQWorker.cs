using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers.UserRabbitMQWorker
{
    public class UserRabbitMQWorker : RabbitMQWorkerBase
    {
        private BytesSerializer<User> m_serializer = new BytesSerializer<User>();

        public UserRabbitMQWorker(string _hostName = "localhost", string _username = null, string _password = null, int _port = -1)
            : base(_hostName, _username, _password, _port) { }

        protected override byte[] ProcessRequest(byte[] _message)
        {
            var request = m_serializer.ByteArrayToObject(_message);
            var responce = new User() { Name = request.Name + "_responce", Age = request.Age + 10 };
            return m_serializer.ObjectToByteArray(responce);
        }
    }
}
