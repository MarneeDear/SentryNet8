using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class PostToGLMatchViewModel : BaseIntegrationViewModel
    {
        public PostToGLMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        #region Match
        public string DesignationName { get; set; }
        public int DesignationName_Weight { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }

        public string DesignationId { get; set; }
        public int DesignationId_Weight { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }

        public DateTime? StartDate { get; set; }
        public int StartDate_Weight { get; set; }
        public string StartDate_BusinessName { get; set; }
        public string StartDate_BusinessDescription { get; set; }

        public DateTime? EndDate { get; set; }
        public int EndDate_Weight { get; set; }
        public string EndDate_BusinessName { get; set; }
        public string EndDate_BusinessDescription { get; set; }

        public string DesignationType { get; set; }
        public int DesignationType_Weight { get; set; }
        public string DesignationType_BusinessName { get; set; }
        public string DesignationType_BusinessDescription { get; set; }

        public string DesignationSubtype { get; set; }
        public int DesignationSubtype_Weight { get; set; }
        public string DesignationSubtype_BusinessName { get; set; }
        public string DesignationSubtype_BusinessDescription { get; set; }

        public string DesignationStatus { get; set; }
        public int DesignationStatus_Weight { get; set; }
        public string DesignationStatus_BusinessName { get; set; }
        public string DesignationStatus_BusinessDescription { get; set; }

        public string DesignationState { get; set; }
        public int DesignationState_Weight { get; set; }
        public string DesignationState_BusinessName { get; set; }
        public string DesignationState_BusinessDescription { get; set; }

        public string UADepartment { get; set; }
        public int UADepartment_Weight { get; set; }
        public string UADepartment_BusinessName { get; set; }
        public string UADepartment_BusinessDescription { get; set; }
        #endregion

        public List<PostToGLMatchSummaryViewModel> PossibleMatches { get; set; }

        public class PostToGLMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public long MasterId { get; set; }

            #region Match
            public string DesignationName { get; set; }
            public int DesignationName_Weight { get; set; }
            public string DesignationName_BusinessName { get; set; }
            public string DesignationName_BusinessDescription { get; set; }

            public string DesignationId { get; set; }
            public int DesignationId_Weight { get; set; }
            public string DesignationId_BusinessName { get; set; }
            public string DesignationId_BusinessDescription { get; set; }

            public DateTime? StartDate { get; set; }
            public int StartDate_Weight { get; set; }
            public string StartDate_BusinessName { get; set; }
            public string StartDate_BusinessDescription { get; set; }

            public DateTime? EndDate { get; set; }
            public int EndDate_Weight { get; set; }
            public string EndDate_BusinessName { get; set; }
            public string EndDate_BusinessDescription { get; set; }

            public string DesignationType { get; set; }
            public int DesignationType_Weight { get; set; }
            public string DesignationType_BusinessName { get; set; }
            public string DesignationType_BusinessDescription { get; set; }

            public string DesignationSubtype { get; set; }
            public int DesignationSubtype_Weight { get; set; }
            public string DesignationSubtype_BusinessName { get; set; }
            public string DesignationSubtype_BusinessDescription { get; set; }

            public string DesignationStatus { get; set; }
            public int DesignationStatus_Weight { get; set; }
            public string DesignationStatus_BusinessName { get; set; }
            public string DesignationStatus_BusinessDescription { get; set; }

            public string DesignationState { get; set; }
            public int DesignationState_Weight { get; set; }
            public string DesignationState_BusinessName { get; set; }
            public string DesignationState_BusinessDescription { get; set; }

            public string UADepartment { get; set; }
            public int UADepartment_Weight { get; set; }
            public string UADepartment_BusinessName { get; set; }
            public string UADepartment_BusinessDescription { get; set; }
            #endregion

        }

    }
}
