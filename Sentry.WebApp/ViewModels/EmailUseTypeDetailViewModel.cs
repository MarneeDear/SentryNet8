using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class EmailUseTypeDetailViewModel
    {
        public string MasterRecordId { get; set; }

        public string EmailAddressUseType { get; set; }

        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
    }
}
