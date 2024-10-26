using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class OfficeLocationPossibleMatch
    {
        [Key]
        public int MasterId { get; set; }
        public int MatchConfidence { get; set; }
        public string OfficeLocationName{ get; set; }
        public string OfficeLocationCode { get; set; }
    }
}
