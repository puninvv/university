using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Heroes
{
    public class HeroesLoader : LoaderBase
    {
        protected override string Uri
        {
            get
            {
                return @"https://api.steampowered.com/IEconDOTA2_570/GetHeroes/v0001/?key=";
            }
        }

        public List<Hero> LoadHeroes(string _apiKey)
        {
            var response = base.Load<Response>(_apiKey);
            return response != null ? response.Result.Heroes.ToList() : new List<Hero>();
        }
    }
}
