using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("OrganizationalUnitTypes", Schema = "MDS")]
    public class OrganizationalUnitType
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }
    }
}
