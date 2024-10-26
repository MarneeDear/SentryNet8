using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationHistoryViewModel
    {
		public DateTime? HistoryDate { get; set; }

		public string Name { get; set; }

		public string Name_Status { get; set; }

		public string BuildingCode { get; set; }
		public string BuildingCode_Status { get; set; }

		public string Address1 { get; set; }

		public string Address1_Status { get; set; }

		public string Address2 { get; set; }

		public string Address2_Status { get; set; }

		public string City { get; set; }

		public string City_Status { get; set; }

		public string State { get; set; }

		public string State_Status { get; set; }

		public string PostalCode { get; set; }

		public string PostalCode_Status { get; set; }

		public string Country { get; set; }

		public string Country_Status { get; set; }

	}
}
