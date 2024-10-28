using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class StudentScholarshipComparisonDetail
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string Student_Compare { get; set; }

        public string AcademicYear_Compare { get; set; }

        public string ScholarshipTerm_Compare { get; set; }

        public string Designation_Compare { get; set; }

        public string Department_Compare { get; set; }

        public string Scholarship_Compare { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount_Compare { get; set; }
    }
}
