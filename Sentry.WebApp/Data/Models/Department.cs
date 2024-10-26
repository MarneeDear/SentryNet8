using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Departments", Schema = "MDS")]
    public class Department
    {
        [Key]
        [Column("MasterRecordId")]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }
    }
}
