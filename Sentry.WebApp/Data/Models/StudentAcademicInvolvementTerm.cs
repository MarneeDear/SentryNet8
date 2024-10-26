using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicInvolvementTerm", Schema = "MDS")]
    public class StudentAcademicInvolvementTerm
    {
        public string AcademicInvolvementTermName { get; set; }

        [Key]
        public string AcademicInvolvementTermCode { get; set; }
    }
}
