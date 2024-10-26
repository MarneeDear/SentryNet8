using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public string MasterId { get; set; }

        public string System { get; set; }

        #region Integration Record

        public string SourceRecordId { get; set; }

        #region Student
        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipAcademicYear
        public string ScholarshipAcademicYear { get; set; }
        public string ScholarshipAcademicYear_BusinessName { get; set; }
        public string ScholarshipAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipTerm
        public string ScholarshipTerm { get; set; }
        public string ScholarshipTerm_BusinessName { get; set; }
        public string ScholarshipTerm_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipDesignation
        public string ScholarshipDesignation { get; set; }
        public string ScholarshipDesignation_BusinessName { get; set; }
        public string ScholarshipDesignation_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipAmount
        public decimal? ScholarshipAmount { get; set; }
        public string ScholarshipAmount_BusinessName { get; set; }
        public string ScholarshipAmount_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipDepartment
        public string ScholarshipDepartment { get; set; }
        public string ScholarshipDepartment_BusinessName { get; set; }
        public string ScholarshipDepartment_BusinessDescription { get; set; }
        #endregion

        #region Scholarship
        public string Scholarship { get; set; }
        public string Scholarship_BusinessName { get; set; }
        public string Scholarship_BusinessDescription { get; set; }
        #endregion

        #endregion

        #region Compare Record
        
        public string Student_Compare { get; set; }
        public string ScholarshipAcademicYear_Compare { get; set; }
        public string ScholarshipTerm_Compare { get; set; }
        public string ScholarshipDesignation_Compare { get; set; }
        public decimal? ScholarshipAmount_Compare { get; set; }
        public string ScholarshipDepartment_Compare { get; set; }
        public string Scholarship_Compare { get; set; }

        #endregion

    }
}
