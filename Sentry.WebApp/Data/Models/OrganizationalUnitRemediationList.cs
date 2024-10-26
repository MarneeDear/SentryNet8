using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
	[Table("OrganizationalUnits_List", Schema = "Integration")]
	public class OrganizationalUnitRemediationList : BaseRemediationList
	{
        [Column("OrganizationalUnitId")]
        public string OrganizationalUnitId { get; set; }

        [Column("OrganizationalUnitName")]
        public string OrganizationalUnitName { get; set; }

        [Column("OrganizationalUnitTypeName")]
        public string OrganizationalUnitTypeName { get; set; }

        [Column("OrganizationName")]
        public string OrganizationName { get; set; }
    }
}
