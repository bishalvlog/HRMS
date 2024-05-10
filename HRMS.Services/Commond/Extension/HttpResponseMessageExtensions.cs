using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Commond.Extension
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TTarget> DeserializeToJsonAsync<TTarget> (this HttpResponseMessage httpResponseMessage)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync ();
            return JsonConvert.DeserializeObject<TTarget>(responseString);
        }

       public static async Task<(TSuccessType success, TErrorType errorType)> DeserializeToJsonAsync<TSuccessType,TErrorType> (this  HttpResponseMessage httpResponseMessage)
        {
            var responseString = await httpResponseMessage.Content.ReadAsStringAsync();

            if(httpResponseMessage.IsSuccessStatusCode)
            {
                var successResObj =  JsonConvert.DeserializeObject<TSuccessType>(responseString);
                return (successResObj, default);
            }
            var errorObj =  JsonConvert.DeserializeObject<TErrorType>(responseString);
            return(default,errorObj);
        }
    }
}
