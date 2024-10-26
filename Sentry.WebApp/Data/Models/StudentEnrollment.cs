using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentEnrollment
    {
        [Key]
        [Column("MasterId")]
        public string MasterRecordId { get; set; }

        [Column("CampusName")]
        public string CampusName { get; set; }

        [Column("CampusMasterId")]
        public string CampusMasterId { get; set; }

        [Column("TermCode")]
        public string TermCode { get; set; }

        [Column("TermName")]
        public string TermName { get; set; }

        [Column("TermMasterId")]
        public string TermMasterId { get; set; }

        [Column("AcademicCareerName")]
        public string AcademicCareerName { get; set; }
    }
}
