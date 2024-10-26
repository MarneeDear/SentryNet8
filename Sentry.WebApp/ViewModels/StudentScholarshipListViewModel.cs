using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipListViewModel : BaseListViewModel
    {
        public StudentScholarshipListViewModel() : base() { }

        public List<StudentScholarshipRemediationListItemViewModel> RemediationList { get; set; }
    }
}
