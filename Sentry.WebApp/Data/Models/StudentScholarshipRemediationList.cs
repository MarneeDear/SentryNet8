using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentScholarships_List", Schema = "Integration")]
    public class StudentScholarshipRemediationList : BaseRemediationList
    {
        [Column("StudentId")]
        public string StudentId { get; set; }

        //[Column("AcademicYear")]
        //public string ScholarshipAcademicYear { get; set; }

        //[Column("ScholarshipName")]
        //public string ScholarshipName { get; internal set; }

        //[Column("Amount", TypeName = "numeric(16,2)")]
        //[Column("Amount", TypeName = "decimal(19,2)")]
        [Column("Amount")]
        public decimal? ScholarshipAmount { get; internal set; }
    }
}
