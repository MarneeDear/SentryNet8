using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentAddressHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        #region ConstituentMasterId
        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_OriginalValue { get; set; }
        #endregion

        #region ConstituentSourceSystemRecordId
        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_OriginalValue { get; set; }
        #endregion


        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_OriginalValue { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_OriginalValue { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_OriginalValue { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public string Address_Status { get; set; }
        public string Address_OriginalValue { get; set; }
        #endregion

        #region AddressMasterId
        public string AddressMasterId { get; set; }
        public string AddressMasterId_Status { get; set; }
        public string AddressMasterId_OriginalValue { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public string City_Status { get; set; }
        public string City_OriginalValue { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }
        public string PostalCode_Status { get; set; }
        public string PostalCode_OriginalValue { get; set; }
        #endregion

        #region DeliveryPointCode
        public string DeliveryPointCode { get; set; }
        public string DeliveryPointCode_Status { get; set; }
        public string DeliveryPointCode_OriginalValue { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public string State_Status { get; set; }
        public string State_OriginalValue { get; set; }
        #endregion

        #region StateSourceSystemRecordId
        public string StateSourceSystemRecordId { get; set; }
        public string StateSourceSystemRecordId_Status { get; set; }
        public string StateSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region StateMasterId
        public string StateMasterId { get; set; }
        public string StateMasterId_Status { get; set; }
        public string StateMasterId_OriginalValue { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public string Country_Status { get; set; }
        public string Country_OriginalValue { get; set; }
        #endregion

        #region CountrySourceSystemRecordId
        public string CountrySourceSystemRecordId { get; set; }
        public string CountrySourceSystemRecordId_Status { get; set; }
        #endregion

        #region CountryMasterId
        public string CountryMasterId { get; set; }
        public string CountryMasterId_Status { get; set; }
        public string CountryMasterId_OriginalValue { get; set; }
        #endregion

        #region AddressUseType
        public string AddressUseType { get; set; }
        public string AddressUseType_Status { get; set; }
        public string AddressUseType_OriginalValue { get; set; }
        #endregion

        #region AddressUseTypeSourceSystemRecordId
        public string AddressUseTypeSourceSystemRecordId { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string AddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region AddressUseTypeMasterId
        public string AddressUseTypeMasterId { get; set; }
        public string AddressUseTypeMasterId_Status { get; set; }
        public string AddressUseTypeMasterId_OriginalValue { get; set; }
        #endregion

        #region AddressIsPrimary
        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimary_Status { get; set; }
        public string AddressIsPrimary_OriginalValue { get; set; }
        #endregion


    }
}
