using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicSubplans", Schema = "MDS")]
    public class StudentAcademicSubplan
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string AcademicSubplanCode { get; set; }

        public string AcademicSubplanName { get; set; }
    }
}
