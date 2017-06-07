using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Matches
{
    [DataContract]
    internal class Result
    {
        /// <summary>
        /// 1 if the request was a success, and 15 if we cannot get match history for a user (because the user has not allowed it)
        /// </summary>
        [DataMember(Name = "status")]
        public int Status 
        {
            get; set;
        }

        /// <summary>
        /// A message explaining the status, if the status is not 1 
        /// </summary>
        [DataMember(Name = "statusDetail")]
        public string StatusDetails
        {
            get;
            set;
        }

        /// <summary>
        /// The number of matches in the response
        /// </summary>
        [DataMember(Name = "num_results")]
        public int MatchesNumber
        {
            get;
            set;
        }

        /// <summary>
        /// The total number of matches for this query
        /// </summary>
        [DataMember(Name = "total_results")]
        public int MatchesTotal
        {
            get;
            set;
        }

        /// <summary>
        /// The total number of matches remaining for this query (MatchesTotal = MatchesRemaining + MatchesNumber)
        /// </summary>
        [DataMember(Name = "results_remaining")]
        public int MatchesRemaining
        {
            get;
            set;
        }

        /// <summary>
        /// An array of matches in this response
        /// </summary>
        [DataMember(Name = "matches")]
        public Match[] Matches
        {
            get;
            set;
        }
    }
}
