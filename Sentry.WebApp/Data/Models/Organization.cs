using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("Organizations", Schema = "MDS")]
    public class Organization
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public string Abbreviation { get; set; }
    }
}
