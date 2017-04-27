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
        public static IRabbitMQWorker GetWorker(RabbitMQTaskType _taskType)
        {
            switch (_taskType)
            {
                case RabbitMQTaskType.ToGrayScale:
                    Console.WriteLine("Task has type {0}, created ToGrayScaleWorker", _taskType);
                    return new ToGrayScaleRabbitMQWorker();
                case RabbitMQTaskType.DetectEdges:
                    Console.WriteLine("Task has type {0}, created DetectEdgesWorker", _taskType);
                    return new DetectEdgesRabbitMQWorker();
                case RabbitMQTaskType.GaussianBlur:
                    Console.WriteLine("Task has type {0}, created GaussianBlurWorker", _taskType);
                    return new GaussianBlurRabbitMQWorker();
                default:
                    Console.WriteLine("Cannot create worker for task type {0}", _taskType);
                    return null;
            }
        }
    }
}
