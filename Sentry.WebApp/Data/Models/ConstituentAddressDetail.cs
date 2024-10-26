using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class ConstituentAddressDetail : BaseDetail
    {

        #region ConstituentSourceSystemRecordId
        public string ConstituentSourceSystemRecordId { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessName { get; set; }
        public string ConstituentSourceSystemRecordId_BusinessDescription { get; set; }
        public string ConstituentSourceSystemRecordId_Status { get; set; }
        public string ConstituentSourceSystemRecordId_Source { get; set; }
        public string ConstituentSourceSystemRecordId_Category { get; set; }
        public int? ConstituentSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region ConstituentMasterId
        public string ConstituentMasterId { get; set; }
        public string ConstituentMasterId_BusinessName { get; set; }
        public string ConstituentMasterId_BusinessDescription { get; set; }
        public string ConstituentMasterId_Status { get; set; }
        public string ConstituentMasterId_Source { get; set; }
        public string ConstituentMasterId_Category { get; set; }
        public int? ConstituentMasterId_AttributeId { get; set; }
        #endregion


        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public int? FirstName_AttributeId { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public int? LastName_AttributeId { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_Source { get; set; }
        public string UAPersonId_Category { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        public string Address_Status { get; set; }
        public string Address_Source { get; set; }
        public string Address_Category { get; set; }
        public int? Address_AttributeId { get; set; }
        #endregion

        #region AddressMasterId
        public string AddressMasterId { get; set; }
        public string AddressMasterId_BusinessName { get; set; }
        public string AddressMasterId_BusinessDescription { get; set; }
        public string AddressMasterId_Status { get; set; }
        public string AddressMasterId_Source { get; set; }
        public string AddressMasterId_Category { get; set; }
        public int? AddressMasterId_AttributeId { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public string City_Status { get; set; }
        public string City_Source { get; set; }
        public string City_Category { get; set; }
        public int? City_AttributeId { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public string PostalCode_Status { get; set; }
        public string PostalCode_Source { get; set; }
        public string PostalCode_Category { get; set; }
        public int? PostalCode_AttributeId { get; set; }
        #endregion

        #region DeliveryPointCode
        public string DeliveryPointCode { get; set; }
        public string DeliveryPointCode_BusinessName { get; set; }
        public string DeliveryPointCode_BusinessDescription { get; set; }
        public string DeliveryPointCode_Status { get; set; }
        public string DeliveryPointCode_Source { get; set; }
        public string DeliveryPointCode_Category { get; set; }
        public int? DeliveryPointCode_AttributeId { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public string State_Status { get; set; }
        public string State_Source { get; set; }
        public string State_Category { get; set; }
        public int? State_AttributeId { get; set; }
        #endregion

        #region StateSourceSystemRecordId
        public string StateSourceSystemRecordId { get; set; }
        public string StateSourceSystemRecordId_BusinessName { get; set; }
        public string StateSourceSystemRecordId_BusinessDescription { get; set; }
        public string StateSourceSystemRecordId_Status { get; set; }
        public string StateSourceSystemRecordId_Source { get; set; }
        public string StateSourceSystemRecordId_Category { get; set; }
        public int? StateSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region StateMasterId
        public string StateMasterId { get; set; }
        public string StateMasterId_BusinessName { get; set; }
        public string StateMasterId_BusinessDescription { get; set; }
        public string StateMasterId_Status { get; set; }
        public string StateMasterId_Source { get; set; }
        public string StateMasterId_Category { get; set; }
        public int? StateMasterId_AttributeId { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public string Country_Status { get; set; }
        public string Country_Source { get; set; }
        public string Country_Category { get; set; }
        public int? Country_AttributeId { get; set; }
        #endregion

        #region CountrySourceSystemRecordId
        public string CountrySourceSystemRecordId { get; set; }
        public string CountrySourceSystemRecordId_BusinessName { get; set; }
        public string CountrySourceSystemRecordId_BusinessDescription { get; set; }
        public string CountrySourceSystemRecordId_Status { get; set; }
        public string CountrySourceSystemRecordId_Source { get; set; }
        public string CountrySourceSystemRecordId_Category { get; set; }
        public int? CountrySourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region CountryMasterId
        public string CountryMasterId { get; set; }
        public string CountryMasterId_BusinessName { get; set; }
        public string CountryMasterId_BusinessDescription { get; set; }
        public string CountryMasterId_Status { get; set; }
        public string CountryMasterId_Source { get; set; }
        public string CountryMasterId_Category { get; set; }
        public int? CountryMasterId_AttributeId { get; set; }
        #endregion

        #region AddressUseType
        public string AddressUseType { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        public string AddressUseType_Status { get; set; }
        public string AddressUseType_Source { get; set; }
        public string AddressUseType_Category { get; set; }
        public int? AddressUseType_AttributeId { get; set; }
        #endregion

        #region AddressUseTypeSourceSystemRecordId
        public string AddressUseTypeSourceSystemRecordId { get; set; }
        public string AddressUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string AddressUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Source { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Category { get; set; }
        public int? AddressUseTypeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AddressUseTypeMasterId
        public string AddressUseTypeMasterId { get; set; }
        public string AddressUseTypeMasterId_BusinessName { get; set; }
        public string AddressUseTypeMasterId_BusinessDescription { get; set; }
        public string AddressUseTypeMasterId_Status { get; set; }
        public string AddressUseTypeMasterId_Source { get; set; }
        public string AddressUseTypeMasterId_Category { get; set; }
        public int? AddressUseTypeMasterId_AttributeId { get; set; }
        #endregion

        #region AddressIsPrimary
        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        public string AddressIsPrimary_Status { get; set; }
        public string AddressIsPrimary_Source { get; set; }
        public string AddressIsPrimary_Category { get; set; }
        public int? AddressIsPrimary_AttributeId { get; set; }
        #endregion


    }
}
