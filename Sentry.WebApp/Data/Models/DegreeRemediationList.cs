using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentDegrees_List", Schema = "Integration")]
    public class DegreeRemediationList : BaseRemediationList
    {
        [Column("StudentId")]
        public string Student { get; set; }

        //public string StudentMasterId { get; set; }

        [Column("EducationalInstitutionName")]
        public string EducationalInstitution { get; set; }

        [Column("PreferredClassOf")]
        public string PreferredClassOf { get; set; }

        //[Column("AwardedDate")]
        //public string AwardedDate { get; set; }

        [Column("AwardedTerm")]
        public string AwardedTerm { get; set; }

    }
}
