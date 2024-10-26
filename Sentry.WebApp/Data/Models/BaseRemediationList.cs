using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	public class BaseRemediationList
	{
        [Column("RecordId")]
        [Key]
		public long? Id { get; set; }

        public int? SystemId { get; set; }

        public string SystemName { get; set; }

        public string ErrorCategories { get; set; }

        public int? ErrorCount { get; set; }

        //[DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}")]
        //[DataType(DataType.Date)]
        public DateTime? IntegrationDate { get; set; }

        public int? IntegrationId { get; set; }

        [Column("RecordDate")]
        public DateTime? CreatedDate { get; set; }

        public string RecordStatus { get; set; }

	}
}