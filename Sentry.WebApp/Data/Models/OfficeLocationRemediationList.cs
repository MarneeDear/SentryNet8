using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	[Table("OfficeLocations_List", Schema = "Integration")]
	public class OfficeLocationRemediationList
	{
        [Column("RecordId")]
		public long? Id { get; set; }

        [Column("SystemId")]
        public int? SystemId { get; set; }

        [Column("SystemName")]
        public string SystemName { get; set; }

        [Column("ErrorCategories")]
        public string ErrorCategories { get; set; }

        [Column("ErrorCount")]
        public int? ErrorCount { get; set; }

        [Column("IntegrationDate")]
        public DateTime? IntegrationDate { get; set; }

        [Column("IntegrationId")]
        public int? IntegrationId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("BuildingCode")]
        public string BuildingCode { get; set; }

        [Column("City")]
        public string City { get; set; }

        [Column("RecordDate")]
        public DateTime? CreatedDate { get; set; }

        [Column("RecordStatus")]
        public string RecordStatus { get; set; }

	}
}