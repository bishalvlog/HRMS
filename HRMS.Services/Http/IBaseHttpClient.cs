using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HRMS.Services.Http
{
    public interface IBaseHttpClient
    {
        HttpClient CreateHttpClient();
        Task<(HttpStatusCode statusCode, T responseType)> GetAsync<T>(
            string requestUri, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, TResponseType responseType, TErrorResponseType errorResponse)>
            GetAsync<TResponseType, TErrorResponseType>(string requestUri, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, T responseType)> PostAsync<T>(
            string requestUri, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, TResponseType responseType, TErrorResponseType errorResponse)> PostAsync<TResponseType, TErrorResponseType>(
            string requestUri, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, TResponse responseType)> PostStreamAsync<TSource, TResponse>(
            string requestUri, TSource sourceObj, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, T responseType)> PutAsync<T>(
            string requestUri, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(
            string requestUri, IEnumerable<KeyValuePair<string, string>> headers = null);
        Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(
            string requestUri, HttpContent content, IEnumerable<KeyValuePair<string, string>> headers = null);
    }
}
