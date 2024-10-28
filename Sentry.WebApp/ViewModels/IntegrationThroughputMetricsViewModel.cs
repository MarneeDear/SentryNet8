using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class IntegrationThroughputMetricsViewModel
    {
        public DateTime date { get; set; }
        public int processed { get; set; }
        public int possibleMatch { get; set; }
        public int bad { get; set; }
    }
}
