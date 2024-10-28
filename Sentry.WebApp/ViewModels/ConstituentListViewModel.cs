using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentListViewModel : BaseListViewModel
    {
        public ConstituentListViewModel() : base() { }

        public List<ConstituentRemediationListItemViewModel> RemediationList { get; set; }
    }
}
