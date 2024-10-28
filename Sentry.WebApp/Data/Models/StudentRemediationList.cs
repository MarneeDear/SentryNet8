using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("Students_List", Schema = "Integration")]
    public class StudentRemediationList : BaseRemediationList
    {
        public string Name { get; set; }

        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("MiddleName")]
        public string MiddleName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("StudentId")]
        public string StudentId { get; set; }
    }
}
