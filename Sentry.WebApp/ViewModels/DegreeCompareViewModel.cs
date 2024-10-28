using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class DegreeCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public string MasterId { get; set; }

        public string StudentName { get; set; }
        public string Student { get; set; }

        public string System { get; set; }

        #region Integration Record

        public string SourceRecordId { get; set; }

        #region DegreeEducationalInstitution
        public string DegreeEducationalInstitution { get; set; }
        public string DegreeEducationalInstitution_BusinessName { get; set; }
        public string DegreeEducationalInstitution_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicYear
        public string DegreeAcademicYear { get; set; }
        public string DegreeAcademicYear_BusinessName { get; set; }
        public string DegreeAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region DegreePreferredClassOf
        public string DegreePreferredClassOf { get; set; }
        public string DegreePreferredClassOf_BusinessName { get; set; }
        public string DegreePreferredClassOf_BusinessDescription { get; set; }
        #endregion

        #region DegreeTerm
        public string DegreeTerm { get; set; }
        public string DegreeTerm_BusinessName { get; set; }
        public string DegreeTerm_BusinessDescription { get; set; }
        #endregion

        #region DegreeDate
        public DateTime? DegreeDate { get; set; }
        public string DegreeDate_BusinessName { get; set; }
        public string DegreeDate_BusinessDescription { get; set; }
        #endregion

        #region DegreeEducationLevel
        public string DegreeEducationLevel { get; set; }
        public string DegreeEducationLevel_BusinessName { get; set; }
        public string DegreeEducationLevel_BusinessDescription { get; set; }
        #endregion

        #region DegreeType
        public string DegreeType { get; set; }
        public string DegreeType_BusinessName { get; set; }
        public string DegreeType_BusinessDescription { get; set; }
        #endregion

        #region DegreeDegree
        public string DegreeDegree { get; set; }
        public string DegreeDegree_BusinessName { get; set; }
        public string DegreeDegree_BusinessDescription { get; set; }
        #endregion

        #region DegreeHonors
        public string DegreeHonors { get; set; }
        public string DegreeHonors_BusinessName { get; set; }
        public string DegreeHonors_BusinessDescription { get; set; }
        #endregion

        #region DegreeStatus
        public string DegreeStatus { get; set; }
        public string DegreeStatus_BusinessName { get; set; }
        public string DegreeStatus_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicProgram
        public string DegreeAcademicProgram { get; set; }
        public string DegreeAcademicProgram_BusinessName { get; set; }
        public string DegreeAcademicProgram_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicPlan
        public string DegreeAcademicPlan { get; set; }
        public string DegreeAcademicPlan_BusinessName { get; set; }
        public string DegreeAcademicPlan_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicPlanType
        public string DegreeAcademicPlanType { get; set; }
        public string DegreeAcademicPlanType_BusinessName { get; set; }
        public string DegreeAcademicPlanType_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicSubplan
        public string DegreeAcademicSubplan { get; set; }
        public string DegreeAcademicSubplan_BusinessName { get; set; }
        public string DegreeAcademicSubplan_BusinessDescription { get; set; }
        #endregion

        #region DegreeAcademicSubplanType
        public string DegreeAcademicSubplanType { get; set; }
        public string DegreeAcademicSubplanType_BusinessName { get; set; }
        public string DegreeAcademicSubplanType_BusinessDescription { get; set; }
        #endregion

        #region DegreeCheckoutStatus
        public string DegreeCheckoutStatus { get; set; }
        public string DegreeCheckoutStatus_BusinessName { get; set; }
        public string DegreeCheckoutStatus_BusinessDescription { get; set; }
        #endregion

        #endregion

        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string DegreeEducationalInstitution_Compare { get; set; }
        public string DegreeAcademicYear_Compare { get; set; }
        public string DegreePreferredClassOf_Compare { get; set; }
        public string DegreeTerm_Compare { get; set; }
        public DateTime? DegreeDate_Compare { get; set; }
        public string DegreeEducationLevel_Compare { get; set; }
        public string DegreeType_Compare { get; set; }
        public string DegreeDegree_Compare { get; set; }
        public string DegreeHonors_Compare { get; set; }
        public string DegreeStatus_Compare { get; set; }
        public string DegreeAcademicProgram_Compare { get; set; }
        public string DegreeAcademicPlan_Compare { get; set; }
        public string DegreeAcademicPlanType_Compare { get; set; }
        public string DegreeAcademicSubplan_Compare { get; set; }
        public string DegreeAcademicSubplanType_Compare { get; set; }
        public string DegreeCheckoutStatus_Compare { get; set; }

        #endregion
    }
}
