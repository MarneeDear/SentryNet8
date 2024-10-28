using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class DesignationRemediationListItemViewModel : BaseRemediationListItemViewModel
    {
        public string DesignationName { get; set; }
        public string DesignationId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string DesignationType { get; set; }
        public string DesignationTypeName { get; set; }
        public string DesignationSubtype { get; set; }
        public string DesignationStatus { get; set; }
        public string DesignationState { get; set; }
        public string UADepartment { get; set; }
        public string Type { get; set; }
        public string DataSource { get; set; }
        public string None { get; set; }
    }
}
