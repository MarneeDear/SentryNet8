using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("OrganizationalUnits", Schema = "MDS")]
    public class OrganizationalUnit
    {
        [Key]
        public int Id { get; set; }

        [Column("OrgUnitCode")]
        public string OrganizationalUnitCode { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        [Column("OrgUnitType")]
        public string Type { get; set; }

        [Column("OrganizationName")]
        public string OrganizationName { get; set; }

        [Column("OrganizationCode")]
        public string OrganizationMasterRecordId { get; set; }
    }
}
