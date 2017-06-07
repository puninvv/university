using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Matches
{
    [DataContract]
    public class Player
    {
        ///<summary>32-bit Steam account ID for the player</summary>
        [DataMember(Name = "account_id")]
        public int AccountId
        {
            get;
            set;
        }

        ///<summary>8-bit unsigned integer representing the player's team and position in the team</summary>
        [DataMember(Name = "player_slot")]
        public int PlayerSlot
        {
            get;
            set;
        }

        ///<summary>ID of the hero played by the player</summary>
        [DataMember(Name = "hero_id")]
        public int HeroId
        {
            get;
            set;
        }
    }
}
