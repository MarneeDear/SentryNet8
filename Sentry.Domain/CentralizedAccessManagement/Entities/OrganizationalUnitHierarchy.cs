using Newtonsoft.Json;
using System.Collections.Generic;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class OrganizationalUnitHierarchy
    {
        public OrganizationalUnit OrganizationalUnit { get; set; }
        public IEnumerable<OrganizationalUnitHierarchy> Children { get; set; }
    }

    public class OrganizationalUnitHierarchyResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OrganizationalUnitHierarchy> OrganizationalUnitHierarchy { get; set; }

    }
}
