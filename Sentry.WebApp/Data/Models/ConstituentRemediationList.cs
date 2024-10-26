using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("Constituents_List", Schema = "Integration")]
    public class ConstituentRemediationList : BaseRemediationList
    {
        [Column("Name")]
        public string Name { get; set; }

        [Column("UAPersonId")]
        public string UAPersonId { get; set; }
    }
}
