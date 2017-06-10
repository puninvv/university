using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Matches
{
    public class MatchesLoader : LoaderBase
    {
        protected override string Uri
        {
            get
            {
                return @"https://api.steampowered.com/IDOTA2Match_570/GetMatchHistory/V001/?key=";
            }
        }

        public List<Match> LoadMatches(string _apiKey)
        {
            var response = base.Load<Response>(_apiKey);
            return response != null ? response.Result.Matches.ToList() : new List<Match>();
        }
    }
}
