using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicTerms", Schema = "MDS")]
    public class AcademicTerm
    {
        [Key]
        [Column("MasterRecordId")]
        public string AcademicTermMasterId { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
