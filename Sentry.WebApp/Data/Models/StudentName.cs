using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Students", Schema = "MDS")]
    public class StudentName
    {
        [Key]
        [Column("MasterRecordId")]
        public string StudentMasterId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string StudentId { get; set; }
    }
}
