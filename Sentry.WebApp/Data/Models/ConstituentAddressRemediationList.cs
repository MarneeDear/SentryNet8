using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("ConstituentAddress_List", Schema = "Integration")]
    public class ConstituentAddressRemediationList : BaseRemediationList
    {
        [Column("FirstName")]
        public string FirstName { get; set; }

        [Column("LastName")]
        public string LastName { get; set; }

        [Column("Address")]
        public string Address { get; set; }

        [Column("AddressUseType")]
        public string AddressUseType { get; set; }

        //[Column("ErrorCategories")]
        //public string ErrorCategories { get; set; }

        //[Column("SystemName")]
        //public string SystemName { get; set; }
    }
}
