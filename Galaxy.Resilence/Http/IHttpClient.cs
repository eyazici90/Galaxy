using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Resilence.Http
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string uri, int timeOut = default, string authorizationToken = null, string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string contentType = null, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> DeleteAsync(string uri, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");

        Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string contentType = null, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer");
    }
}
