using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers
{
    internal class DefaultRabbitMQWorker : IRabbitMQWorker
    {
        public byte[] ProcessTask(RabbitMQTask _task)
        {
            return _task.Data;
        }
    }
}
