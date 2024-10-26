using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentDetailViewModel
    {
        public string MasterRecordId { get; set; }
        
        public string CampusMasterId { get; set; }
        
        public string TermName { get; set; }
        
        public string TermMasterId { get; set; }

        public string TermSourceSystemRecordId { get; set; }
    }
}
