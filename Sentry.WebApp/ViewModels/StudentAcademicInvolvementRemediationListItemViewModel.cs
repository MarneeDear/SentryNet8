using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string StudentId { get; set; }

        #region Academic Involvement
        public string AcademicInvolvementAcademicYear { get; set; }

        public string AcademicInvolvementTerm { get; set; }

        public string AcademicInvolvementType { get; set; }

        public string AcademicInvolvementName { get; set; }
        
        #endregion

    }
}
