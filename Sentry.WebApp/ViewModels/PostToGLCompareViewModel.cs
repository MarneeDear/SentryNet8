using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class PostToGLCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public int? MasterId { get; set; }

        public string System { get; set; }


        #region Integration Record

        public string SourceRecordId { get; set; }

        public string DesignationName { get; set; }
        public string DesignationName_BusinessName { get; set; }
        public string DesignationName_BusinessDescription { get; set; }

        public string DesignationId { get; set; }
        public string DesignationId_BusinessName { get; set; }
        public string DesignationId_BusinessDescription { get; set; }

        public DateTime? StartDate { get; internal set; }
        public string StartDate_BusinessName { get; internal set; }
        public string StartDate_BusinessDescription { get; internal set; }

        public DateTime? EndDate { get; internal set; }
        public string EndDate_BusinessName { get; internal set; }
        public string EndDate_BusinessDescription { get; internal set; }

        public string DesignationType { get; set; }
        public string DesignationType_BusinessName { get; set; }
        public string DesignationType_BusinessDescription { get; set; }

        public string DesignationSubtype { get; set; }
        public string DesignationSubtype_BusinessName { get; set; }
        public string DesignationSubtype_BusinessDescription { get; set; }

        public string DesignationStatus { get; set; }
        public string DesignationStatus_BusinessName { get; set; }
        public string DesignationStatus_BusinessDescription { get; set; }

        public string DesignationState { get; set; }
        public string DesignationState_BusinessName { get; set; }
        public string DesignationState_BusinessDescription { get; set; }

        public string UADepartment { get; set; }
        public string UADepartment_BusinessName { get; set; }
        public string UADepartment_BusinessDescription { get; set; }

        #endregion

        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string DesignationName_Compare { get; set; }

        public string DesignationId_Compare { get; set; }

        public DateTime? StartDate_Compare { get; internal set; }

        public DateTime? EndDate_Compare { get; internal set; }

        public string DesignationType_Compare { get; internal set; }

        public string DesignationSubtype_Compare { get; internal set; }

        public string DesignationStatus_Compare { get; internal set; }

        public string DesignationState_Compare { get; internal set; }

        public string UADepartment_Compare { get; internal set; }

        #endregion
    }
}
