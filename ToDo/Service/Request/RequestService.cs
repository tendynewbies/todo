using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using ToDo.Model;
using Xamarin.Essentials;

namespace ToDo.Service.Request
{
    public class RequestService : IRequestService
    {
        public HttpClient _httpClient;
        private const int _REQUEST_TIMEOUT = 30;

        public async Task<ServiceResponse<TResult>> Get<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null)
        {
            return await SendAsync<TRequest, TResult>(HttpMethod.Get, url, body, headers, queryParams).ConfigureAwait(false);
        }

        public async Task<ServiceResponse<TResult>> Post<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null)
        {
            return await SendAsync<TRequest, TResult>(HttpMethod.Post, url, body, headers).ConfigureAwait(false);
        }

        public async Task<ServiceResponse<TResult>> Put<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null)
        {
            return await SendAsync<TRequest, TResult>(HttpMethod.Put, url, body, headers).ConfigureAwait(false);
        }

        public async Task<ServiceResponse<TResult>> Delete<TRequest, TResult>(string url, TRequest body, Dictionary<string, string> headers = null)
        {
            return await SendAsync<TRequest, TResult>(HttpMethod.Delete, url, body, headers).ConfigureAwait(false);
        }


        private async Task<ServiceResponse<TResult>> SendAsync<TRequest, TResult>(HttpMethod method, string url, TRequest body, Dictionary<string, string> headers = null, Dictionary<string, string> queryParams = null, bool simpleRequest = false)
        {
            InitializeHttpClient();

            ServiceResponse<TResult> serviceResponse = null;

            HttpResponseMessage response = new HttpResponseMessage();
            try
            {
                serviceResponse = CheckNetwork<TResult>();

                //check internet connection
                if (null != serviceResponse)
                {
                    return serviceResponse;
                }

                serviceResponse = new ServiceResponse<TResult>
                {
                    IsSuccess = true
                };

                HttpContent requestContent = null;
                if (body != default)
                {
                    var json = JsonConvert.SerializeObject(body);
                    requestContent = new StringContent(json, Encoding.UTF8, "application/json");
                }

                var request = CreateRequestMessage(method, url, headers, requestContent, queryParams);
                response = await _httpClient.SendAsync(request);

                //validate the webRequestStatus and get the exception
                serviceResponse.ResponseCode = response.StatusCode;
                string content = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    serviceResponse.Result = JsonConvert.DeserializeObject<TResult>(content, new JsonSerializerSettings
                    { NullValueHandling = NullValueHandling.Ignore });
                }
                else
                {
                    serviceResponse.Exception = new Exception(content);
                }
            }
            catch (Exception ex)
            {
                serviceResponse.Exception = ex;
            }
            finally
            {
                response.Dispose();
            }

            return serviceResponse;
        }

        private void InitializeHttpClient()
        {
            _httpClient = new HttpClient
            {
                Timeout = TimeSpan.FromSeconds(_REQUEST_TIMEOUT)
            };
        }

        private ServiceResponse<T> CheckNetwork<T>()
        {
            ServiceResponse<T> serviceResponse = null;
            NetworkAccess current = Connectivity.NetworkAccess;
            if (current != NetworkAccess.Internet)
            {
                serviceResponse = new ServiceResponse<T>
                {
                    IsSuccess = false
                };
            }
            return serviceResponse;
        }

        /// <summary>
        /// Wrap the request parameters into HttpRequestMessage
        /// </summary>
        /// <param name="method">HttpMethod</param>
        /// <param name="path">Complete API URL</param>
        /// <param name="headers">Headers to add</param>
        /// <param name="requestContent">T body</param>
        /// <param name="queryParams">Query Parameters, if any</param>
        /// <returns></returns>
        private HttpRequestMessage CreateRequestMessage(HttpMethod method, String path, Dictionary<string, string> headers, HttpContent requestContent = null, Dictionary<string, string> queryParams = null)
        {
            UriBuilder builder = null;
            if (!string.IsNullOrWhiteSpace(path))
            {
                builder = new UriBuilder(path);
            }
            else
            {
                builder = new UriBuilder();
            }

            if (queryParams != null)
            {
                var query = HttpUtility.ParseQueryString(builder.Query);
                foreach (var queryParam in queryParams)
                {
                    query.Add(queryParam.Key, queryParam.Value);
                }
                builder.Query = query.ToString();
            }

            var request = new HttpRequestMessage(method, builder.ToString());
            if (null != requestContent)
            {
                request.Content = requestContent;
            }

            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }
            return request;
        }
    }
}
