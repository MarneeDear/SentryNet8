using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string ParentName { get; set; }
        public string FirstName { get; set; }
        public string PreferredName { get; set; }
        public string LastName { get; set; }

        public string StudentName { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }

        public string Relationship { get; set; }
    }
}
