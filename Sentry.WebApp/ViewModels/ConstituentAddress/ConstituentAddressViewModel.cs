using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentAddressViewModel : BaseIntegrationViewModel
    {
        public ConstituentAddressViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region ConstituentMasterId
        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_BusinessName { get; set; }
        public string ConstituentMasterId_BusinessDescription { get; set; }
        public string ConstituentMasterId_Source { get; set; }
        public string ConstituentMasterId_Category { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public int? ConstituentMasterId_AttributeId { get; set; }
        public bool ConstituentMasterId_IsReadOnly { get; set; }
        public string ConstituentMasterId_OriginalValue { get; set; }
        #endregion

        #region ConstituentSourceSystemRecordId
        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessName { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessDescription { get; set; }
        public string ConstituentSourceSystemRecordId_Source { get; set; }
        public string ConstituentSourceSystemRecordId_Category { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public int? ConstituentSourceSystemRecordId_AttributeId { get; set; }
        public bool ConstituentSourceSystemRecordId_IsReadOnly { get; set; }
        public string ConstituentSourceSystemRecordId_OriginalValue { get; set; }
        #endregion


        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public string FirstName_Status { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public bool FirstName_IsReadOnly { get; set; }
        public string FirstName_OriginalValue { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public string LastName_Status { get; set; }
        public int? LastName_AttributeId { get; set; }
        public bool LastName_IsReadOnly { get; set; }
        public string LastName_OriginalValue { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Source { get; set; }
        public string UAPersonId_Category { get; set; }
        public string UAPersonId_Status { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        public bool UAPersonId_IsReadOnly { get; set; }
        public string UAPersonId_OriginalValue { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        public string Address_Source { get; set; }
        public string Address_Category { get; set; }
        public string Address_Status { get; set; }
        public int? Address_AttributeId { get; set; }
        public bool Address_IsReadOnly { get; set; }
        public string Address_OriginalValue { get; set; }
        #endregion

        #region Address Master Id
        public string AddressMasterId { get; set; }
        public string AddressMasterId_BusinessName { get; set; }
        public string AddressMasterId_BusinessDescription { get; set; }
        public string AddressMasterId_Source { get; set; }
        public string AddressMasterId_Category { get; set; }
        public string AddressMasterId_Status { get; set; }
        public int? AddressMasterId_AttributeId { get; set; }
        public bool AddressMasterId_IsReadOnly { get; set; }
        public string AddressMasterId_OriginalValue { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public string City_Source { get; set; }
        public string City_Category { get; set; }
        public string City_Status { get; set; }
        public int? City_AttributeId { get; set; }
        public bool City_IsReadOnly { get; set; }
        public string City_OriginalValue { get; set; }
        #endregion

        #region Postal Code
        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public string PostalCode_Source { get; set; }
        public string PostalCode_Category { get; set; }
        public string PostalCode_Status { get; set; }
        public int? PostalCode_AttributeId { get; set; }
        public bool PostalCode_IsReadOnly { get; set; }
        public string PostalCode_OriginalValue { get; set; }
        #endregion

        #region Delivery Point Code
        public string DeliveryPointCode { get; set; }
        public string DeliveryPointCode_BusinessName { get; set; }
        public string DeliveryPointCode_BusinessDescription { get; set; }
        public string DeliveryPointCode_Source { get; set; }
        public string DeliveryPointCode_Category { get; set; }
        public string DeliveryPointCode_Status { get; set; }
        public int? DeliveryPointCode_AttributeId { get; set; }
        public bool DeliveryPointCode_IsReadOnly { get; set; }
        public string DeliveryPointCode_OriginalValue { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public string State_Source { get; set; }
        public string State_Category { get; set; }
        public string State_Status { get; set; }
        public int? State_AttributeId { get; set; }
        public bool State_IsReadOnly { get; set; }
        public string State_OriginalValue { get; set; }
        #endregion

        #region State Source System Record Id
        public string StateSourceSystemRecordId { get; set; }
        public string StateSourceSystemRecordId_BusinessName { get; set; }
        public string StateSourceSystemRecordId_BusinessDescription { get; set; }
        public string StateSourceSystemRecordId_Source { get; set; }
        public string StateSourceSystemRecordId_Category { get; set; }
        public string StateSourceSystemRecordId_Status { get; set; }
        public int? StateSourceSystemRecordId_AttributeId { get; set; }
        public bool StateSourceSystemRecordId_IsReadOnly { get; set; }
        public string StateSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region State Master Id
        public string StateMasterId { get; set; }
        public string StateMasterId_BusinessName { get; set; }
        public string StateMasterId_BusinessDescription { get; set; }
        public string StateMasterId_Source { get; set; }
        public string StateMasterId_Category { get; set; }
        public string StateMasterId_Status { get; set; }
        public int? StateMasterId_AttributeId { get; set; }
        public bool StateMasterId_IsReadOnly { get; set; }
        public string StateMasterId_OriginalValue { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public string Country_Source { get; set; }
        public string Country_Category { get; set; }
        public string Country_Status { get; set; }
        public int? Country_AttributeId { get; set; }
        public bool Country_IsReadOnly { get; set; }
        public string Country_OriginalValue { get; set; }
        #endregion

        #region Country Source System Record Id
        public string CountrySourceSystemRecordId { get; set; }
        public string CountrySourceSystemRecordId_BusinessName { get; set; }
        public string CountrySourceSystemRecordId_BusinessDescription { get; set; }
        public string CountrySourceSystemRecordId_Source { get; set; }
        public string CountrySourceSystemRecordId_Category { get; set; }
        public string CountrySourceSystemRecordId_Status { get; set; }
        public int? CountrySourceSystemRecordId_AttributeId { get; set; }
        public bool CountrySourceSystemRecordId_IsReadOnly { get; set; }
        public string CountrySourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region Country Master Id
        public string CountryMasterId { get; set; }
        public string CountryMasterId_BusinessName { get; set; }
        public string CountryMasterId_BusinessDescription { get; set; }
        public string CountryMasterId_Source { get; set; }
        public string CountryMasterId_Category { get; set; }
        public string CountryMasterId_Status { get; set; }
        public int? CountryMasterId_AttributeId { get; set; }
        public bool CountryMasterId_IsReadOnly { get; set; }
        public string CountryMasterId_OriginalValue { get; set; }
        #endregion

        #region Address Use Type
        public string AddressUseType { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        public string AddressUseType_Source { get; set; }
        public string AddressUseType_Category { get; set; }
        public string AddressUseType_Status { get; set; }
        public int? AddressUseType_AttributeId { get; set; }
        public bool AddressUseType_IsReadOnly { get; set; }
        public string AddressUseType_OriginalValue { get; set; }
        #endregion

        #region Address Use Type Source System Record Id
        public string AddressUseTypeSourceSystemRecordId { get; set; }
        public string AddressUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string AddressUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Source { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Category { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Status { get; set; }
        public int? AddressUseTypeSourceSystemRecordId_AttributeId { get; set; }
        public bool AddressUseTypeSourceSystemRecordId_IsReadOnly { get; set; }
        public string AddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region Address Type Master Id
        public string AddressUseTypeMasterId { get; set; }
        public string AddressUseTypeMasterId_BusinessName { get; set; }
        public string AddressUseTypeMasterId_BusinessDescription { get; set; }
        public string AddressUseTypeMasterId_Source { get; set; }
        public string AddressUseTypeMasterId_Category { get; set; }
        public string AddressUseTypeMasterId_Status { get; set; }
        public int? AddressUseTypeMasterId_AttributeId { get; set; }
        public bool AddressUseTypeMasterId_IsReadOnly { get; set; }
        public string AddressUseTypeMasterId_OriginalValue { get; set; }
        #endregion

        #region AddressIsPrimary
        public bool? AddressIsPrimary { get; set; }
        public bool TempAddressIsPrimary { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        public string AddressIsPrimary_Source { get; set; }
        public string AddressIsPrimary_Category { get; set; }
        public string AddressIsPrimary_Status { get; set; }
        public int? AddressIsPrimary_AttributeId { get; set; }
        public bool AddressIsPrimary_IsReadOnly { get; set; }
        public string AddressIsPrimary_OriginalValue { get; set; }
        #endregion


        #region Dropdowns
        public List<SelectListItem> AddressUseTypeList { get; set; }
        public IEnumerable<SelectListItem> CountryList { get; set; }
        public IEnumerable<SelectListItem> StatesList { get; set; }

        #endregion


        public List<ConstituentAddressHistoryViewModel> HistoryData { get; set; }

    }

}
