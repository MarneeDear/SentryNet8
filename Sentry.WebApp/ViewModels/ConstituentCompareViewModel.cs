using System.Collections.Generic;
using Sentry.WebApp.Data.Models;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentCompareViewModel : BaseDetail
    {
        public string MasterId { get; set; }
        public string System { get; set; }

        public bool AllowMatch { get; set; }

        public bool AllowMerge { get; set; }

        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_Compare { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public bool FirstName_IsDifferent { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_Compare { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public bool PreferredName_IsDifferent { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_Compare { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public bool MiddleName_IsDifferent { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_Compare { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IsDifferent { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_Compare { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public bool MaidenName_IsDifferent { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_Compare { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public bool UAPersonId_IsDifferent { get; set; }
        #endregion

        #region ConstituentTitle
        public string ConstituentTitle { get; set; }
        public string ConstituentTitle_Compare { get; set; }
        public string ConstituentTitle_BusinessName { get; set; }
        public string ConstituentTitle_BusinessDescription { get; set; }
        public bool ConstituentTitle_IsDifferent { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_Compare { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IsDifferent { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public string BirthDate_Compare { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public bool BirthDate_IsDifferent { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_Compare { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public bool DeceasedDate_IsDifferent { get; set; }
        #endregion       

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_Compare { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public bool MaritalStatus_IsDifferent { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public string Address_Compare { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        public bool Address_IsDifferent { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public string City_Compare { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public bool City_IsDifferent { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }
        public string PostalCode_Compare { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public bool PostalCode_IsDifferent { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public string State_Compare { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public bool State_IsDifferent { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public string Country_Compare { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public bool Country_IsDifferent { get; set; }
        #endregion

        #region AddressUseType
        public string AddressUseType { get; set; }
        public string AddressUseType_Compare { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        public bool AddressUseType_IsDifferent { get; set; }
        #endregion

        #region AddressIsPrimary
        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimaryDisplay { get; set; }
        public bool? AddressIsPrimary_Compare { get; set; }
        public string AddressIsPrimaryDisplay_Compare { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        public bool AddressIsPrimary_IsDifferent { get; set; }
        #endregion

        #region EmailAddress
        public string EmailAddress { get; set; }
        public string EmailAddress_Compare { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        public bool EmailAddress_IsDifferent { get; set; }
        #endregion

        #region EmailAddressUseType
        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_Compare { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        public bool EmailAddressUseType_IsDifferent { get; set; }
        #endregion

        #region EmailIsPrimary
        public bool? EmailIsPrimary { get; set; }
        public string EmailIsPrimaryDisplay { get; set; }
        public bool? EmailIsPrimary_Compare { get; set; }
        public string EmailIsPrimaryDisplay_Compare { get; set; }
        public string EmailIsPrimary_BusinessName { get; set; }
        public string EmailIsPrimary_BusinessDescription { get; set; }
        public bool EmailIsPrimary_IsDifferent { get; set; }
        #endregion

        #region PhoneNumber
        public string PhoneNumber { get; set; }
        public string PhoneNumber_Compare { get; set; }
        public string PhoneNumber_BusinessName { get; set; }
        public string PhoneNumber_BusinessDescription { get; set; }
        public bool PhoneNumber_IsDifferent { get; set; }
        #endregion

        #region PhoneExtension
        public string PhoneExtension { get; set; }
        public string PhoneExtension_Compare { get; set; }
        public string PhoneExtension_BusinessName { get; set; }
        public string PhoneExtension_BusinessDescription { get; set; }
        public bool PhoneExtension_IsDifferent { get; set; }
        #endregion

        #region PhoneCountryCode
        public string PhoneCountryCode { get; set; }
        public string PhoneCountryCode_Compare { get; set; }
        public string PhoneCountryCode_BusinessName { get; set; }
        public string PhoneCountryCode_BusinessDescription { get; set; }
        public bool PhoneCountryCode_IsDifferent { get; set; }
        #endregion

        #region PhoneLineType
        public string PhoneLineType { get; set; }
        public string PhoneLineType_Compare { get; set; }
        public string PhoneLineType_BusinessName { get; set; }
        public string PhoneLineType_BusinessDescription { get; set; }
        public bool PhoneLineType_IsDifferent { get; set; }
        #endregion

        #region PhoneUseType
        public string PhoneUseType { get; set; }
        public string PhoneUseType_Compare { get; set; }
        public string PhoneUseType_BusinessName { get; set; }
        public string PhoneUseType_BusinessDescription { get; set; }
        public bool PhoneUseType_IsDifferent { get; set; }
        #endregion

        #region PhoneIsPrimary
        public bool? PhoneIsPrimary { get; set; }
        public string PhoneIsPrimaryDisplay { get; set; }
        public bool? PhoneIsPrimary_Compare { get; set; }
        public string PhoneIsPrimaryDisplay_Compare { get; set; }
        public string PhoneIsPrimary_BusinessName { get; set; }
        public string PhoneIsPrimary_BusinessDescription { get; set; }
        public bool PhoneIsPrimary_IsDifferent { get; set; }
        #endregion

        public List<SystemRecord> SystemRecords { get; set; }
    }
}
