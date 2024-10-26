using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sentry.Domain.AccountsPayable.Entities.JournalEntryBatch
{
    public class NewJournalEntryBatchReponse
    {
        [JsonProperty("statusCode")]
        public int StatusCode { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("data")]
        public NewJournalEntryBatch NewJournalEntryBatch { get; set; }

    }

    public class NewJournalEntryBatch
    {
        public long RecordId { get; set; }
    }
}
