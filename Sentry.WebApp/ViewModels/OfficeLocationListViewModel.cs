using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationListViewModel : BaseListViewModel
	{
        public OfficeLocationListViewModel() : base() { }

        public List<OfficeLocationRemediationListItemViewModel> RemediationList { get; set; }
	}
}