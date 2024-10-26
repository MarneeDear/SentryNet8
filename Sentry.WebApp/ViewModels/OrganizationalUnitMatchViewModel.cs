using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitMatchViewModel : BaseIntegrationViewModel
    {

        public OrganizationalUnitMatchViewModel() : base() { }

        #region Integration Record

        public DateTime? IntegrationDate { get; set; }

        public string OrganizationalUnitName { get; set; }
        public int OrganizationalUnitName_Weight { get; set; }
        public string OrganizationalUnitName_BusinessName { get; set; }
        public string OrganizationalUnitName_BusinessDescription { get; set; }

        public string OrganizationalUnitCode { get; set; }
        public int OrganizationalUnitCode_Weight { get; set; }
        public string OrganizationalUnitCode_BusinessName { get; set; }
        public string OrganizationalUnitCode_BusinessDescription { get; set; }

        public string OrganizationalUnitType { get; set; }
        public int OrganizationalUnitType_Weight { get; set; }
        public string OrganizationalUnitType_BusinessName { get; set; }
        public string OrganizationalUnitType_BusinessDescription { get; set; }

        public string Organization { get; set; }
        public int Organization_Weight { get; set; }
        public string Organization_BusinessName { get; set; }
        public string Organization_BusinessDescription { get; set; }

        #endregion

        public List<OrganizationalUnitMatchSummaryViewModel> PossibleMatches { get; set; }

    }

    public class OrganizationalUnitMatchSummaryViewModel
    {
        public bool Selected { get; set; }
        public int MatchConfidence { get; set; }
        public string OrganizationalUnitName { get; set; }
        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitType { get; set; }
        public string OrganizationName { get; set; }
        public string MasterId { get; set; }
    }
}
