using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("DegreeTypes", Schema = "MDS")]
    public class DegreeType
    {
        [Column("Name")]
        public string DegreeTypeName { get; set; }

        [Key]
        [Column("MasterRecordId")]
        public string DegreeTypeCode { get; set; }
    }
}
