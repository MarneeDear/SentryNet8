using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
    [Table("Degrees_Base", Schema = "Integration")]
    public class DegreeHistory : BaseHistory
    {
        public string StudentId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentMasterId { get; set; }
        public string StudentMasterId_Status { get; set; }

        //public DateTime? HistoryDate { get; set; }

        #region Degree

        public string EducationalInstitutionMasterId { get; set; }
        public string EducationalInstitutionMasterId_Status { get; set; }

        public string ClassOf { get; set; }
        public string ClassOf_Status { get; set; }

        public string PreferredClassOf { get; set; }
        public string PreferredClassOf_Status { get; set; }

        public string AwardedTerm { get; set; }
        public string AwardedTerm_Status { get; set; }

        public DateTime? AwardedDate { get; set; }
        public string AwardedDate_Status { get; set; }

        public DateTime? HonorsCollegeAwardedDate { get; set; }
        public string HonorsCollegeAwardedDate_Status { get; set; }

        public string EducationLevelCode { get; set; }
        public string EducationLevelCode_Status { get; set; }

        public string EducationLevelDescription { get; set; }
        public string EducationLevelDescription_Status { get; set; }

        //public string DegreeType { get; set; }
        //public string DegreeType_Status { get; set; }

        public string DegreeCode { get; set; }
        public string DegreeCode_Status { get; set; }

        public string DegreeDescription { get; set; }
        public string DegreeDescription_Status { get; set; }

        public string DegreeMasterId { get; set; }
        public string DegreeMasterId_Status { get; set; }

        public string HonorCode { get; set; }
        public string HonorCode_Status { get; set; }

        public string HonorMasterId { get; set; }
        public string HonorMasterId_Status { get; set; }

        public string DegreeStatusCode { get; set; }
        public string DegreeStatusCode_Status { get; set; }

        public string DegreeStatusDescription { get; set; }
        public string DegreeStatusDescription_Status { get; set; }

        public string DegreeStatusMasterId { get; set; }
        public string DegreeStatusMasterId_Status { get; set; }

        public string AcademicProgramCode { get; set; }
        public string AcademicProgramCode_Status { get; set; }

        public string AcademicProgramDescription { get; set; }
        public string AcademicProgramDescription_Status { get; set; }

        public string AcademicProgramMasterId { get; set; }
        public string AcademicProgramMasterId_Status { get; set; }

        public string AcademicPlanCode { get; set; }
        public string AcademicPlanCode_Status { get; set; }

        public string AcademicPlanDescription { get; set; }
        public string AcademicPlanDescription_Status { get; set; }

        public string AcademicPlanMasterId { get; set; }
        public string AcademicPlanMasterId_Status { get; set; }

        public string AcademicPlanTypeCode { get; set; }
        public string AcademicPlanTypeCode_Status { get; set; }

        public string AcademicPlanTypeDescription { get; set; }
        public string AcademicPlanTypeDescription_Status { get; set; }

        public string AcademicPlanTypeMasterId { get; set; }
        public string AcademicPlanTypeMasterId_Status { get; set; }

        public string AcademicSubplanCode { get; set; }
        public string AcademicSubplanCode_Status { get; set; }

        public string AcademicSubplanDescription { get; set; }
        public string AcademicSubplanDescription_Status { get; set; }

        public string AcademicSubplanMasterId { get; set; }
        public string AcademicSubplanMasterId_Status { get; set; }

        public string AcademicSubplanTypeCode { get; set; }
        public string AcademicSubplanTypeCode_Status { get; set; }

        public string AcademicSubplanTypeDescription { get; set; }
        public string AcademicSubplanTypeDescription_Status { get; set; }

        public string AcademicSubplanTypeMasterId { get; set; }
        public string AcademicSubplanTypeMasterId_Status { get; set; }

        #endregion
    }
}
