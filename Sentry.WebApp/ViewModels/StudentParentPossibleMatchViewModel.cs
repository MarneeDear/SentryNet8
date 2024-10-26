using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentPossibleMatchViewModel
    {
        public List<StudentParentMatchSummaryViewModel> PossibleMatches { get; set; }

        public class StudentParentMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string StudentParentTitle { get; set; }
            public string Name { get; set; }
            public string FirstName { get; set; }
            public string PreferredName { get; set; }
            public string LastName { get; set; }
            public string Suffix { get; set; }
            public string StudentId { get; set; }
            public string StudentFirstName { get; set; }
            public string StudentLastName { get; set; }
            public string Relationship { get; set; }
            public string Phone1Number { get; set; }
            public string Phone1Extension { get; set; }
            public string Phone1CountryCode { get; set; }
            public string Phone2Number { get; set; }
            public string Phone2Extension { get; set; }
            public string Phone2CountryCode { get; set; }
            public string EmailAddress1 { get; set; }
            public string EmailAddress2 { get; set; }
            public string Address1 { get; set; }
            public string PostalCode { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string Country { get; set; }
        }
    }
}
