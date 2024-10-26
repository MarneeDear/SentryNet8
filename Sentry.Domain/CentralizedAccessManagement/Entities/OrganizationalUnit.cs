using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentry.Domain.CentralizedAccessManagement.Entities
{
    public class OrganizationalUnit
    {
        [JsonProperty("code")]
        public string Code { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class OrganizationalUnitReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data")]
        public IEnumerable<OrganizationalUnit> OrganizationalUnits { get; set; }

    }
}
