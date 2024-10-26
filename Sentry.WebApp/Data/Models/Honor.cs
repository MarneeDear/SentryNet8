using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DegreeHonors", Schema = "MDS")]
    public class Honor
    {
        [Key]
        [Column("MasterRecordId")]
        public string HonorMasterId { get; set; }

        [Column("Name")]
        public string HonorName { get; set; }
    }
}
