using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationPossibleMatchViewModel
    {
        public List<DesignationMatchSummaryViewModel> PossibleMatches { get; set; }

        public class DesignationMatchSummaryViewModel
        {
            public bool Selected { get; set; }
            public int MatchConfidence { get; set; }
            public string MasterId { get; set; }

            public string DesignationId { get; set; }
            public string DesignationName { get; set; }
            public string Description { get; set; }
            public string DesignationType { get; set; }
            public string StartDate { get; set; }
            public string EndDate { get; set; }
            public string KFSAccount { get; set; }
            public string VSECategory { get; set; }
            public string GLOrganization { get; set; }
        }
    }
}
