using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementListViewModel : BaseListViewModel
    {
        public StudentAcademicInvolvementListViewModel() : base() { }

        public List<StudentAcademicInvolvementRemediationListItemViewModel> RemediationList { get; set; }
    }
}
