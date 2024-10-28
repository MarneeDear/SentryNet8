using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeeHistoryViewModel
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

        public string EmployeeTitle { get; set; }
        public string EmployeeTitle_Status { get; set; }
        public string EmployeeTitle_OriginalValue { get; set; }

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

        public string Organization { get; set; }
        public string Organization_Status { get; set; }
        public string Organization_OriginalValue { get; set; }

        public string OrganizationName { get; set; }
        public string OrganizationName_Status { get; set; }
        public string OrganizationName_OriginalValue { get; set; }

        public string OrganizationSourceSystemRecordId { get; set; }
        public string OrganizationSourceSystemRecordId_Status { get; set; }
        public string OrganizationSourceSystemRecordId_OriginalValue { get; set; }

        public string OrganizationMasterId { get; set; }
        public string OrganizationMasterId_Status { get; set; }
        public string OrganizationMasterId_OriginalValue { get; set; }

        public string HireDate { get; set; }
        public string HireDate_Status { get; set; }
        public string HireDate_OriginalValue { get; set; }

        public string TerminationDate { get; set; }
        public string TerminationDate_Status { get; set; }
        public string TerminationDate_OriginalValue { get; set; }

        public string EmployeeType { get; set; }
        public string EmployeeType_Status { get; set; }
        public string EmployeeType_OriginalValue { get; set; }

        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeName_Status { get; set; }
        public string EmployeeTypeName_OriginalValue { get; set; }

        public string EmployeeTypeSourceSystemRecordId { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Status { get; set; }
        public string EmployeeTypeSourceSystemRecordId_OriginalValue { get; set; }

        public string EmployeeTypeMasterId { get; set; }
        public string EmployeeTypeMasterId_Status { get; set; }
        public string EmployeeTypeMasterId_OriginalValue { get; set; }

        public string EmailAddress1 { get; set; }
        public string EmailAddress1_Status { get; set; }
        public string EmailAddress1_OriginalValue { get; set; }

        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public string EmailAddress1MasterRecordId_OriginalValue { get; set; }

        public string EmailAddress2 { get; set; }
        public string EmailAddress2_Status { get; set; }
        public string EmailAddress2_OriginalValue { get; set; }

        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public string EmailAddress2MasterRecordId_OriginalValue { get; set; }

        public string NetId { get; set; }
        public string NetId_Status { get; set; }
        public string NetId_OriginalValue { get; set; }


    }
}