using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationMatchViewModel : BaseIntegrationViewModel
    {
        public DesignationMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        #region Match
        public string DesignationId { get; set; }
        public int DesignationId_Weight { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }

        public string DesignationName { get; set; }
        public int DesignationName_Weight { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }

        public string Description { get; set; }
        public int Description_Weight { get; set; }
        public string Description_BusinessName { get; set; }
        public string Description_BusinessDescription { get; set; }

        public string DesignationTypeName { get; set; }
        public int DesignationTypeName_Weight { get; set; }
        public string DesignationTypeName_BusinessName { get; set; }
        public string DesignationTypeName_BusinessDescription { get; set; }

        public string StartDate { get; set; }
        public int StartDate_Weight { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }

        public string EndDate { get; set; }
        public int EndDate_Weight { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }

        public string KFSAccountCode { get; set; }
        public int KFSAccountCode_Weight { get; set; }
        public string KFSAccountCode_BusinessName { get; set; }
        public string KFSAccountCode_BusinessDescription { get; set; }

        public string KFSAccountMasterId { get; set; }
        public int KFSAccountMasterId_Weight { get; set; }
        public string KFSAccountMasterId_BusinessName { get; set; }
        public string KFSAccountMasterId_BusinessDescription { get; set; }

        public string VSECategoryName { get; set; }
        public int VSECategoryName_Weight { get; set; }
        public string VSECategoryName_BusinessName { get; set; }
        public string VSECategoryName_BusinessDescription { get; set; }

        public string VSECategorySourceSystemRecordId { get; set; }
        public int VSECategorySourceSystemRecordId_Weight { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessName { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessDescription { get; set; }

        public string VSECategoryMasterId { get; set; }
        public int VSECategoryMasterId_Weight { get; set; }
        public string VSECategoryMasterId_BusinessName { get; set; }
        public string VSECategoryMasterId_BusinessDescription { get; set; }

        public string GLOrganizationName { get; set; }
        public int GLOrganizationName_Weight { get; set; }
        public string GLOrganizationName_BusinessName { get; set; }
        public string GLOrganizationName_BusinessDescription { get; set; }

        public string GLOrganizationCode { get; set; }
        public int GLOrganizationCode_Weight { get; set; }
        public string GLOrganizationCode_BusinessName { get; set; }
        public string GLOrganizationCode_BusinessDescription { get; set; }

        public string GLOrganizationMasterId { get; set; }
        public int GLOrganizationMasterId_Weight { get; set; }
        public string GLOrganizationMasterId_BusinessName { get; set; }
        public string GLOrganizationMasterId_BusinessDescription { get; set; }

        public string DesignationUseTypeName { get; set; }
        public int DesignationUseTypeName_Weight { get; set; }
        public string DesignationUseTypeName_BusinessName { get; set; }
        public string DesignationUseTypeName_BusinessDescription { get; set; }

        public string DesignationUseTypeMasterId { get; set; }
        public int DesignationUseTypeMasterId_Weight { get; set; }
        public string DesignationUseTypeMasterId_BusinessName { get; set; }
        public string DesignationUseTypeMasterId_BusinessDescription { get; set; }

        public string OrganizationalUnit { get; set; }
        public int OrganizationalUnit_Weight { get; set; }
        public string OrganizationalUnit_BusinessName { get; set; }
        public string OrganizationalUnit_BusinessDescription { get; set; }

        public string OrganizationalUnitMasterId { get; set; }
        public int OrganizationalUnitMasterId_Weight { get; set; }
        public string OrganizationalUnitMasterId_BusinessName { get; set; }
        public string OrganizationalUnitMasterId_BusinessDescription { get; set; }
        #endregion

        public List<DesignationMatchSummaryViewModel> PossibleMatches { get; set; }

        public class DesignationMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            #region Match
            public string DesignationId { get; set; }
            public int DesignationId_Weight { get; set; }
            public string DesignationId_BusinessName { get; set; }
            public string DesignationId_BusinessDescription { get; set; }

            public string DesignationName { get; set; }
            public int DesignationName_Weight { get; set; }
            public string DesignationName_BusinessName { get; set; }
            public string DesignationName_BusinessDescription { get; set; }
            #endregion

        }

    }
}
