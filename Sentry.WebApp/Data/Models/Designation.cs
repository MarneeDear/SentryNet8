using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    [Table("Designations", Schema = "MDS")]
    public class Designation
    {
        [Key]
        [Column("MasterRecordId")]
        public string DesignationMasterId { get; set; }

        [Column("Name")]
        public string DesignationName { get; set; }

        [Column("DesignationId")]
        public string DesignationId { get; set; }

        [Column("KFSAccount")]
        public string KFSAccount { get; set; }
    }
}
