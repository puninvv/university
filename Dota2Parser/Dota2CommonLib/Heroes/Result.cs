using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Heroes
{
    [DataContract]
    internal class Result
    {
        [DataMember(Name = "heroes")]
        public Hero[] Heroes
        {
            get; set;
        }
        [DataMember(Name = "status")]
        public int Status
        {
            get; set;
        }
        [DataMember(Name = "count")]
        public string Count
        {
            get; set;
        }
    }
}
