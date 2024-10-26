using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class ConstituentComparisonDetail : BaseDetail
    {
        public string MasterRecordId { get; set; }
        public bool AllowMatch { get; set; }
        public bool AllowMerge { get; set; }

        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Compare { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Compare { get; set; }

        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public string MiddleName_Compare { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Compare { get; set; }

        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public string MaidenName_Compare { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Compare { get; set; }
        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Compare { get; set; }

        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public string BirthDate_Compare { get; set; }

        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public string DeceasedDate_Compare { get; set; }

        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public string MaritalStatus_Compare { get; set; }

        public string Address { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        public string Address_Compare { get; set; }

        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public string City_Compare { get; set; }

        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public string State_Compare { get; set; }

        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public string PostalCode_Compare { get; set; }

        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public string Country_Compare { get; set; }

        public string AddressUseType { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        public string AddressUseType_Compare { get; set; }

        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        public bool? AddressIsPrimary_Compare { get; set; }

        public string EmailAddress { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        public string EmailAddress_Compare { get; set; }

        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        public string EmailAddressUseType_Compare { get; set; }

        public bool? EmailIsPrimary { get; set; }
        public string EmailIsPrimary_BusinessName { get; set; }
        public string EmailIsPrimary_BusinessDescription { get; set; }
        public bool? EmailIsPrimary_Compare { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneNumber_BusinessName { get; set; }
        public string PhoneNumber_BusinessDescription { get; set; }
        public string PhoneNumber_Compare { get; set; }

        public string PhoneExtension { get; set; }
        public string PhoneExtension_BusinessName { get; set; }
        public string PhoneExtension_BusinessDescription { get; set; }
        public string PhoneExtension_Compare { get; set; }

        public string PhoneCountryCode { get; set; }
        public string PhoneCountryCode_BusinessName { get; set; }
        public string PhoneCountryCode_BusinessDescription { get; set; }
        public string PhoneCountryCode_Compare { get; set; }

        public string PhoneUseType { get; set; }
        public string PhoneUseType_BusinessName { get; set; }
        public string PhoneUseType_BusinessDescription { get; set; }
        public string PhoneUseType_Compare { get; set; }

        public bool? PhoneIsPrimary { get; set; }
        public string PhoneIsPrimary_BusinessName { get; set; }
        public string PhoneIsPrimary_BusinessDescription { get; set; }
        public bool? PhoneIsPrimary_Compare { get; set; }


        [NotMapped]
        [ForeignKey("MasterRecordCode")]
        public ICollection<SystemRecord> SystemRecords { get; set; }

    }
}
