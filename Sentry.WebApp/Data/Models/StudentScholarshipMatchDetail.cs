using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentScholarshipMatchDetail : BaseDetail
    {
        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        public bool Student_IncludeInMatch { get; set; }
        public int Student_MatchWeight { get; set; }

        public string AcademicYear { get; set; }
        public string AcademicYear_BusinessName { get; set; }
        public string AcademicYear_BusinessDescription { get; set; }
        public bool AcademicYear_IncludeInMatch { get; set; }
        public int AcademicYear_MatchWeight { get; set; }

        public string ScholarshipTerm { get; set; }
        public string ScholarshipTerm_BusinessName { get; set; }
        public string ScholarshipTerm_BusinessDescription { get; set; }
        public bool ScholarshipTerm_IncludeInMatch { get; set; }
        public int ScholarshipTerm_MatchWeight { get; set; }

        public string Designation { get; set; }
        public string Designation_BusinessName { get; set; }
        public string Designation_BusinessDescription { get; set; }
        public bool Designation_IncludeInMatch { get; set; }
        public int Designation_MatchWeight { get; set; }

        public string Department { get; set; }
        public string Department_BusinessName { get; set; }
        public string Department_BusinessDescription { get; set; }
        public bool Department_IncludeInMatch { get; set; }
        public int Department_MatchWeight { get; set; }

        public string Scholarship { get; set; }
        public string Scholarship_BusinessName { get; set; }
        public string Scholarship_BusinessDescription { get; set; }
        public bool Scholarship_IncludeInMatch { get; set; }
        public int Scholarship_MatchWeight { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string Amount_BusinessName { get; set; }
        public string Amount_BusinessDescription { get; set; }
        public bool Amount_IncludeInMatch { get; set; }
        public int Amount_MatchWeight { get; set; }
    }
}
