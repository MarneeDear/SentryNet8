using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{

    [Table("Constituents_Detail", Schema = "Integration")]
    public class ConstituentDetail : BaseDetail
    {
        //No key here because it is in the BasseDetail
        #region Name

        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public int? FirstName_AttributeId { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Status { get; set; }
        public string PreferredName_Source { get; set; }
        public string PreferredName_Category { get; set; }
        public int? PreferredName_AttributeId { get; set; }

        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public string MiddleName_Status { get; set; }
        public string MiddleName_Source { get; set; }
        public string MiddleName_Category { get; set; }
        public int? MiddleName_AttributeId { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public int? LastName_AttributeId { get; set; }

        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public string MaidenName_Status { get; set; }
        public string MaidenName_Source { get; set; }
        public string MaidenName_Category { get; set; }
        public int? MaidenName_AttributeId { get; set; }

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


        #region Suffix

        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Status { get; set; }
        public string Suffix_Source { get; set; }
        public string Suffix_Category { get; set; }
        public int? Suffix_AttributeId { get; set; }

        #endregion

        #region SuffixSourceSystemRecordId

        public string SuffixSourceSystemRecordId { get; set; }
        public string SuffixSourceSystemRecordId_BusinessName { get; set; }
        public string SuffixSourceSystemRecordId_BusinessDescription { get; set; }
        public string SuffixSourceSystemRecordId_Status { get; set; }
        public string SuffixSourceSystemRecordId_Source { get; set; }
        public string SuffixSourceSystemRecordId_Category { get; set; }
        public int? SuffixSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region SuffixMasterId

        public string SuffixMasterId { get; set; }
        public string SuffixMasterId_BusinessName { get; set; }
        public string SuffixMasterId_BusinessDescription { get; set; }
        public string SuffixMasterId_Status { get; set; }
        public string SuffixMasterId_Source { get; set; }
        public string SuffixMasterId_Category { get; set; }
        public int? SuffixMasterId_AttributeId { get; set; }

        #endregion

        #region BirthDate

        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public string BirthDate_Status { get; set; }
        public string BirthDate_Source { get; set; }
        public string BirthDate_Category { get; set; }
        public int? BirthDate_AttributeId { get; set; }

        #endregion

        #region DeceasedDate

        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public string DeceasedDate_Status { get; set; }
        public string DeceasedDate_Source { get; set; }
        public string DeceasedDate_Category { get; set; }
        public int? DeceasedDate_AttributeId { get; set; }

        #endregion

        #region MaritalStatus

        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public string MaritalStatus_Status { get; set; }
        public string MaritalStatus_Source { get; set; }
        public string MaritalStatus_Category { get; set; }
        public int? MaritalStatus_AttributeId { get; set; }

        #endregion

        #region MaritalStatusSourceSystemRecordId

        public string MaritalStatusSourceSystemRecordId { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessName { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public string MaritalStatusSourceSystemRecordId_Status { get; set; }
        public string MaritalStatusSourceSystemRecordId_Source { get; set; }
        public string MaritalStatusSourceSystemRecordId_Category { get; set; }
        public int? MaritalStatusSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region MaritalStatusMasterId

        public string MaritalStatusMasterId { get; set; }
        public string MaritalStatusMasterId_BusinessName { get; set; }
        public string MaritalStatusMasterId_BusinessDescription { get; set; }
        public string MaritalStatusMasterId_Status { get; set; }
        public string MaritalStatusMasterId_Source { get; set; }
        public string MaritalStatusMasterId_Category { get; set; }
        public int? MaritalStatusMasterId_AttributeId { get; set; }

        #endregion

        #region Address

        public string Address { get; set; }

        public string Address_BusinessName { get; set; }

        public string Address_BusinessDescription { get; set; }

        public string Address_Source { get; set; }

        public string Address_Category { get; set; }

        public string Address_Status { get; set; }

        public int? Address_AttributeId { get; set; }

        #endregion

        #region AddressSourceSystemRecordId
        public string AddressSourceSystemRecordId { get; set; }

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

        public string City_Source { get; set; }

        public string City_Category { get; set; }

        public string City_Status { get; set; }

        public int? City_AttributeId { get; set; }

        #endregion

        #region PostalCode

        public string PostalCode { get; set; }

        public string PostalCode_BusinessName { get; set; }

        public string PostalCode_BusinessDescription { get; set; }

        public string PostalCode_Source { get; set; }

        public string PostalCode_Category { get; set; }

        public string PostalCode_Status { get; set; }

        public int? PostalCode_AttributeId { get; set; }

        #endregion

        #region State

        public string State { get; set; }

        public string State_BusinessName { get; set; }

        public string State_BusinessDescription { get; set; }

        public string State_Source { get; set; }

        public string State_Category { get; set; }

        public string State_Status { get; set; }

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

        public string Country_Source { get; set; }

        public string Country_Category { get; set; }

        public string Country_Status { get; set; }

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

        public string AddressUseType_Source { get; set; }

        public string AddressUseType_Category { get; set; }

        public string AddressUseType_Status { get; set; }

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

        public string AddressIsPrimary_Source { get; set; }

        public string AddressIsPrimary_Category { get; set; }

        public string AddressIsPrimary_Status { get; set; }

        public int? AddressIsPrimary_AttributeId { get; set; }

        #endregion

        #region EmailAddress

        public string EmailAddress { get; set; }

        public string EmailAddress_BusinessName { get; set; }

        public string EmailAddress_BusinessDescription { get; set; }

        public string EmailAddress_Source { get; set; }

        public string EmailAddress_Category { get; set; }

        public string EmailAddress_Status { get; set; }

        public int? EmailAddress_AttributeId { get; set; }

        #endregion

        #region EmailAddressSourceSystemRecordId 
        public string EmailAddressSourceSystemRecordId { get; set; }

        #endregion


        #region EmailAddressMasterId

        public string EmailAddressMasterId { get; set; }
        public string EmailAddressMasterId_BusinessName { get; set; }
        public string EmailAddressMasterId_BusinessDescription { get; set; }
        public string EmailAddressMasterId_Status { get; set; }
        public string EmailAddressMasterId_Source { get; set; }
        public string EmailAddressMasterId_Category { get; set; }
        public int? EmailAddressMasterId_AttributeId { get; set; }

        #endregion

        #region EmailAddressUseType

        public string EmailAddressUseType { get; set; }

        public string EmailAddressUseType_BusinessName { get; set; }

        public string EmailAddressUseType_BusinessDescription { get; set; }

        public string EmailAddressUseType_Source { get; set; }

        public string EmailAddressUseType_Category { get; set; }

        public string EmailAddressUseType_Status { get; set; }

        public int? EmailAddressUseType_AttributeId { get; set; }

        #endregion

        #region EmailAddressUseTypeSourceSystemRecordId

        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Source { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Category { get; set; }
        public int? EmailAddressUseTypeSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region EmailAddressUseTypeMasterId

        public string EmailAddressUseTypeMasterId { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessName { get; set; }
        public string EmailAddressUseTypeMasterId_BusinessDescription { get; set; }
        public string EmailAddressUseTypeMasterId_Status { get; set; }
        public string EmailAddressUseTypeMasterId_Source { get; set; }
        public string EmailAddressUseTypeMasterId_Category { get; set; }
        public int? EmailAddressUseTypeMasterId_AttributeId { get; set; }

        #endregion

        #region EmailIsPrimary

        public bool? EmailIsPrimary { get; set; }

        public string EmailIsPrimary_BusinessName { get; set; }

        public string EmailIsPrimary_BusinessDescription { get; set; }

        public string EmailIsPrimary_Source { get; set; }

        public string EmailIsPrimary_Category { get; set; }

        public string EmailIsPrimary_Status { get; set; }

        public int? EmailIsPrimary_AttributeId { get; set; }

        #endregion

        #region PhoneNumber

        public string PhoneNumber { get; set; }

        public string PhoneNumber_BusinessName { get; set; }

        public string PhoneNumber_BusinessDescription { get; set; }

        public string PhoneNumber_Source { get; set; }

        public string PhoneNumber_Category { get; set; }

        public string PhoneNumber_Status { get; set; }

        public int? PhoneNumber_AttributeId { get; set; }

        #endregion

        #region PhoneNumberSourceSystemRecordId 
        public string PhoneNumberSourceSystemRecordId { get; set; }

        #endregion


        #region PhoneExtension

        public string PhoneExtension { get; set; }

        public string PhoneExtension_BusinessName { get; set; }

        public string PhoneExtension_BusinessDescription { get; set; }

        public string PhoneExtension_Source { get; set; }

        public string PhoneExtension_Category { get; set; }

        public string PhoneExtension_Status { get; set; }

        public int? PhoneExtension_AttributeId { get; set; }

        #endregion

        #region PhoneCountryCode

        public string PhoneCountryCode { get; set; }

        public string PhoneCountryCode_BusinessName { get; set; }

        public string PhoneCountryCode_BusinessDescription { get; set; }

        public string PhoneCountryCode_Source { get; set; }

        public string PhoneCountryCode_Category { get; set; }

        public string PhoneCountryCode_Status { get; set; }

        public int? PhoneCountryCode_AttributeId { get; set; }

        #endregion

        #region PhoneCountrySourceSystemRecordId

        public string PhoneCountrySourceSystemRecordId { get; set; }
        public string PhoneCountrySourceSystemRecordId_BusinessName { get; set; }
        public string PhoneCountrySourceSystemRecordId_BusinessDescription { get; set; }
        public string PhoneCountrySourceSystemRecordId_Status { get; set; }
        public string PhoneCountrySourceSystemRecordId_Source { get; set; }
        public string PhoneCountrySourceSystemRecordId_Category { get; set; }
        public int? PhoneCountrySourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region PhoneCountryMasterId

        public string PhoneCountryMasterRecordId { get; set; }
        public string PhoneCountryMasterRecordId_BusinessName { get; set; }
        public string PhoneCountryMasterRecordId_BusinessDescription { get; set; }
        public string PhoneCountryMasterRecordId_Status { get; set; }
        public string PhoneCountryMasterRecordId_Source { get; set; }
        public string PhoneCountryMasterRecordId_Category { get; set; }
        public int? PhoneCountryMasterRecordId_AttributeId { get; set; }

        #endregion

        #region PhoneLineType

        public string PhoneLineType { get; set; }

        public string PhoneLineType_BusinessName { get; set; }

        public string PhoneLineType_BusinessDescription { get; set; }

        public string PhoneLineType_Source { get; set; }

        public string PhoneLineType_Category { get; set; }

        public string PhoneLineType_Status { get; set; }

        public int? PhoneLineType_AttributeId { get; set; }

        #endregion

        #region PhoneLineTypeSourceSystemRecordId

        public string PhoneLineTypeSourceSystemRecordId { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_BusinessName { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Source { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Category { get; set; }
        public int? PhoneLineTypeSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region PhoneLineTypeMasterRecordId

        public string PhoneLineTypeMasterRecordId { get; set; }
        public string PhoneLineTypeMasterRecordId_BusinessName { get; set; }
        public string PhoneLineTypeMasterRecordId_BusinessDescription { get; set; }
        public string PhoneLineTypeMasterRecordId_Status { get; set; }
        public string PhoneLineTypeMasterRecordId_Source { get; set; }
        public string PhoneLineTypeMasterRecordId_Category { get; set; }
        public int? PhoneLineTypeMasterRecordId_AttributeId { get; set; }

        #endregion

        #region PhoneUseType

        public string PhoneUseType { get; set; }

        public string PhoneUseType_BusinessName { get; set; }

        public string PhoneUseType_BusinessDescription { get; set; }

        public string PhoneUseType_Source { get; set; }

        public string PhoneUseType_Category { get; set; }

        public string PhoneUseType_Status { get; set; }

        public int? PhoneUseType_AttributeId { get; set; }

        #endregion

        #region PhoneUseTypeSourceSystemRecordId

        public string PhoneUseTypeSourceSystemRecordId { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_BusinessName { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Source { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Category { get; set; }
        public int? PhoneUseTypeSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region PhoneUseTypeMasterId

        public string PhoneUseTypeMasterId { get; set; }
        public string PhoneUseTypeMasterId_BusinessName { get; set; }
        public string PhoneUseTypeMasterId_BusinessDescription { get; set; }
        public string PhoneUseTypeMasterId_Status { get; set; }
        public string PhoneUseTypeMasterId_Source { get; set; }
        public string PhoneUseTypeMasterId_Category { get; set; }
        public int? PhoneUseTypeMasterId_AttributeId { get; set; }

        #endregion

        #region PhoneMasterId

        public string PhoneMasterId { get; set; }
        public string PhoneMasterId_BusinessName { get; set; }
        public string PhoneMasterId_BusinessDescription { get; set; }
        public string PhoneMasterId_Status { get; set; }
        public string PhoneMasterId_Source { get; set; }
        public string PhoneMasterId_Category { get; set; }
        public int? PhoneMasterId_AttributeId { get; set; }

        #endregion

        #region PhoneIsPrimary

        public bool? PhoneIsPrimary { get; set; }

        public string PhoneIsPrimary_BusinessName { get; set; }

        public string PhoneIsPrimary_BusinessDescription { get; set; }

        public string PhoneIsPrimary_Source { get; set; }

        public string PhoneIsPrimary_Category { get; set; }

        public string PhoneIsPrimary_Status { get; set; }

        public int? PhoneIsPrimary_AttributeId { get; set; }

        #endregion

    }
}
