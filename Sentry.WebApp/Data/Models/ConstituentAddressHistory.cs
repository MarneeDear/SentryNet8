using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    //TODO find out more about how this is used
    [Table("ConstituentAddress_Base", Schema = "Integration")]
    public class ConstituentAddressHistory : BaseHistory
    {
        #region First Name
        public string FirstName { get; set; }

        public string FirstName_Status { get; set; }

        public int? FirstName_AttributeId { get; set; }
        #endregion

        #region Last Name
        public string LastName { get; set; }

        public string LastName_Status { get; set; }

        public int? LastName_AttributeId { get; set; }
        #endregion

        #region Address
        public string Address { get; set; }

        public string Address_Status { get; set; }

        public int? Address_AttributeId { get; set; }
        #endregion

        #region Address Master Id
        public string AddressMasterId { get; set; }

        public string AddressMasterId_Status { get; set; }

        public int? AddressMasterId_AttributeId { get; set; }
        #endregion

        #region City
        public string City { get; set; }

        public string City_Status { get; set; }

        public int? City_AttributeId { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }

        public string PostalCode_Status { get; set; }

        public int? PostalCode_AttributeId { get; set; }
        #endregion


        #region Termination Date
        public DateTime? TerminationDate { get; set; }

        public string TerminationDate_Status { get; set; }

        public int? TerminationDate_AttributeId { get; set; }
        #endregion

        #region UA Email Address
        public string UAEmailAddress { get; set; }

        public string UAEmailAddress_Status { get; set; }

        public int? UAEmailAddress_AttributeId { get; set; }
        #endregion

        #region UA Email Address Box

        public string UAEmailAddressBox { get; set; }

        public string UAEmailAddressBox_Status { get; set; }

        public int? UAEmailAddressBox_AttributeId { get; set; }

        #endregion

        #region UA Email Address Domain

        public string UAEmailAddressDomain { get; set; }

        public string UAEmailAddressDomain_Status { get; set; }

        public int? UAEmailAddressDomain_AttributeId { get; set; }

        #endregion

        #region  UA Email Address Box Is Good

        public string UAEmailAddressBoxIsGood { get; set; }

        public string UAEmailAddressBoxIsGood_Status { get; set; }

        public int? UAEmailAddressBoxIsGood_AttributeId { get; set; }
        #endregion

        #region UDP
        public string UDP { get; set; }

        public string UDP_Status { get; set; }

        public int? UDP_AttributeId { get; set; }
        #endregion

        #region Organization Name
        public string OrganizationName { get; set; }

        public string OrganizationName_Status { get; set; }

        public int? OrganizationName_AttributeId { get; set; }
        #endregion

        #region Organization Code
        public string OrganizationCode { get; set; }

        public string OrganizationCode_Status { get; set; }

        public int? OrganizationCode_AttributeId { get; set; }
        #endregion

        #region Organization Master Id
        public string OrganizationMasterId { get; set; }

        public string OrganizationMasterId_Status { get; set; }

        public int? OrganizationMasterId_AttributeId { get; set; }
        #endregion

        #region BirthDate
        public DateTime? BirthDate { get; set; }

        public string BirthDate_Status { get; set; }

        public int? BirthDate_AttributeId { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }

        public string MaritalStatus_Status { get; set; }

        public int? MaritalStatus_AttributeId { get; set; }
        #endregion

        #region DepartmentCode
        public string DepartmentCode { get; set; }

        public string DepartmentCode_Status { get; set; }

        public int? DepartmentCode_AttributeId { get; set; }
        #endregion

        #region DepartmentName
        public string DepartmentName { get; set; }

        public string DepartmentName_Status { get; set; }

        public int? DepartmentName_AttributeId { get; set; }
        #endregion

        #region DepartmentMasterId
        public string DepartmentMasterId { get; set; }

        public string DepartmentMasterId_Status { get; set; }

        public int? DepartmentMasterId_AttributeId { get; set; }
        #endregion

        #region PositionControlNumber
        public string PositionControlNumber { get; set; }

        public string PositionControlNumber_Status { get; set; }

        public int? PositionControlNumber_AttributeId { get; set; }
        #endregion

        #region EmployeeTitle
        public string EmployeeTitle { get; set; }

        public string EmployeeTitle_Status { get; set; }

        public int? EmployeeTitle_AttributeId { get; set; }
        #endregion

        #region ABORCode
        public string ABORCode { get; set; }

        public string ABORCode_Status { get; set; }

        public int? ABORCode_AttributeId { get; set; }
        #endregion

        #region EmployeeStatus
        public string EmployeeStatus { get; set; }

        public string EmployeeStatus_Status { get; set; }

        public int? EmployeeStatus_AttributeId { get; set; }
        #endregion

        #region FullOrPartTime
        public string FullOrPartTime { get; set; }

        public string FullOrPartTime_Status { get; set; }

        public int? FullOrPartTime_AttributeId { get; set; }
        #endregion

        #region FullTimeEquivalent
        [Column(TypeName = "decimal(18,2)")]
        public decimal? FullTimeEquivalent { get; set; }

        public string FullTimeEquivalent_Status { get; set; }

        public int? FullTimeEquivalent_AttributeId { get; set; }
        #endregion

        #region Salary
        [Column(TypeName = "decimal(18,2)")]
        public decimal? Salary { get; set; }

        public string Salary_Status { get; set; }

        public int? Salary_AttributeId { get; set; }
        #endregion

        #region PreferredEmailAddress
        public string PreferredEmailAddress { get; set; }

        public string PreferredEmailAddress_Status { get; set; }

        public int? PreferredEmailAddress_AttributeId { get; set; }
        #endregion

        #region Preferred Email Address Box

        public string PreferredEmailAddressBox { get; set; }

        public string PreferredEmailAddressBox_Status { get; set; }

        public int? PreferredEmailAddressBox_AttributeId { get; set; }

        #endregion

        #region Preferred Email Address Domain

        public string PreferredEmailAddressDomain { get; set; }

        public string PreferredEmailAddressDomain_Status { get; set; }

        public int? PreferredEmailAddressDomain_AttributeId { get; set; }

        #endregion

        #region Preferred Email Address Box Is Good

        public string PreferredEmailAddressBoxIsGood { get; set; }

        public string PreferredEmailAddressBoxIsGood_Status { get; set; }

        public int? PreferredEmailAddressBoxIsGood_AttributeId { get; set; }

        #endregion

        #region PreferredEmailAddressType
        public string PreferredEmailAddressType { get; set; }

        public string PreferredEmailAddressType_Status { get; set; }

        public int? PreferredEmailAddressType_AttributeId { get; set; }
        #endregion

        #region ManagerEmployeeId
        public string ManagerEmployeeId { get; set; }

        public string ManagerEmployeeId_Status { get; set; }

        public int? ManagerEmployeeId_AttributeId { get; set; }
        #endregion

        #region ManagerMasterId
        public string ManagerMasterId { get; set; }

        public string ManagerMasterId_Status { get; set; }

        public int? ManagerMasterId_AttributeId { get; set; }

        #endregion

        #region ManagerSystemRecordId
        public string ManagerSystemRecordId { get; set; }

        public string ManagerSystemRecordId_Status { get; set; }

        public int? ManagerSystemRecordId_AttributeId { get; set; }

        #endregion

        #region Preferred Name
        public string PreferredName { get; set; }

        public string PreferredName_Status { get; set; }

        public int? PreferredName_AttributeId { get; set; }

        #endregion
    }
}
