using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class DegreeRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Student { get; set; }

        #region Degree
        public string EducationalInstitution { get; set; }
        public string PreferredClassOf { get; set; }
        public string AwardedDate { get; set; }
        public string AwardedTerm { get; set; }
        #endregion
    }
}
