using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Persons", Schema = "MDS")]
    public class PersonName
    {
        [Key]
        [Column("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        public string FirstName { get; set; }
        
        public string LastName { get; set; }

        [Column("UAPersonId")]
        public string UAPersonId { get; set; }
    }
}
