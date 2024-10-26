using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentPossibleMatchViewModel
    {
        public List<StudentMatchSummaryViewModel> PossibleMatches { get; set; }

        public class StudentMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string Name { get; set; }
            public string FirstName { get; set; }
            public string PreferredName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string StudentId { get; set; }
            public string EmailAddress1 { get; set; }
            public string BirthDate { get; set; }
        }
    }
}
