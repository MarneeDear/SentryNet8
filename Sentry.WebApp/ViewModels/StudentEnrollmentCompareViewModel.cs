using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public string MasterId { get; set; }

        public string StudentName { get; set; }
        public string StudentId { get; set; }

        public string System { get; set; }

        #region Integration Record

        public string SourceRecordId { get; set; }

        #region EnrollmentAcademicYear
        public string EnrollmentAcademicYear { get; set; }
        public string EnrollmentAcademicYear_BusinessName { get; set; }
        public string EnrollmentAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentTerm
        public string EnrollmentTerm { get; set; }
        public string EnrollmentTerm_BusinessName { get; set; }
        public string EnrollmentTerm_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentCampus
        public string EnrollmentCampus { get; set; }
        public string EnrollmentCampus_BusinessName { get; set; }
        public string EnrollmentCampus_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentLocation
        public string EnrollmentLocation { get; set; }
        public string EnrollmentLocation_BusinessName { get; set; }
        public string EnrollmentLocation_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentTotalCumulativeUnits
        public int? EnrollmentTotalCumulativeUnits { get; set; }
        public string EnrollmentTotalCumulativeUnits_BusinessName { get; set; }
        public string EnrollmentTotalCumulativeUnits_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicCareer
        public string EnrollmentAcademicCareer { get; set; }
        public string EnrollmentAcademicCareer_BusinessName { get; set; }
        public string EnrollmentAcademicCareer_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicLevel
        public string EnrollmentAcademicLevel { get; set; }
        public string EnrollmentAcademicLevel_BusinessName { get; set; }
        public string EnrollmentAcademicLevel_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicProgram
        public string EnrollmentAcademicProgram { get; set; }
        public string EnrollmentAcademicProgram_BusinessName { get; set; }
        public string EnrollmentAcademicProgram_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicPlan
        public string EnrollmentAcademicPlan { get; set; }
        public string EnrollmentAcademicPlan_BusinessName { get; set; }
        public string EnrollmentAcademicPlan_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicPlanType
        public string EnrollmentAcademicPlanType { get; set; }
        public string EnrollmentAcademicPlanType_BusinessName { get; set; }
        public string EnrollmentAcademicPlanType_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicSubplan
        public string EnrollmentAcademicSubplan { get; set; }
        public string EnrollmentAcademicSubplan_BusinessName { get; set; }
        public string EnrollmentAcademicSubplan_BusinessDescription { get; set; }
        #endregion

        #region EnrollmentAcademicSubplanType
        public string EnrollmentAcademicSubplanType { get; set; }
        public string EnrollmentAcademicSubplanType_BusinessName { get; set; }
        public string EnrollmentAcademicSubplanType_BusinessDescription { get; set; }
        #endregion

        #endregion

        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string EnrollmentAcademicYear_Compare { get; set; }
        public string EnrollmentTerm_Compare { get; set; }
        public string EnrollmentCampus_Compare { get; set; }
        public string EnrollmentLocation_Compare { get; set; }
        public int? EnrollmentTotalCumulativeUnits_Compare { get; set; }
        public string EnrollmentAcademicCareer_Compare { get; set; }
        public string EnrollmentAcademicLevel_Compare { get; set; }
        public string EnrollmentAcademicProgram_Compare { get; set; }
        public string EnrollmentAcademicPlan_Compare { get; set; }
        public string EnrollmentAcademicPlanType_Compare { get; set; }
        public string EnrollmentAcademicSubplan_Compare { get; set; }
        public string EnrollmentAcademicSubplanType_Compare { get; set; }

        #endregion
    }
}
