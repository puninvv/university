using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib
{
    [Serializable]
    public class RabbitMQMessage
    {
        public Queue<RabbitMQTask> Tasks
        {
            get;
            set;
        }
        
        public byte[] Data
        {
            get;
            set;
        }

        //for deserealization only
        public RabbitMQMessage() { }

        public RabbitMQMessage(byte[] _data, RabbitMQTaskType _task, params RabbitMQTaskType[] _tasks)
        {
            Data = _data;

            Tasks = new Queue<RabbitMQTask>();
            Tasks.Enqueue(new RabbitMQTask(_task));

            if (_tasks != null && _tasks.Length != 0)
                foreach (var item in _tasks)
                    Tasks.Enqueue(new RabbitMQTask(item)); 
        }

        internal RabbitMQMessage(byte[] _data, RabbitMQTask _task)
        {
            Data = _data;

            Tasks = new Queue<RabbitMQTask>();
            Tasks.Enqueue(_task);
        }

        public RabbitMQMessage(byte[] _data, Queue<RabbitMQTask> _tasks)
        {
            Data = _data;
            Tasks = _tasks;
        }
    }

    [Serializable]
    public class RabbitMQTask
    {
        public Guid Id
        {
            get;
            set;
        }

        public RabbitMQTaskType TaskType
        {
            get;
            set;
        }

        public RabbitMQTaskResultType ResultType
        {
            get;
            set;
        }

        public RabbitMQTask() { }

        public RabbitMQTask(RabbitMQTaskType _type)
        {
            Id = Guid.NewGuid();
            TaskType = _type;
            ResultType = RabbitMQTaskResultType.Undefined;
        }

        public RabbitMQTask(RabbitMQTaskType _type, Guid _id)
            :this (_type)
        {
            Id = _id;
        }
    }

    public enum RabbitMQTaskType
    {
        ToGrayScale,
        DetectEdges,
        GaussianBlur
    }

    public enum RabbitMQTaskResultType
    {
        Undefined,
        Success,
        Failed
    }
}
