using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("EmployeeTypes", Schema = "MDS")]
    public class EmployeeType
    {
        public string Id { get; set; }

        public string EmployeeTypeName { get; set; }
    }
}
