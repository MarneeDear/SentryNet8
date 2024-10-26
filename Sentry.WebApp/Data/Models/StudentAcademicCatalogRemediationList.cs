using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicCatalog_List", Schema = "Integration")]
    public class StudentAcademicCatalogRemediationList : BaseRemediationList
    {
        [Column("DegreeTypeName")]
        public string DegreeType { get; set; }

        [Column("DepartmentName")]
        public string Department { get; set; }

        [Column("AcademicCareerName")]
        public string AcademicCareer { get; set; }
    }
}
