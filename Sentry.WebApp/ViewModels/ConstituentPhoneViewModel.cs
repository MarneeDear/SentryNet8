using System;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentPhoneViewModel : BaseIntegrationViewModel
    {
        public ConstituentPhoneViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }




        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessName { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessDescription { get; set; }
        public int? ConstituentSourceSystemRecordId_AttributeId { get; set; }
        public string ConstituentSourceSystemRecordId_OriginalValue { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_Source { get; set; }
        public bool ConstituentSourceSystemRecordId_IsReadOnly { get; set; }

        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public string FirstName_OriginalValue { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public bool FirstName_IsReadOnly { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public int? LastName_AttributeId { get; set; }
        public string LastName_OriginalValue { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public bool LastName_IsReadOnly { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        public string UAPersonId_OriginalValue { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_Source { get; set; }
        public bool UAPersonId_IsReadOnly { get; set; }

        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_BusinessName { get; set; }
        public string ConstituentMasterId_BusinessDescription { get; set; }
        public int? ConstituentMasterId_AttributeId { get; set; }
        public string ConstituentMasterId_OriginalValue { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_Source { get; set; }
        public bool ConstituentMasterId_IsReadOnly { get; set; }



        public string PhoneNumber { get; set; }
        public string PhoneNumber_BusinessName { get; set; }
        public string PhoneNumber_BusinessDescription { get; set; }
        public int? PhoneNumber_AttributeId { get; set; }
        public string PhoneNumber_OriginalValue { get; set; }
        public string PhoneNumber_Status { get; set; }
        public string PhoneNumber_Source { get; set; }
        public bool PhoneNumber_IsReadOnly { get; set; }

        //public string PhoneSystemRecordId { get; set; }
        //public string PhoneSystemRecordId_BusinessName { get; set; }
        //public string PhoneSystemRecordId_BusinessDescription { get; set; }
        //public int? PhoneSystemRecordId_AttributeId { get; set; }
        //public string PhoneSystemRecordId_OriginalValue { get; set; }
        //public string PhoneSystemRecordId_Status { get; set; }
        //public string PhoneSystemRecordId_Source { get; set; }
        //public bool PhoneSystemRecordId_IsReadOnly { get; set; }

        public string PhoneMasterId { get; set; }
        public string PhoneMasterId_BusinessName { get; set; }
        public string PhoneMasterId_BusinessDescription { get; set; }
        public int? PhoneMasterId_AttributeId { get; set; }
        public string PhoneMasterId_OriginalValue { get; set; }
        public string PhoneMasterId_Status { get; set; }
        public string PhoneMasterId_Source { get; set; }
        public bool PhoneMasterId_IsReadOnly { get; set; }



        public string Extension { get; set; }
        public string Extension_BusinessName { get; set; }
        public string Extension_BusinessDescription { get; set; }
        public int? Extension_AttributeId { get; set; }
        public string Extension_OriginalValue { get; set; }
        public string Extension_Status { get; set; }
        public string Extension_Source { get; set; }
        public bool Extension_IsReadOnly { get; set; }



        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public int? Country_AttributeId { get; set; }
        public string Country_OriginalValue { get; set; }
        public string Country_Status { get; set; }
        public string Country_Source { get; set; }
        public bool Country_IsReadOnly { get; set; }

        public string CountrySourceSystemRecordId { get; set; }
        public string CountrySourceSystemRecordId_BusinessName { get; set; }
        public string CountrySourceSystemRecordId_BusinessDescription { get; set; }
        public int? CountrySourceSystemRecordId_AttributeId { get; set; }
        public string CountrySourceSystemRecordId_OriginalValue { get; set; }
        public string CountrySourceSystemRecordId_Status { get; set; }
        public string CountrySourceSystemRecordId_Source { get; set; }
        public bool CountrySourceSystemRecordId_IsReadOnly { get; set; }

        public string CountryMasterId { get; set; }
        public string CountryMasterId_BusinessName { get; set; }
        public string CountryMasterId_BusinessDescription { get; set; }
        public int? CountryMasterId_AttributeId { get; set; }
        public string CountryMasterId_OriginalValue { get; set; }
        public string CountryMasterId_Status { get; set; }
        public string CountryMasterId_Source { get; set; }
        public bool CountryMasterId_IsReadOnly { get; set; }



        public string PhoneLineType { get; set; }
        public string PhoneLineType_BusinessName { get; set; }
        public string PhoneLineType_BusinessDescription { get; set; }
        public int? PhoneLineType_AttributeId { get; set; }
        public string PhoneLineType_OriginalValue { get; set; }
        public string PhoneLineType_Status { get; set; }
        public string PhoneLineType_Source { get; set; }
        public bool PhoneLineType_IsReadOnly { get; set; }

        public string PhoneLineTypeSourceSystemRecordId { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_BusinessName { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public int? PhoneLineTypeSourceSystemRecordId_AttributeId { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_OriginalValue { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Source { get; set; }
        public bool PhoneLineTypeSourceSystemRecordId_IsReadOnly { get; set; }

        public string PhoneLineTypeMasterId { get; set; }
        public string PhoneLineTypeMasterId_BusinessName { get; set; }
        public string PhoneLineTypeMasterId_BusinessDescription { get; set; }
        public int? PhoneLineTypeMasterId_AttributeId { get; set; }
        public string PhoneLineTypeMasterId_OriginalValue { get; set; }
        public string PhoneLineTypeMasterId_Status { get; set; }
        public string PhoneLineTypeMasterId_Source { get; set; }
        public bool PhoneLineTypeMasterId_IsReadOnly { get; set; }



        public string PhoneUseType { get; set; }
        public string PhoneUseType_BusinessName { get; set; }
        public string PhoneUseType_BusinessDescription { get; set; }
        public int? PhoneUseType_AttributeId { get; set; }
        public string PhoneUseType_OriginalValue { get; set; }
        public string PhoneUseType_Status { get; set; }
        public string PhoneUseType_Source { get; set; }
        public bool PhoneUseType_IsReadOnly { get; set; }

        public string PhoneUseTypeSourceSystemRecordId { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public int? PhoneUseTypeSourceSystemRecordId_AttributeId { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_OriginalValue { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Source { get; set; }
        public bool PhoneUseTypeSourceSystemRecordId_IsReadOnly { get; set; }

        public string PhoneUseTypeMasterId { get; set; }
        public string PhoneUseTypeMasterId_BusinessName { get; set; }
        public string PhoneUseTypeMasterId_BusinessDescription { get; set; }
        public int? PhoneUseTypeMasterId_AttributeId { get; set; }
        public string PhoneUseTypeMasterId_OriginalValue { get; set; }
        public string PhoneUseTypeMasterId_Status { get; set; }
        public string PhoneUseTypeMasterId_Source { get; set; }
        public bool PhoneUseTypeMasterId_IsReadOnly { get; set; }

        public bool? PhoneIsPrimary { get; set; }
        public bool TempPhoneIsPrimary { get; set; }
        public string PhoneIsPrimary_BusinessName { get; set; }
        public string PhoneIsPrimary_BusinessDescription { get; set; }
        public string PhoneIsPrimary_Status { get; set; }
        public string PhoneIsPrimary_Source { get; set; }
        public string PhoneIsPrimary_Category { get; set; }
        public string PhoneIsPrimary_OriginalValue { get; set; }
        public int? PhoneIsPrimary_AttributeId { get; set; }
        public bool PhoneIsPrimary_IsReadOnly { get; set; }



        #region Dropdowns
        public List<SelectListItem> ConstituentList { get; set; }
        public List<SelectListItem> PhoneList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> PhoneLineTypeList { get; set; }
        public List<SelectListItem> PhoneUseTypeList { get; set; }

        #endregion

        public List<ConstituentPhoneHistoryViewModel> HistoryData { get; set; }

    }
}
