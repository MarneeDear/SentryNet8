using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Name { get; set; }
        public string StudentId { get; set; }
        public string Type { get; set; }
        public string DataSource { get; set; }
        public string None { get; set; }
    }
}
