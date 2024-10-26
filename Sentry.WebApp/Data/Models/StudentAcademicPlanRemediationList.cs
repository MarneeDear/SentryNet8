using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicPlan_List", Schema = "Integration")]
    public class StudentAcademicPlanRemediationList : BaseRemediationList
    {
        [Column("StudentId")]
        public string Student { get; set; }

        //[Column("Campus")]
        //public string Campus { get; set; }

        [Column("TermName")]
        public string Term { get; set; }

        [Column("AcademicCareerName")]
        public string AcademicCareer { get; set; }

        [Column("AcademicPlanName")]
        public string AcademicPlan { get; set; }

        [Column("AcademicSubplanName")]
        public string AcademicSubplan { get; set; }
    }
}
