using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    //[Table("StudentEnrollmentCampus", Schema = "MDS")]
    public class StudentEnrollmentCampus
    {
        public string EnrollmentCampusName { get; set; }

        [Key]
        public string EnrollmentCampusCode { get; set; }
    }
}