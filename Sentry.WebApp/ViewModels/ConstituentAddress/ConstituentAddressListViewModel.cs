using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentAddressListViewModel : BaseListViewModel
    {
        public ConstituentAddressListViewModel() : base() { }

        public List<ConstituentAddressRemediationListItemViewModel> RemediationList { get; set; }
    }
}
