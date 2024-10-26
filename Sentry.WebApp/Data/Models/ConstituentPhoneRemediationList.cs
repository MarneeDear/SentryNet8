using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("ConstituentPhone_List", Schema = "Integration")]
    public class ConstituentPhoneRemediationList : BaseRemediationList
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("PhoneNumber")]
        public string PhoneNumber { get; set; }

        [Column("PhoneExtension")]
        public string Extension { get; set; }

        [Column("CountryCode")]
        public string Country { get; set; }

        [Column("PhoneLineType")]
        public string PhoneLineType { get; set; }

        [Column("PhoneUseType")]
        public string PhoneUseType { get; set; }
    }
}
