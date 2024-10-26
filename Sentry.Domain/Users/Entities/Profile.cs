using Newtonsoft.Json;
using Sentry.Domain.AccountsPayable.Entities;

namespace Sentry.Domain.Users.Entities
{
    public class Profile
    {
        public string DefaultPage { get; set; }
    }

    public class ProfileReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public Profile Profile { get; set; }

    }
}
