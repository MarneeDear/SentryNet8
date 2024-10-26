using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class OfficeLocationViewModel : BaseIntegrationViewModel
    {
        public OfficeLocationViewModel() : base() { }

        #region Name
        
        public string Name { get; set; }

        public string Name_Status { get; set; }

        public string Name_BusinessName { get; set; }

        public string Name_BusinessDescription { get; set; }

		public string Name_Source { get; set; }

		public int? Name_FieldId { get; set; }

        public string Name_OriginalValue { get; set; }

        #endregion

        #region Building Code
        
        public string BuildingCode { get; set; }

        public string BuildingCode_Status { get; set; }

        public string BuildingCode_BusinessName { get; set; }

        public string BuildingCode_BusinessDescription { get; set; }

        public string BuildingCode_Source { get; set; }

        public int? BuildingCode_FieldId { get; set; }

        public string BuildingCode_OriginalValue { get; set; }

        #endregion

        #region Address
        
		public string Address1 { get; set; }

        public string Address1_Status { get; set; }

        public string Address1_BusinessName { get; set; }

        public string Address1_BusinessDescription { get; set; }

        public string Address1_Source { get; set; }

        public int? Address1_FieldId { get; set; }

        public string Address1_OriginalValue { get; set; }
        
		public string Address2 { get; set; }

        public string Address2_Status { get; set; }

        public string Address2_BusinessName { get; set; }

        public string Address2_BusinessDescription { get; set; }

        public string Address2_Source { get; set; }

        public int? Address2_FieldId { get; set; }

        public string Address2_OriginalValue { get; set; }
        
		public string City { get; set; }

        public string City_Status { get; set; }

        public string City_BusinessName { get; set; }

        public string City_BusinessDescription { get; set; }

        public string City_Source { get; set; }

        public int? City_FieldId { get; set; }

        public string City_OriginalValue { get; set; }
        
		public string State { get; set; }

        public string State_Status { get; set; }

        public string State_BusinessName { get; set; }

        public string State_BusinessDescription { get; set; }

        public string State_Source { get; set; }

        public int? State_FieldId { get; set; }

        public string State_OriginalValue { get; set; }

        public int? StateMasterId { get; set; }

        public string StateMasterId_Status { get; set; }

        public string StateMasterId_BusinessName { get; set; }

        public string StateMasterId_BusinessDescription { get; set; }

        public string StateMasterId_Source { get; set; }

        public int? StateMasterId_FieldId { get; set; }

        public int? StateMasterId_OriginalValue { get; set; }

        public string PostalCode { get; set; }

        public string PostalCode_Status { get; set; }

        public string PostalCode_BusinessName { get; set; }

        public string PostalCode_BusinessDescription { get; set; }

        public string PostalCode_Source { get; set; }

        public int? PostalCode_FieldId { get; set; }

        public string PostalCode_OriginalValue { get; set; }
        
		public string Country { get; set; }

        public string Country_Status { get; set; }

        public string Country_BusinessName { get; set; }

        public string Country_BusinessDescription { get; set; }

        public string Country_Source { get; set; }

        public int? Country_FieldId { get; set; }

        public string Country_OriginalValue { get; set; }

        public int? CountryMasterId { get; set; }

        public string CountryMasterId_Status { get; set; }

        public string CountryMasterId_BusinessName { get; set; }

        public string CountryMasterId_BusinessDescription { get; set; }

        public string CountryMasterId_Source { get; set; }

        public int? CountryMasterId_FieldId { get; set; }

        public int? CountryMasterId_OriginalValue { get; set; }

        #endregion

        #region Drop-Downs

        public List<SelectListItem> OfficeLocationList { get; set; }
        
		public List<SelectListItem> StateList { get; set; }
        
		public List<SelectListItem> CountryList { get; set; }

        #endregion

        public List<OfficeLocationHistoryViewModel> HistoryData { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }

}