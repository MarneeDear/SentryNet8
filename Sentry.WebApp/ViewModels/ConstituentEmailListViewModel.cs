using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentEmailListViewModel : BaseListViewModel
    {
        public ConstituentEmailListViewModel() : base() { }

        public List<ConstituentEmailRemediationListItemViewModel> RemediationList { get; set; }
    }
}
