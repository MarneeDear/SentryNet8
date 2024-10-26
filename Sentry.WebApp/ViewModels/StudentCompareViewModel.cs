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
    public class StudentCompareViewModel : BaseDetail
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

        #region StudentId
        public string StudentId { get; set; }
        public string StudentId_Compare { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public bool StudentId_IsDifferent { get; set; }
        #endregion

        #region StudentTitle
        public string StudentTitle { get; set; }
        public string StudentTitle_Compare { get; set; }
        public string StudentTitle_BusinessName { get; set; }
        public string StudentTitle_BusinessDescription { get; set; }
        public bool StudentTitle_IsDifferent { get; set; }
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

        #region InformationReleaseStatus
        public string InformationReleaseStatus { get; set; }
        public string InformationReleaseStatus_Compare { get; set; }
        public string InformationReleaseStatus_BusinessName { get; set; }
        public string InformationReleaseStatus_BusinessDescription { get; set; }
        public bool InformationReleaseStatus_IsDifferent { get; set; }
        #endregion

        #region CitizenshipCountry
        public string CitizenshipCountry { get; set; }
        public string CitizenshipCountry_Compare { get; set; }
        public string CitizenshipCountry_BusinessName { get; set; }
        public string CitizenshipCountry_BusinessDescription { get; set; }
        public bool CitizenshipCountry_IsDifferent { get; set; }
        #endregion

        #region Discharged
        public string Discharged { get; set; }
        public string Discharged_Compare { get; set; }
        public string Discharged_BusinessName { get; set; }
        public string Discharged_BusinessDescription { get; set; }
        public bool Discharged_IsDifferent { get; set; }
        #endregion

        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public string AcademicCareerName_Compare { get; set; }
        public string AcademicCareerName_BusinessName { get; set; }
        public string AcademicCareerName_BusinessDescription { get; set; }
        public bool AcademicCareerName_IsDifferent { get; set; }
        #endregion

        #region DischargedTerm
        public string DischargedTerm { get; set; }
        public string DischargedTerm_Compare { get; set; }
        public string DischargedTerm_BusinessName { get; set; }
        public string DischargedTerm_BusinessDescription { get; set; }
        public bool DischargedTerm_IsDifferent { get; set; }
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

        public List<SystemRecord> SystemRecords { get; set; }
    }
}
