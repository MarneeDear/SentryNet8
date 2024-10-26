using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class OrganizationalUnitComparisonDetail
    {
        [Key]
        public long RecordId { get; set; }

		public DateTime? IntegrationDate { get; set; }

        public string System { get; set; }

        public int SystemId { get; set; }

        public string SourceRecordId { get; set; }
        public string SourceRecordId_Compare { get; set; }

        public string OrganizationalUnitName { get; set; }
        //public string OrganizationalUnitName_BusinessName { get; set; }
        //public string OrganizationalUnitName_BusinessDescription { get; set; }
        public string OrganizationalUnitName_Compare { get; set; }

        public string OrganizationalUnitCode { get; set; }
        //public string OrganizationalUnitCode_BusinessName { get; set; }
        //public string OrganizationalUnitCode_BusinessDescription { get; set; }
        public string OrganizationalUnitCode_Compare { get; set; }

        public string OrganizationalUnitType { get; set; }
        //public string OrganizationalUnitType_BusinessName { get; internal set; }
        //public string OrganizationalUnitType_BusinessDescription { get; internal set; }
        public string OrganizationalUnitType_Compare { get; set; }

        public string OrganizationName { get; set; }
        //public string OrganizationName_BusinessName { get; internal set; }
        //public string OrganizationName_BusinessDescription { get; internal set; }
        public string OrganizationName_Compare { get; set; }

    }
}
