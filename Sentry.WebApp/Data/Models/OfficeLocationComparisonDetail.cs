using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class OfficeLocationComparisonDetail
    {
        [Key]
        public string SourceRecordId { get; set; }

        public DateTime? IntegrationDate { get; set; }

        public string System { get; set; }


        public string Name { get; set; }

        public string BuildingCode { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        public string SourceRecordId_Compare { get; set; }

        public string Name_Compare { get; set; }

        public string BuildingCode_Compare { get; set; }

        public string Address1_Compare { get; set; }

        public string Address2_Compare { get; set; }

        public string City_Compare { get; set; }

        public string State_Compare { get; set; }

        public string PostalCode_Compare { get; set; }

        public string Country_Compare { get; set; }
    }
}
