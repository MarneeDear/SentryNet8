using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class CountryDetailViewModel
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string MasterRecordId { get; set; }

        public string SourceSystemRecordId { get; set; }
    }
}
