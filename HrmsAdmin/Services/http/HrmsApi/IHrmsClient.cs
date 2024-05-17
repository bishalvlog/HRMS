using System.Net;

namespace HrmsSystemAdmin.Web.Services.http.HrmsApi
{
    public interface IHrmsClient
    {
        HttpClient GetHttpClient();
        Task<(HttpStatusCode statusCode, T responseType)> GetAsync<T>(string requestUri);
        Task<(HttpStatusCode statusCode, T responseType)> PostAsync<T>(string requestUri, HttpContent content);
        Task<(HttpStatusCode statusCode, T responseType)> PutAsync<T>(string requestUri, HttpContent content);
        Task<(HttpStatusCode statusCode, T responseType)> DeleteAsync<T>(string requestUri);
    }
}
