using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Title", Schema = "MDS")]
    public class Title
    {
        [Column("Title_Code")]
        [Key]
        public string Title_Code { get; set; }

        [Column("Title_Name")]
        public string Title_Name { get; set; }
    }
}
