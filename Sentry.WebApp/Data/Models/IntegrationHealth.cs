using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class IntegrationHealth
    {
        [Key, Column(Order = 0)]
        public int SystemId { get; set; }
        [Key, Column(Order = 1)]
        public int IntegrationId { get; set; }
        public int UnprocessedRecords { get; set; }
        public int UnmasteredRecords { get; set; }
        public int UnpromotedRecords { get; set; }
        public bool HistoryTriggerEnabled { get; set; }
        public bool StgTriggerEnabled { get; set; }
        public bool GoodTriggerEnabled { get; set; }
        public bool BadTriggerEnabled { get; set; }
    }

    public class CategorySystemIntegration
    {
        public int SystemId { get; set; }
        public int IntegrationId { get; set; }
        [Key]
        public string CategoryId { get; set; }
        public string CategoryDisplayName { get; set; }
        public string SystemName { get; set; }
    }
}
