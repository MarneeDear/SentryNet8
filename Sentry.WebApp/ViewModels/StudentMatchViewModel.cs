using Sentry.WebApp.Data.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentMatchViewModel : BaseIntegrationViewModel
    {
        public StudentMatchViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        #region FirstName
        public string FirstName { get; set; }
        public int FirstName_Weight { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public int PreferredName_Weight { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public int? MiddleName_Weight { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public int? LastName_Weight { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public int? MaidenName_Weight { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        #endregion

        #region StudentId
        public string StudentId { get; set; }
        public int? StudentId_Weight { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        #endregion

        #region CommonTitle
        public string CommonTitle { get; set; }
        public int? CommonTitle_Weight { get; set; }
        public string CommonTitle_BusinessName { get; set; }
        public string CommonTitle_BusinessDescription { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public int? Suffix_Weight { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        #endregion
        
        #region BirthDate
        public string BirthDate { get; set; }
        public int? BirthDate_Weight { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        #endregion
        
        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public int? DeceasedDate_Weight { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        #endregion       

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public int? MaritalStatus_Weight { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        #endregion

        #region InformationReleaseStatus
        public string InformationReleaseStatus { get; set; }
        public int? InformationReleaseStatus_Weight { get; set; }
        public string InformationReleaseStatus_BusinessName { get; set; }
        public string InformationReleaseStatus_BusinessDescription { get; set; }
        #endregion

        #region CitizenshipCountry
        public string CitizenshipCountry { get; set; }
        public int? CitizenshipCountry_Weight { get; set; }
        public string CitizenshipCountry_BusinessName { get; set; }
        public string CitizenshipCountry_BusinessDescription { get; set; }
        #endregion

        #region Discharged
        public string Discharged { get; set; }
        public int? Discharged_Weight { get; set; }
        public string Discharged_BusinessName { get; set; }
        public string Discharged_BusinessDescription { get; set; }
        #endregion

        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public int? AcademicCareerName_Weight { get; set; }
        public string AcademicCareerName_BusinessName { get; set; }
        public string AcademicCareerName_BusinessDescription { get; set; }
        #endregion

        #region DischargedTerm
        public string DischargedTerm { get; set; }
        public int? DischargedTerm_Weight { get; set; }
        public string DischargedTerm_BusinessName { get; set; }
        public string DischargedTerm_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public int? EmailAddress1_Weight { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress1MasterRecordId
        public string EmailAddress1MasterRecordId { get; set; }
        public int? EmailAddress1MasterRecordId_Weight { get; set; }
        public string EmailAddress1MasterRecordId_BusinessName { get; set; }
        public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public int? EmailAddress2_Weight { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress2MasterRecordId
        public string EmailAddress2MasterRecordId { get; set; }
        public int? EmailAddress2MasterRecordId_Weight { get; set; }
        public string EmailAddress2MasterRecordId_BusinessName { get; set; }
        public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }
        #endregion

        public List<StudentMatchSummaryViewModel> PossibleMatches { get; set; }

    }

    public class StudentMatchSummaryViewModel
    {
        public bool Selected { get; set; }
        public int MatchConfidence { get; set; }
        public string MasterId { get; set; }

        public string FirstName { get; set; }
        public string PreferredName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string StudentId { get; set; }
        public string BirthDate { get; set; }

    }
}
