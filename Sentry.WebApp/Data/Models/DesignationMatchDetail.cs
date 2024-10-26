using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class DesignationMatchDetail : BaseDetail
    {

        #region Designation

        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }
        public bool DesignationId_IncludeInMatch { get; set; }
        public int DesignationId_MatchWeight { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }
        public bool DesignationName_IncludeInMatch { get; set; }
        public int DesignationName_MatchWeight { get; set; }

        public string Description { get; set; }
        public string Description_BusinessName { get; set; }
        public string Description_BusinessDescription { get; set; }
        public bool Description_IncludeInMatch { get; set; }
        public int Description_MatchWeight { get; set; }

        public string StartDate { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }
        public bool StartDate_IncludeInMatch { get; set; }
        public int StartDate_MatchWeight { get; set; }

        public string EndDate { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }
        public bool EndDate_IncludeInMatch { get; set; }
        public int EndDate_MatchWeight { get; set; }

        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_BusinessName { get; set; }
        public string DesignationTypeName_BusinessDescription { get; set; }
        public bool DesignationTypeName_IncludeInMatch { get; set; }
        public int DesignationTypeName_MatchWeight { get; set; }

        public string KFSAccountCode { get; set; }
        public string KFSAccountCode_BusinessName { get; set; }
        public string KFSAccountCode_BusinessDescription { get; set; }
        public bool KFSAccountCode_IncludeInMatch { get; set; }
        public int KFSAccountCode_MatchWeight { get; set; }

        //public string KFSAccountMasterId { get; set; }
        //public string KFSAccountMasterId_BusinessName { get; set; }
        //public string KFSAccountMasterId_BusinessDescription { get; set; }
        //public bool KFSAccountMasterId_IncludeInMatch { get; set; }
        //public int KFSAccountMasterId_MatchWeight { get; set; }

        public string VSECategoryName { get; set; }
        public string VSECategoryName_BusinessName { get; set; }
        public string VSECategoryName_BusinessDescription { get; set; }
        public bool VSECategoryName_IncludeInMatch { get; set; }
        public int VSECategoryName_MatchWeight { get; set; }
        public string VSECategoryMasterId { get; set; }
        public string VSECategoryMasterId_BusinessName { get; set; }
        public string VSECategoryMasterId_BusinessDescription { get; set; }
        public bool VSECategoryMasterId_IncludeInMatch { get; set; }
        public int VSECategoryMasterId_MatchWeight { get; set; }

        public string GLOrganizationName { get; set; }
        public string GLOrganizationName_BusinessName { get; set; }
        public string GLOrganizationName_BusinessDescription { get; set; }
        public bool GLOrganizationName_IncludeInMatch { get; set; }
        public int GLOrganizationName_MatchWeight { get; set; }

        public string GLOrganizationCode { get; set; }
        public string GLOrganizationCode_BusinessName { get; set; }
        public string GLOrganizationCode_BusinessDescription { get; set; }
        public bool GLOrganizationCode_IncludeInMatch { get; set; }
        public int GLOrganizationCode_MatchWeight { get; set; }

        public string GLOrganizationMasterId { get; set; }
        public string GLOrganizationMasterId_BusinessName { get; set; }
        public string GLOrganizationMasterId_BusinessDescription { get; set; }
        public bool GLOrganizationMasterId_IncludeInMatch { get; set; }
        public int GLOrganizationMasterId_MatchWeight { get; set; }

        public string DesignationUseTypeName { get; set; }
        public string DesignationUseTypeName_BusinessName { get; set; }
        public string DesignationUseTypeName_BusinessDescription { get; set; }
        public bool DesignationUseTypeName_IncludeInMatch { get; set; }
        public int DesignationUseTypeName_MatchWeight { get; set; }

        public string DesignationUseTypeMasterId { get; set; }
        public string DesignationUseTypeMasterId_BusinessName { get; set; }
        public string DesignationUseTypeMasterId_BusinessDescription { get; set; }
        public bool DesignationUseTypeMasterId_IncludeInMatch { get; set; }
        public int DesignationUseTypeMasterId_MatchWeight { get; set; }

        public string OrganizationalUnit { get; set; }
        public string OrganizationalUnit_BusinessName { get; set; }
        public string OrganizationalUnit_BusinessDescription { get; set; }
        public bool OrganizationalUnit_IncludeInMatch { get; set; }
        public int OrganizationalUnit_MatchWeight { get; set; }

        public string OrganizationalUnitMasterId { get; set; }
        public string OrganizationalUnitMasterId_BusinessName { get; set; }
        public string OrganizationalUnitMasterId_BusinessDescription { get; set; }
        public bool OrganizationalUnitMasterId_IncludeInMatch { get; set; }
        public int OrganizationalUnitMasterId_MatchWeight { get; set; }

        #endregion

    }
}
