using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentAddressRemediationListItemViewModel : BaseRemediationListItemViewModel
    {

        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string AddressUseType { get; set; }

    }

}
