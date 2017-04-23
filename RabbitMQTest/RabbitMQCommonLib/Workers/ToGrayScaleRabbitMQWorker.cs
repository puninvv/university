using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCommonLib.Workers;

namespace RabbitMQCommonLib.Workers.ImageRabbitMQWorker
{
    internal class ToGrayScaleRabbitMQWorker : IRabbitMQWorker
    {
        public byte[] ProcessTask(RabbitMQTask _task)
        {
            return _task.Data;
        }
    }
}
