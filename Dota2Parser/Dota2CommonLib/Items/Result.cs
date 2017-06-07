using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Items
{
    [DataContract]
    internal class Result
    {
        [DataMember(Name = "items")]
        public Item[] Items
        {
            get; set;
        }
        [DataMember(Name = "status")]
        public int Status
        {
            get; set;
        }
    }
}
