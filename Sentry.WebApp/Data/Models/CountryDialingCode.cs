using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("PhoneCountryCode", Schema = "MDS")]
    public class CountryDialingCode
    {
        [Key]
        public string MasterRecordId { get; set; }

        public string DialingCodeName { get; set; }

        public string CountryDisplayName { get; set; }
    }
}
