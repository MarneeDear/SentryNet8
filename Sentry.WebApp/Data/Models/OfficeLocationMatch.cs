using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
	[Table("OfficeLocations_Base", Schema = "Integration")]
	public class OfficeLocationMatch
	{
		public string Name { get; set; }

		public int Name_Weight { get; set; }

		public string BuildingCode { get; set; }

		public int BuildingCode_Weight { get; set; }

		public string Address1 { get; set; }

		public string Address2 { get; set; }

		public string City { get; set; }

		public string State { get; set; }

		public string PostalCode { get; set; }

		public string Country { get; set; }

	}
}
