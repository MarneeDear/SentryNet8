using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicCatalogDegreeType", Schema = "MDS")]
    public class StudentAcademicCatalogDegreeType
    {

        public string AcademicCatalogDegreeTypeName { get; set; }

        [Key]
        public string AcademicCatalogDegreeTypeCode { get; set; }
    }
}
