using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("StudentDegreeName", Schema = "MDS")]
    public class DegreeName
    {
        public string DegreeNameName { get; set; }

        [Key]
        public string DegreeNameCode { get; set; }
    }
}
