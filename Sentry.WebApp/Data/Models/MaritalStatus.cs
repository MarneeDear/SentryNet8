using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("MaritalStatus", Schema = "MDS")]
    public class MaritalStatus
    {
        [Key]
        public string MaritalStatus_Code { get; set; }

        public string MaritalStatus_Name { get; set; }
    }
}
