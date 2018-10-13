using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Galaxy.Resilence.Http
{
    public class StandardHttpClient : IHttpClient
    {
        private HttpClient _client;

        public StandardHttpClient()
        {
            _client = new HttpClient();
        }

        public async Task<string> GetStringAsync(string uri, int timeOut = default, string authorizationToken = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, uri);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            if (timeOut != default)
            {
                this._client.Timeout = TimeSpan.FromMilliseconds(timeOut);
            }

            var response = await _client.SendAsync(requestMessage);


            return await response.Content.ReadAsStringAsync();
        }

        private async Task<HttpResponseMessage> DoPostPutAsync<T>(HttpMethod method, string uri, T item, string contentType = null
                                                    , int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            if (method != HttpMethod.Post && method != HttpMethod.Put)
            {
                throw new ArgumentException("Value must be either post or put.", nameof(method));
            }
            if (timeOut != default)
            {
                this._client.Timeout = TimeSpan.FromMilliseconds(timeOut);
            }

            // a new StringContent must be created for each retry 
            // as it is disposed after each call

            var requestMessage = new HttpRequestMessage(method, uri);

            if (contentType != null)
            {
                requestMessage.Content = new StringContent(item.ToString(), System.Text.Encoding.UTF8, contentType);
                requestMessage.Content.Headers.ContentType = MediaTypeHeaderValue.Parse(contentType);
            }
            else requestMessage.Content = new StringContent(JsonConvert.SerializeObject(item), System.Text.Encoding.UTF8, "application/json");


            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            var response = await _client.SendAsync(requestMessage);
            
            if (response.StatusCode == HttpStatusCode.InternalServerError)
            {
                throw new HttpRequestException();
            }

            return response;
        }


        public async Task<HttpResponseMessage> PostAsync<T>(string uri, T item, string contentType = null, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Post, uri, item, contentType, timeOut, authorizationToken, requestId, authorizationMethod);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(string uri, T item, string contentType = null, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            return await DoPostPutAsync(HttpMethod.Put, uri, item, contentType, timeOut, authorizationToken, requestId, authorizationToken);
        }
        public async Task<HttpResponseMessage> DeleteAsync(string uri, int timeOut = default, string authorizationToken = null, string requestId = null, string authorizationMethod = "Bearer")
        {
            var requestMessage = new HttpRequestMessage(HttpMethod.Delete, uri);

            if (authorizationToken != null)
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue(authorizationMethod, authorizationToken);
            }
            if (timeOut != default)
            {
                this._client.Timeout = TimeSpan.FromMilliseconds(timeOut);
            }

            if (requestId != null)
            {
                requestMessage.Headers.Add("x-requestid", requestId);
            }

            return await _client.SendAsync(requestMessage);
        }
    }
}
