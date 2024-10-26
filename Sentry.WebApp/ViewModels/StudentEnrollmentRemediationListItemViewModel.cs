using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string StudentName { get; set; }
        public string StudentId { get; set; }

        #region Enrollment
        public string EnrollmentTerm { get; set; }
        public string EnrollmentCampus { get; set; }
        public string EnrollmentAcademicYear { get; set; }
        public string EnrollmentAcademicCareer { get; set; }
        public string EnrollmentAcademicLevel { get; set; }
        #endregion
    }
}
