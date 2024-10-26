using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
	public class BaseHistory 
	{
		[Key]
		public long? RecordId { get; set; }

		public int SystemId { get; set; }

		public DateTime IntegrationDate { get; set; }

		public int IntegrationId { get; set; }

		public long? LineageKey { get; set; }

		public string SystemRecordId { get; set; }

		public DateTime? RecordDate { get; set; }

		public string RecordStatus { get; set; }
	}
}
