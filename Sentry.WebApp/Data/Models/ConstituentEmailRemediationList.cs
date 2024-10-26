using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("ConstituentEmail_List", Schema = "Integration")]
    public class ConstituentEmailRemediationList : BaseRemediationList
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("EmailAddress")]
        public string EmailAddress { get; set; }

        [Column("EmailAddressUseType")]
        public string EmailAddressUseType { get; set; }
    }
}
