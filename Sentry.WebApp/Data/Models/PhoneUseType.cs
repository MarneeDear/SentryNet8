using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("PhoneNumberUseTypes", Schema = "MDS")]
    public class PhoneUseType
    {
        [Key]
        [Column("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [Column("UseType")]
        public string UseType { get; set; }
    }
}
