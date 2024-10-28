using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationViewModel : BaseIntegrationViewModel
    {
        public DesignationViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region Designation

        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }
        public string DesignationId_Status { get; set; }
        public string DesignationId_Source { get; set; }
        public int? DesignationId_FieldId { get; set; }
        public string DesignationId_OriginalValue { get; set; }
        public int? DesignationId_AttributeId { get; set; }
        public bool DesignationId_IsReadOnly { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }
        public string DesignationName_Status { get; set; }
        public string DesignationName_Source { get; set; }
        public string DesignationName_Category { get; set; }
        public string DesignationName_OriginalValue { get; set; }
        public int? DesignationName_AttributeId { get; set; }
        public bool DesignationName_IsReadOnly { get; set; }

        public string Description { get; set; }
        public string Description_BusinessName { get; set; }
        public string Description_BusinessDescription { get; set; }
        public string Description_Status { get; set; }
        public string Description_Source { get; set; }
        public string Description_Category { get; set; }
        public string Description_OriginalValue { get; set; }
        public int? Description_AttributeId { get; set; }
        public bool Description_IsReadOnly { get; set; }

        public string StartDate { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }
        public string StartDate_Status { get; set; }
        public string StartDate_Source { get; set; }
        public int? StartDate_FieldId { get; set; }
        public string StartDate_OriginalValue { get; set; }
        public int? StartDate_AttributeId { get; set; }
        public bool StartDate_IsReadOnly { get; set; }

        public string EndDate { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }
        public string EndDate_Status { get; set; }
        public string EndDate_Source { get; set; }
        public int? EndDate_FieldId { get; set; }
        public string EndDate_OriginalValue { get; set; }
        public int? EndDate_AttributeId { get; set; }
        public bool EndDate_IsReadOnly { get; set; }

        public string OrganizationalUnit { get; set; }
        public string OrganizationalUnit_BusinessName { get; set; }
        public string OrganizationalUnit_BusinessDescription { get; set; }
        public string OrganizationalUnit_Status { get; set; }
        public string OrganizationalUnit_Source { get; set; }
        public int? OrganizationalUnit_FieldId { get; set; }
        public string OrganizationalUnit_OriginalValue { get; set; }
        public int? OrganizationalUnit_AttributeId { get; set; }
        public bool OrganizationalUnit_IsReadOnly { get; set; }

        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitCode_BusinessName { get; set; }
        public string OrganizationalUnitCode_BusinessDescription { get; set; }
        public string OrganizationalUnitCode_Status { get; set; }
        public string OrganizationalUnitCode_Source { get; set; }
        public int? OrganizationalUnitCode_FieldId { get; set; }
        public string OrganizationalUnitCode_OriginalValue { get; set; }
        public int? OrganizationalUnitCode_AttributeId { get; set; }
        public bool OrganizationalUnitCode_IsReadOnly { get; set; }

        public string OrganizationalUnitMasterId { get; set; }
        public string OrganizationalUnitMasterId_BusinessName { get; set; }
        public string OrganizationalUnitMasterId_BusinessDescription { get; set; }
        public string OrganizationalUnitMasterId_Status { get; set; }
        public string OrganizationalUnitMasterId_Source { get; set; }
        public int? OrganizationalUnitMasterId_FieldId { get; set; }
        public string OrganizationalUnitMasterId_OriginalValue { get; set; }
        public int? OrganizationalUnitMasterId_AttributeId { get; set; }
        public bool OrganizationalUnitMasterId_IsReadOnly { get; set; }

        public string DesignationType { get; set; }
        public string DesignationType_BusinessName { get; set; }
        public string DesignationType_BusinessDescription { get; set; }
        public string DesignationType_Status { get; set; }
        public string DesignationType_Source { get; set; }
        public int? DesignationType_FieldId { get; set; }
        public string DesignationType_OriginalValue { get; set; }
        public int? DesignationType_AttributeId { get; set; }
        public bool DesignationType_IsReadOnly { get; set; }

        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_BusinessName { get; set; }
        public string DesignationTypeName_BusinessDescription { get; set; }
        public string DesignationTypeName_Status { get; set; }
        public string DesignationTypeName_Source { get; set; }
        public int? DesignationTypeName_FieldId { get; set; }
        public string DesignationTypeName_OriginalValue { get; set; }
        public int? DesignationTypeName_AttributeId { get; set; }
        public bool DesignationTypeName_IsReadOnly { get; set; }

        public string DesignationTypeSourceSystemRecordId { get; set; }
        public string DesignationTypeSourceSystemRecordId_BusinessName { get; set; }
        public string DesignationTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string DesignationTypeSourceSystemRecordId_Status { get; set; }
        public string DesignationTypeSourceSystemRecordId_Source { get; set; }
        public int? DesignationTypeSourceSystemRecordId_FieldId { get; set; }
        public string DesignationTypeSourceSystemRecordId_OriginalValue { get; set; }
        public int? DesignationTypeSourceSystemRecordId_AttributeId { get; set; }
        public bool DesignationTypeSourceSystemRecordId_IsReadOnly { get; set; }

        public string DesignationTypeMasterId { get; set; }
        public string DesignationTypeMasterId_BusinessName { get; set; }
        public string DesignationTypeMasterId_BusinessDescription { get; set; }
        public string DesignationTypeMasterId_Status { get; set; }
        public string DesignationTypeMasterId_Source { get; set; }
        public int? DesignationTypeMasterId_FieldId { get; set; }
        public string DesignationTypeMasterId_OriginalValue { get; set; }
        public int? DesignationTypeMasterId_AttributeId { get; set; }
        public bool DesignationTypeMasterId_IsReadOnly { get; set; }

        public string KFSAccountCode { get; set; }
        public string KFSAccountCode_BusinessName { get; set; }
        public string KFSAccountCode_BusinessDescription { get; set; }
        public string KFSAccountCode_Status { get; set; }
        public string KFSAccountCode_Source { get; set; }
        public int? KFSAccountCode_FieldId { get; set; }
        public string KFSAccountCode_OriginalValue { get; set; }
        public int? KFSAccountCode_AttributeId { get; set; }
        public bool KFSAccountCode_IsReadOnly { get; set; }

        public string VSECategoryName { get; set; }
        public string VSECategoryName_BusinessName { get; set; }
        public string VSECategoryName_BusinessDescription { get; set; }
        public string VSECategoryName_Status { get; set; }
        public string VSECategoryName_Source { get; set; }
        public int? VSECategoryName_FieldId { get; set; }
        public string VSECategoryName_OriginalValue { get; set; }
        public int? VSECategoryName_AttributeId { get; set; }
        public bool VSECategoryName_IsReadOnly { get; set; }

        public string VSECategorySourceSystemRecordId { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessName { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessDescription { get; set; }
        public string VSECategorySourceSystemRecordId_Status { get; set; }
        public string VSECategorySourceSystemRecordId_Source { get; set; }
        public int? VSECategorySourceSystemRecordId_FieldId { get; set; }
        public string VSECategorySourceSystemRecordId_OriginalValue { get; set; }
        public int? VSECategorySourceSystemRecordId_AttributeId { get; set; }
        public bool VSECategorySourceSystemRecordId_IsReadOnly { get; set; }

        public string VSECategoryMasterId { get; set; }
        public string VSECategoryMasterId_BusinessName { get; set; }
        public string VSECategoryMasterId_BusinessDescription { get; set; }
        public string VSECategoryMasterId_Status { get; set; }
        public string VSECategoryMasterId_Source { get; set; }
        public int? VSECategoryMasterId_FieldId { get; set; }
        public string VSECategoryMasterId_OriginalValue { get; set; }
        public int? VSECategoryMasterId_AttributeId { get; set; }
        public bool VSECategoryMasterId_IsReadOnly { get; set; }

        public string GLOrganizationName { get; set; }
        public string GLOrganizationName_BusinessName { get; set; }
        public string GLOrganizationName_BusinessDescription { get; set; }
        public string GLOrganizationName_Status { get; set; }
        public string GLOrganizationName_Source { get; set; }
        public int? GLOrganizationName_FieldId { get; set; }
        public string GLOrganizationName_OriginalValue { get; set; }
        public int? GLOrganizationName_AttributeId { get; set; }
        public bool GLOrganizationName_IsReadOnly { get; set; }

        public string GLOrganizationCode { get; set; }
        public string GLOrganizationCode_BusinessName { get; set; }
        public string GLOrganizationCode_BusinessDescription { get; set; }
        public string GLOrganizationCode_Status { get; set; }
        public string GLOrganizationCode_Source { get; set; }
        public int? GLOrganizationCode_FieldId { get; set; }
        public string GLOrganizationCode_OriginalValue { get; set; }
        public int? GLOrganizationCode_AttributeId { get; set; }
        public bool GLOrganizationCode_IsReadOnly { get; set; }

        public string GLOrganizationMasterId { get; set; }
        public string GLOrganizationMasterId_BusinessName { get; set; }
        public string GLOrganizationMasterId_BusinessDescription { get; set; }
        public string GLOrganizationMasterId_Status { get; set; }
        public string GLOrganizationMasterId_Source { get; set; }
        public int? GLOrganizationMasterId_FieldId { get; set; }
        public string GLOrganizationMasterId_OriginalValue { get; set; }
        public int? GLOrganizationMasterId_AttributeId { get; set; }
        public bool GLOrganizationMasterId_IsReadOnly { get; set; }

        public string DesignationUseTypeName { get; set; }
        public string DesignationUseTypeName_BusinessName { get; set; }
        public string DesignationUseTypeName_BusinessDescription { get; set; }
        public string DesignationUseTypeName_Status { get; set; }
        public string DesignationUseTypeName_Source { get; set; }
        public int? DesignationUseTypeName_FieldId { get; set; }
        public string DesignationUseTypeName_OriginalValue { get; set; }
        public int? DesignationUseTypeName_AttributeId { get; set; }
        public bool DesignationUseTypeName_IsReadOnly { get; set; }

        public string DesignationUseTypeMasterId { get; set; }
        public string DesignationUseTypeMasterId_BusinessName { get; set; }
        public string DesignationUseTypeMasterId_BusinessDescription { get; set; }
        public string DesignationUseTypeMasterId_Status { get; set; }
        public string DesignationUseTypeMasterId_Source { get; set; }
        public int? DesignationUseTypeMasterId_FieldId { get; set; }
        public string DesignationUseTypeMasterId_OriginalValue { get; set; }
        public int? DesignationUseTypeMasterId_AttributeId { get; set; }
        public bool DesignationUseTypeMasterId_IsReadOnly { get; set; }

        public string DesignationStatus { get; set; }
        public string DesignationStatus_BusinessName { get; set; }
        public string DesignationStatus_BusinessDescription { get; set; }
        public string DesignationStatus_Status { get; set; }
        public string DesignationStatus_Source { get; set; }
        public int? DesignationStatus_FieldId { get; set; }
        public string DesignationStatus_OriginalValue { get; set; }
        public int? DesignationStatus_AttributeId { get; set; }
        public bool DesignationStatus_IsReadOnly { get; set; }

        public string DesignationStatusMasterId { get; set; }
        public string DesignationStatusMasterId_BusinessName { get; set; }
        public string DesignationStatusMasterId_BusinessDescription { get; set; }
        public string DesignationStatusMasterId_Status { get; set; }
        public string DesignationStatusMasterId_Source { get; set; }
        public int? DesignationStatusMasterId_FieldId { get; set; }
        public string DesignationStatusMasterId_OriginalValue { get; set; }
        public int? DesignationStatusMasterId_AttributeId { get; set; }
        public bool DesignationStatusMasterId_IsReadOnly { get; set; }

        #endregion


        #region Drop-Downs

        public List<SelectListItem> GetDepartmentList { get; set; }
        public List<SelectListItem> DesignationTypeList { get; set; }
        public List<SelectListItem> VSECategoryList { get; set; }
        public List<SelectListItem> GLOrganizationList { get; set; }
        public List<SelectListItem> DesignationUseTypeList { get; set; }
        public List<SelectListItem> OrganizationalUnitList { get; set; }
        public List<SelectListItem> DesignationStatusList { get; set; }

        #endregion

        public List<DesignationHistoryViewModel> HistoryData { get; set; }
    }
}
