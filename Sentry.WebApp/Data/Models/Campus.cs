using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Campuses", Schema = "MDS")]
    public class Campus
    {
        //[Column("CampusSourceSystemRecordId")]
        //public string CampusSourceSystemRecordId { get; set; }

        [Key]
        [Column("CampusMasterId")]
        public string CampusMasterId { get; set; }

        [Column("CampusName")]
        public string CampusName { get; set; }
    }
}
