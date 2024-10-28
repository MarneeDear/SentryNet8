using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class DegreeListViewModel : BaseListViewModel
    {
        public DegreeListViewModel() : base() { }

        public List<DegreeRemediationListItemViewModel> RemediationList { get; set; }
    }
}
