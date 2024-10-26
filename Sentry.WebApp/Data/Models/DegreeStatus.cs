using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DegreeStatuses", Schema = "MDS")]
    public class DegreeStatus
    {
        [Key]
        [Column("MasterRecordId")]
        public string StatusMasterId { get; set; }

        [Column("Name")]
        public string StatusName { get; set; }
    }
}
