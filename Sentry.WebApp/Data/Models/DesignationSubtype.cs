using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DesignationSubtypes", Schema = "MDS")]
    public class DesignationSubtype
    {
        public string DesignationSubtypeCode { get; set; }
        public string DesignationSubtypeName { get; set; }
        [Key]
        public string DesignationSubtypeMasterId { get; set; }
    }
}
