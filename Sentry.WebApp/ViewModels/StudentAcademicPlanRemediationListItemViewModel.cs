using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicPlanRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Student { get; set; }
        public string Campus { get; set; }
        public string Term { get; set; }
        public string AcademicCareer { get; set; }
        public string AcademicProgram { get; set; }
        public string AcademicPlan { get; set; }
        public string AcademicSubplan { get; set; }
    }
}
