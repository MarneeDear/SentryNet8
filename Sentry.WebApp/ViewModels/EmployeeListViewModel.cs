using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeeListViewModel : BaseListViewModel
    {
        public EmployeeListViewModel() : base() { }

        public List<EmployeeRemediationListItemViewModel> RemediationList { get; set; }
    }
}
