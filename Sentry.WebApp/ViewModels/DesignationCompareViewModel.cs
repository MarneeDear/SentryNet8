using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public string MasterId { get; set; }

        public string System { get; set; }

        #region Integration Record
        public string SourceRecordId { get; set; }

        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }
        public bool DesignationId_IsDifferent { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }
        public bool DesignationName_IsDifferent { get; set; }

        public string Description { get; set; }
        public string Description_BusinessName { get; set; }
        public string Description_BusinessDescription { get; set; }
        public bool Description_IsDifferent { get; set; }

        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_BusinessName { get; set; }
        public string DesignationTypeName_BusinessDescription { get; set; }
        public bool DesignationTypeName_IsDifferent { get; set; }

        public string DesignationTypeMasterId { get; set; }
        public string DesignationTypeMasterId_BusinessName { get; set; }
        public string DesignationTypeMasterId_BusinessDescription { get; set; }

        public string StartDate { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }
        public bool StartDate_IsDifferent { get; set; }

        public string EndDate { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }
        public bool EndDate_IsDifferent { get; set; }

        public string KFSAccountCode { get; set; }
        public string KFSAccountCode_BusinessName { get; set; }
        public string KFSAccountCode_BusinessDescription { get; set; }
        public bool KFSAccountCode_IsDifferent { get; set; }

        public string KFSAccountMasterId { get; set; }
        public string KFSAccountMasterId_BusinessName { get; set; }
        public string KFSAccountMasterId_BusinessDescription { get; set; }

        public string VSECategoryName { get; set; }
        public string VSECategoryName_BusinessName { get; set; }
        public string VSECategoryName_BusinessDescription { get; set; }
        public bool VSECategoryName_IsDifferent { get; set; }

        public string VSECategorySourceSystemRecordId { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessName { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessDescription { get; set; }

        public string VSECategoryMasterId { get; set; }
        public string VSECategoryMasterId_BusinessName { get; set; }
        public string VSECategoryMasterId_BusinessDescription { get; set; }

        public string GLOrganizationName { get; set; }
        public string GLOrganizationName_BusinessName { get; set; }
        public string GLOrganizationName_BusinessDescription { get; set; }
        public bool GLOrganizationName_IsDifferent { get; set; }

        public string GLOrganizationCode { get; set; }
        public string GLOrganizationCode_BusinessName { get; set; }
        public string GLOrganizationCode_BusinessDescription { get; set; }

        public string GLOrganizationMasterId { get; set; }
        public string GLOrganizationMasterId_BusinessName { get; set; }
        public string GLOrganizationMasterId_BusinessDescription { get; set; }

        public string DesignationUseTypeName { get; set; }
        public string DesignationUseTypeName_BusinessName { get; set; }
        public string DesignationUseTypeName_BusinessDescription { get; set; }
        public bool DesignationUseTypeName_IsDifferent { get; set; }

        public string DesignationUseTypeMasterId { get; set; }
        public string DesignationUseTypeMasterId_BusinessName { get; set; }
        public string DesignationUseTypeMasterId_BusinessDescription { get; set; }

        public string OrganizationalUnit { get; set; }
        public string OrganizationalUnit_BusinessName { get; set; }
        public string OrganizationalUnit_BusinessDescription { get; set; }
        public bool OrganizationalUnit_IsDifferent { get; set; }

        public string OrganizationalUnitMasterId { get; set; }
        public string OrganizationalUnitMasterId_BusinessName { get; set; }
        public string OrganizationalUnitMasterId_BusinessDescription { get; set; }
        #endregion

        #region Compare Record
        public string SourceRecordId_Compare { get; set; }

        public string DesignationId_Compare { get; set; }
        public string DesignationName_Compare { get; set; }
        public string Description_Compare { get; set; }
        public string DesignationTypeName_Compare { get; set; }
        public string DesignationTypeMasterId_Compare { get; set; }
        public DateTime? StartDate_Compare { get; set; }
        public DateTime? EndDate_Compare { get; set; }
        public string KFSAccountCode_Compare { get; set; }
        public string KFSAccountMasterId_Compare { get; set; }
        public string VSECategoryName_Compare { get; set; }
        public string VSECategorySourceSystemRecordId_Compare { get; set; }
        public string VSECategoryMasterId_Compare { get; set; }
        public string GLOrganizationName_Compare { get; set; }
        public string GLOrganizationCode_Compare { get; set; }
        public string GLOrganizationMasterId_Compare { get; set; }
        public string DesignationUseTypeName_Compare { get; set; }
        public string DesignationUseTypeMasterId_Compare { get; set; }
        public string OrganizationalUnit_Compare { get; set; }
        public string OrganizationalUnitMasterId_Compare { get; set; }
        #endregion

    }
}
