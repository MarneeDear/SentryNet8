using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Name { get; set; }

        public string BuildingCode { get; set; }

        public string City { get; set; }
    }
}
