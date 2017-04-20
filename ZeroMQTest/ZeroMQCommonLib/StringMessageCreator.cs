using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZeroMQ;

namespace ZeroMQCommonLib
{
    public class StringMessageCreator : IMessageCreator
    {
        public ZMessage CreateMessage(object _data)
        {
            var result = new ZMessage();

            result.Add(new ZFrame(Encoding.UTF8.GetBytes(_data.ToString())));

            return result;
        }
    }
}
