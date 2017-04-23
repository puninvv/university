using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib
{
    [Serializable]
    public class RabbitMQTask
    {
        public RabbitMQTaskType TaskType
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

    public enum RabbitMQTaskType
    {
        ToGrayScale,
        DetectEdges
    }
}
