using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("PhoneNumbers", Schema = "MDS")]
    public class ConstituentPhoneNumber
    {
        public string PhoneNumber { get; set; }

        [Key]
        public string MasterRecordId { get; set; }

        public string CountryCode { get; set; }

        public string CountryCodeDescription { get; set; }

        public string CountryDialingCode { get; set; }

        public string LineTypeCode { get; set; }

        public string LineType { get; set; }
    }
}
