using Microsoft.Extensions.Configuration;
using System;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalHierarchyViewModel : BaseIntegrationViewModel
    {

        public OrganizationalHierarchyViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        public int? OuId { get; set; }
        public string OuName { get; set; }
        public string OuName_BusinessName { get; set; }
        public string OuName_BusinessDescription { get; set; }
        public string OuName_AttributeId { get; set; }
        public string OuName_Status { get; set; }
        public string OuName_Source { get; set; }
        public string OuIcon { get; set; }
        public string OuCode { get; set; }
        public string OuCode_BusinessName { get; set; }
        public string OuCode_BusinessDescription { get; set; }
        public string OuCode_AttributeId { get; set; }
        public string OuCode_Source { get; set; }
        public string OuCode_Status { get; set; }
        public DateTime? OuCode_Date { get; set; }

        public string OrgUnitCode { get; set; }
        public string OrgUnitCode_BusinessName { get; set; }
        public string OrgUnitCode_BusinessDescription { get; set; }
        public string OrgUnitCode_AttributeId { get; set; }
        public string OrgUnitCode_Status { get; set; }
        public string OrgUnitCode_Source { get; set; }
        public DateTime? OrgUnitCode_Date { get; set; }

        public string OrgUnitType { get; set; }
        public string OrgUnitType_BusinessName { get; set; }
        public string OrgUnitType_BusinessDescription { get; set; }
        public string OrgUnitType_AttributeId { get; set; }
        public string OrgUnitType_Status { get; set; }
        public string OrgUnitType_Source { get; set; }
        public DateTime? OrgUnitType_Date { get; set; }

        public string Org { get; set; }
        public string Org_BusinessName { get; set; }
        public string Org_BusinessDescription { get; set; }
        public string Org_AttributeId { get; set; }
        public string Org_Status { get; set; }
        public string Org_Source { get; set; }
        public DateTime? Org_Date { get; set; }

        public string ParentOrgUnit { get; set; }
        public string ParentOrgUnit_BusinessName { get; set; }
        public string ParentOrgUnit_BusinessDescription { get; set; }
        public string ParentOrgUnit_AttributeId { get; set; }
        public string ParentOrgUnit_Status { get; set; }
        public string ParentOrgUnit_Source { get; set; }
        public DateTime? ParentOrgUnit_Date { get; set; }

        public string Avatar { get; set; }

    }
}
