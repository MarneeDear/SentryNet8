using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeeViewModel : BaseIntegrationViewModel
    {
        public EmployeeViewModel() : base() { }

        public bool IsReadOnly { get; set; }

        public DateTime? IntegrationDate { get; set; }
        public DateTime? CreatedDate { get; set; }


        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public string FirstName_Status { get; set; }
        public int? FirstName_AttributeId { get; set; }
        public bool FirstName_IsReadOnly { get; set; }
        public string FirstName_OriginalValue { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Source { get; set; }
        public string PreferredName_Category { get; set; }
        public string PreferredName_Status { get; set; }
        public int? PreferredName_AttributeId { get; set; }
        public bool PreferredName_IsReadOnly { get; set; }
        public string PreferredName_OriginalValue { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public string MiddleName_Source { get; set; }
        public string MiddleName_Category { get; set; }
        public string MiddleName_Status { get; set; }
        public int? MiddleName_AttributeId { get; set; }
        public bool MiddleName_IsReadOnly { get; set; }
        public string MiddleName_OriginalValue { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public string LastName_Status { get; set; }
        public int? LastName_AttributeId { get; set; }
        public bool LastName_IsReadOnly { get; set; }
        public string LastName_OriginalValue { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public string MaidenName_Source { get; set; }
        public string MaidenName_Category { get; set; }
        public string MaidenName_Status { get; set; }
        public int? MaidenName_AttributeId { get; set; }
        public bool MaidenName_IsReadOnly { get; set; }
        public string MaidenName_OriginalValue { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Source { get; set; }
        public string UAPersonId_Category { get; set; }
        public string UAPersonId_Status { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        public bool UAPersonId_IsReadOnly { get; set; }
        public string UAPersonId_OriginalValue { get; set; }
        #endregion

        #region EmployeeConstituentId
        public string EmployeeConstituentId { get; set; }
        public string EmployeeConstituentId_BusinessName { get; set; }
        public string EmployeeConstituentId_BusinessDescription { get; set; }
        public string EmployeeConstituentId_Source { get; set; }
        public string EmployeeConstituentId_Category { get; set; }
        public string EmployeeConstituentId_Status { get; set; }
        public int? EmployeeConstituentId_AttributeId { get; set; }
        public bool EmployeeConstituentId_IsReadOnly { get; set; }
        public string EmployeeConstituentId_OriginalValue { get; set; }
        #endregion

        #region EmployeeTitle
        public string EmployeeTitle { get; set; }
        public string EmployeeTitle_BusinessName { get; set; }
        public string EmployeeTitle_BusinessDescription { get; set; }
        public string EmployeeTitle_Source { get; set; }
        public string EmployeeTitle_Category { get; set; }
        public string EmployeeTitle_Status { get; set; }
        public int? EmployeeTitle_AttributeId { get; set; }
        public bool EmployeeTitle_IsReadOnly { get; set; }
        public string EmployeeTitle_OriginalValue { get; set; }
        #endregion

        #region TitleSourceSystemRecordId
        public string TitleSourceSystemRecordId { get; set; }
        public string TitleSourceSystemRecordId_BusinessName { get; set; }
        public string TitleSourceSystemRecordId_BusinessDescription { get; set; }
        public string TitleSourceSystemRecordId_Status { get; set; }
        public string TitleSourceSystemRecordId_Source { get; set; }
        public string TitleSourceSystemRecordId_Category { get; set; }
        public string TitleSourceSystemRecordId_OriginalValue { get; set; }
        public int? TitleSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region TitleMasterId
        public string TitleMasterId { get; set; }
        public string TitleMasterId_BusinessName { get; set; }
        public string TitleMasterId_BusinessDescription { get; set; }
        public string TitleMasterId_Status { get; set; }
        public string TitleMasterId_Source { get; set; }
        public string TitleMasterId_Category { get; set; }
        public string TitleMasterId_OriginalValue { get; set; }
        public int? TitleMasterId_AttributeId { get; set; }
        public bool TitleMasterId_IsReadOnly { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Source { get; set; }
        public string Suffix_Category { get; set; }
        public string Suffix_Status { get; set; }
        public int? Suffix_AttributeId { get; set; }
        public bool Suffix_IsReadOnly { get; set; }
        public string Suffix_OriginalValue { get; set; }
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
        public string BirthDate_Source { get; set; }
        public string BirthDate_Category { get; set; }
        public string BirthDate_Status { get; set; }
        public int? BirthDate_AttributeId { get; set; }
        public bool BirthDate_IsReadOnly { get; set; }
        public string BirthDate_OriginalValue { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public string DeceasedDate_Source { get; set; }
        public string DeceasedDate_Category { get; set; }
        public string DeceasedDate_Status { get; set; }
        public int? DeceasedDate_AttributeId { get; set; }
        public bool DeceasedDate_IsReadOnly { get; set; }
        public string DeceasedDate_OriginalValue { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public string MaritalStatus_Source { get; set; }
        public string MaritalStatus_Category { get; set; }
        public string MaritalStatus_Status { get; set; }
        public int? MaritalStatus_AttributeId { get; set; }
        public bool MaritalStatus_IsReadOnly { get; set; }
        public string MaritalStatus_OriginalValue { get; set; }
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

        #region Organization
        public string Organization { get; set; }
        public string Organization_BusinessName { get; set; }
        public string Organization_BusinessDescription { get; set; }
        public string Organization_Source { get; set; }
        public string Organization_Category { get; set; }
        public string Organization_Status { get; set; }
        public int? Organization_AttributeId { get; set; }
        public bool Organization_IsReadOnly { get; set; }
        public string Organization_OriginalValue { get; set; }
        #endregion

        #region OrganizationName
        public string OrganizationName { get; set; }
        public string OrganizationName_BusinessName { get; set; }
        public string OrganizationName_BusinessDescription { get; set; }
        public string OrganizationName_Source { get; set; }
        public string OrganizationName_Category { get; set; }
        public string OrganizationName_Status { get; set; }
        public int? OrganizationName_AttributeId { get; set; }
        public bool OrganizationName_IsReadOnly { get; set; }
        public string OrganizationName_OriginalValue { get; set; }
        #endregion

        #region OrganizationSourceSystemRecordId
        public string OrganizationSourceSystemRecordId { get; set; }
        public string OrganizationSourceSystemRecordId_BusinessName { get; set; }
        public string OrganizationSourceSystemRecordId_BusinessDescription { get; set; }
        public string OrganizationSourceSystemRecordId_Source { get; set; }
        public string OrganizationSourceSystemRecordId_Category { get; set; }
        public string OrganizationSourceSystemRecordId_Status { get; set; }
        public int? OrganizationSourceSystemRecordId_AttributeId { get; set; }
        public bool OrganizationSourceSystemRecordId_IsReadOnly { get; set; }
        public string OrganizationSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region OrganizationMasterId
        public string OrganizationMasterId { get; set; }
        public string OrganizationMasterId_BusinessName { get; set; }
        public string OrganizationMasterId_BusinessDescription { get; set; }
        public string OrganizationMasterId_Source { get; set; }
        public string OrganizationMasterId_Category { get; set; }
        public string OrganizationMasterId_Status { get; set; }
        public int? OrganizationMasterId_AttributeId { get; set; }
        public bool OrganizationMasterId_IsReadOnly { get; set; }
        public string OrganizationMasterId_OriginalValue { get; set; }
        #endregion

        #region HireDate
        public string HireDate { get; set; }
        public string HireDate_BusinessName { get; set; }
        public string HireDate_BusinessDescription { get; set; }
        public string HireDate_Source { get; set; }
        public string HireDate_Category { get; set; }
        public string HireDate_Status { get; set; }
        public int? HireDate_AttributeId { get; set; }
        public bool HireDate_IsReadOnly { get; set; }
        public string HireDate_OriginalValue { get; set; }
        #endregion

        #region TerminationDate
        public string TerminationDate { get; set; }
        public string TerminationDate_BusinessName { get; set; }
        public string TerminationDate_BusinessDescription { get; set; }
        public string TerminationDate_Source { get; set; }
        public string TerminationDate_Category { get; set; }
        public string TerminationDate_Status { get; set; }
        public int? TerminationDate_AttributeId { get; set; }
        public bool TerminationDate_IsReadOnly { get; set; }
        public string TerminationDate_OriginalValue { get; set; }
        #endregion

        #region EmployeeType
        public string EmployeeType { get; set; }
        public string EmployeeType_BusinessName { get; set; }
        public string EmployeeType_BusinessDescription { get; set; }
        public string EmployeeType_Source { get; set; }
        public string EmployeeType_Category { get; set; }
        public string EmployeeType_Status { get; set; }
        public int? EmployeeType_AttributeId { get; set; }
        public bool EmployeeType_IsReadOnly { get; set; }
        public string EmployeeType_OriginalValue { get; set; }
        #endregion

        #region EmployeeTypeName
        public string EmployeeTypeName { get; set; }
        public string EmployeeTypeName_BusinessName { get; set; }
        public string EmployeeTypeName_BusinessDescription { get; set; }
        public string EmployeeTypeName_Source { get; set; }
        public string EmployeeTypeName_Category { get; set; }
        public string EmployeeTypeName_Status { get; set; }
        public int? EmployeeTypeName_AttributeId { get; set; }
        public bool EmployeeTypeName_IsReadOnly { get; set; }
        public string EmployeeTypeName_OriginalValue { get; set; }
        #endregion

        #region EmployeeTypeSourceSystemRecordId
        public string EmployeeTypeSourceSystemRecordId { get; set; }
        public string EmployeeTypeSourceSystemRecordId_BusinessName { get; set; }
        public string EmployeeTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Source { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Category { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Status { get; set; }
        public int? EmployeeTypeSourceSystemRecordId_AttributeId { get; set; }
        public bool EmployeeTypeSourceSystemRecordId_IsReadOnly { get; set; }
        public string EmployeeTypeSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region EmployeeTypeMasterId
        public string EmployeeTypeMasterId { get; set; }
        public string EmployeeTypeMasterId_BusinessName { get; set; }
        public string EmployeeTypeMasterId_BusinessDescription { get; set; }
        public string EmployeeTypeMasterId_Source { get; set; }
        public string EmployeeTypeMasterId_Category { get; set; }
        public string EmployeeTypeMasterId_Status { get; set; }
        public int? EmployeeTypeMasterId_AttributeId { get; set; }
        public bool EmployeeTypeMasterId_IsReadOnly { get; set; }
        public string EmployeeTypeMasterId_OriginalValue { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public string EmailAddress1_Source { get; set; }
        public string EmailAddress1_Category { get; set; }
        public string EmailAddress1_Status { get; set; }
        public int? EmailAddress1_AttributeId { get; set; }
        public bool EmailAddress1_IsReadOnly { get; set; }
        public string EmailAddress1_OriginalValue { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress1MasterRecordId_Source { get; set; }
        public string EmailAddress1MasterRecordId_Category { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public int? EmailAddress1MasterRecordId_AttributeId { get; set; }
        public bool EmailAddress1MasterRecordId_IsReadOnly { get; set; }
        public string EmailAddress1MasterRecordId_OriginalValue { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public string EmailAddress2_Source { get; set; }
        public string EmailAddress2_Category { get; set; }
        public string EmailAddress2_Status { get; set; }
        public int? EmailAddress2_AttributeId { get; set; }
        public bool EmailAddress2_IsReadOnly { get; set; }
        public string EmailAddress2_OriginalValue { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress2MasterRecordId_Source { get; set; }
        public string EmailAddress2MasterRecordId_Category { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public int? EmailAddress2MasterRecordId_AttributeId { get; set; }
        public bool EmailAddress2MasterRecordId_IsReadOnly { get; set; }
        public string EmailAddress2MasterRecordId_OriginalValue { get; set; }
        #endregion
        #region NetId
        public string NetId { get; set; }
        public string NetId_BusinessName { get; set; }
        public string NetId_BusinessDescription { get; set; }
        public string NetId_Source { get; set; }
        public string NetId_Category { get; set; }
        public string NetId_Status { get; set; }
        public int? NetId_AttributeId { get; set; }
        public bool NetId_IsReadOnly { get; set; }
        public string NetId_OriginalValue { get; set; }
        #endregion


        #region Drop Downs
        public List<SelectListItem> TitleList { get; set; }
        public List<SelectListItem> SuffixList { get; set; }
        public List<SelectListItem> MaritalStatusList { get; set; }
        public List<SelectListItem> OrganizationalUnitList { get; set; }
        public IEnumerable<SelectListItem> OrganizationList { get; set; }
        public List<SelectListItem> EmployeeTypeList { get; set; }
        #endregion

        public List<EmployeeHistoryViewModel> HistoryData { get; set; }

    }
}
