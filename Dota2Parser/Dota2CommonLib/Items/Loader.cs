using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Items
{
    public class ItemsLoader
    {
        private const string m_urlPrefix = @"https://api.steampowered.com/IEconDOTA2_570/GetGameItems/V001/?key=";
        private const string m_urlPostfix = @"&language=en_en";

        public List<Item> LoadItems(string _apiKey)
        {
            try
            {
                var requestUrl = m_urlPrefix + _apiKey + m_urlPostfix;

                var request = WebRequest.Create(requestUrl) as HttpWebRequest;
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format(
                        "Server error (HTTP {0}: {1}).",
                        response.StatusCode,
                        response.StatusDescription));

                    var jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                    var objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                    var jsonResponse = objResponse as Response;

                    return jsonResponse.Result.Items.ToList();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
