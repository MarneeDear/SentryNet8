using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class DegreeViewModel : BaseIntegrationViewModel
    {
        public DegreeViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }



        public string ClassOf { get; set; }
        public string ClassOf_BusinessName { get; set; }
        public string ClassOf_BusinessDescription { get; set; }
        public int? ClassOf_AttributeId { get; set; }
        public string ClassOf_OriginalValue { get; set; }
        public string ClassOf_Status { get; set; }
        public string ClassOf_Source { get; set; }
        public bool ClassOf_IsReadOnly { get; set; }




        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        public int? Student_AttributeId { get; set; }
        public string Student_OriginalValue { get; set; }
        public string Student_Status { get; set; }
        public string Student_Source { get; set; }
        public bool Student_IsReadOnly { get; set; }

        public string StudentSourceSystemRecordId { get; set; }
        public string StudentSourceSystemRecordId_BusinessName { get; set; }
        public string StudentSourceSystemRecordId_BusinessDescription { get; set; }
        public int? StudentSourceSystemRecordId_AttributeId { get; set; }
        public string StudentSourceSystemRecordId_OriginalValue { get; set; }
        public string StudentSourceSystemRecordId_Status { get; set; }
        public string StudentSourceSystemRecordId_Source { get; set; }
        public bool StudentSourceSystemRecordId_IsReadOnly { get; set; }

        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public int? StudentMasterId_AttributeId { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }
        public bool StudentMasterId_IsReadOnly { get; set; }



        public string EducationalInstitution { get; set; }
        public string EducationalInstitution_BusinessName { get; set; }
        public string EducationalInstitution_BusinessDescription { get; set; }
        public int? EducationalInstitution_AttributeId { get; set; }
        public string EducationalInstitution_OriginalValue { get; set; }
        public string EducationalInstitution_Status { get; set; }
        public string EducationalInstitution_Source { get; set; }
        public bool EducationalInstitution_IsReadOnly { get; set; }

        public string EducationalInstitutionSourceSystemRecordId { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_BusinessName { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_BusinessDescription { get; set; }
        public int? EducationalInstitutionSourceSystemRecordId_AttributeId { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_OriginalValue { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_Status { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_Source { get; set; }
        public bool EducationalInstitutionSourceSystemRecordId_IsReadOnly { get; set; }

        public string EducationalInstitutionMasterId { get; set; }
        public string EducationalInstitutionMasterId_BusinessName { get; set; }
        public string EducationalInstitutionMasterId_BusinessDescription { get; set; }
        public int? EducationalInstitutionMasterId_AttributeId { get; set; }
        public string EducationalInstitutionMasterId_OriginalValue { get; set; }
        public string EducationalInstitutionMasterId_Status { get; set; }
        public string EducationalInstitutionMasterId_Source { get; set; }
        public bool EducationalInstitutionMasterId_IsReadOnly { get; set; }



        public string PreferredClassOf { get; set; }
        public string PreferredClassOf_BusinessName { get; set; }
        public string PreferredClassOf_BusinessDescription { get; set; }
        public int? PreferredClassOf_AttributeId { get; set; }
        public string PreferredClassOf_OriginalValue { get; set; }
        public string PreferredClassOf_Status { get; set; }
        public string PreferredClassOf_Source { get; set; }
        public bool PreferredClassOf_IsReadOnly { get; set; }



        public string AwardedDate { get; set; }
        public string AwardedDate_BusinessName { get; set; }
        public string AwardedDate_BusinessDescription { get; set; }
        public int? AwardedDate_AttributeId { get; set; }
        public string AwardedDate_OriginalValue { get; set; }
        public string AwardedDate_Status { get; set; }
        public string AwardedDate_Source { get; set; }
        public bool AwardedDate_IsReadOnly { get; set; }



        public string HonorSourceSystemRecordId { get; set; }
        public string HonorSourceSystemRecordId_BusinessName { get; set; }
        public string HonorSourceSystemRecordId_BusinessDescription { get; set; }
        public int? HonorSourceSystemRecordId_AttributeId { get; set; }
        public string HonorSourceSystemRecordId_OriginalValue { get; set; }
        public string HonorSourceSystemRecordId_Status { get; set; }
        public string HonorSourceSystemRecordId_Source { get; set; }
        public bool HonorSourceSystemRecordId_IsReadOnly { get; set; }

        public string HonorMasterId { get; set; }
        public string HonorMasterId_BusinessName { get; set; }
        public string HonorMasterId_BusinessDescription { get; set; }
        public int? HonorMasterId_AttributeId { get; set; }
        public string HonorMasterId_OriginalValue { get; set; }
        public string HonorMasterId_Status { get; set; }
        public string HonorMasterId_Source { get; set; }
        public bool HonorMasterId_IsReadOnly { get; set; }



        public string DegreeStatus { get; set; }
        public string DegreeStatus_BusinessName { get; set; }
        public string DegreeStatus_BusinessDescription { get; set; }
        public int? DegreeStatus_AttributeId { get; set; }
        public string DegreeStatus_OriginalValue { get; set; }
        public string DegreeStatus_Status { get; set; }
        public string DegreeStatus_Source { get; set; }
        public bool DegreeStatus_IsReadOnly { get; set; }

        public string DegreeStatusSourceSystemRecordId { get; set; }
        public string DegreeStatusSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public int? DegreeStatusSourceSystemRecordId_AttributeId { get; set; }
        public string DegreeStatusSourceSystemRecordId_OriginalValue { get; set; }
        public string DegreeStatusSourceSystemRecordId_Status { get; set; }
        public string DegreeStatusSourceSystemRecordId_Source { get; set; }
        public bool DegreeStatusSourceSystemRecordId_IsReadOnly { get; set; }

        public string DegreeStatusMasterId { get; set; }
        public string DegreeStatusMasterId_BusinessName { get; set; }
        public string DegreeStatusMasterId_BusinessDescription { get; set; }
        public int? DegreeStatusMasterId_AttributeId { get; set; }
        public string DegreeStatusMasterId_OriginalValue { get; set; }
        public string DegreeStatusMasterId_Status { get; set; }
        public string DegreeStatusMasterId_Source { get; set; }
        public bool DegreeStatusMasterId_IsReadOnly { get; set; }



        public string AwardedTerm { get; set; }
        public string AwardedTerm_BusinessName { get; set; }
        public string AwardedTerm_BusinessDescription { get; set; }
        public int? AwardedTerm_AttributeId { get; set; }
        public string AwardedTerm_OriginalValue { get; set; }
        public string AwardedTerm_Status { get; set; }
        public string AwardedTerm_Source { get; set; }
        public bool AwardedTerm_IsReadOnly { get; set; }

        public string AwardedTermMasterId { get; set; }
        public string AwardedTermMasterId_BusinessName { get; set; }
        public string AwardedTermMasterId_BusinessDescription { get; set; }
        public int? AwardedTermMasterId_AttributeId { get; set; }
        public string AwardedTermMasterId_OriginalValue { get; set; }
        public string AwardedTermMasterId_Status { get; set; }
        public string AwardedTermMasterId_Source { get; set; }
        public bool AwardedTermMasterId_IsReadOnly { get; set; }



        public string DegreeType { get; set; }
        public string DegreeType_BusinessName { get; set; }
        public string DegreeType_BusinessDescription { get; set; }
        public int? DegreeType_AttributeId { get; set; }
        public string DegreeType_OriginalValue { get; set; }
        public string DegreeType_Status { get; set; }
        public string DegreeType_Source { get; set; }
        public bool DegreeType_IsReadOnly { get; set; }

        public string DegreeTypeSourceSystemRecordId { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public int? DegreeTypeSourceSystemRecordId_AttributeId { get; set; }
        public string DegreeTypeSourceSystemRecordId_OriginalValue { get; set; }
        public string DegreeTypeSourceSystemRecordId_Status { get; set; }
        public string DegreeTypeSourceSystemRecordId_Source { get; set; }
        public bool DegreeTypeSourceSystemRecordId_IsReadOnly { get; set; }

        public string DegreeTypeMasterId { get; set; }
        public string DegreeTypeMasterId_BusinessName { get; set; }
        public string DegreeTypeMasterId_BusinessDescription { get; set; }
        public int? DegreeTypeMasterId_AttributeId { get; set; }
        public string DegreeTypeMasterId_OriginalValue { get; set; }
        public string DegreeTypeMasterId_Status { get; set; }
        public string DegreeTypeMasterId_Source { get; set; }
        public bool DegreeTypeMasterId_IsReadOnly { get; set; }



        public string AcademicCareer { get; set; }
        public string AcademicCareer_BusinessName { get; set; }
        public string AcademicCareer_BusinessDescription { get; set; }
        public int? AcademicCareer_AttributeId { get; set; }
        public string AcademicCareer_OriginalValue { get; set; }
        public string AcademicCareer_Status { get; set; }
        public string AcademicCareer_Source { get; set; }
        public bool AcademicCareer_IsReadOnly { get; set; }

        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessDescription { get; set; }
        public int? AcademicCareerSourceSystemRecordId_AttributeId { get; set; }
        public string AcademicCareerSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }
        public string AcademicCareerSourceSystemRecordId_Source { get; set; }
        public bool AcademicCareerSourceSystemRecordId_IsReadOnly { get; set; }

        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_BusinessName { get; set; }
        public string AcademicCareerMasterId_BusinessDescription { get; set; }
        public int? AcademicCareerMasterId_AttributeId { get; set; }
        public string AcademicCareerMasterId_OriginalValue { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }
        public string AcademicCareerMasterId_Source { get; set; }
        public bool AcademicCareerMasterId_IsReadOnly { get; set; }


        #region Drop Downs
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> EducationalInstitutionList { get; set; }
        public List<SelectListItem> HonorList { get; set; }
        public List<SelectListItem> DegreeStatusList { get; set; }
        public List<SelectListItem> DegreeTypeList { get; set; }
        public List<SelectListItem> AwardedTermList { get; set; }
        public List<SelectListItem> AcademicCareerList { get; set; }
        #endregion

        public List<StudentDegreeHistoryViewModel> HistoryData { get; set; }

    }
}
