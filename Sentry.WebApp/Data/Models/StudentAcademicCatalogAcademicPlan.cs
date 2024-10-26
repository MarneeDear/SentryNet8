using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{ 

    [Table("AcademicPlans", Schema = "MDS")]
    public class StudentAcademicCatalogAcademicPlan
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string Name { get; set; }
    }
}
