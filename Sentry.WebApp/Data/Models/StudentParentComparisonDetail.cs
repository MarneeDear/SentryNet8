using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class StudentParentComparisonDetail : BaseDetail
    {
        public string MasterRecordId { get; set; }
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Compare { get; set; }

        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Compare { get; set; }

        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Compare { get; set; }

        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Compare { get; set; }

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public string StudentId_Compare { get; set; }

        public string StudentFirstName { get; set; }
        public string StudentFirstName_BusinessName { get; set; }
        public string StudentFirstName_BusinessDescription { get; set; }
        public string StudentFirstName_Compare { get; set; }

        public string StudentLastName { get; set; }
        public string StudentLastName_BusinessName { get; set; }
        public string StudentLastName_BusinessDescription { get; set; }
        public string StudentLastName_Compare { get; set; }

        public string Relationship { get; set; }
        public string Relationship_BusinessName { get; set; }
        public string Relationship_BusinessDescription { get; set; }
        public string Relationship_Compare { get; set; }

        public string Phone1Number { get; set; }
        public string Phone1Number_BusinessName { get; set; }
        public string Phone1Number_BusinessDescription { get; set; }
        public string Phone1Number_Compare { get; set; }

        public string Phone1Extension { get; set; }
        public string Phone1Extension_BusinessName { get; set; }
        public string Phone1Extension_BusinessDescription { get; set; }
        public string Phone1Extension_Compare { get; set; }

        public string Phone1CountryCode { get; set; }
        public string Phone1CountryCode_BusinessName { get; set; }
        public string Phone1CountryCode_BusinessDescription { get; set; }
        public string Phone1CountryCode_Compare { get; set; }

        public string Phone2Number { get; set; }
        public string Phone2Number_BusinessName { get; set; }
        public string Phone2Number_BusinessDescription { get; set; }
        public string Phone2Number_Compare { get; set; }

        public string Phone2Extension { get; set; }
        public string Phone2Extension_BusinessName { get; set; }
        public string Phone2Extension_BusinessDescription { get; set; }
        public string Phone2Extension_Compare { get; set; }

        public string Phone2CountryCode { get; set; }
        public string Phone2CountryCode_BusinessName { get; set; }
        public string Phone2CountryCode_BusinessDescription { get; set; }
        public string Phone2CountryCode_Compare { get; set; }

        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public string EmailAddress1_Compare { get; set; }

        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public string EmailAddress2_Compare { get; set; }

        [NotMapped]
        [ForeignKey("MasterRecordId")]
        public ICollection<SystemRecord> SystemRecords { get; set; }

    }
}
