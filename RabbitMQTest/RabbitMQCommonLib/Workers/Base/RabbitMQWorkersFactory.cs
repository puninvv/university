using RabbitMQCommonLib.Workers.ImageRabbitMQWorker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers
{
    internal static class RabbitMQWorkersFactory
    {
        public static IRabbitMQWorker GetWorker(RabbitMQTask _task)
        {
            switch (_task.TaskType)
            {
                case RabbitMQTaskType.ToGrayScale:
                    return new ToGrayScaleRabbitMQWorker();
                case RabbitMQTaskType.DetectEdges:
                    return new DetectEdgesRabbitMQWorker();
                default:
                    return new DefaultRabbitMQWorker();
            }
        }
    }
}
