using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string OrganizationalUnitType { get; set; }

        public string OrganizationalUnitName { get; set; }

        public string OrganizationalUnitCode { get; set; }
    }
}
