using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicPlanListViewModel : BaseListViewModel
    {
        public StudentAcademicPlanListViewModel() : base() { }

        public List<StudentAcademicPlanRemediationListItemViewModel> RemediationList { get; set; }
    }
}
