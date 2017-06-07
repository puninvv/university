using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;

namespace Dota2CommonLib.Common
{
    public abstract class LoaderBase
    {
        private const string m_uriPostfix = "&language=en_en";

        protected abstract string Uri { get; }

        protected T Load<T>(string _apiKey) where T : class
        {
            try
            {
                var requestUrl = Uri + _apiKey + m_uriPostfix;

                var request = WebRequest.Create(requestUrl) as HttpWebRequest;
                using (var response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                        throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                    var jsonSerializer = new DataContractJsonSerializer(typeof(T));
                    var objResponse = jsonSerializer.ReadObject(response.GetResponseStream());

                    return objResponse as T;
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
                return null;
            }
        }
    }
}
