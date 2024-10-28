using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;

namespace Sentry.WebApp.ViewModels
{
    public class ConstituentHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        public string FirstName { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_OriginalValue { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_Status { get; set; }
        public string PreferredName_OriginalValue { get; set; }

        public string MiddleName { get; set; }
        public string MiddleName_Status { get; set; }
        public string MiddleName_OriginalValue { get; set; }

        public string LastName { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_OriginalValue { get; set; }

        public string MaidenName { get; set; }
        public string MaidenName_Status { get; set; }
        public string MaidenName_OriginalValue { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_Status { get; set; }
        public string UAPersonId_OriginalValue { get; set; }

        public string ConstituentTitle { get; set; }
        public string ConstituentTitle_Status { get; set; }
        public string ConstituentTitle_OriginalValue { get; set; }

        public string TitleSourceSystemRecordId { get; set; }
        public string TitleSourceSystemRecordId_Status { get; set; }
        public string TitleSourceSystemRecordId_OriginalValue { get; set; }

        public string TitleMasterId { get; set; }
        public string TitleMasterId_Status { get; set; }
        public string TitleMasterId_OriginalValue { get; set; }

        public string Suffix { get; set; }
        public string Suffix_Status { get; set; }
        public string Suffix_OriginalValue { get; set; }

        public string SuffixSourceSystemRecordId { get; set; }
        public string SuffixSourceSystemRecordId_Status { get; set; }
        public string SuffixSourceSystemRecordId_OriginalValue { get; set; }

        public string SuffixMasterId { get; set; }
        public string SuffixMasterId_Status { get; set; }
        public string SuffixMasterId_OriginalValue { get; set; }

        public string BirthDate { get; set; }
        public string BirthDate_Status { get; set; }
        public string BirthDate_OriginalValue { get; set; }
        
        public string DeceasedDate { get; set; }
        public string DeceasedDate_Status { get; set; }
        public string DeceasedDate_OriginalValue { get; set; }

        public string MaritalStatus { get; set; }
        public string MaritalStatus_Status { get; set; }
        public string MaritalStatus_OriginalValue { get; set; }

        public string MaritalStatusSourceSystemRecordId { get; set; }
        public string MaritalStatusSourceSystemRecordId_Status { get; set; }
        public string MaritalStatusSourceSystemRecordId_OriginalValue { get; set; }

        public string MaritalStatusMasterId { get; set; }
        public string MaritalStatusMasterId_Status { get; set; }
        public string MaritalStatusMasterId_OriginalValue { get; set; }
       

        public string Address { get; set; }
        public string Address_Status { get; set; }
        public string Address_OriginalValue { get; set; }

        public string AddressMasterId { get; set; }
        public string AddressMasterId_Status { get; set; }
        public string AddressMasterId_OriginalValue { get; set; }

        public string City { get; set; }
        public string City_Status { get; set; }
        public string City_OriginalValue { get; set; }

        public string PostalCode { get; set; }
        public string PostalCode_Status { get; set; }
        public string PostalCode_OriginalValue { get; set; }

        public string State { get; set; }
        public string State_Status { get; set; }
        public string State_OriginalValue { get; set; }

        public string StateSourceSystemRecordId { get; set; }
        public string StateSourceSystemRecordId_Status { get; set; }
        public string StateSourceSystemRecordId_OriginalValue { get; set; }

        public string StateMasterId { get; set; }
        public string StateMasterId_Status { get; set; }
        public string StateMasterId_OriginalValue { get; set; }

        public string Country { get; set; }
        public string Country_Status { get; set; }
        public string Country_OriginalValue { get; set; }

        public string CountrySourceSystemRecordId { get; set; }
        public string CountrySourceSystemRecordId_Status { get; set; }
        public string CountrySourceSystemRecordId_OriginalValue { get; set; }

        public string CountryMasterId { get; set; }
        public string CountryMasterId_Status { get; set; }
        public string CountryMasterId_OriginalValue { get; set; }

        public string AddressUseType { get; set; }
        public string AddressUseType_Status { get; set; }
        public string AddressUseType_OriginalValue { get; set; }

        public string AddressUseTypeSourceSystemRecordId { get; set; }
        public string AddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string AddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string AddressUseTypeMasterId { get; set; }
        public string AddressUseTypeMasterId_Status { get; set; }
        public string AddressUseTypeMasterId_OriginalValue { get; set; }

        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimary_Status { get; set; }
        public string AddressIsPrimary_OriginalValue { get; set; }

        public string EmailAddress { get; set; }
        public string EmailAddress_Status { get; set; }
        public string EmailAddress_OriginalValue { get; set; }

        public string EmailAddressMasterId { get; set; }
        public string EmailAddressMasterId_Status { get; set; }
        public string EmailAddressMasterId_OriginalValue { get; set; }

        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_Status { get; set; }
        public string EmailAddressUseType_OriginalValue { get; set; }

        public string EmailAddressUseTypeSourceSystemRecordId { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_Status { get; set; }
        public string EmailAddressUseTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string EmailAddressUseTypeMasterId { get; set; }
        public string EmailAddressUseTypeMasterId_Status { get; set; }
        public string EmailAddressUseTypeMasterId_OriginalValue { get; set; }

        public bool? EmailIsPrimary { get; set; }
        public string EmailIsPrimary_Status { get; set; }
        public string EmailIsPrimary_OriginalValue { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneNumber_Status { get; set; }
        public string PhoneNumber_OriginalValue { get; set; }

        public string PhoneExtension { get; set; }
        public string PhoneExtension_Status { get; set; }
        public string PhoneExtension_OriginalValue { get; set; }

        public string PhoneCountryCode { get; set; }
        public string PhoneCountryCode_Status { get; set; }
        public string PhoneCountryCode_OriginalValue { get; set; }

        public string PhoneCountrySourceSystemRecordId { get; set; }
        public string PhoneCountrySourceSystemRecordId_Status { get; set; }
        public string PhoneCountrySourceSystemRecordId_OriginalValue { get; set; }

        public string PhoneCountryMasterId { get; set; }
        public string PhoneCountryMasterId_Status { get; set; }
        public string PhoneCountryMasterId_OriginalValue { get; set; }

        public string PhoneLineType { get; set; }
        public string PhoneLineType_Status { get; set; }
        public string PhoneLineType_OriginalValue { get; set; }

        public string PhoneLineTypeSourceSystemRecordId { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneLineTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string PhoneLineTypeMasterId { get; set; }
        public string PhoneLineTypeMasterId_Status { get; set; }
        public string PhoneLineTypeMasterId_OriginalValue { get; set; }

        public string PhoneUseType { get; set; }
        public string PhoneUseType_Status { get; set; }
        public string PhoneUseType_OriginalValue { get; set; }

        public string PhoneUseTypeSourceSystemRecordId { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_Status { get; set; }
        public string PhoneUseTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string PhoneUseTypeMasterId { get; set; }
        public string PhoneUseTypeMasterId_Status { get; set; }
        public string PhoneUseTypeMasterId_OriginalValue { get; set; }

        public string PhoneMasterId { get; set; }
        public string PhoneMasterId_Status { get; set; }
        public string PhoneMasterId_OriginalValue { get; set; }

        public bool? PhoneIsPrimary { get; set; }
        public string PhoneIsPrimary_Status { get; set; }
        public string PhoneIsPrimary_OriginalValue { get; set; }
    }
}
