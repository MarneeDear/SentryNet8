using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicInvolvements_List", Schema = "Integration")]
    public class StudentAcademicInvolvementRemediationList : BaseRemediationList
    {
        [Column("StudentId")]
        public string StudentId { get; set; }

        [Column("AcademicYear")]
        public string AcademicInvolvementAcademicYear { get; set; }

        [Column("Term")]
        public string AcademicInvolvementTerm { get; set; }

        [Column("AcademicInvolvementType")]
        public string AcademicInvolvementType { get; set; }

        [Column("AcademicInvolvementName")]
        public string AcademicInvolvementName { get; set; }
    }
}
