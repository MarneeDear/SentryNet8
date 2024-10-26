using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicPlanTypes", Schema = "MDS")]
    public class StudentAcademicCatalogAcademicPlanType
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string Name { get; set; }
    }
}
