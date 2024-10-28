using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class AcademicPlanViewModel : BaseIntegrationViewModel
    {
        public AcademicPlanViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }




        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        public int? Student_AttributeId { get; set; }
        public string Student_OriginalValue { get; set; }
        public string Student_Status { get; set; }
        public string Student_Source { get; set; }
        public bool Student_IsReadOnly { get; set; }

        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public int? StudentMasterId_AttributeId { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }
        public bool StudentMasterId_IsReadOnly { get; set; }



        public string Enrollment { get; set; }
        public string Enrollment_BusinessName { get; set; }
        public string Enrollment_BusinessDescription { get; set; }
        //public int? Enrollment_AttributeId { get; set; }
        //public string Enrollment_OriginalValue { get; set; }
        public string Enrollment_Status { get; set; }
        //public string Enrollment_Source { get; set; }
        public bool Enrollment_IsReadOnly { get; set; }

        //public string EnrollmentSourceSystemRecordId { get; set; }
        //public string EnrollmentSourceSystemRecordId_BusinessName { get; set; }
        //public string EnrollmentSourceSystemRecordId_BusinessDescription { get; set; }
        //public int? EnrollmentSourceSystemRecordId_AttributeId { get; set; }
        //public string EnrollmentSourceSystemRecordId_OriginalValue { get; set; }
        //public string EnrollmentSourceSystemRecordId_Status { get; set; }
        //public string EnrollmentSourceSystemRecordId_Source { get; set; }
        //public bool EnrollmentSourceSystemRecordId_IsReadOnly { get; set; }

        //public string EnrollmentMasterId { get; set; }
        //public string EnrollmentMasterId_BusinessName { get; set; }
        //public string EnrollmentMasterId_BusinessDescription { get; set; }
        //public int? EnrollmentMasterId_AttributeId { get; set; }
        //public string EnrollmentMasterId_OriginalValue { get; set; }
        //public string EnrollmentMasterId_Status { get; set; }
        //public string EnrollmentMasterId_Source { get; set; }
        //public bool EnrollmentMasterId_IsReadOnly { get; set; }



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



        public string TermName { get; set; }
        public string TermName_BusinessName { get; set; }
        public string TermName_BusinessDescription { get; set; }
        public int? TermName_AttributeId { get; set; }
        public string TermName_OriginalValue { get; set; }
        public string TermName_Status { get; set; }
        public string TermName_Source { get; set; }
        public bool TermName_IsReadOnly { get; set; }

        public string TermCode { get; set; }
        public string TermCode_BusinessName { get; set; }
        public string TermCode_BusinessDescription { get; set; }
        public int? TermCode_AttributeId { get; set; }
        public string TermCode_OriginalValue { get; set; }
        public string TermCode_Status { get; set; }
        public string TermCode_Source { get; set; }
        public bool TermCode_IsReadOnly { get; set; }

        public string TermSourceSystemRecordId { get; set; }
        public string TermSourceSystemRecordId_BusinessName { get; set; }
        public string TermSourceSystemRecordId_BusinessDescription { get; set; }
        public int? TermSourceSystemRecordId_AttributeId { get; set; }
        public string TermSourceSystemRecordId_OriginalValue { get; set; }
        public string TermSourceSystemRecordId_Status { get; set; }
        public string TermSourceSystemRecordId_Source { get; set; }
        public bool TermSourceSystemRecordId_IsReadOnly { get; set; }

        public string TermMasterId { get; set; }
        public string TermMasterId_BusinessName { get; set; }
        public string TermMasterId_BusinessDescription { get; set; }
        public int? TermMasterId_AttributeId { get; set; }
        public string TermMasterId_OriginalValue { get; set; }
        public string TermMasterId_Status { get; set; }
        public string TermMasterId_Source { get; set; }
        public bool TermMasterId_IsReadOnly { get; set; }



        //public string CampusName { get; set; }
        //public string CampusName_BusinessName { get; set; }
        //public string CampusName_BusinessDescription { get; set; }
        //public int? CampusName_AttributeId { get; set; }
        //public string CampusName_OriginalValue { get; set; }
        //public string CampusName_Status { get; set; }
        //public string CampusName_Source { get; set; }
        //public bool CampusName_IsReadOnly { get; set; }

        public string CampusSourceSystemRecordId { get; set; }
        public string CampusSourceSystemRecordId_BusinessName { get; set; }
        public string CampusSourceSystemRecordId_BusinessDescription { get; set; }
        public int? CampusSourceSystemRecordId_AttributeId { get; set; }
        public string CampusSourceSystemRecordId_OriginalValue { get; set; }
        public string CampusSourceSystemRecordId_Status { get; set; }
        public string CampusSourceSystemRecordId_Source { get; set; }
        public bool CampusSourceSystemRecordId_IsReadOnly { get; set; }

        public string CampusMasterId { get; set; }
        public string CampusMasterId_BusinessName { get; set; }
        public string CampusMasterId_BusinessDescription { get; set; }
        public int? CampusMasterId_AttributeId { get; set; }
        public string CampusMasterId_OriginalValue { get; set; }
        public string CampusMasterId_Status { get; set; }
        public string CampusMasterId_Source { get; set; }
        public bool CampusMasterId_IsReadOnly { get; set; }



        public string Degree { get; set; }
        public string Degree_BusinessName { get; set; }
        public string Degree_BusinessDescription { get; set; }
        public int? Degree_AttributeId { get; set; }
        public string Degree_OriginalValue { get; set; }
        public string Degree_Status { get; set; }
        public string Degree_Source { get; set; }
        public bool Degree_IsReadOnly { get; set; }

        public string DegreeSourceSystemRecordId { get; set; }
        public string DegreeSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeSourceSystemRecordId_BusinessDescription { get; set; }
        public int? DegreeSourceSystemRecordId_AttributeId { get; set; }
        public string DegreeSourceSystemRecordId_OriginalValue { get; set; }
        public string DegreeSourceSystemRecordId_Status { get; set; }
        public string DegreeSourceSystemRecordId_Source { get; set; }
        public bool DegreeSourceSystemRecordId_IsReadOnly { get; set; }

        public string DegreeMasterId { get; set; }
        public string DegreeMasterId_BusinessName { get; set; }
        public string DegreeMasterId_BusinessDescription { get; set; }
        public int? DegreeMasterId_AttributeId { get; set; }
        public string DegreeMasterId_OriginalValue { get; set; }
        public string DegreeMasterId_Status { get; set; }
        public string DegreeMasterId_Source { get; set; }
        public bool DegreeMasterId_IsReadOnly { get; set; }



        public string AcademicPlan { get; set; }
        public string AcademicPlan_BusinessName { get; set; }
        public string AcademicPlan_BusinessDescription { get; set; }
        public int? AcademicPlan_AttributeId { get; set; }
        public string AcademicPlan_OriginalValue { get; set; }
        public string AcademicPlan_Status { get; set; }
        public string AcademicPlan_Source { get; set; }
        public bool AcademicPlan_IsReadOnly { get; set; }

        public string AcademicPlanSourceSystemRecordId { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessDescription { get; set; }
        public int? AcademicPlanSourceSystemRecordId_AttributeId { get; set; }
        public string AcademicPlanSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicPlanSourceSystemRecordId_Status { get; set; }
        public string AcademicPlanSourceSystemRecordId_Source { get; set; }
        public bool AcademicPlanSourceSystemRecordId_IsReadOnly { get; set; }

        public string AcademicPlanMasterId { get; set; }
        public string AcademicPlanMasterId_BusinessName { get; set; }
        public string AcademicPlanMasterId_BusinessDescription { get; set; }
        public int? AcademicPlanMasterId_AttributeId { get; set; }
        public string AcademicPlanMasterId_OriginalValue { get; set; }
        public string AcademicPlanMasterId_Status { get; set; }
        public string AcademicPlanMasterId_Source { get; set; }
        public bool AcademicPlanMasterId_IsReadOnly { get; set; }



        public string AcademicCatalog { get; set; }
        public string AcademicCatalog_BusinessName { get; set; }
        public string AcademicCatalog_BusinessDescription { get; set; }
        public int? AcademicCatalog_AttributeId { get; set; }
        public string AcademicCatalog_OriginalValue { get; set; }
        public string AcademicCatalog_Status { get; set; }
        public string AcademicCatalog_Source { get; set; }
        public bool AcademicCatalog_IsReadOnly { get; set; }

        public string AcademicCatalogSourceSystemRecordId { get; set; }
        public string AcademicCatalogSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCatalogSourceSystemRecordId_BusinessDescription { get; set; }
        public int? AcademicCatalogSourceSystemRecordId_AttributeId { get; set; }
        public string AcademicCatalogSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Status { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Source { get; set; }
        public bool AcademicCatalogSourceSystemRecordId_IsReadOnly { get; set; }

        public string AcademicCatalogMasterId { get; set; }
        public string AcademicCatalogMasterId_BusinessName { get; set; }
        public string AcademicCatalogMasterId_BusinessDescription { get; set; }
        public int? AcademicCatalogMasterId_AttributeId { get; set; }
        public string AcademicCatalogMasterId_OriginalValue { get; set; }
        public string AcademicCatalogMasterId_Status { get; set; }
        public string AcademicCatalogMasterId_Source { get; set; }
        public bool AcademicCatalogMasterId_IsReadOnly { get; set; }



        public string AcademicSubplan { get; set; }
        public string AcademicSubplan_BusinessName { get; set; }
        public string AcademicSubplan_BusinessDescription { get; set; }
        public int? AcademicSubplan_AttributeId { get; set; }
        public string AcademicSubplan_OriginalValue { get; set; }
        public string AcademicSubplan_Status { get; set; }
        public string AcademicSubplan_Source { get; set; }
        public bool AcademicSubplan_IsReadOnly { get; set; }

        public string AcademicSubplanSourceSystemRecordId { get; set; }
        public string AcademicSubplanSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicSubplanSourceSystemRecordId_BusinessDescription { get; set; }
        public int? AcademicSubplanSourceSystemRecordId_AttributeId { get; set; }
        public string AcademicSubplanSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Status { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Source { get; set; }
        public bool AcademicSubplanSourceSystemRecordId_IsReadOnly { get; set; }

        public string AcademicSubplanMasterId { get; set; }
        public string AcademicSubplanMasterId_BusinessName { get; set; }
        public string AcademicSubplanMasterId_BusinessDescription { get; set; }
        public int? AcademicSubplanMasterId_AttributeId { get; set; }
        public string AcademicSubplanMasterId_OriginalValue { get; set; }
        public string AcademicSubplanMasterId_Status { get; set; }
        public string AcademicSubplanMasterId_Source { get; set; }
        public bool AcademicSubplanMasterId_IsReadOnly { get; set; }



        public string StudentAcademicPlanStatus { get; set; }
        public string StudentAcademicPlanStatus_BusinessName { get; set; }
        public string StudentAcademicPlanStatus_BusinessDescription { get; set; }
        public int? StudentAcademicPlanStatus_AttributeId { get; set; }
        public string StudentAcademicPlanStatus_OriginalValue { get; set; }
        public string StudentAcademicPlanStatus_Status { get; set; }
        public string StudentAcademicPlanStatus_Source { get; set; }
        public bool StudentAcademicPlanStatus_IsReadOnly { get; set; }

        public string StudentAcademicPlanStatusSourceSystemRecordId { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_BusinessName { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public int? StudentAcademicPlanStatusSourceSystemRecordId_AttributeId { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_OriginalValue { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Status { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Source { get; set; }
        public bool StudentAcademicPlanStatusSourceSystemRecordId_IsReadOnly { get; set; }

        public string StudentAcademicPlanStatusMasterId { get; set; }
        public string StudentAcademicPlanStatusMasterId_BusinessName { get; set; }
        public string StudentAcademicPlanStatusMasterId_BusinessDescription { get; set; }
        public int? StudentAcademicPlanStatusMasterId_AttributeId { get; set; }
        public string StudentAcademicPlanStatusMasterId_OriginalValue { get; set; }
        public string StudentAcademicPlanStatusMasterId_Status { get; set; }
        public string StudentAcademicPlanStatusMasterId_Source { get; set; }
        public bool StudentAcademicPlanStatusMasterId_IsReadOnly { get; set; }



        public string ExpectedGraduationTerm { get; set; }
        public string ExpectedGraduationTerm_BusinessName { get; set; }
        public string ExpectedGraduationTerm_BusinessDescription { get; set; }
        public int? ExpectedGraduationTerm_AttributeId { get; set; }
        public string ExpectedGraduationTerm_OriginalValue { get; set; }
        public string ExpectedGraduationTerm_Status { get; set; }
        public string ExpectedGraduationTerm_Source { get; set; }
        public bool ExpectedGraduationTerm_IsReadOnly { get; set; }

        public string ExpectedGraduationTermSourceSystemRecordId { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_BusinessName { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_BusinessDescription { get; set; }
        public int? ExpectedGraduationTermSourceSystemRecordId_AttributeId { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_OriginalValue { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Status { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Source { get; set; }
        public bool ExpectedGraduationTermSourceSystemRecordId_IsReadOnly { get; set; }

        public string ExpectedGraduationTermMasterId { get; set; }
        public string ExpectedGraduationTermMasterId_BusinessName { get; set; }
        public string ExpectedGraduationTermMasterId_BusinessDescription { get; set; }
        public int? ExpectedGraduationTermMasterId_AttributeId { get; set; }
        public string ExpectedGraduationTermMasterId_OriginalValue { get; set; }
        public string ExpectedGraduationTermMasterId_Status { get; set; }
        public string ExpectedGraduationTermMasterId_Source { get; set; }
        public bool ExpectedGraduationTermMasterId_IsReadOnly { get; set; }




        #region Drop Downs
        public List<SelectListItem> StudentList { get; set; }
        public List<SelectListItem> EnrollmentList { get; set; }
        public List<SelectListItem> CampusList { get; set; }
        public List<SelectListItem> DegreeList { get; set; }
        public List<SelectListItem> AcademicProgramList { get; set; }
        public List<SelectListItem> AcademicPlanList { get; set; }
        public List<SelectListItem> AcademicCatalogList { get; set; }
        public List<SelectListItem> AcademicCareerList { get; set; }
        public List<SelectListItem> AcademicSubplanList { get; set; }
        public List<SelectListItem> AcademicPlanStatusList { get; set; }
        public List<SelectListItem> ExpectedGraduationTermList { get; set; }
        #endregion

        public List<StudentAcademicPlanHistoryViewModel> HistoryData { get; set; }
    }
}
