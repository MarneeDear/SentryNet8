using Microsoft.Extensions.Configuration;
using System;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentMatchViewModel : BaseIntegrationViewModel
    {
        public ConstituentMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        #region FirstName
        public string FirstName { get; set; }
        public int FirstName_Weight { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public int PreferredName_Weight { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public int? MiddleName_Weight { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public int? LastName_Weight { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public int? MaidenName_Weight { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public int? UAPersonId_Weight { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        #endregion

        #region ConstituentTitle
        public string ConstituentTitle { get; set; }
        public int? ConstituentTitle_Weight { get; set; }
        public string ConstituentTitle_BusinessName { get; set; }
        public string ConstituentTitle_BusinessDescription { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public int? Suffix_Weight { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public int? BirthDate_Weight { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public int? DeceasedDate_Weight { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        #endregion       

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public int? MaritalStatus_Weight { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }
        public int? Address_Weight { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public int? City_Weight { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }
        public int? PostalCode_Weight { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public int? State_Weight { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public int? Country_Weight { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        #endregion

        #region AddressUseType
        public string AddressUseType { get; set; }
        public int? AddressUseType_Weight { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        #endregion

        #region AddressIsPrimary
        public bool? AddressIsPrimary { get; set; }
        public int? AddressIsPrimary_Weight { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress
        public string EmailAddress { get; set; }
        public int? EmailAddress_Weight { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        #endregion

        #region EmailAddressUseType
        public string EmailAddressUseType { get; set; }
        public int? EmailAddressUseType_Weight { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        #endregion

        #region EmailIsPrimary
        public bool? EmailIsPrimary { get; set; }
        public int? EmailIsPrimary_Weight { get; set; }
        public string EmailIsPrimary_BusinessName { get; set; }
        public string EmailIsPrimary_BusinessDescription { get; set; }
        #endregion

        #region PhoneNumber
        public string PhoneNumber { get; set; }
        public int? PhoneNumber_Weight { get; set; }
        public string PhoneNumber_BusinessName { get; set; }
        public string PhoneNumber_BusinessDescription { get; set; }
        #endregion

        #region PhoneExtension
        public string PhoneExtension { get; set; }
        public int? PhoneExtension_Weight { get; set; }
        public string PhoneExtension_BusinessName { get; set; }
        public string PhoneExtension_BusinessDescription { get; set; }
        #endregion

        #region PhoneCountryCode
        public string PhoneCountryCode { get; set; }
        public int? PhoneCountryCode_Weight { get; set; }
        public string PhoneCountryCode_BusinessName { get; set; }
        public string PhoneCountryCode_BusinessDescription { get; set; }
        #endregion

        #region PhoneLineType
        public string PhoneLineType { get; set; }
        public int? PhoneLineType_Weight { get; set; }
        public string PhoneLineType_BusinessName { get; set; }
        public string PhoneLineType_BusinessDescription { get; set; }
        #endregion

        #region PhoneUseType
        public string PhoneUseType { get; set; }
        public int? PhoneUseType_Weight { get; set; }
        public string PhoneUseType_BusinessName { get; set; }
        public string PhoneUseType_BusinessDescription { get; set; }
        #endregion

        #region PhoneIsPrimary
        public bool? PhoneIsPrimary { get; set; }
        public int? PhoneIsPrimary_Weight { get; set; }
        public string PhoneIsPrimary_BusinessName { get; set; }
        public string PhoneIsPrimary_BusinessDescription { get; set; }
        #endregion


        public class ConstituentMatchSummaryViewModel
        {
            public bool Selected { get; set; }

            public int MatchConfidence { get; set; }

            public string MasterId { get; set; }

            public string FirstName { get; set; }

            public string PreferredName { get; set; }

            public string MiddleName { get; set; }

            public string LastName { get; set; }

            public string MaidenName { get; set; }

            public string UAPersonId { get; set; }

            public string BirthDate { get; set; }
        }
    }
}