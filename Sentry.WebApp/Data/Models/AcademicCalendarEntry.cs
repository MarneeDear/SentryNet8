using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicCalendarEntries", Schema = "MDS")]
    public class AcademicCalendarEntry
    {
        [Key]
        [Column("MasterRecordId")]
        public string Id { get; set; }

        [Column("Name")]
        public string Name { get; set; }

        [Column("AcademicCareer")]
        public string AcademicCareer { get; set; }

        [Column("AcademicTermCode")]
        public string AcademicTermCode { get; set; }

        [Column("AcademicYear")]
        public string AcademicYear { get; set; }

    }
}
