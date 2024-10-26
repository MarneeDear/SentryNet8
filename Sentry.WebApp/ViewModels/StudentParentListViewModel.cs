using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentListViewModel : BaseListViewModel
    {
        public StudentParentListViewModel() : base() { }

        public List<StudentParentRemediationListItemViewModel> RemediationList { get; set; }
    }
}
