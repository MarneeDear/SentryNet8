using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("IntegrationTriggerStatus", Schema = "Integration")]
    public class IntegrationStatus
    {
        public int IntegrationId { get; set; }
        public string IntegrationName { get; set; }
        public string IntegationDescription { get; set; }
        public int SystemId { get; set; }
        public string SystemName { get; set; }
        public string SystemDescription { get; set; }
        public int IsProducer { get; set; }
        public string StagingTable { get; set; }
        public int StagingTableEnabled { get; set; }
        public string BadTable { get; set; }
        public int BadTableEnabled { get; set; }
        public string GoodTable { get; set; }
        public int GoodTableEnabled { get; set; }
    }
}
