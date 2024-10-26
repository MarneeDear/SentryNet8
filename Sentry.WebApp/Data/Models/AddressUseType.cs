using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("AddressUseTypes", Schema = "MDS")]
    public class AddressUseType
    {
        [Column("MasterRecordId")]
        [Key]
        public string MasterRecordId { get; set; }

        [Column("AddressUseType")]
        public string UseType { get; set; }
    }
}
