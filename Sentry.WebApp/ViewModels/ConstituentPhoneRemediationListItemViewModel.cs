using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentPhoneRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
        public string Country { get; set; }
        public string PhoneLineType { get; set; }
        public string PhoneUseType { get; set; }
    }
}
