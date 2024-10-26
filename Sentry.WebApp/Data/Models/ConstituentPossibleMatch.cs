using System;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class ConstituentPossibleMatch
    {
        [Key]
        public string MasterId { get; set; }
        public int MatchConfidence { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UAPersonId { get; set; }
        
        public string BirthDate { get; set; }
        public bool AllowMatch { get; set; }

    }
}
