using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;
using Sentry.Domain.Security.Entities;

namespace Sentry.Domain.Security
{
    public class SecurityOperations : WebServiceOperations
    {
        public SecurityOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            _urlBase = urlBase;
            _apiKey = apiKey;
            //_correlationId = correlationId;
        }


        public async Task<UserDetails> GetUserDetails(string userName)
        {
            UserDetailsReponse result = null;
            try
            {
                string requestUrl = $"access/users/{userName}";
                var response = await ExecuteRequestWithAuthToken(requestUrl, Method.Get);
                if (response.StatusCode == HttpStatusCode.OK)
                    result = JsonConvert.DeserializeObject<UserDetailsReponse>(response.Content);
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    //throw new ArgumentException("User not found");
                    return null;
                else
                {
                    throw new Exception($"Error getting user access from UAF Services Error [{response.StatusCode}] [{response.ErrorMessage}] [{response.Content}]");
                }
            }
            catch 
            {
                throw;
            }

            return result.UserDetails;
        }

        public async Task<UserPermissions> GetUserAccessPermissions(string employeeId)
        {
            UserPermissionsResponse result = null;

            try
            {
                string requestUrl = $"access/users/{employeeId}/permissions";

                var response = await ExecuteRequestWithAuthToken(requestUrl, Method.Get);

                if (response.StatusCode == HttpStatusCode.OK)
                    result = JsonConvert.DeserializeObject<UserPermissionsResponse>(response.Content);
                else if (response.StatusCode == HttpStatusCode.NotFound)
                    throw new ArgumentException("User not found");
                else
                {
                    throw new Exception($"Error getting user access from UAF Services Error [{response.StatusCode}] [{response.ErrorMessage}] [{response.Content}]");
                }
            }
            catch 
            {                
                throw;
            }

            return result.UserPermissions;
        }
    }
}
