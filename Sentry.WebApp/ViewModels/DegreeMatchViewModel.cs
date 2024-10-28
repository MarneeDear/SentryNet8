using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class DegreeMatchViewModel : BaseIntegrationViewModel
    {
        public DegreeMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        public string StudentName { get; set; }
        public string Student { get; set; }

        #region Match

        #region DegreeEducationalInstitution
        public string DegreeEducationalInstitution { get; set; }
        public int DegreeEducationalInstitution_Weight { get; set; }
        public string DegreeEducationalInstitution_BusinessName { get; set; }
        public string DegreeEducationalInstitution_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicYear
        public string DegreeAcademicYear { get; set; }
        public int? DegreeAcademicYear_Weight { get; set; }
        public string DegreeAcademicYear_BusinessName { get; set; }
        public string DegreeAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region DegreePreferredClassOf
        public string DegreePreferredClassOf { get; set; }
        public int? DegreePreferredClassOf_Weight { get; set; }
        public string DegreePreferredClassOf_BusinessName { get; set; }
        public string DegreePreferredClassOf_BusinessDescription { get; set; }
        #endregion

        #region DegreeTerm
        public string DegreeTerm { get; set; }
        public int? DegreeTerm_Weight { get; set; }
        public string DegreeTerm_BusinessName { get; set; }
        public string DegreeTerm_BusinessDescription { get; set; }
        #endregion

        #region DegreeDate
        public DateTime? DegreeDate { get; set; }
        public int? DegreeDate_Weight { get; set; }
        public string DegreeDate_BusinessName { get; set; }
        public string DegreeDate_BusinessDescription { get; set; }
        #endregion

        #region DegreeEducationLevel
        public string DegreeEducationLevel { get; set; }
        public int? DegreeEducationLevel_Weight { get; set; }
        public string DegreeEducationLevel_BusinessName { get; set; }
        public string DegreeEducationLevel_BusinessDescription { get; set; }
        #endregion

        #region DegreeType
        public string DegreeType { get; set; }
        public int? DegreeType_Weight { get; set; }
        public string DegreeType_BusinessName { get; set; }
        public string DegreeType_BusinessDescription { get; set; }
        #endregion

        #region DegreeDegree
        public string DegreeDegree { get; set; }
        public int? DegreeDegree_Weight { get; set; }
        public string DegreeDegree_BusinessName { get; set; }
        public string DegreeDegree_BusinessDescription { get; set; }
        #endregion

        #region DegreeHonors
        public string DegreeHonors { get; set; }
        public int? DegreeHonors_Weight { get; set; }
        public string DegreeHonors_BusinessName { get; set; }
        public string DegreeHonors_BusinessDescription { get; set; }
        #endregion

        #region DegreeStatus
        public string DegreeStatus { get; set; }
        public int? DegreeStatus_Weight { get; set; }
        public string DegreeStatus_BusinessName { get; set; }
        public string DegreeStatus_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicProgram
        public string DegreeAcademicProgram { get; set; }
        public int? DegreeAcademicProgram_Weight { get; set; }
        public string DegreeAcademicProgram_BusinessName { get; set; }
        public string DegreeAcademicProgram_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicPlan
        public string DegreeAcademicPlan { get; set; }
        public int? DegreeAcademicPlan_Weight { get; set; }
        public string DegreeAcademicPlan_BusinessName { get; set; }
        public string DegreeAcademicPlan_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicPlanType
        public string DegreeAcademicPlanType { get; set; }
        public int? DegreeAcademicPlanType_Weight { get; set; }
        public string DegreeAcademicPlanType_BusinessName { get; set; }
        public string DegreeAcademicPlanType_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicSubplan
        public string DegreeAcademicSubplan { get; set; }
        public int? DegreeAcademicSubplan_Weight { get; set; }
        public string DegreeAcademicSubplan_BusinessName { get; set; }
        public string DegreeAcademicSubplan_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicSubplanType
        public string DegreeAcademicSubplanType { get; set; }
        public int? DegreeAcademicSubplanType_Weight { get; set; }
        public string DegreeAcademicSubplanType_BusinessName { get; set; }
        public string DegreeAcademicSubplanType_BusinessDescription { get; set; }
        #endregion

        #region DegreeCheckoutStatus
        public string DegreeCheckoutStatus { get; set; }
        public int? DegreeCheckoutStatus_Weight { get; set; }
        public string DegreeCheckoutStatus_BusinessName { get; set; }
        public string DegreeCheckoutStatus_BusinessDescription { get; set; }
        #endregion

        #endregion

        public List<DegreeMatchSummaryViewModel> PossibleMatches { get; set; }

        public class DegreeMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string StudentName { get; set; }
            public string Student { get; set; }

            #region Match

            #region DegreeTerm
            public string DegreeTerm { get; set; }
            public int? DegreeTerm_Weight { get; set; }
            public string DegreeTerm_BusinessName { get; set; }
            public string DegreeTerm_BusinessDescription { get; set; }
            #endregion

            #endregion

        }
    }
}
