using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Commond.Extension
{
    public static class HttpClientExtensions
    {
        public static HttpClient SetHeaders(this HttpClient client,IEnumerable <KeyValuePair<string, string>> headers)
        {
            if(headers is null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            foreach(var headerkey in headers)
            {
                if (client.DefaultRequestHeaders.TryGetValues(headerkey.Key, out var _))
                    client.DefaultRequestHeaders.Remove(headerkey.Key);

                client.DefaultRequestHeaders.Add(headerkey.Key, headerkey.Value);
            }
            return client;
        }
        public static HttpClient SetHeaders(this HttpClient client , Dictionary<string, string> headers)
        {
            if(client is null)
                throw new ArgumentNullException (nameof(client));

            foreach(var headerkey in headers)
            {
                if (client.DefaultRequestHeaders.TryGetValues(headerkey.Key, out var _))
                    client.DefaultRequestHeaders.Remove(headerkey.Key);


                client.DefaultRequestHeaders.Add(headerkey.Key, headerkey.Value);
            }
            return client;
        }
    }
}
