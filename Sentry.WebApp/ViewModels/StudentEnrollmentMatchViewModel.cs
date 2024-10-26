using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentMatchViewModel : BaseIntegrationViewModel
    {
        public StudentEnrollmentMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        public string StudentName { get; set; }
        public string StudentId { get; set; }

        #region Match

        #region EnrollmentAcademicYear
        public string EnrollmentAcademicYear { get; set; }
        public int EnrollmentAcademicYear_Weight { get; set; }
        public string EnrollmentAcademicYear_BusinessName { get; set; }
        public string EnrollmentAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentTerm
        public string EnrollmentTerm { get; set; }
        public int? EnrollmentTerm_Weight { get; set; }
        public string EnrollmentTerm_BusinessName { get; set; }
        public string EnrollmentTerm_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentCampus
        public string EnrollmentCampus { get; set; }
        public int? EnrollmentCampus_Weight { get; set; }
        public string EnrollmentCampus_BusinessName { get; set; }
        public string EnrollmentCampus_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentLocation
        public string EnrollmentLocation { get; set; }
        public int? EnrollmentLocation_Weight { get; set; }
        public string EnrollmentLocation_BusinessName { get; set; }
        public string EnrollmentLocation_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentTotalCumulativeUnits
        public int? EnrollmentTotalCumulativeUnits { get; set; }
        public int? EnrollmentTotalCumulativeUnits_Weight { get; set; }
        public string EnrollmentTotalCumulativeUnits_BusinessName { get; set; }
        public string EnrollmentTotalCumulativeUnits_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicCareer
        public string EnrollmentAcademicCareer { get; set; }
        public int? EnrollmentAcademicCareer_Weight { get; set; }
        public string EnrollmentAcademicCareer_BusinessName { get; set; }
        public string EnrollmentAcademicCareer_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicLevel
        public string EnrollmentAcademicLevel { get; set; }
        public int? EnrollmentAcademicLevel_Weight { get; set; }
        public string EnrollmentAcademicLevel_BusinessName { get; set; }
        public string EnrollmentAcademicLevel_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicProgram
        public string EnrollmentAcademicProgram { get; set; }
        public int? EnrollmentAcademicProgram_Weight { get; set; }
        public string EnrollmentAcademicProgram_BusinessName { get; set; }
        public string EnrollmentAcademicProgram_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicPlan
        public string EnrollmentAcademicPlan { get; set; }
        public int? EnrollmentAcademicPlan_Weight { get; set; }
        public string EnrollmentAcademicPlan_BusinessName { get; set; }
        public string EnrollmentAcademicPlan_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicPlanType
        public string EnrollmentAcademicPlanType { get; set; }
        public int? EnrollmentAcademicPlanType_Weight { get; set; }
        public string EnrollmentAcademicPlanType_BusinessName { get; set; }
        public string EnrollmentAcademicPlanType_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicSubplan
        public string EnrollmentAcademicSubplan { get; set; }
        public int? EnrollmentAcademicSubplan_Weight { get; set; }
        public string EnrollmentAcademicSubplan_BusinessName { get; set; }
        public string EnrollmentAcademicSubplan_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicSubplanType
        public string EnrollmentAcademicSubplanType { get; set; }
        public int? EnrollmentAcademicSubplanType_Weight { get; set; }
        public string EnrollmentAcademicSubplanType_BusinessName { get; set; }
        public string EnrollmentAcademicSubplanType_BusinessDescription { get; set; }
        #endregion

        #endregion

        public List<StudentEnrollmentMatchSummaryViewModel> PossibleMatches { get; set; }

        public class StudentEnrollmentMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string StudentName { get; set; }
            public string StudentId { get; set; }

            #region Match

            #region EnrollmentAcademicYear
            public string EnrollmentAcademicYear { get; set; }
            public int EnrollmentAcademicYear_Weight { get; set; }
            public string EnrollmentAcademicYear_BusinessName { get; set; }
            public string EnrollmentAcademicYear_BusinessDescription { get; set; }
            #endregion

            #region EnrollmentTerm
            public string EnrollmentTerm { get; set; }
            public int? EnrollmentTerm_Weight { get; set; }
            public string EnrollmentTerm_BusinessName { get; set; }
            public string EnrollmentTerm_BusinessDescription { get; set; }
            #endregion

            #region EnrollmentAcademicCareer
            public string EnrollmentAcademicCareer { get; set; }
            public int? EnrollmentAcademicCareer_Weight { get; set; }
            public string EnrollmentAcademicCareer_BusinessName { get; set; }
            public string EnrollmentAcademicCareer_BusinessDescription { get; set; }
            #endregion

            #region EnrollmentAcademicLevel
            public string EnrollmentAcademicLevel { get; set; }
            public int? EnrollmentAcademicLevel_Weight { get; set; }
            public string EnrollmentAcademicLevel_BusinessName { get; set; }
            public string EnrollmentAcademicLevel_BusinessDescription { get; set; }
            #endregion

            #region EnrollmentAcademicProgram
            public string EnrollmentAcademicProgram { get; set; }
            public int? EnrollmentAcademicProgram_Weight { get; set; }
            public string EnrollmentAcademicProgram_BusinessName { get; set; }
            public string EnrollmentAcademicProgram_BusinessDescription { get; set; }
            #endregion

            #endregion

        }
    }
}
