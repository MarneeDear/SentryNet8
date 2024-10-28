using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class IntegrationDataQualityMetricsViewModel
    {
        public DateTime date { get; set; }
        public int enriched { get; set; }
        public int good { get; set; }
        public int bad { get; set; }
    }
}
