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
    public class EmployeeCompareViewModel : BaseDetail
    {
        //public DateTime? IntegrationDate { get; set; }

        //public long Id { get; set; }
        //public int IntegrationId { get; set; }
        //public int SystemId { get; set; }
        public string MasterId { get; set; }
        public string System { get; set; }
        //public string SourceRecordId { get; set; }
        public string SourceRecordId_Compare { get; set; }

        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_Compare { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public bool FirstName_IsDifferent { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_Compare { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public bool PreferredName_IsDifferent { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_Compare { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public bool MiddleName_IsDifferent { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_Compare { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IsDifferent { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_Compare { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public bool MaidenName_IsDifferent { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_Compare { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public bool UAPersonId_IsDifferent { get; set; }
        #endregion

        #region EmployeeTitle
        public string EmployeeTitle { get; set; }
        public string EmployeeTitle_Compare { get; set; }
        public string EmployeeTitle_BusinessName { get; set; }
        public string EmployeeTitle_BusinessDescription { get; set; }
        public bool EmployeeTitle_IsDifferent { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_Compare { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IsDifferent { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public string BirthDate_Compare { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public bool BirthDate_IsDifferent { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_Compare { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public bool DeceasedDate_IsDifferent { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_Compare { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public bool MaritalStatus_IsDifferent { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_Compare { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public bool EmailAddress1_IsDifferent { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public string EmailAddress1MasterRecordId_Compare { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        public bool EmailAddress1MasterRecordId_IsDifferent { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_Compare { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public bool EmailAddress2_IsDifferent { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public string EmailAddress2MasterRecordId_Compare { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        public bool EmailAddress2MasterRecordId_IsDifferent { get; set; }
        #endregion

        #region NetId
        public string NetId { get; set; }
        public string NetId_Compare { get; set; }
        public string NetId_BusinessName { get; set; }
        public string NetId_BusinessDescription { get; set; }
        public bool NetId_IsDifferent { get; set; }

        #endregion

        #region OrganizationName
        public string OrganizationName { get; set; }
        public string OrganizationName_Compare { get; set; }
        public string OrganizationName_BusinessName { get; set; }
        public string OrganizationName_BusinessDescription { get; set; }
        public bool OrganizationName_IsDifferent { get; set; }
        #endregion

        #region HireDate
        public string HireDate { get; set; }
        public string HireDate_Compare { get; set; }
        public string HireDate_BusinessName { get; set; }
        public string HireDate_BusinessDescription { get; set; }
        public bool HireDate_IsDifferent { get; set; }
        #endregion

        #region TerminationDate
        public string TerminationDate { get; set; }
        public string TerminationDate_Compare { get; set; }
        public string TerminationDate_BusinessName { get; set; }
        public string TerminationDate_BusinessDescription { get; set; }
        public bool TerminationDate_IsDifferent { get; set; }
        #endregion

        #region EmployeeType
        public string EmployeeType { get; set; }
        public string EmployeeType_Compare { get; set; }
        public string EmployeeType_BusinessName { get; set; }
        public string EmployeeType_BusinessDescription { get; set; }
        public bool EmployeeType_IsDifferent { get; set; }
        #endregion

        public IEnumerable<SystemRecord> SystemRecords { get; set; }

    }
}
