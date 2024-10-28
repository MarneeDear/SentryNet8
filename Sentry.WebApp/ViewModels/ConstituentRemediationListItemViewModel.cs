using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string Name { get; set; }
        public string UAPersonId { get; set; }
        public string BirthDate { get; set; }
        public string Type { get; set; }
        public string DataSource { get; set; }
        public string None { get; set; }
    }
}
