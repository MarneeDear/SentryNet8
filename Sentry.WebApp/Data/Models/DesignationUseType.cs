using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("DesignationUseType", Schema = "MDS")]
    public class DesignationUseType
    {
        public string Name { get; set; }

        [Key]
        public string Code { get; set; }
    }
}
