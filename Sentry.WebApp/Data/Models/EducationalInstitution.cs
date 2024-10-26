using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("EducationalInstitutions", Schema = "MDS")]
    public class EducationalInstitution
    {
        [Key]
        [Column("MasterRecordId")]
        public string EducationalInstitutionMasterId { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("Type")]
        public string Type { get; set; }
    }
}
