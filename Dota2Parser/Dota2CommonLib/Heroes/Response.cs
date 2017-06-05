using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Heroes
{
    [DataContract]
    internal class Response
    {
        [DataMember(Name = "result")]
        public Result Result
        {
            get; set;
        }
    }
}
