using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Suffix", Schema = "MDS")]
    public class Suffix
    {
        [Column("Code")]
        [Key]
        public string Code { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
