using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementMatchViewModel : BaseIntegrationViewModel
    {
        public StudentAcademicInvolvementMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        public string StudentName { get; set; }
        public string StudentId { get; set; }

        #region Match

        #region AcademicYear
        public string AcademicInvolvementAcademicYear { get; set; }
        public int? AcademicInvolvementAcademicYear_Weight { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessName { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region Term
        public string AcademicInvolvementTerm { get; set; }
        public int? AcademicInvolvementTerm_Weight { get; set; }
        public string AcademicInvolvementTerm_BusinessName { get; set; }
        public string AcademicInvolvementTerm_BusinessDescription { get; set; }
        #endregion

        #region Type
        public string AcademicInvolvementType { get; set; }
        public int? AcademicInvolvementType_Weight { get; set; }
        public string AcademicInvolvementType_BusinessName { get; set; }
        public string AcademicInvolvementType_BusinessDescription { get; set; }
        #endregion

        #region Name
        public string AcademicInvolvementName { get; set; }
        public int? AcademicInvolvementName_Weight { get; set; }
        public string AcademicInvolvementName_BusinessName { get; set; }
        public string AcademicInvolvementName_BusinessDescription { get; set; }
        #endregion

        #endregion

        public List<StudentAcademicInvolvementMatchSummaryViewModel> PossibleMatches { get; set; }

        public class StudentAcademicInvolvementMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string StudentName { get; set; }
            public string StudentId { get; set; }

            #region Match

            #region AcademicYear
            public string AcademicInvolvementAcademicYear { get; set; }
            public int? AcademicInvolvementAcademicYear_Weight { get; set; }
            public string AcademicInvolvementAcademicYear_BusinessName { get; set; }
            public string AcademicInvolvementAcademicYear_BusinessDescription { get; set; }
            #endregion

            #region Term
            public string AcademicInvolvementTerm { get; set; }
            public int? AcademicInvolvementTerm_Weight { get; set; }
            public string AcademicInvolvementTerm_BusinessName { get; set; }
            public string AcademicInvolvementTerm_BusinessDescription { get; set; }
            #endregion

            #region Type
            public string AcademicInvolvementType { get; set; }
            public int? AcademicInvolvementType_Weight { get; set; }
            public string AcademicInvolvementType_BusinessName { get; set; }
            public string AcademicInvolvementType_BusinessDescription { get; set; }
            #endregion

            #region Name
            public string AcademicInvolvementName { get; set; }
            public int? AcademicInvolvementName_Weight { get; set; }
            public string AcademicInvolvementName_BusinessName { get; set; }
            public string AcademicInvolvementName_BusinessDescription { get; set; }
            #endregion

            #endregion

        }
    }
}
