using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationMatchViewModel : BaseIntegrationViewModel
    {

        public OfficeLocationMatchViewModel() : base() { }

        #region Integration Record

        public DateTime? IntegrationDate { get; set; }

        public string Name { get; set; }

        public int Name_Weight { get; set; }

        public string BuildingCode { get; set; }

        public int BuildingCode_Weight { get; set; }

        public string Address1 { get; set; }

        public string Address2 { get; set; }

        public string City { get; set; }

        public string State { get; set; }

        public string PostalCode { get; set; }

        public string Country { get; set; }

        #endregion

        public List<OfficeLocationMatchSummaryViewModel> PossibleMatches { get; set; }

    }

    public class OfficeLocationMatchSummaryViewModel
    {
        public bool Selected { get; set; }

        public int MatchConfidence { get; set; }

        public string Name { get; set; }

        public string BuildingCode { get; set; }

        public long MasterId { get; set; }
    }
}
