using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicLevels", Schema = "MDS")]
    public class AcademicLevel
    {
        [Key]
        [Column("AcademicLevelName")]
        public string AcademicLevelName { get; set; }

        [Column("AcademicLevelSourceSystemRecordId")]
        public string AcademicLevelSourceSystemRecordId { get; set; }

        //[Column("AcademicLevelMasterId")]
        //public string AcademicLevelMasterId { get; set; }
    }
}
