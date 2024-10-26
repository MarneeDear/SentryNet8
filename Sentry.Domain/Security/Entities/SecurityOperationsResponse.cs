using Newtonsoft.Json;

namespace Sentry.Domain.Security.Entities
{
    public class SecurityOperationsResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }

    public class OrgUnitOperationResponse : SecurityOperationsResponse
    {
        [JsonProperty("data")]
        public OrgUnitValidationResult ValidationResult { get; set; }
    }


    public class OrgUnitValidationResult
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
    }

    public class ProjectOperationResponse : SecurityOperationsResponse
    {
        [JsonProperty("data")]
        public ProjectValidationResult ValidationResult { get; set; }
    }

    public class ProjectValidationResult
    {
        [JsonProperty("id")]
        public string ProjectId { get; set; }

        [JsonProperty("isValid")]
        public bool IsValid { get; set; }
    }
}
