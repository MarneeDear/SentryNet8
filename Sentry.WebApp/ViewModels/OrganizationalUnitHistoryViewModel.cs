using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }


        public string OrganizationalUnitName { get; set; }
        public string OrganizationalUnitName_Status { get; set; }
        public string OrganizationalUnitName_Source { get; set; }

        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitCode_Status { get; set; }
        public string OrganizationalUnitCode_Source { get; set; }

        public string OrganizationalUnitType { get; set; }
        public string OrganizationalUnitType_Status { get; set; }
        public string OrganizationalUnitType_Source { get; set; }


        public int? ParentOrganizationalUnitName { get; set; }
        public string ParentOrganizationalUnitName_Status { get; set; }
        public string ParentOrganizationalUnitName_Source { get; set; }

        public int? ParentOrganizationalUnitType { get; set; }
        public string ParentOrganizationalUnitType_Status { get; set; }

        public string ParentOrganizationalUnitMasterId { get; set; }
        public string ParentOrganizationalUnitMasterId_Status { get; set; }


        public string OrganizationName { get; set; }
        public string OrganizationName_Status { get; set; }

        public string OrganizationMasterId { get; set; }
        public string OrganizationMasterId_Status { get; set; }

        public string OrganizationCode { get; set; }
        public string OrganizationCode_Status { get; set; }

        public int? OrganizationType { get; set; }
        public string OrganizationType_Status { get; set; }
    }
}
