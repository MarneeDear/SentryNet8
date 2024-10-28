using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentPhoneListViewModel : BaseListViewModel
    {
        public ConstituentPhoneListViewModel() : base() { }

        public List<ConstituentPhoneRemediationListItemViewModel> RemediationList { get; set; }
    }
}
