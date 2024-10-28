using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentScholarshipPossibleMatch
    {
        [Key]
        public string MasterId { get; set; }

        public int MatchConfidence { get; set; }

        public string Student { get; set; }

        public string AcademicYear { get; set; }

        public string ScholarshipTerm { get; set; }

        public string Designation { get; set; }

        public string Department { get; set; }

        public string Scholarship { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
    }
}
