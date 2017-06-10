using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Matches
{
    [DataContract]
    public class Match
    {
        /// <summary>The match's unique ID</summary>
        [DataMember(Name = "match_id")]
        public uint Id
        {
            get;
            set;
        }

        ///<summary>The sequence number representing the order in which matches were recorded</summary>
        [DataMember(Name = "match_seq_num")]
        public uint SequenceNumber
        {
            get;
            set;
        }

        ///<summary>Unix timestamp of when the match began</summary>
        [DataMember(Name = "start_time")]
        public uint StartTime
        {
            get;
            set;
        }

        ///<summary>The match's lobby type</summary>
        [DataMember(Name = "lobby_type")]
        public LobbyType Type
        {
            get;
            set;
        }

        ///<summary>Team ID for the Radiant</summary>
        [DataMember(Name = "radiant_team_id")]
        public uint RadiantTeamId
        {
            get;
            set;
        }

        ///<summary>Team ID for the Dire</summary>
        [DataMember(Name = "dire_team_id")]
        public uint DireTeamId
        {
            get;
            set;
        }

        ///<summary>An array of the players for this match</summary>
        [DataMember(Name = "players")]
        public Player[] Players
        {
            get;
            set;
        }

        public enum LobbyType
        {
            Invalid = -1,
            PublicMatchmaking,
            Practice,
            Tournament,
            Tutorial,
            CoopWithBots,
            TeamMatch,
            SoloQueue,
            Ranked,
            SoloMid1VS1
        }

    }
}
