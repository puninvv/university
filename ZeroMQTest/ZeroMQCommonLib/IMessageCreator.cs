using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace ZeroMQCommonLib
{
    public interface IMessageCreator
    {
        ZMessage CreateMessage(object _data);
    }
}
