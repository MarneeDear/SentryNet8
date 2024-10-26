using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class EmployeeDetail : BaseDetail
    {

        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public string FirstName_Source { get; set; }
        public string FirstName_Category { get; set; }
        public string FirstName_Status { get; set; }
        public int? FirstName_AttributeId { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public string PreferredName_Source { get; set; }
        public string PreferredName_Category { get; set; }
        public string PreferredName_Status { get; set; }
        public int? PreferredName_AttributeId { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public string MiddleName_Source { get; set; }
        public string MiddleName_Category { get; set; }
        public string MiddleName_Status { get; set; }
        public int? MiddleName_AttributeId { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public string LastName_Source { get; set; }
        public string LastName_Category { get; set; }
        public string LastName_Status { get; set; }
        public int? LastName_AttributeId { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public string MaidenName_Source { get; set; }
        public string MaidenName_Category { get; set; }
        public string MaidenName_Status { get; set; }
        public int? MaidenName_AttributeId { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public string UAPersonId_Source { get; set; }
        public string UAPersonId_Category { get; set; }
        public string UAPersonId_Status { get; set; }
        public int? UAPersonId_AttributeId { get; set; }
        #endregion

        #region EmployeeConstituentId
        public string EmployeeConstituentId { get; set; }
        public string EmployeeConstituentId_BusinessName { get; set; }
        public string EmployeeConstituentId_BusinessDescription { get; set; }
        public string EmployeeConstituentId_Source { get; set; }
        public string EmployeeConstituentId_Category { get; set; }
        public string EmployeeConstituentId_Status { get; set; }
        public int? EmployeeConstituentId_AttributeId { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public string Suffix_Source { get; set; }
        public string Suffix_Category { get; set; }
        public string Suffix_Status { get; set; }
        public int? Suffix_AttributeId { get; set; }
        #endregion

        #region SuffixSourceSystemRecordId
        public string SuffixSourceSystemRecordId { get; set; }
        public string SuffixSourceSystemRecordId_BusinessName { get; set; }
        public string SuffixSourceSystemRecordId_BusinessDescription { get; set; }
        public string SuffixSourceSystemRecordId_Source { get; set; }
        public string SuffixSourceSystemRecordId_Category { get; set; }
        public string SuffixSourceSystemRecordId_Status { get; set; }
        public int? SuffixSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region SuffixMasterId
        public string SuffixMasterId { get; set; }
        public string SuffixMasterId_BusinessName { get; set; }
        public string SuffixMasterId_BusinessDescription { get; set; }
        public string SuffixMasterId_Source { get; set; }
        public string SuffixMasterId_Category { get; set; }
        public string SuffixMasterId_Status { get; set; }
        public int? SuffixMasterId_AttributeId { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public string BirthDate_Source { get; set; }
        public string BirthDate_Category { get; set; }
        public string BirthDate_Status { get; set; }
        public int? BirthDate_AttributeId { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public string DeceasedDate_Source { get; set; }
        public string DeceasedDate_Category { get; set; }
        public string DeceasedDate_Status { get; set; }
        public int? DeceasedDate_AttributeId { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public string MaritalStatus_Source { get; set; }
        public string MaritalStatus_Category { get; set; }
        public string MaritalStatus_Status { get; set; }
        public int? MaritalStatus_AttributeId { get; set; }
        #endregion

        #region MartialStatusSourceSystemRecordId
        public string MaritalStatusSourceSystemRecordId { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessName { get; set; }
        public string MaritalStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public string MaritalStatusSourceSystemRecordId_Source { get; set; }
        public string MaritalStatusSourceSystemRecordId_Category { get; set; }
        public string MaritalStatusSourceSystemRecordId_Status { get; set; }
        public int? MaritalStatusSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region MaritalStatusMasterId
        public string MaritalStatusMasterId { get; set; }
        public string MaritalStatusMasterId_BusinessName { get; set; }
        public string MaritalStatusMasterId_BusinessDescription { get; set; }
        public string MaritalStatusMasterId_Source { get; set; }
        public string MaritalStatusMasterId_Category { get; set; }
        public string MaritalStatusMasterId_Status { get; set; }
        public int? MaritalStatusMasterId_AttributeId { get; set; }
        #endregion

        #region OrganizationName
        public string OrganizationName { get; set; }
        public string OrganizationName_BusinessName { get; set; }
        public string OrganizationName_BusinessDescription { get; set; }
        public string OrganizationName_Source { get; set; }
        public string OrganizationName_Category { get; set; }
        public string OrganizationName_Status { get; set; }
        public int? OrganizationName_AttributeId { get; set; }
        #endregion

        #region OrganizationSourceSystemRecordId
        public string OrganizationSourceSystemRecordId { get; set; }
        public string OrganizationSourceSystemRecordId_BusinessName { get; set; }
        public string OrganizationSourceSystemRecordId_BusinessDescription { get; set; }
        public string OrganizationSourceSystemRecordId_Source { get; set; }
        public string OrganizationSourceSystemRecordId_Category { get; set; }
        public string OrganizationSourceSystemRecordId_Status { get; set; }
        public int? OrganizationSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region OrganizationMasterId
        public string OrganizationMasterId { get; set; }
        public string OrganizationMasterId_BusinessName { get; set; }
        public string OrganizationMasterId_BusinessDescription { get; set; }
        public string OrganizationMasterId_Source { get; set; }
        public string OrganizationMasterId_Category { get; set; }
        public string OrganizationMasterId_Status { get; set; }
        public int? OrganizationMasterId_AttributeId { get; set; }
        #endregion

        #region HireDate
        public string HireDate { get; set; }
        public string HireDate_BusinessName { get; set; }
        public string HireDate_BusinessDescription { get; set; }
        public string HireDate_Source { get; set; }
        public string HireDate_Category { get; set; }
        public string HireDate_Status { get; set; }
        public int? HireDate_AttributeId { get; set; }
        #endregion

        #region TerminationDate
        public string TerminationDate { get; set; }
        public string TerminationDate_BusinessName { get; set; }
        public string TerminationDate_BusinessDescription { get; set; }
        public string TerminationDate_Source { get; set; }
        public string TerminationDate_Category { get; set; }
        public string TerminationDate_Status { get; set; }
        public int? TerminationDate_AttributeId { get; set; }
        #endregion

        #region EmployeeType
        public string EmployeeType { get; set; }
        public string EmployeeType_BusinessName { get; set; }
        public string EmployeeType_BusinessDescription { get; set; }
        public string EmployeeType_Source { get; set; }
        public string EmployeeType_Category { get; set; }
        public string EmployeeType_Status { get; set; }
        public int? EmployeeType_AttributeId { get; set; }
        #endregion

        #region EmployeeTypeSourceSystemRecordId
        public string EmployeeTypeSourceSystemRecordId { get; set; }
        public string EmployeeTypeSourceSystemRecordId_BusinessName { get; set; }
        public string EmployeeTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Source { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Category { get; set; }
        public string EmployeeTypeSourceSystemRecordId_Status { get; set; }
        public int? EmployeeTypeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region EmployeeTypeMasterId
        public string EmployeeTypeMasterId { get; set; }
        public string EmployeeTypeMasterId_BusinessName { get; set; }
        public string EmployeeTypeMasterId_BusinessDescription { get; set; }
        public string EmployeeTypeMasterId_Source { get; set; }
        public string EmployeeTypeMasterId_Category { get; set; }
        public string EmployeeTypeMasterId_Status { get; set; }
        public int? EmployeeTypeMasterId_AttributeId { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public string EmailAddress1_Source { get; set; }
        public string EmailAddress1_Category { get; set; }
        public string EmailAddress1_Status { get; set; }
        public int? EmailAddress1_AttributeId { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress1MasterRecordId_Source { get; set; }
        public string EmailAddress1MasterRecordId_Category { get; set; }
        public string EmailAddress1MasterRecordId_Status { get; set; }
        public int? EmailAddress1MasterRecordId_AttributeId { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public string EmailAddress2_Source { get; set; }
        public string EmailAddress2_Category { get; set; }
        public string EmailAddress2_Status { get; set; }
        public int? EmailAddress2_AttributeId { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        public string EmailAddress2MasterRecordId_Source { get; set; }
        public string EmailAddress2MasterRecordId_Category { get; set; }
        public string EmailAddress2MasterRecordId_Status { get; set; }
        public int? EmailAddress2MasterRecordId_AttributeId { get; set; }
        #endregion

        #region NetId
        public string NetId { get; set; }
        public string NetId_BusinessName { get; set; }
        public string NetId_BusinessDescription { get; set; }
        public string NetId_Source { get; set; }
        public string NetId_Category { get; set; }
        public string NetId_Status { get; set; }
        public int? NetId_AttributeId { get; set; }
        #endregion
    }
}
