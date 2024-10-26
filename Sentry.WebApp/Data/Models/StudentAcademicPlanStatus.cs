using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentAcademicPlanStatus", Schema = "MDS")]
    public class StudentAcademicPlanStatus
    {
        [Key]
        [Column("AcademicPlanStatusMasterId")]
        public string AcademicPlanStatusMasterId { get; set; }

        [Column("AcademicPlanStatus")]
        public string AcademicPlanStatus { get; set; }
    }
}
