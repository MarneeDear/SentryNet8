using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Sentry.Domain
{
    public class WebServiceOperations
    {
        protected string _urlBase;
        protected string _apiKey;
        readonly RestClient _client;

        public WebServiceOperations(string urlBase, string apiKey)
        {
            _urlBase = urlBase;
            _apiKey = apiKey;
            _client = new RestClient(urlBase);
        }

        protected async Task<string> GetAuthToken()
        {
            string result = null;

            try
            {
                //var client = new RestClient(_urlBase);

                var request = new RestRequest("authorization", Method.Get);
                request.AddHeader("X-API-Key", _apiKey);
                request.AddHeader("accept", "text/plain");

                var response = await _client.ExecuteAsync(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    if (response.Content.StartsWith("\"") && response.Content.EndsWith("\""))
                        result = response.Content.Substring(1, response.Content.Length - 2);
                    else
                        result = response.Content;
                }
                else
                {
                    throw new Exception($"Error getting auth token from UAF Services [{response.ErrorMessage}]");
                }
            }
            catch (Exception)
            {
                result = null;
            }

            return result;
        }

        protected async Task<RestResponse> ExecuteRequest(string resource, Method method, string requestBody = null)
        {
            var authToken = String.Empty;
            
            if (!String.IsNullOrEmpty(_apiKey))
            {
                authToken = await GetAuthToken();

                if (string.IsNullOrEmpty(authToken))
                    throw new Exception("Unable to get auth token");
            }

            //var client = new RestClient(_urlBase);

            var request = new RestRequest(resource, method);
            //request.AddHeader("authorization", authToken);
            //request.AddHeader("accept", "application/json");

            if (requestBody != null)
            {
                request.AddParameter(
                    "application/json",
                    requestBody,
                    ParameterType.RequestBody
                );                
            }

            if (!(String.IsNullOrEmpty(authToken)))
            {
                request.AddParameter(
                    "authorization",
                    authToken,
                    ParameterType.HttpHeader
                    );
            }

            var response = await _client.ExecuteAsync(request);

            return response;
        }

        protected async Task<RestResponse> ExecuteRequestWithAuthToken(string resource, Method method, string requestBody = null)
        {
            var authToken = await GetAuthToken();

            if (string.IsNullOrEmpty(authToken))
                throw new Exception("Unable to get auth token");

            //var client = new RestClient(_urlBase);

            var request = new RestRequest(resource, method);
            request.AddHeader("authorization", authToken);
            request.AddHeader("accept", "application/json");

            if (requestBody != null)
            {
                request.AddParameter(
                    "application/json",
                    requestBody,
                    ParameterType.RequestBody
                );
            }

            var response = _client.Execute(request);

            return (RestResponse)response;
        }

    }
}
