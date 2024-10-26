using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("Employees_List", Schema = "Integration")]
    public class EmployeeRemediationList : BaseRemediationList
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("MiddleName")]
        public string MiddleName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("UAPersonId")]
        public string UAPersonId { get; set; }

        //[Column("OrganizationName")]
        //public string OrganizationName { get; set; }

        //[Column("UDP")]
        //public string UDP { get; set; }
    }
}
