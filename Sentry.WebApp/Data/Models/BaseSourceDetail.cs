using System;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
	public class BaseSourceDetail
	{
		[Key]
		public long RecordId { get; set; }
		public int? IntegrationId { get; set; }
		public DateTime IntegrationDate { get; set; }
		public int? SystemId { get; set; }
		public string System { get; set; }
		public string SystemRecordId { get; set; }
    }
}