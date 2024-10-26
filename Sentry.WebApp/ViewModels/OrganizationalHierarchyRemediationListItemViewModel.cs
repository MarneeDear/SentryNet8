using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalHierarchyRemediationListItemViewModel : BaseRemediationListItemViewModel
    {

        public int? OuId { get; set; }
        public string OuName { get; set; }
        public string OuIcon { get; set; }
        public string OuCode { get; set; }
        public string OuCodeSource { get; set; }
        public DateTime? OuCodeDate { get; set; }

        public string OrgUnitCode { get; set; }
        public string OrgUnitCodeSource { get; set; }
        public DateTime? OrgUnitCodeDate { get; set; }

        public string OrgUnitType { get; set; }
        public string OrgUnitTypeSource { get; set; }
        public DateTime? OrgUnitTypeDate { get; set; }

        public string Org { get; set; }
        public string OrgSource { get; set; }
        public DateTime? OrgDate { get; set; }

        public string ParentOrgUnit { get; set; }
        public string ParentOrgUnitSource { get; set; }
        public DateTime? ParentOrgUnitDate { get; set; }

        public string Avatar { get; set; }

    }
}
