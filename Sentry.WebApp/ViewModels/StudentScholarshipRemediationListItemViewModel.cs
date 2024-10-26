using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string StudentId { get; set; }

        public string ScholarshipAcademicYear { get; set; }

        public string ScholarshipName { get; set; }

        public decimal? ScholarshipAmount { get; set; }

    }
}
