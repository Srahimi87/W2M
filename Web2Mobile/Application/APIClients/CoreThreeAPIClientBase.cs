using System.Net.Http;
using System.Net.Http.Headers;
using Application.DTOs;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Application.APIClients
{
    public class APIConfig
	{
		public string BaseURL { get; set; }
		public string APIKey { get; set; }
    }

    public class CustomJwt
    {
        public string PreferredUsername { get; set; }
    }

    public class CoreThreeAPIClientBase
    {
        protected readonly HttpClient _client;
        public AuthenticationHeaderValue? Authentication { get; set; }
        public MediaTypeHeaderValue? ContentMediaType { get; set; } = new MediaTypeHeaderValue("application/json");

        public MediaTypeWithQualityHeaderValue? AcceptMediaType { get; set; }
        public bool AlwaysIncludeMediaType { get; set; } = false;
        public CoreThreeAPIClientBase(HttpClient client)
        {
            _client = client;
        }
        public virtual Task<TOut?> Get<TOut>(string url, string authToken = null, string deviceId = null)
        {
            return ProcessRequest<TOut>(new HttpRequestMessage(HttpMethod.Get, url), authToken, deviceId);
        }
        public virtual Task<TOut?> Get<TRequest, TOut>(TRequest request, string url)
        {
            var http = new HttpRequestMessage(HttpMethod.Get, url);
            http.Content = new StringContent(JsonConvert.SerializeObject(request));
            return ProcessRequest<TOut>(http);
        }

        public virtual async Task<string> ProcessRequest(HttpRequestMessage httpPayload, string authToken = null, string deviceId = null)
        {
            if (authToken != null)
                httpPayload.Headers.Add("c3-auth-token", authToken);

            if (deviceId != null)
                httpPayload.Headers.Add("c3-device-id", deviceId);

            httpPayload.Headers.Add("userId", "W2M");
            httpPayload.Headers.Add("User-Agent", "W2M");
            httpPayload.Headers.Add("Accept", "*/*");

            if (AcceptMediaType != null)
            {
                httpPayload.Headers.Accept.Add(AcceptMediaType);
            }

            httpPayload.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            if (httpPayload.Headers.Authorization == null)
            {
                httpPayload.Headers.Authorization = Authentication;
            }

            if (httpPayload.Content != null)
            {
                httpPayload.Content.Headers.ContentType = ContentMediaType;
            }
            else if (AlwaysIncludeMediaType && ContentMediaType != null)
            {
                httpPayload.Content = new StringContent(string.Empty, ContentMediaType);
            }

            var responseObject = await _client.SendAsync(httpPayload);


            if (responseObject.IsSuccessStatusCode)
            {
                return await responseObject.Content.ReadAsStringAsync();
            }

            var responceData = await responseObject.Content.ReadAsStringAsync();
            throw new HttpRequestException(responceData, null, responseObject.StatusCode);
        }



        public virtual async Task<TOut?> ProcessRequest<TOut>(HttpRequestMessage httpPayload, string authToken = null, string deviceId = null)
        {
            var payload = await ProcessRequest(httpPayload, authToken, deviceId);
            return !string.IsNullOrEmpty(payload) ? JsonConvert.DeserializeObject<TOut>(payload) : default;
        }

        public virtual Task Post<TRequest>(string url, TRequest request)
        {
            var http = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request)),
            };

            return ProcessRequest(http);
        }


        public virtual Task<TOut?> Post<TRequest, TOut>(string url, TRequest request, string authToken, string deviceId = null)
        {
            string serialised = JsonConvert.SerializeObject(request, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

            var http = new HttpRequestMessage(HttpMethod.Post, url)
            {
                Content = new StringContent(serialised)
            };
            return ProcessRequest<TOut>(http, authToken, deviceId);
        }
        public virtual Task<TOut?> Put<TRequest, TOut>(string url, TRequest request)
        {
            var http = new HttpRequestMessage(HttpMethod.Put, url)
            {
                Content = new StringContent(JsonConvert.SerializeObject(request))
            };
            return ProcessRequest<TOut>(http);
        }

        public async virtual Task<bool> Delete<TRequest>(string url, TRequest request)
        {
            try
            {
                var http = new HttpRequestMessage(HttpMethod.Delete, url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(request))
                };
                await ProcessRequest(http);
            }
            catch
            {
                return false;
            }
            return true;
        }

    }
}

