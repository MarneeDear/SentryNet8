using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitListViewModel : BaseListViewModel
    {
        public OrganizationalUnitListViewModel() : base() { }

        public List<OrganizationalUnitRemediationListItemViewModel> RemediationList { get; set; }
    }
}
