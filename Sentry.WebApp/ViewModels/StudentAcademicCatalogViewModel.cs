using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicCatalogViewModel : BaseIntegrationViewModel
    {
        public StudentAcademicCatalogViewModel() : base() { }        

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }


        #region DegreeTypeName
        public string DegreeTypeName { get; set; }
        public string DegreeTypeName_BusinessName { get; set; }
        public string DegreeTypeName_BusinessDescription { get; set; }
        public string DegreeTypeName_Status { get; set; }
        public string DegreeTypeName_Source { get; set; }
        public string DegreeTypeName_Category { get; set; }
        public string DegreeTypeName_OriginalValue { get; set; }
        public int? DegreeTypeName_AttributeId { get; set; }
        public bool DegreeTypeName_IsReadOnly { get; set; }
        #endregion

        #region DegreeTypeSourceSystemRecordId
        public string DegreeTypeSourceSystemRecordId { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string DegreeTypeSourceSystemRecordId_Status { get; set; }
        public string DegreeTypeSourceSystemRecordId_Source { get; set; }
        public string DegreeTypeSourceSystemRecordId_Category { get; set; }
        public string DegreeTypeSourceSystemRecordId_OriginalValue { get; set; }
        public int? DegreeTypeSourceSystemRecordId_AttributeId { get; set; }
        public bool DegreeTypeSourceSystemRecordId_IsReadOnly { get; set; }
        #endregion

        #region DegreeTypeMasterId
        public string DegreeTypeMasterId { get; set; }
        public string DegreeTypeMasterId_BusinessName { get; set; }
        public string DegreeTypeMasterId_BusinessDescription { get; set; }
        public string DegreeTypeMasterId_Status { get; set; }
        public string DegreeTypeMasterId_Source { get; set; }
        public string DegreeTypeMasterId_Category { get; set; }
        public string DegreeTypeMasterId_OriginalValue { get; set; }
        public int? DegreeTypeMasterId_AttributeId { get; set; }
        public bool DegreeTypeMasterId_IsReadOnly { get; set; }
        #endregion


        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public string AcademicCareerName_BusinessName { get; set; }
        public string AcademicCareerName_BusinessDescription { get; set; }
        public string AcademicCareerName_Status { get; set; }
        public string AcademicCareerName_Source { get; set; }
        public string AcademicCareerName_Category { get; set; }
        public string AcademicCareerName_OriginalValue { get; set; }
        public int? AcademicCareerName_AttributeId { get; set; }
        public bool AcademicCareerName_IsReadOnly { get; set; }
        #endregion

        #region AcademicCareerSourceSystemRecordId
        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }
        public string AcademicCareerSourceSystemRecordId_Source { get; set; }
        public string AcademicCareerSourceSystemRecordId_Category { get; set; }
        public string AcademicCareerSourceSystemRecordId_OriginalValue { get; set; }
        public int? AcademicCareerSourceSystemRecordId_AttributeId { get; set; }
        public bool AcademicCareerSourceSystemRecordId_IsReadOnly { get; set; }
        #endregion

        #region AcademicCareerMasterId
        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_BusinessName { get; set; }
        public string AcademicCareerMasterId_BusinessDescription { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }
        public string AcademicCareerMasterId_Source { get; set; }
        public string AcademicCareerMasterId_Category { get; set; }
        public string AcademicCareerMasterId_OriginalValue { get; set; }
        public int? AcademicCareerMasterId_AttributeId { get; set; }
        public bool AcademicCareerMasterId_IsReadOnly { get; set; }
        #endregion


        #region AcademicProgramName
        public string AcademicProgramName { get; set; }
        public string AcademicProgramName_BusinessName { get; set; }
        public string AcademicProgramName_BusinessDescription { get; set; }
        public string AcademicProgramName_Status { get; set; }
        public string AcademicProgramName_Source { get; set; }
        public string AcademicProgramName_Category { get; set; }
        public string AcademicProgramName_OriginalValue { get; set; }
        public int? AcademicProgramName_AttributeId { get; set; }
        public bool AcademicProgramName_IsReadOnly { get; set; }
        #endregion

        #region AcademicProgramSourceSystemRecordId
        public string AcademicProgramSourceSystemRecordId { get; set; }
        public string AcademicProgramSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicProgramSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicProgramSourceSystemRecordId_Status { get; set; }
        public string AcademicProgramSourceSystemRecordId_Source { get; set; }
        public string AcademicProgramSourceSystemRecordId_Category { get; set; }
        public string AcademicProgramSourceSystemRecordId_OriginalValue { get; set; }
        public int? AcademicProgramSourceSystemRecordId_AttributeId { get; set; }
        public bool AcademicProgramSourceSystemRecordId_IsReadOnly { get; set; }
        #endregion

        #region AcademicProgramMasterId
        public string AcademicProgramMasterId { get; set; }
        public string AcademicProgramMasterId_BusinessName { get; set; }
        public string AcademicProgramMasterId_BusinessDescription { get; set; }
        public string AcademicProgramMasterId_Status { get; set; }
        public string AcademicProgramMasterId_Source { get; set; }
        public string AcademicProgramMasterId_Category { get; set; }
        public string AcademicProgramMasterId_OriginalValue { get; set; }
        public int? AcademicProgramMasterId_AttributeId { get; set; }
        public bool AcademicProgramMasterId_IsReadOnly { get; set; }
        #endregion


        #region AcademicPlanName
        public string AcademicPlanName { get; set; }
        public string AcademicPlanName_BusinessName { get; set; }
        public string AcademicPlanName_BusinessDescription { get; set; }
        public string AcademicPlanName_Status { get; set; }
        public string AcademicPlanName_Source { get; set; }
        public string AcademicPlanName_Category { get; set; }
        public string AcademicPlanName_OriginalValue { get; set; }
        public int? AcademicPlanName_AttributeId { get; set; }
        public bool AcademicPlanName_IsReadOnly { get; set; }
        #endregion

        #region AcademicPlanSourceSystemRecordId
        public string AcademicPlanSourceSystemRecordId { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicPlanSourceSystemRecordId_Status { get; set; }
        public string AcademicPlanSourceSystemRecordId_Source { get; set; }
        public string AcademicPlanSourceSystemRecordId_Category { get; set; }
        public string AcademicPlanSourceSystemRecordId_OriginalValue { get; set; }
        public int? AcademicPlanSourceSystemRecordId_AttributeId { get; set; }
        public bool AcademicPlanSourceSystemRecordId_IsReadOnly { get; set; }
        #endregion

        #region AcademicPlanMasterId
        public string AcademicPlanMasterId { get; set; }
        public string AcademicPlanMasterId_BusinessName { get; set; }
        public string AcademicPlanMasterId_BusinessDescription { get; set; }
        public string AcademicPlanMasterId_Status { get; set; }
        public string AcademicPlanMasterId_Source { get; set; }
        public string AcademicPlanMasterId_Category { get; set; }
        public string AcademicPlanMasterId_OriginalValue { get; set; }
        public int? AcademicPlanMasterId_AttributeId { get; set; }
        public bool AcademicPlanMasterId_IsReadOnly { get; set; }
        #endregion


        #region DepartmentName
        public string DepartmentName { get; set; }
        public string DepartmentName_BusinessName { get; set; }
        public string DepartmentName_BusinessDescription { get; set; }
        public string DepartmentName_Status { get; set; }
        public string DepartmentName_Source { get; set; }
        public string DepartmentName_Category { get; set; }
        public string DepartmentName_OriginalValue { get; set; }
        public int? DepartmentName_AttributeId { get; set; }
        public bool DepartmentName_IsReadOnly { get; set; }
        #endregion

        #region DepartmentCode
        public string DepartmentCode { get; set; }
        public string DepartmentCode_BusinessName { get; set; }
        public string DepartmentCode_BusinessDescription { get; set; }
        public string DepartmentCode_Status { get; set; }
        public string DepartmentCode_Source { get; set; }
        public string DepartmentCode_Category { get; set; }
        public string DepartmentCode_OriginalValue { get; set; }
        public int? DepartmentCode_AttributeId { get; set; }
        public bool DepartmentCode_IsReadOnly { get; set; }
        #endregion

        #region DepartmentMasterId
        public string DepartmentMasterId { get; set; }
        public string DepartmentMasterId_BusinessName { get; set; }
        public string DepartmentMasterId_BusinessDescription { get; set; }
        public string DepartmentMasterId_Status { get; set; }
        public string DepartmentMasterId_Source { get; set; }
        public string DepartmentMasterId_Category { get; set; }
        public string DepartmentMasterId_OriginalValue { get; set; }
        public int? DepartmentMasterId_AttributeId { get; set; }
        public bool DepartmentMasterId_IsReadOnly { get; set; }
        #endregion


        #region AcademicPlanTypeName
        public string AcademicPlanTypeName { get; set; }
        public string AcademicPlanTypeName_BusinessName { get; set; }
        public string AcademicPlanTypeName_BusinessDescription { get; set; }
        public string AcademicPlanTypeName_Status { get; set; }
        public string AcademicPlanTypeName_Source { get; set; }
        public string AcademicPlanTypeName_Category { get; set; }
        public string AcademicPlanTypeName_OriginalValue { get; set; }
        public int? AcademicPlanTypeName_AttributeId { get; set; }
        public bool AcademicPlanTypeName_IsReadOnly { get; set; }
        #endregion

        #region AcademicPlanTypeSourceSystemRecordId
        public string AcademicPlanTypeSourceSystemRecordId { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_Status { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_Source { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_Category { get; set; }
        public string AcademicPlanTypeSourceSystemRecordId_OriginalValue { get; set; }
        public int? AcademicPlanTypeSourceSystemRecordId_AttributeId { get; set; }
        public bool AcademicPlanTypeSourceSystemRecordId_IsReadOnly { get; set; }
        #endregion

        #region AcademicPlanTypeMasterId
        public string AcademicPlanTypeMasterId { get; set; }
        public string AcademicPlanTypeMasterId_BusinessName { get; set; }
        public string AcademicPlanTypeMasterId_BusinessDescription { get; set; }
        public string AcademicPlanTypeMasterId_Status { get; set; }
        public string AcademicPlanTypeMasterId_Source { get; set; }
        public string AcademicPlanTypeMasterId_Category { get; set; }
        public string AcademicPlanTypeMasterId_OriginalValue { get; set; }
        public int? AcademicPlanTypeMasterId_AttributeId { get; set; }
        public bool AcademicPlanTypeMasterId_IsReadOnly { get; set; }
        #endregion


        #region TranscriptDescription
        public string TranscriptDescription { get; set; }
        public string TranscriptDescription_BusinessName { get; set; }
        public string TranscriptDescription_BusinessDescription { get; set; }
        public string TranscriptDescription_Status { get; set; }
        public string TranscriptDescription_Source { get; set; }
        public string TranscriptDescription_Category { get; set; }
        public string TranscriptDescription_OriginalValue { get; set; }
        public int? TranscriptDescription_AttributeId { get; set; }
        public bool TranscriptDescription_IsReadOnly { get; set; }
        #endregion

        #region Drop-Downs
        public List<SelectListItem> DegreeTypeList { get; set; }
        public List<SelectListItem> AcademicCareerList { get; set; }
        public List<SelectListItem> AcademicProgramList { get; set; }
        public List<SelectListItem> AcademicPlanList { get; set; }
        public List<SelectListItem> DepartmentList { get; set; }
        public List<SelectListItem> AcademicPlanTypeList { get; set; }
        #endregion

        public List<StudentAcademicCatalogHistoryViewModel> HistoryData { get; set; }

    }
}
