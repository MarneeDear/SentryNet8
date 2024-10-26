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
    public class StudentHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        #region Student Bio/Dem

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

        public string StudentId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_OriginalValue { get; set; }

        public string StudentTitle { get; set; }
        public string StudentTitle_Status { get; set; }
        public string StudentTitle_OriginalValue { get; set; }

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

        public string MaritalStatusCode { get; set; }
        public string MaritalStatusCode_Status { get; set; }
        public string MaritalStatusCode_OriginalValue { get; set; }

        public string MaritalStatusSourceSystemRecordId { get; set; }
        public string MaritalStatusSourceSystemRecordId_Status { get; set; }
        public string MaritalStatusSourceSystemRecordId_OriginalValue { get; set; }

        public string MaritalStatusMasterId { get; set; }
        public string MaritalStatusMasterId_Status { get; set; }
        public string MaritalStatusMasterId_OriginalValue { get; set; }

        public string FERPAInformationRelease { get; set; }
        public string FERPAInformationRelease_Status { get; set; }
        public string FERPAInformationRelease_OriginalValue { get; set; }

        #endregion

        #region Student Additional Information

        #region CitizenshipCountryCode
        public string CitizenshipCountryCode { get; set; }
        public string CitizenshipCountryCode_Status { get; set; }
        public string CitizenshipCountryCode_OriginalValue { get; set; }
        #endregion

        #region CitizenshipCountryMasterId
        public string CitizenshipCountryMasterId { get; set; }
        public string CitizenshipCountryMasterId_Status { get; set; }
        public string CitizenshipCountryMasterId_OriginalValue { get; set; }
        #endregion

        #region TermName
        public string TermName { get; set; }
        public string TermName_Status { get; set; }
        public string TermName_OriginalValue { get; set; }
        #endregion

        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public string AcademicCareerName_Status { get; set; }
        public string AcademicCareerName_OriginalValue { get; set; }
        #endregion

        #region AcademicCareerCode
        public string AcademicCareerCode { get; set; }
        public string AcademicCareerCode_Status { get; set; }
        public string AcademicCareerCode_OriginalValue { get; set; }
        #endregion

        #region AcademicTermCode
        public string AcademicTermCode { get; set; }
        public string AcademicTermCode_Status { get; set; }
        public string AcademicTermCode_OriginalValue { get; set; }
        #endregion

        #region DischargedTermCode
        public string DischargedTermCode { get; set; }
        public string DischargedTermCode_Status { get; set; }
        public string DischargedTermCode_OriginalValue { get; set; }
        #endregion

        #region AcademicCalendarMasterId
        public string AcademicCalendarMasterId { get; set; }
        public string AcademicCalendarMasterId_Status { get; set; }
        public string AcademicCalendarMasterId_OriginalValue { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_Status { get; set; }
        public string EmailAddress1_OriginalValue { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public string EmailAddress1MasterRecordId_OriginalValue { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_Status { get; set; }
        public string EmailAddress2_OriginalValue { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public string EmailAddress2MasterRecordId_OriginalValue { get; set; }
        #endregion

        #endregion

    }
}
