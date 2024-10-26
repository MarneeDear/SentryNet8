using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipViewModel : BaseIntegrationViewModel
    {
        public StudentScholarshipViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region Student

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public string StudentId_OriginalValue { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_Source { get; set; }
        public bool StudentId_IsReadOnly { get; set; }

        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }
        public bool StudentMasterId_IsReadOnly { get; set; }

        #endregion

        #region Awarded Term

        public string TermCode { get; set; }
        public string TermCode_BusinessName { get; set; }
        public string TermCode_BusinessDescription { get; set; }
        public string TermCode_OriginalValue { get; set; }
        public string TermCode_Status { get; set; }
        public string TermCode_Source { get; set; }
        public bool TermCode_IsReadOnly { get; set; }

        public string TermMasterId { get; set; }
        public string TermMasterId_BusinessName { get; set; }
        public string TermMasterId_BusinessDescription { get; set; }
        public string TermMasterId_OriginalValue { get; set; }
        public string TermMasterId_Status { get; set; }
        public string TermMasterId_Source { get; set; }
        public bool TermMasterId_IsReadOnly { get; set; }

        #endregion

        #region AcademicCalendarName
        public string AcademicCalendarName { get; set; }
        public string AcademicCalendarName_BusinessName { get; set; }
        public string AcademicCalendarName_BusinessDescription { get; set; }
        public string AcademicCalendarName_Status { get; set; }
        public string AcademicCalendarName_Source { get; set; }
        public string AcademicCalendarName_Category { get; set; }
        public string AcademicCalendarName_OriginalValue { get; set; }
        public int? AcademicCalendarName_AttributeId { get; set; }
        public bool AcademicCalendarName_IsReadOnly { get; set; }
        #endregion

        #region AcademicCalendarMasterId
        public string AcademicCalendarMasterId { get; set; }
        public string AcademicCalendarMasterId_BusinessName { get; set; }
        public string AcademicCalendarMasterId_BusinessDescription { get; set; }
        public string AcademicCalendarMasterId_Status { get; set; }
        public string AcademicCalendarMasterId_Source { get; set; }
        public string AcademicCalendarMasterId_Category { get; set; }
        public string AcademicCalendarMasterId_OriginalValue { get; set; }
        public int? AcademicCalendarMasterId_AttributeId { get; set; }
        public bool AcademicCalendarMasterId_IsReadOnly { get; set; }
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

        #region AcademicTermCode
        public string AcademicTermCode { get; set; }
        public string AcademicTermCode_BusinessName { get; set; }
        public string AcademicTermCode_BusinessDescription { get; set; }
        public string AcademicTermCode_Status { get; set; }
        public string AcademicTermCode_Source { get; set; }
        public string AcademicTermCode_Category { get; set; }
        public string AcademicTermCode_OriginalValue { get; set; }
        public int? AcademicTermCode_AttributeId { get; set; }
        public bool AcademicTermCode_IsReadOnly { get; set; }
        #endregion

        #region AcademicCareerCode
        public string AcademicCareerCode { get; set; }
        public string AcademicCareerCode_BusinessName { get; set; }
        public string AcademicCareerCode_BusinessDescription { get; set; }
        public string AcademicCareerCode_Status { get; set; }
        public string AcademicCareerCode_Source { get; set; }
        public string AcademicCareerCode_Category { get; set; }
        public string AcademicCareerCode_OriginalValue { get; set; }
        public int? AcademicCareerCode_AttributeId { get; set; }
        public bool AcademicCareerCode_IsReadOnly { get; set; }
        #endregion

        #region DischargedTermCode
        public string DischargedTermCode { get; set; }
        public string DischargedTermCode_BusinessName { get; set; }
        public string DischargedTermCode_BusinessDescription { get; set; }
        public string DischargedTermCode_Status { get; set; }
        public string DischargedTermCode_Source { get; set; }
        public string DischargedTermCode_Category { get; set; }
        public string DischargedTermCode_OriginalValue { get; set; }
        public int? DischargedTermCode_AttributeId { get; set; }
        #endregion

        #region Designation

        public string ScholarshipKFSAccount { get; set; }
        public string ScholarshipKFSAccount_BusinessName { get; set; }
        public string ScholarshipKFSAccount_BusinessDescription { get; set; }
        public string ScholarshipKFSAccount_OriginalValue { get; set; }
        public string ScholarshipKFSAccount_Status { get; set; }
        public string ScholarshipKFSAccount_Source { get; set; }
        public bool ScholarshipKFSAccount_IsReadOnly { get; set; }

        public string ScholarshipDesignationMasterId { get; set; }
        public string ScholarshipDesignationMasterId_BusinessName { get; set; }
        public string ScholarshipDesignationMasterId_BusinessDescription { get; set; }
        public string ScholarshipDesignationMasterId_OriginalValue { get; set; }
        public string ScholarshipDesignationMasterId_Status { get; set; }
        public string ScholarshipDesignationMasterId_Source { get; set; }
        public bool ScholarshipDesignationMasterId_IsReadOnly { get; set; }

        #endregion

        #region Department

        public string ScholarshipDepartmentCode { get; set; }
        public string ScholarshipDepartmentCode_BusinessName { get; set; }
        public string ScholarshipDepartmentCode_BusinessDescription { get; set; }
        public string ScholarshipDepartmentCode_OriginalValue { get; set; }
        public string ScholarshipDepartmentCode_Status { get; set; }
        public string ScholarshipDepartmentCode_Source { get; set; }
        public bool ScholarshipDepartmentCode_IsReadOnly { get; set; }

        public string ScholarshipDepartmentMasterId { get; set; }
        public string ScholarshipDepartmentMasterId_BusinessName { get; set; }
        public string ScholarshipDepartmentMasterId_BusinessDescription { get; set; }
        public string ScholarshipDepartmentMasterId_OriginalValue { get; set; }
        public string ScholarshipDepartmentMasterId_Status { get; set; }
        public string ScholarshipDepartmentMasterId_Source { get; set; }
        public bool ScholarshipDepartmentMasterId_IsReadOnly { get; set; }

        #endregion

        #region Scholarship

        public string ScholarshipCode { get; set; }
        public string ScholarshipCode_BusinessName { get; set; }
        public string ScholarshipCode_BusinessDescription { get; set; }
        public string ScholarshipCode_OriginalValue { get; set; }
        public string ScholarshipCode_Status { get; set; }
        public string ScholarshipCode_Source { get; set; }
        public bool ScholarshipCode_IsReadOnly { get; set; }

        public string ScholarshipName { get; set; }
        public string ScholarshipName_BusinessName { get; set; }
        public string ScholarshipName_BusinessDescription { get; set; }
        public string ScholarshipName_OriginalValue { get; set; }
        public string ScholarshipName_Status { get; set; }
        public string ScholarshipName_Source { get; set; }
        public bool ScholarshipName_IsReadOnly { get; set; }

        public string ScholarshipMasterId { get; set; }
        public string ScholarshipMasterId_BusinessName { get; set; }
        public string ScholarshipMasterId_BusinessDescription { get; set; }
        public string ScholarshipMasterId_OriginalValue { get; set; }
        public string ScholarshipMasterId_Status { get; set; }
        public string ScholarshipMasterId_Source { get; set; }
        public bool ScholarshipMasterId_IsReadOnly { get; set; }

        #endregion

        #region Amount

        public decimal? ScholarshipAmount { get; set; }
        public string ScholarshipAmount_BusinessName { get; set; }
        public string ScholarshipAmount_BusinessDescription { get; set; }
        public decimal? ScholarshipAmount_OriginalValue { get; set; }
        public string ScholarshipAmount_Status { get; set; }
        public string ScholarshipAmount_Source { get; set; }
        public bool ScholarshipAmount_IsReadOnly { get; set; }

        #endregion

        public List<StudentScholarshipHistoryViewModel> HistoryData { get; set; }

        public List<SelectListItem> Students { get; set; }

        public List<SelectListItem> AcademicTerms { get; set; }

        public List<SelectListItem> Designations { get; set; }

        public List<SelectListItem> Scholarships { get; set; }

        public List<SelectListItem> Departments { get; set; }
    }
}
