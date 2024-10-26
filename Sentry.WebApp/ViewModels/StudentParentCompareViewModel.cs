using System.Collections.Generic;
using Sentry.WebApp.Data.Models;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentCompareViewModel : BaseDetail
    {
        public string MasterId { get; set; }
        public string System { get; set; }

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

        #region LastName
        public string LastName { get; set; }
        public string LastName_Compare { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IsDifferent { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_Compare { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IsDifferent { get; set; }
        #endregion

        #region StudentId
        public string StudentId { get; set; }
        public string StudentId_Compare { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public bool StudentId_IsDifferent { get; set; }
        #endregion

        #region StudentFirstName
        public string StudentFirstName { get; set; }
        public string StudentFirstName_Compare { get; set; }
        public string StudentFirstName_BusinessName { get; set; }
        public string StudentFirstName_BusinessDescription { get; set; }
        public bool StudentFirstName_IsDifferent { get; set; }
        #endregion

        #region StudentLastName
        public string StudentLastName { get; set; }
        public string StudentLastName_Compare { get; set; }
        public string StudentLastName_BusinessName { get; set; }
        public string StudentLastName_BusinessDescription { get; set; }
        public bool StudentLastName_IsDifferent { get; set; }
        #endregion

        #region Relationship
        public string Relationship { get; set; }
        public string Relationship_Compare { get; set; }
        public string Relationship_BusinessName { get; set; }
        public string Relationship_BusinessDescription { get; set; }
        public bool Relationship_IsDifferent { get; set; }
        #endregion

        #region Phone1Number
        public string Phone1Number { get; set; }
        public string Phone1Number_Compare { get; set; }
        public string Phone1Number_BusinessName { get; set; }
        public string Phone1Number_BusinessDescription { get; set; }
        public bool Phone1Number_IsDifferent { get; set; }
        #endregion

        #region Phone1Extension
        public string Phone1Extension { get; set; }
        public string Phone1Extension_Compare { get; set; }
        public string Phone1Extension_BusinessName { get; set; }
        public string Phone1Extension_BusinessDescription { get; set; }
        public bool Phone1Extension_IsDifferent { get; set; }
        #endregion

        #region Phone1CountryCode
        public string Phone1CountryCode { get; set; }
        public string Phone1CountryCode_Compare { get; set; }
        public string Phone1CountryCode_BusinessName { get; set; }
        public string Phone1CountryCode_BusinessDescription { get; set; }
        public bool Phone1CountryCode_IsDifferent { get; set; }
        #endregion

        #region Phone2Number
        public string Phone2Number { get; set; }
        public string Phone2Number_Compare { get; set; }
        public string Phone2Number_BusinessName { get; set; }
        public string Phone2Number_BusinessDescription { get; set; }
        public bool Phone2Number_IsDifferent { get; set; }
        #endregion

        #region Phone2Extension
        public string Phone2Extension { get; set; }
        public string Phone2Extension_Compare { get; set; }
        public string Phone2Extension_BusinessName { get; set; }
        public string Phone2Extension_BusinessDescription { get; set; }
        public bool Phone2Extension_IsDifferent { get; set; }
        #endregion

        #region Phone2CountryCode
        public string Phone2CountryCode { get; set; }
        public string Phone2CountryCode_Compare { get; set; }
        public string Phone2CountryCode_BusinessName { get; set; }
        public string Phone2CountryCode_BusinessDescription { get; set; }
        public bool Phone2CountryCode_IsDifferent { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_Compare { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public bool EmailAddress1_IsDifferent { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_Compare { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public bool EmailAddress2_IsDifferent { get; set; }
        #endregion

        #region Address1
        public string Address1 { get; set; }
        public string Address1_Compare { get; set; }
        public string Address1_BusinessName { get; set; }
        public string Address1_BusinessDescription { get; set; }
        public bool Address1_IsDifferent { get; set; }
        #endregion

        #region PostalCode
        public string PostalCode { get; set; }
        public string PostalCode_Compare { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        public bool PostalCode_IsDifferent { get; set; }
        #endregion

        #region City
        public string City { get; set; }
        public string City_Compare { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        public bool City_IsDifferent { get; set; }
        #endregion

        #region State
        public string State { get; set; }
        public string State_Compare { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        public bool State_IsDifferent { get; set; }
        #endregion

        #region Country
        public string Country { get; set; }
        public string Country_Compare { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        public bool Country_IsDifferent { get; set; }
        #endregion


        public List<SystemRecord> SystemRecords { get; set; }

    }
}
