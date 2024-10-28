using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeePossibleMatchViewModel
    {
        public List<EmployeeMatchSummaryViewModel> PossibleMatches { get; set; }

        public class EmployeeMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string Name { get; set; }
            public string FirstName { get; set; }
            public string PreferredName { get; set; }
            public string MiddleName { get; set; }
            public string LastName { get; set; }
            public string UAPersonId { get; set; }
            public string BirthDate { get; set; }
        }
    }
}
