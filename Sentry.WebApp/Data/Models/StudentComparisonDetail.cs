using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class StudentComparisonDetail : BaseDetail
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

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public string StudentId_Compare { get; set; }

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

        public string FERPAInformationRelease { get; set; }
        public string FERPAInformationRelease_BusinessName { get; set; }
        public string FERPAInformationRelease_BusinessDescription { get; set; }
        public string FERPAInformationRelease_Compare { get; set; }

        public string CitizenshipCountry { get; set; }
        public string CitizenshipCountry_BusinessName { get; set; }
        public string CitizenshipCountry_BusinessDescription { get; set; }
        public string CitizenshipCountry_Compare { get; set; }

        public string Discharged { get; set; }
        public string Discharged_BusinessName { get; set; }
        public string Discharged_BusinessDescription { get; set; }
        public string Discharged_Compare { get; set; }

        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public string EmailAddress1_Compare { get; set; }

        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public string EmailAddress2_Compare { get; set; }

        [NotMapped]
        [ForeignKey("MasterRecordCode")]
        public ICollection<SystemRecord> SystemRecords { get; set; }
    }
}
