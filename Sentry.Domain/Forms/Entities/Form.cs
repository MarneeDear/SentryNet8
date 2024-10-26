using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sentry.Domain.Forms.Entities
{
    public class Form
    {
        [JsonProperty("formNumber")]
        public string FormNumber { get; set; }
    }
    public class FormResponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public Form Form { get; set; }
    }
}
