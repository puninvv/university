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
                    Console.WriteLine("Task has type {0}, created ToGrayScaleWorker", _task.TaskType);
                    return new ToGrayScaleRabbitMQWorker();
                case RabbitMQTaskType.DetectEdges:
                    Console.WriteLine("Task has type {0}, created DetectEdgesWorker", _task.TaskType);
                    return new DetectEdgesRabbitMQWorker();
                case RabbitMQTaskType.GaussianBlur:
                    Console.WriteLine("Task has type {0}, created GaussianBlurWorker", _task.TaskType);
                    return new GaussianBlurRabbitMQWorker();
                default:
                    return new DefaultRabbitMQWorker();
            }
        }
    }
}
