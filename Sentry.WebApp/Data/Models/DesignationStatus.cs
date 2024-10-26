using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DesignationStatus", Schema = "MDS")]
    public class DesignationStatus
    {
        public string DesignationStatusName { get; set; }
        [Key]
        public string DesignationStatusMasterId { get; set; }
    }
}
