using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DesignationState", Schema = "MDS")]
    public class DesignationState
    {
        public string DesignationStateName { get; set; }
        [Key]
        public string DesignationStateMasterId { get; set; }
    }
}
