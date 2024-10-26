
namespace Sentry.WebApp.Data.Models
{
    public class ConstituentMatchDetail : BaseDetail
    {
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public bool FirstName_IncludeInMatch { get; set; }
        public int FirstName_MatchWeight { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public bool PreferredName_IncludeInMatch { get; set; }
        public int PreferredName_MatchWeight { get; set; }

        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public bool MiddleName_IncludeInMatch { get; set; }
        public int MiddleName_MatchWeight { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IncludeInMatch { get; set; }
        public int LastName_MatchWeight { get; set; }

        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public bool MaidenName_IncludeInMatch { get; set; }
        public int MaidenName_MatchWeight { get; set; }

        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public bool UAPersonId_IncludeInMatch { get; set; }
        public int UAPersonId_MatchWeight { get; set; }
       

        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IncludeInMatch { get; set; }
        public int Suffix_MatchWeight { get; set; }

        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public bool BirthDate_IncludeInMatch { get; set; }
        public int BirthDate_MatchWeight { get; set; }

        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public bool DeceasedDate_IncludeInMatch { get; set; }
        public int DeceasedDate_MatchWeight { get; set; }

        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public bool MaritalStatus_IncludeInMatch { get; set; }
        public int MaritalStatus_MatchWeight { get; set; }

        public string Address { get; set; }
        public string Address_BusinessName { get; set; }
        public string Address_BusinessDescription { get; set; }
        public bool Address_IncludeInMatch { get; set; }
        public int Address_MatchWeight { get; set; }

        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public bool City_IncludeInMatch { get; set; }
        public int City_MatchWeight { get; set; }

        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public bool PostalCode_IncludeInMatch { get; set; }
        public int PostalCode_MatchWeight { get; set; }

        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public bool State_IncludeInMatch { get; set; }
        public int State_MatchWeight { get; set; }

        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public bool Country_IncludeInMatch { get; set; }
        public int Country_MatchWeight { get; set; }


        public string AddressUseType { get; set; }
        public string AddressUseType_BusinessName { get; set; }
        public string AddressUseType_BusinessDescription { get; set; }
        public bool AddressUseType_IncludeInMatch { get; set; }
        public int AddressUseType_MatchWeight { get; set; }

        public bool? AddressIsPrimary { get; set; }
        public string AddressIsPrimary_BusinessName { get; set; }
        public string AddressIsPrimary_BusinessDescription { get; set; }
        public bool AddressIsPrimary_IncludeInMatch { get; set; }
        public int AddressIsPrimary_MatchWeight { get; set; }

        public string EmailAddress { get; set; }
        public string EmailAddress_BusinessName { get; set; }
        public string EmailAddress_BusinessDescription { get; set; }
        public bool EmailAddress_IncludeInMatch { get; set; }
        public int EmailAddress_MatchWeight { get; set; }

        public string EmailAddressUseType { get; set; }
        public string EmailAddressUseType_BusinessName { get; set; }
        public string EmailAddressUseType_BusinessDescription { get; set; }
        public bool EmailAddressUseType_IncludeInMatch { get; set; }
        public int EmailAddressUseType_MatchWeight { get; set; }

        public bool? EmailIsPrimary { get; set; }
        public string EmailIsPrimary_BusinessName { get; set; }
        public string EmailIsPrimary_BusinessDescription { get; set; }
        public bool EmailIsPrimary_IncludeInMatch { get; set; }
        public int EmailIsPrimary_MatchWeight { get; set; }

        public string PhoneNumber { get; set; }
        public string PhoneNumber_BusinessName { get; set; }
        public string PhoneNumber_BusinessDescription { get; set; }
        public bool PhoneNumber_IncludeInMatch { get; set; }
        public int PhoneNumber_MatchWeight { get; set; }

        public string PhoneExtension { get; set; }
        public string PhoneExtension_BusinessName { get; set; }
        public string PhoneExtension_BusinessDescription { get; set; }
        public bool PhoneExtension_IncludeInMatch { get; set; }
        public int PhoneExtension_MatchWeight { get; set; }

        public string PhoneCountryCode { get; set; }
        public string PhoneCountryCode_BusinessName { get; set; }
        public string PhoneCountryCode_BusinessDescription { get; set; }
        public bool PhoneCountryCode_IncludeInMatch { get; set; }
        public int PhoneCountryCode_MatchWeight { get; set; }

        public string PhoneLineType { get; set; }
        public string PhoneLineType_BusinessName { get; set; }
        public string PhoneLineType_BusinessDescription { get; set; }
        public bool PhoneLineType_IncludeInMatch { get; set; }
        public int PhoneLineType_MatchWeight { get; set; }

        public string PhoneUseType { get; set; }
        public string PhoneUseType_BusinessName { get; set; }
        public string PhoneUseType_BusinessDescription { get; set; }
        public bool PhoneUseType_IncludeInMatch { get; set; }
        public int PhoneUseType_MatchWeight { get; set; }

        public bool? PhoneIsPrimary { get; set; }
        public string PhoneIsPrimary_BusinessName { get; set; }
        public string PhoneIsPrimary_BusinessDescription { get; set; }
        public bool PhoneIsPrimary_IncludeInMatch { get; set; }
        public int PhoneIsPrimary_MatchWeight { get; set; }











    }
}
