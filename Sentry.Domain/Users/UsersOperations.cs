using Newtonsoft.Json;
using Sentry.Domain.Users.Entities;
using System;
using System.Threading.Tasks;

namespace Sentry.Domain.Users
{
    public class UsersOperations : Sentry.Domain.WebServiceOperations
    {
        public UsersOperations(string urlBase, string apiKey) : base(urlBase, apiKey)
        {
            base._urlBase = urlBase;
            base._apiKey = apiKey;
        }

        public async Task<Profile> GetProfile(string userName)
        {
            ProfileReponse result = null;
            try
            {
                string uri = $"users/{userName}/profile";
                var response = await ExecuteRequest(uri, RestSharp.Method.Get);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    result = JsonConvert.DeserializeObject<ProfileReponse>(response.Content);
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return new Profile();
                }
                else
                {
                    throw new Exception($"Error getting profile for [{userName}]. ERROR [{response.StatusCode}] [{response.Content}]");
                }
            }
            catch
            {
                throw;
            }

            return result.Profile;
        }
    }
}
