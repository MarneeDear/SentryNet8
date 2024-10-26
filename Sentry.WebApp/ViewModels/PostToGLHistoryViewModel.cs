using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class PostToGLHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_Status { get; set; }

        public string DesignationId { get; set; }
        public string DesignationId_Status { get; set; }

        public DateTime? StartDate { get; set; }
        public string StartDate_Status { get; set; }

        public DateTime? EndDate { get; set; }
        public string EndDate_Status { get; set; }

        public string DesignationType { get; set; }
        public string DesignationType_Status { get; set; }

        public string DesignationSubtype { get; set; }
        public string DesignationSubtype_Status { get; set; }

        public string DesignationStatus { get; set; }
        public string DesignationStatus_Status { get; set; }

        public string DesignationState { get; set; }
        public string DesignationState_Status { get; set; }

        public string UADepartment { get; set; }
        public string UADepartment_Status { get; set; }

    }
}
