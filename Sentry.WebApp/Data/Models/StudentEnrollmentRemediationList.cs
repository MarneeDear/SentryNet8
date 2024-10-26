using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentEnrollment_List", Schema = "Integration")]
    public class StudentEnrollmentRemediationList : BaseRemediationList
    {
        [Column("StudentId")]
        public string StudentId { get; set; }

        [Column("TermName")]
        public string EnrollmentTerm { get; set; }

        [Column("CampusName")]
        public string EnrollmentCampus { get; set; }

        [Column("AcademicCareerName")]
        public string AcademicCareerName { get; set; }

        [Column("AcademicLevelName")]
        public string AcademicLevelName { get; set; }

        [Column("TotalTransferUnits")]
        public string TotalTransferUnits { get; set; }

        [Column("TotalCumulativeUnits")]
        public string TotalCumulativeUnits { get; set; }

    }
}
