using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib
{
    [Serializable]
    public class RabbitMQTaskResult
    {
        public RabbitMQTaskResultType ResultType
        {
            get;
            set;
        }

        public byte[] Data
        {
            get;
            set;
        }
    }

    public enum RabbitMQTaskResultType
    {
        Success,
        Failed
    }
}
