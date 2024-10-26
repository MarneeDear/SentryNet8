using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OrganizationalUnitCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public int? MasterId { get; set; }

        public string System { get; set; }


        #region Integration Record

        public string SourceRecordId { get; set; }

        public string OrganizationalUnitName { get; set; }
        public string OrganizationalUnitName_BusinessName { get; set; }
        public string OrganizationalUnitName_BusinessDescription { get; set; }

        public string OrganizationalUnitCode { get; set; }
        public string OrganizationalUnitCode_BusinessName { get; set; }
        public string OrganizationalUnitCode_BusinessDescription { get; set; }

        public string OrganizationalUnitType { get; internal set; }
        public string OrganizationalUnitType_BusinessName { get; internal set; }
        public string OrganizationalUnitType_BusinessDescription { get; internal set; }

        public string OrganizationName { get; internal set; }
        public string OrganizationName_BusinessName { get; internal set; }
        public string OrganizationName_BusinessDescription { get; internal set; }

        #endregion


        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string OrganizationalUnitName_Compare { get; set; }

        public string OrganizationalUnitCode_Compare { get; set; }
        
        public string OrganizationalUnitType_Compare { get; internal set; }

        public string OrganizationName_Compare { get; internal set; }

        #endregion
    }
}
