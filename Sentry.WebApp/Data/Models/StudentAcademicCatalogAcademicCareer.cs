using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicCareers", Schema = "MDS")]
    public class StudentAcademicCatalogAcademicCareer
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string Name { get; set; }
    }
}
