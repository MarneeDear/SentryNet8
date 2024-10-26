using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicCatalogRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string DegreeType { get; set; }
        public string AcademicCareer { get; set; }
        public string Department { get; set; }
    }
}
