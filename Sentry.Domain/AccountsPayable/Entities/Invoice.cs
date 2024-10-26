using Newtonsoft.Json;
using System;

namespace Sentry.Domain.AccountsPayable.Entities.Invoice
{
    public class CreateInvoice
    { 
        public long DisbursementId { get; set; }
        public DateTime PostDate { get; set; }
    }

    public class NewInvoiceReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public NewInvoice NewInvoice { get; set; }

    }

    public class NewInvoice
    {
        public long InvoiceId { get; set; }
    }
}
