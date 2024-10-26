using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("PhoneNumberLineTypes", Schema = "MDS")]
    public class PhoneLineType
    {
        [Key]
        [Column("MasterRecordId")]
        public string MasterRecordId { get; set; }

        [Column("LineType")]
        public string LineType { get; set; }
    }
}
