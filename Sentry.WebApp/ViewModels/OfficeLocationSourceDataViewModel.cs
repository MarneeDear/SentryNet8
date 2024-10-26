using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationSourceDataViewModel
    {
        public long RecordId { get; set; }
        public int? IntegrationId { get; set; }
        public DateTime IntegrationDate { get; set; }
        public int? SystemId { get; set; }
        public string System { get; set; }
        public string SystemRecordId { get; set; }
        public string Name_SourceValue { get; set; }
        public string Name_CurrentValue { get; set; }
        public string Name_Source { get; set; }
        public string Name_Status { get; set; }
        public int? Name_AttributeId { get; set; }
        public string BuildingCode_SourceValue { get; set; }
        public string BuildingCode_CurrentValue { get; set; }
        public string BuildingCode_Source { get; set; }
        public string BuildingCode_Status { get; set; }
        public int? BuildingCode_AttributeId { get; set; }
        public string Address1_SourceValue { get; set; }
        public string Address1_CurrentValue { get; set; }
        public string Address1_Source { get; set; }
        public string Address1_Status { get; set; }
        public int? Address1_AttributeId { get; set; }
        public string Address2_SourceValue { get; set; }
        public string Address2_CurrentValue { get; set; }
        public string Address2_Source { get; set; }
        public string Address2_Status { get; set; }
        public int? Address2_AttributeId { get; set; }
        public string City_SourceValue { get; set; }
        public string City_CurrentValue { get; set; }
        public string City_Source { get; set; }
        public string City_Status { get; set; }
        public int? City_AttributeId { get; set; }
        public string State_SourceValue { get; set; }
        public string State_CurrentValue { get; set; }
        public string State_Source { get; set; }
        public string State_Status { get; set; }
        public int? State_AttributeId { get; set; }
        public string PostalCode_SourceValue { get; set; }
        public string PostalCode_CurrentValue { get; set; }
        public string PostalCode_Source { get; set; }
        public string PostalCode_Status { get; set; }
        public int? PostalCode_AttributeId { get; set; }
        public string Country_SourceValue { get; set; }
        public string Country_CurrentValue { get; set; }
        public string Country_Source { get; set; }
        public string Country_Status { get; set; }
        public int? Country_AttributeId { get; set; }

    }
}
