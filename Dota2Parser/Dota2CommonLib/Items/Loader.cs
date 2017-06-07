using Dota2CommonLib.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Items
{
    public class ItemsLoader : LoaderBase
    {
        protected override string Uri
        {
            get
            {
                return @"https://api.steampowered.com/IEconDOTA2_570/GetGameItems/V001/?key=";
            }
        }

        public List<Item> LoadItems(string _apiKey)
        {
            var response = base.Load<Response>(_apiKey);
            return response != null ? response.Result.Items.ToList() : new List<Item>();
        }
    }
}
