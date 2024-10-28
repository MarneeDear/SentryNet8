using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class ThroughputMetric
    {
        [Key]
        [Column("DATE")]
        public DateTime Date { get; set; }

        [Column("PROCESSED")]
        public int Processed { get; set; }

        [Column("BAD")]
        public int Bad { get; set; }

        [Column("POSSIBLEMATCH")]
        public int PossibleMatch { get; set; }
    }
}
