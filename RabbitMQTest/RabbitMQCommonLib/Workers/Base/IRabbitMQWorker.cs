using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQCommonLib.Workers
{
    internal interface IRabbitMQWorker
    {
        byte[] ProcessTask(byte[] _data);
    }
}
