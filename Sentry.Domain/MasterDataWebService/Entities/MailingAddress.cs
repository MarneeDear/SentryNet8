using Newtonsoft.Json;

namespace Sentry.Domain.MasterDataWebService.Entities
{
    public class MailingAddress
    {
        [JsonProperty("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("StateCode")]
        public string StateCode { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }
    }
}
