using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    [Table("Scholarships", Schema = "MDS")]
    public class Scholarship
    {
        [Key]
        [Column("MasterRecordId")]
        public string ScholarshipMasterId { get; set; }

        [Column("Name")]
        public string ScholarshipName { get; set; }

        [Column("UACode")]
        public string UACode { get; set; }
    }
}
