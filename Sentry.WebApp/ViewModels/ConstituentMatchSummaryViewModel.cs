using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentPossibleMatchViewModel
    {
        public List<ConstituentMatchSummaryViewModel> PossibleMatches { get; set; }
    }

    public class ConstituentMatchSummaryViewModel
    {
        public bool Selected { get; set; }
        public int MatchConfidence { get; set; }
        public string MasterId { get; set; }

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UAPersonId { get; set; }
        public string BirthDate { get; set; }
        public string AllowMatch { get; set; } = "Disallow";
    }
}
