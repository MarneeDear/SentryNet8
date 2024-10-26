using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("EmailAddressUseTypes", Schema = "MDS")]
    public class EmailUseType
    {
        [Key]
        [Column("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [Column("EmailAddressUseType")]
        public string EmailAddressUseType { get; set; }
    }
}
