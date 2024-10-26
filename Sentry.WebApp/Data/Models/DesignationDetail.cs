using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    //[Table("Designations_Detail", Schema = "Integration")]
    public class DesignationDetail : BaseDetail
    {                
        #region DesignationId
        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }
        public string DesignationId_Source { get; set; }
        public string DesignationId_Category { get; set; }
        public string DesignationId_Status { get; set; }
        public int? DesignationId_AttributeId { get; set; }
        #endregion

        #region DesignationName
        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }
        public string DesignationName_Source { get; set; }
        public string DesignationName_Category { get; set; }
        public string DesignationName_Status { get; set; }
        public int? DesignationName_AttributeId { get; set; }
        #endregion

        #region Description
        public string Description { get; set; }
        public string Description_BusinessName { get; set; }
        public string Description_BusinessDescription { get; set; }
        public string Description_Source { get; set; }
        public string Description_Category { get; set; }
        public string Description_Status { get; set; }
        public int? Description_AttributeId { get; set; }
        #endregion

        #region StartDate
        public string StartDate { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }
        public string StartDate_Source { get; set; }
        public string StartDate_Category { get; set; }
        public string StartDate_Status { get; set; }
        public int? StartDate_AttributeId { get; set; }
        #endregion

        #region EndDate
        public string EndDate { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }
        public string EndDate_Source { get; set; }
        public string EndDate_Category { get; set; }
        public string EndDate_Status { get; set; }
        public int? EndDate_AttributeId { get; set; }
        #endregion

        #region OrganizationalUnit
        public string OrganizationalUnit { get; set; }
        public string OrganizationalUnit_BusinessName { get; set; }
        public string OrganizationalUnit_BusinessDescription { get; set; }
        public string OrganizationalUnit_Source { get; set; }
        public string OrganizationalUnit_Category { get; set; }
        public string OrganizationalUnit_Status { get; set; }
        public int? OrganizationalUnit_AttributeId { get; set; }
        #endregion

        #region OrganizationalUnitCode
        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitCode_BusinessName { get; set; }
        public string OrganizationalUnitCode_BusinessDescription { get; set; }
        public string OrganizationalUnitCode_Source { get; set; }
        public string OrganizationalUnitCode_Category { get; set; }
        public string OrganizationalUnitCode_Status { get; set; }
        public int? OrganizationalUnitCode_AttributeId { get; set; }
        #endregion


        #region OrganizationalUnitMasterId
        public string OrganizationalUnitMasterId { get; set; }
        public string OrganizationalUnitMasterId_BusinessName { get; set; }
        public string OrganizationalUnitMasterId_BusinessDescription { get; set; }
        public string OrganizationalUnitMasterId_Source { get; set; }
        public string OrganizationalUnitMasterId_Category { get; set; }
        public string OrganizationalUnitMasterId_Status { get; set; }
        public int? OrganizationalUnitMasterId_AttributeId { get; set; }
        #endregion

        #region DesignationTypeName
        public string DesignationTypeName { get; set; }
        public string DesignationTypeName_BusinessName { get; set; }
        public string DesignationTypeName_BusinessDescription { get; set; }
        public string DesignationTypeName_Source { get; set; }
        public string DesignationTypeName_Category { get; set; }
        public string DesignationTypeName_Status { get; set; }
        public int? DesignationTypeName_AttributeId { get; set; }
        #endregion

        #region DesignationTypeSourceSystemRecordId
        public string DesignationTypeSourceSystemRecordId { get; set; }
        public string DesignationTypeSourceSystemRecordId_BusinessName { get; set; }
        public string DesignationTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string DesignationTypeSourceSystemRecordId_Source { get; set; }
        public string DesignationTypeSourceSystemRecordId_Category { get; set; }
        public string DesignationTypeSourceSystemRecordId_Status { get; set; }
        public int? DesignationTypeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region DesignationTypeMasterId
        public string DesignationTypeMasterId { get; set; }
        public string DesignationTypeMasterId_BusinessName { get; set; }
        public string DesignationTypeMasterId_BusinessDescription { get; set; }
        public string DesignationTypeMasterId_Source { get; set; }
        public string DesignationTypeMasterId_Category { get; set; }
        public string DesignationTypeMasterId_Status { get; set; }
        public int? DesignationTypeMasterId_AttributeId { get; set; }
        #endregion

        #region KFSAccountCode
        public string KFSAccountCode { get; set; }
        public string KFSAccountCode_BusinessName { get; set; }
        public string KFSAccountCode_BusinessDescription { get; set; }
        public string KFSAccountCode_Source { get; set; }
        public string KFSAccountCode_Category { get; set; }
        public string KFSAccountCode_Status { get; set; }
        public int? KFSAccountCode_AttributeId { get; set; }
        #endregion

        #region VSECategoryName
        public string VSECategoryName { get; set; }
        public string VSECategoryName_BusinessName { get; set; }
        public string VSECategoryName_BusinessDescription { get; set; }
        public string VSECategoryName_Source { get; set; }
        public string VSECategoryName_Category { get; set; }
        public string VSECategoryName_Status { get; set; }
        public int? VSECategoryName_AttributeId { get; set; }
        #endregion

        #region VSECategorySourceSystemRecordId
        public string VSECategorySourceSystemRecordId { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessName { get; set; }
        public string VSECategorySourceSystemRecordId_BusinessDescription { get; set; }
        public string VSECategorySourceSystemRecordId_Source { get; set; }
        public string VSECategorySourceSystemRecordId_Category { get; set; }
        public string VSECategorySourceSystemRecordId_Status { get; set; }
        public int? VSECategorySourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region VSECategoryMasterId
        public string VSECategoryMasterId { get; set; }
        public string VSECategoryMasterId_BusinessName { get; set; }
        public string VSECategoryMasterId_BusinessDescription { get; set; }
        public string VSECategoryMasterId_Source { get; set; }
        public string VSECategoryMasterId_Category { get; set; }
        public string VSECategoryMasterId_Status { get; set; }
        public int? VSECategoryMasterId_AttributeId { get; set; }
        #endregion

        #region GLOrganizationName
        public string GLOrganizationName { get; set; }
        public string GLOrganizationName_BusinessName { get; set; }
        public string GLOrganizationName_BusinessDescription { get; set; }
        public string GLOrganizationName_Source { get; set; }
        public string GLOrganizationName_Category { get; set; }
        public string GLOrganizationName_Status { get; set; }
        public int? GLOrganizationName_AttributeId { get; set; }
        #endregion

        #region GLOrganizationCode
        public string GLOrganizationCode { get; set; }
        public string GLOrganizationCode_BusinessName { get; set; }
        public string GLOrganizationCode_BusinessDescription { get; set; }
        public string GLOrganizationCode_Source { get; set; }
        public string GLOrganizationCode_Category { get; set; }
        public string GLOrganizationCode_Status { get; set; }
        public int? GLOrganizationCode_AttributeId { get; set; }
        #endregion

        #region GLOrganizationMasterId
        public string GLOrganizationMasterId { get; set; }
        public string GLOrganizationMasterId_BusinessName { get; set; }
        public string GLOrganizationMasterId_BusinessDescription { get; set; }
        public string GLOrganizationMasterId_Source { get; set; }
        public string GLOrganizationMasterId_Category { get; set; }
        public string GLOrganizationMasterId_Status { get; set; }
        public int? GLOrganizationMasterId_AttributeId { get; set; }
        #endregion

        #region DesignationUseTypeName
        public string DesignationUseTypeName { get; set; }
        public string DesignationUseTypeName_BusinessName { get; set; }
        public string DesignationUseTypeName_BusinessDescription { get; set; }
        public string DesignationUseTypeName_Source { get; set; }
        public string DesignationUseTypeName_Category { get; set; }
        public string DesignationUseTypeName_Status { get; set; }
        public int? DesignationUseTypeName_AttributeId { get; set; }
        #endregion

        #region DesignationUseTypeMasterId
        public string DesignationUseTypeMasterId { get; set; }
        public string DesignationUseTypeMasterId_BusinessName { get; set; }
        public string DesignationUseTypeMasterId_BusinessDescription { get; set; }
        public string DesignationUseTypeMasterId_Source { get; set; }
        public string DesignationUseTypeMasterId_Category { get; set; }
        public string DesignationUseTypeMasterId_Status { get; set; }
        public int? DesignationUseTypeMasterId_AttributeId { get; set; }
        #endregion

        #region DesignationStatus
        public string DesignationStatus { get; set; }
        public string DesignationStatus_BusinessName { get; set; }
        public string DesignationStatus_BusinessDescription { get; set; }
        public string DesignationStatus_Source { get; set; }
        public string DesignationStatus_Category { get; set; }
        public string DesignationStatus_Status { get; set; }
        public int? DesignationStatus_AttributeId { get; set; }
        #endregion

        #region DesignationStatusMasterId
        public string DesignationStatusMasterId { get; set; }
        public string DesignationStatusMasterId_BusinessName { get; set; }
        public string DesignationStatusMasterId_BusinessDescription { get; set; }
        public string DesignationStatusMasterId_Source { get; set; }
        public string DesignationStatusMasterId_Category { get; set; }
        public string DesignationStatusMasterId_Status { get; set; }
        public int? DesignationStatusMasterId_AttributeId { get; set; }
        #endregion
    }
}
