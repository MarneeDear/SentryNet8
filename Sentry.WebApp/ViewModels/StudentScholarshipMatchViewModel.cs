using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipMatchViewModel : BaseIntegrationViewModel
    {
        public StudentScholarshipMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        public string Student { get; set; }
        public int Student_Weight { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }

        #region Match

        #region ScholarshipAcademicYear
        public string ScholarshipAcademicYear { get; set; }
        public int ScholarshipAcademicYear_Weight { get; set; }
        public string ScholarshipAcademicYear_BusinessName { get; set; }
        public string ScholarshipAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipTerm
        public string ScholarshipTerm { get; set; }
        public int? ScholarshipTerm_Weight { get; set; }
        public string ScholarshipTerm_BusinessName { get; set; }
        public string ScholarshipTerm_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipDesignation
        public string ScholarshipDesignation { get; set; }
        public int? ScholarshipDesignation_Weight { get; set; }
        public string ScholarshipDesignation_BusinessName { get; set; }
        public string ScholarshipDesignation_BusinessDescription { get; set; }
        #endregion

        #region Scholarship
        public string Scholarship { get; set; }
        public int? Scholarship_Weight { get; set; }
        public string Scholarship_BusinessName { get; set; }
        public string Scholarship_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipAmount
        public decimal? ScholarshipAmount { get; set; }
        public int? ScholarshipAmount_Weight { get; set; }
        public string ScholarshipAmount_BusinessName { get; set; }
        public string ScholarshipAmount_BusinessDescription { get; set; }
        #endregion

        #region ScholarshipDepartment
        public string ScholarshipDepartment { get; set; }
        public int? ScholarshipDepartment_Weight { get; set; }
        public string ScholarshipDepartment_BusinessName { get; set; }
        public string ScholarshipDepartment_BusinessDescription { get; set; }
        #endregion

        #endregion

        public List<StudentScholarshipMatchSummaryViewModel> PossibleMatches { get; set; }

        public class StudentScholarshipMatchSummaryViewModel
        {
            public bool Selected { get; set; }

            public int MatchConfidence { get; set; }

            public string MasterId { get; set; }

            public string Student { get; set; }

            public string ScholarshipAcademicYear { get; set; }

            public string ScholarshipTerm { get; set; }

            public string ScholarshipDesignation { get; set; }

            public decimal? ScholarshipAmount { get; set; }

            public string ScholarshipDepartment { get; set; }

            public string Scholarship { get; set; }
        }
    }
}