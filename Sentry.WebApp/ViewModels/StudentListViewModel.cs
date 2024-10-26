using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentListViewModel : BaseListViewModel
    {
        public StudentListViewModel() : base() { }

        public List<StudentRemediationListItemViewModel> RemediationList { get; set; }
    }
}
