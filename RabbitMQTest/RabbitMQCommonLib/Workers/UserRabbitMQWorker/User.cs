using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers.UserRabbitMQWorker
{
    [Serializable]
    public class User
    {
        public string Name
        {
            get;
            set;
        }

        public int Age
        {
            get;
            set;
        }

        public override string ToString()
        {
            return "{User: Name=" + Name + "; Age=" + Age + "}";
        }
    }
}
