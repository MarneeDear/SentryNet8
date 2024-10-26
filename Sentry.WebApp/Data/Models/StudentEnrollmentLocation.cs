using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    //[Table("StudentEnrollmentLocation", Schema = "MDS")]
    public class StudentEnrollmentLocation
    {
        public string EnrollmentLocationName { get; set; }

        [Key]
        public string EnrollmentLocationCode { get; set; }
    }
}
