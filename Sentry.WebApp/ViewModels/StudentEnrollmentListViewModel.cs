using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentListViewModel : BaseListViewModel
    {
        public StudentEnrollmentListViewModel() : base() { }

        public List<StudentEnrollmentRemediationListItemViewModel> RemediationList { get; set; }
    }
}
