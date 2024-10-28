using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentParentMatchDetail : BaseDetail
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

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IncludeInMatch { get; set; }
        public int LastName_MatchWeight { get; set; }

        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IncludeInMatch { get; set; }
        public int Suffix_MatchWeight { get; set; }

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public bool StudentId_IncludeInMatch { get; set; }
        public int StudentId_MatchWeight { get; set; }

        public string StudentFirstName { get; set; }
        public string StudentFirstName_BusinessName { get; set; }
        public string StudentFirstName_BusinessDescription { get; set; }
        public bool StudentFirstName_IncludeInMatch { get; set; }
        public int StudentFirstName_MatchWeight { get; set; }

        public string StudentLastName { get; set; }
        public string StudentLastName_BusinessName { get; set; }
        public string StudentLastName_BusinessDescription { get; set; }
        public bool StudentLastName_IncludeInMatch { get; set; }
        public int StudentLastName_MatchWeight { get; set; }

        public string Relationship { get; set; }
        public string Relationship_BusinessName { get; set; }
        public string Relationship_BusinessDescription { get; set; }
        public bool Relationship_IncludeInMatch { get; set; }
        public int Relationship_MatchWeight { get; set; }

        public string Phone1Number { get; set; }
        public string Phone1Number_BusinessName { get; set; }
        public string Phone1Number_BusinessDescription { get; set; }
        public bool Phone1Number_IncludeInMatch { get; set; }
        public int Phone1Number_MatchWeight { get; set; }

        public string Phone1Extension { get; set; }
        public string Phone1Extension_BusinessName { get; set; }
        public string Phone1Extension_BusinessDescription { get; set; }
        public bool Phone1Extension_IncludeInMatch { get; set; }
        public int Phone1Extension_MatchWeight { get; set; }

        public string Phone1CountryCode { get; set; }
        public string Phone1CountryCode_BusinessName { get; set; }
        public string Phone1CountryCode_BusinessDescription { get; set; }
        public bool Phone1CountryCode_IncludeInMatch { get; set; }
        public int Phone1CountryCode_MatchWeight { get; set; }

        public string Phone2Number { get; set; }
        public string Phone2Number_BusinessName { get; set; }
        public string Phone2Number_BusinessDescription { get; set; }
        public bool Phone2Number_IncludeInMatch { get; set; }
        public int Phone2Number_MatchWeight { get; set; }

        public string Phone2Extension { get; set; }
        public string Phone2Extension_BusinessName { get; set; }
        public string Phone2Extension_BusinessDescription { get; set; }
        public bool Phone2Extension_IncludeInMatch { get; set; }
        public int Phone2Extension_MatchWeight { get; set; }

        public string Phone2CountryCode { get; set; }
        public string Phone2CountryCode_BusinessName { get; set; }
        public string Phone2CountryCode_BusinessDescription { get; set; }
        public bool Phone2CountryCode_IncludeInMatch { get; set; }
        public int Phone2CountryCode_MatchWeight { get; set; }

        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public bool EmailAddress1_IncludeInMatch { get; set; }
        public int EmailAddress1_MatchWeight { get; set; }

        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public bool EmailAddress2_IncludeInMatch { get; set; }
        public int EmailAddress2_MatchWeight { get; set; }

        public string Address1 { get; set; }
        public string Address1_BusinessName { get; set; }
        public string Address1_BusinessDescription { get; set; }
        public bool Address1_IncludeInMatch { get; set; }
        public int Address1_MatchWeight { get; set; }

        public string City { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public bool City_IncludeInMatch { get; set; }
        public int City_MatchWeight { get; set; }

        public string State { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public bool State_IncludeInMatch { get; set; }
        public int State_MatchWeight { get; set; }

        public string PostalCode { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public bool PostalCode_IncludeInMatch { get; set; }
        public int PostalCode_MatchWeight { get; set; }

        public string Country { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public bool Country_IncludeInMatch { get; set; }
        public int Country_MatchWeight { get; set; }

    }
}
