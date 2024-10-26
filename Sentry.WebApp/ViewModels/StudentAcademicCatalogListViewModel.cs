using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicCatalogListViewModel : BaseListViewModel
    {
        public StudentAcademicCatalogListViewModel() : base() { }

        public List<StudentAcademicCatalogRemediationListItemViewModel> RemediationList { get; set; }
    }
}
