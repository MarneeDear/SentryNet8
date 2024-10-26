using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicInvolvementName", Schema = "MDS")]
    public class StudentAcademicInvolvementName
    {

        public string AcademicInvolvementNameName { get; set; }

        [Key]
        public string AcademicInvolvementNameCode { get; set; }
    }
}
