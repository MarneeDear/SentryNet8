using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentViewModel : BaseIntegrationViewModel
    {
        public StudentViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        #region Student Bio/Dem

        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Status { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public string FirstName_OriginalValue { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public bool FirstName_IsReadOnly { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Status { get; set; }
        public string PreferredName_Source { get; set; }
        public string PreferredName_Category { get; set; }
        public string PreferredName_OriginalValue { get; set; }
        public int? PreferredName_AttributeId { get; set; }
        public bool PreferredName_IsReadOnly { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public string MiddleName_Status { get; set; }
        public string MiddleName_Source { get; set; }
        public string MiddleName_Category { get; set; }
        public string MiddleName_OriginalValue { get; set; }
        public int? MiddleName_AttributeId { get; set; }
        public bool MiddleName_IsReadOnly { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Status { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public string LastName_OriginalValue { get; set; }
        public int? LastName_AttributeId { get; set; }
        public bool LastName_IsReadOnly { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public string MaidenName_Status { get; set; }
        public string MaidenName_Source { get; set; }
        public string MaidenName_Category { get; set; }
        public string MaidenName_OriginalValue { get; set; }
        public int? MaidenName_AttributeId { get; set; }
        public bool MaidenName_IsReadOnly { get; set; }
        #endregion

        #region StudentId
        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_Source { get; set; }
        public string StudentId_Category { get; set; }
        public string StudentId_OriginalValue { get; set; }
        public int? StudentId_AttributeId { get; set; }
        public bool StudentId_IsReadOnly { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Status { get; set; }
        public string Suffix_Source { get; set; }
        public string Suffix_Category { get; set; }
        public string Suffix_OriginalValue { get; set; }
        public int? Suffix_AttributeId { get; set; }
        #endregion

        #region SuffixSourceSystemRecordId
        public string SuffixSourceSystemRecordId { get; set; }
        public string SuffixSourceSystemRecordId_BusinessName { get; set; }
        public string SuffixSourceSystemRecordId_BusinessDescription { get; set; }
        public string SuffixSourceSystemRecordId_Status { get; set; }
        public string SuffixSourceSystemRecordId_Source { get; set; }
        public string SuffixSourceSystemRecordId_Category { get; set; }
        public string SuffixSourceSystemRecordId_OriginalValue { get; set; }
        public int? SuffixSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region SuffixMasterId
        public string SuffixMasterId { get; set; }
        public string SuffixMasterId_BusinessName { get; set; }
        public string SuffixMasterId_BusinessDescription { get; set; }
        public string SuffixMasterId_Status { get; set; }
        public string SuffixMasterId_Source { get; set; }
        public string SuffixMasterId_Category { get; set; }
        public string SuffixMasterId_OriginalValue { get; set; }
        public int? SuffixMasterId_AttributeId { get; set; }
        public bool SuffixMasterId_IsReadOnly { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public string BirthDate_Status { get; set; }
        public string BirthDate_Source { get; set; }
        public string BirthDate_Category { get; set; }
        public string BirthDate_OriginalValue { get; set; }
        public int? BirthDate_AttributeId { get; set; }
        public bool BirthDate_IsReadOnly { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public string DeceasedDate_Status { get; set; }
        public string DeceasedDate_Source { get; set; }
        public string DeceasedDate_Category { get; set; }
        public string DeceasedDate_OriginalValue { get; set; }
        public int? DeceasedDate_AttributeId { get; set; }
        public bool DeceasedDate_IsReadOnly { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public string MaritalStatus_Status { get; set; }
        public string MaritalStatus_Source { get; set; }
        public string MaritalStatus_Category { get; set; }
        public string MaritalStatus_OriginalValue { get; set; }
        public int? MaritalStatus_AttributeId { get; set; }
        #endregion

        #region MaritalStatusCode
        public string MaritalStatusCode { get; set; }
        public string MaritalStatusCode_BusinessName { get; set; }
        public string MaritalStatusCode_BusinessDescription { get; set; }
        public string MaritalStatusCode_Status { get; set; }
        public string MaritalStatusCode_Source { get; set; }
        public string MaritalStatusCode_Category { get; set; }
        public string MaritalStatusCode_OriginalValue { get; set; }
        public int? MaritalStatusCode_AttributeId { get; set; }
        #endregion

        #region MaritalStatusSourceSystemRecordId
        public string MaritalStatusSourceSystemRecordId { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessName { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public string MaritalStatusSourceSystemRecordId_Status { get; set; }
        public string MaritalStatusSourceSystemRecordId_Source { get; set; }
        public string MaritalStatusSourceSystemRecordId_Category { get; set; }
        public string MaritalStatusSourceSystemRecordId_OriginalValue { get; set; }
        public int? MaritalStatusSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region MaritalStatusMasterId
        public string MaritalStatusMasterId { get; set; }
        public string MaritalStatusMasterId_BusinessName { get; set; }
        public string MaritalStatusMasterId_BusinessDescription { get; set; }
        public string MaritalStatusMasterId_Status { get; set; }
        public string MaritalStatusMasterId_Source { get; set; }
        public string MaritalStatusMasterId_Category { get; set; }
        public string MaritalStatusMasterId_OriginalValue { get; set; }
        public int? MaritalStatusMasterId_AttributeId { get; set; }
        public bool MaritalStatusMasterId_IsReadOnly { get; set; }
        #endregion

        #region CitizenshipCountryCode
        public string CitizenshipCountryCode { get; set; }
        public string CitizenshipCountryCode_BusinessName { get; set; }
        public string CitizenshipCountryCode_BusinessDescription { get; set; }
        public string CitizenshipCountryCode_Status { get; set; }
        public string CitizenshipCountryCode_Source { get; set; }
        public string CitizenshipCountryCode_Category { get; set; }
        public string CitizenshipCountryCode_OriginalValue { get; set; }
        public int? CitizenshipCountryCode_AttributeId { get; set; }
        #endregion

        #region CitizenshipCountryMasterId
        public string CitizenshipCountryMasterId { get; set; }
        public string CitizenshipCountryMasterId_BusinessName { get; set; }
        public string CitizenshipCountryMasterId_BusinessDescription { get; set; }
        public string CitizenshipCountryMasterId_Status { get; set; }
        public string CitizenshipCountryMasterId_Source { get; set; }
        public string CitizenshipCountryMasterId_Category { get; set; }
        public string CitizenshipCountryMasterId_OriginalValue { get; set; }
        public int? CitizenshipCountryMasterId_AttributeId { get; set; }
        public bool CitizenshipCountryMasterId_IsReadOnly { get; set; }
        #endregion

        #endregion

        #region Student Additional Information

        #region FERPAInformationRelease
        public string FERPAInformationRelease { get; set; }
        public string FERPAInformationRelease_BusinessName { get; set; }
        public string FERPAInformationRelease_BusinessDescription { get; set; }
        public string FERPAInformationRelease_Status { get; set; }
        public string FERPAInformationRelease_Source { get; set; }
        public string FERPAInformationRelease_Category { get; set; }
        public string FERPAInformationRelease_OriginalValue { get; set; }
        public int? FERPAInformationRelease_AttributeId { get; set; }
        public bool FERPAInformationRelease_IsReadOnly { get; set; }
        #endregion

        #region TermName
        public string TermName { get; set; }
        public string TermName_BusinessName { get; set; }
        public string TermName_BusinessDescription { get; set; }
        public string TermName_Status { get; set; }
        public string TermName_Source { get; set; }
        public string TermName_Category { get; set; }
        public string TermName_OriginalValue { get; set; }
        public int? TermName_AttributeId { get; set; }
        public bool TermName_IsReadOnly { get; set; }
        #endregion

        #region AcademicCalendarName
        public string AcademicCalendarName { get; set; }
        public string AcademicCalendarName_BusinessName { get; set; }
        public string AcademicCalendarName_BusinessDescription { get; set; }
        public string AcademicCalendarName_Status { get; set; }
        public string AcademicCalendarName_Source { get; set; }
        public string AcademicCalendarName_Category { get; set; }
        public string AcademicCalendarName_OriginalValue { get; set; }
        public int? AcademicCalendarName_AttributeId { get; set; }
        public bool AcademicCalendarName_IsReadOnly { get; set; }
        #endregion

        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public string AcademicCareerName_BusinessName { get; set; }
        public string AcademicCareerName_BusinessDescription { get; set; }
        public string AcademicCareerName_Status { get; set; }
        public string AcademicCareerName_Source { get; set; }
        public string AcademicCareerName_Category { get; set; }
        public string AcademicCareerName_OriginalValue { get; set; }
        public int? AcademicCareerName_AttributeId { get; set; }
        public bool AcademicCareerName_IsReadOnly { get; set; }
        #endregion

        #region AcademicTermCode
        public string AcademicTermCode { get; set; }
        public string AcademicTermCode_BusinessName { get; set; }
        public string AcademicTermCode_BusinessDescription { get; set; }
        public string AcademicTermCode_Status { get; set; }
        public string AcademicTermCode_Source { get; set; }
        public string AcademicTermCode_Category { get; set; }
        public string AcademicTermCode_OriginalValue { get; set; }
        public int? AcademicTermCode_AttributeId { get; set; }
        public bool AcademicTermCode_IsReadOnly { get; set; }
        #endregion

        #region AcademicCareerCode
        public string AcademicCareerCode { get; set; }
        public string AcademicCareerCode_BusinessName { get; set; }
        public string AcademicCareerCode_BusinessDescription { get; set; }
        public string AcademicCareerCode_Status { get; set; }
        public string AcademicCareerCode_Source { get; set; }
        public string AcademicCareerCode_Category { get; set; }
        public string AcademicCareerCode_OriginalValue { get; set; }
        public int? AcademicCareerCode_AttributeId { get; set; }
        public bool AcademicCareerCode_IsReadOnly { get; set; }
        #endregion

        #region DischargedTermCode
        public string DischargedTermCode { get; set; }
        public string DischargedTermCode_BusinessName { get; set; }
        public string DischargedTermCode_BusinessDescription { get; set; }
        public string DischargedTermCode_Status { get; set; }
        public string DischargedTermCode_Source { get; set; }
        public string DischargedTermCode_Category { get; set; }
        public string DischargedTermCode_OriginalValue { get; set; }
        public int? DischargedTermCode_AttributeId { get; set; }
        #endregion

        public string DischargedTermName { get; set; }

        #region AcademicCalendarMasterId
        public string AcademicCalendarMasterId { get; set; }
        public string AcademicCalendarMasterId_BusinessName { get; set; }
        public string AcademicCalendarMasterId_BusinessDescription { get; set; }
        public string AcademicCalendarMasterId_Status { get; set; }
        public string AcademicCalendarMasterId_Source { get; set; }
        public string AcademicCalendarMasterId_Category { get; set; }
        public string AcademicCalendarMasterId_OriginalValue { get; set; }
        public int? AcademicCalendarMasterId_AttributeId { get; set; }
        public bool AcademicCalendarMasterId_IsReadOnly { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public string EmailAddress1_Status { get; set; }
        public string EmailAddress1_Source { get; set; }
        public string EmailAddress1_Category { get; set; }
        public string EmailAddress1_OriginalValue { get; set; }
        public int? EmailAddress1_AttributeId { get; set; }
        public bool EmailAddress1_IsReadOnly { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public string EmailAddress1MasterRecordId_Source { get; set; }
        public string EmailAddress1MasterRecordId_Category { get; set; }
        public string EmailAddress1MasterRecordId_OriginalValue { get; set; }
        public int? EmailAddress1MasterRecordId_AttributeId { get; set; }
        public bool EmailAddress1MasterRecordId_IsReadOnly { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public string EmailAddress2_Status { get; set; }
        public string EmailAddress2_Source { get; set; }
        public string EmailAddress2_Category { get; set; }
        public string EmailAddress2_OriginalValue { get; set; }
        public int? EmailAddress2_AttributeId { get; set; }
        public bool EmailAddress2_IsReadOnly { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public string EmailAddress2MasterRecordId_Source { get; set; }
        public string EmailAddress2MasterRecordId_Category { get; set; }
        public string EmailAddress2MasterRecordId_OriginalValue { get; set; }
        public int? EmailAddress2MasterRecordId_AttributeId { get; set; }
        public bool EmailAddress2MasterRecordId_IsReadOnly { get; set; }
        #endregion

        #endregion

        #region Drop-Downs
        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> SuffixList { get; set; }
        public List<SelectListItem> MaritalStatusList { get; set; }
        public List<SelectListItem> CountryList { get; set; }
        public List<SelectListItem> TermList { get; set; }
        #endregion

        public List<StudentHistoryViewModel> HistoryData { get; set; }


    }
}
