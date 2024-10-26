using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class DataQualityMetric
    {
        [Key]
        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("BAD")]
        public int Bad { get; set; }

        [Column("GOOD")]
        public int Good { get; set; }

        [Column("ENRICHED")]
        public int Enriched { get; set; }
    }
}
