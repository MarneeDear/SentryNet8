using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Parent_List", Schema = "Integration")]
    public class StudentParentRemediationList : BaseRemediationList
    {
        [Column("FirstName")]
        public string FirstName { get; set; }
        [Column("PreferredName")]
        public string PreferredName { get; set; }
        [Column("LastName")]
        public string LastName { get; set; }
        [Column("StudentFirstName")]
        public string StudentFirstName { get; set; }
        [Column("StudentLastName")]
        public string StudentLastName { get; set; }
        [Column("Relationship")]
        public string Relationship { get; set; }
    }
}
