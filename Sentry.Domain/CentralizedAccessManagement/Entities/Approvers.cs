using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class Approver
    {
        public string EmployeeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    public class ApproverReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public IEnumerable<Approver> Approvers { get; set; }

    }
}
