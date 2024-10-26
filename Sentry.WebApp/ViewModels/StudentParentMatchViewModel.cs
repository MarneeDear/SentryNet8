using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentParentMatchViewModel : BaseIntegrationViewModel
    {
        public StudentParentMatchViewModel() : base() { }

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

        #region LastName
        public string LastName { get; set; }
        public int? LastName_Weight { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public int? Suffix_Weight { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        #endregion

        #region StudentId
        public string StudentId { get; set; }
        public int? StudentId_Weight { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        #endregion

        #region StudentFirstName
        public string StudentFirstName { get; set; }
        public int? StudentFirstName_Weight { get; set; }
        public string StudentFirstName_BusinessName { get; set; }
        public string StudentFirstName_BusinessDescription { get; set; }
        #endregion

        #region StudentLastName
        public string StudentLastName { get; set; }
        public int? StudentLastName_Weight { get; set; }
        public string StudentLastName_BusinessName { get; set; }
        public string StudentLastName_BusinessDescription { get; set; }
        #endregion

        #region Relationship
        public string Relationship { get; set; }
        public int? Relationship_Weight { get; set; }
        public string Relationship_BusinessName { get; set; }
        public string Relationship_BusinessDescription { get; set; }
        #endregion

        #region Phone1Number
        public string Phone1Number { get; set; }
        public int? Phone1Number_Weight { get; set; }
        public string Phone1Number_BusinessName { get; set; }
        public string Phone1Number_BusinessDescription { get; set; }
        #endregion

        #region Phone1Extension
        public string Phone1Extension { get; set; }
        public int? Phone1Extension_Weight { get; set; }
        public string Phone1Extension_BusinessName { get; set; }
        public string Phone1Extension_BusinessDescription { get; set; }
        #endregion

        #region Phone1CountryCode
        public string Phone1CountryCode { get; set; }
        public int? Phone1CountryCode_Weight { get; set; }
        public string Phone1CountryCode_BusinessName { get; set; }
        public string Phone1CountryCode_BusinessDescription { get; set; }
        #endregion

        #region Phone2Number
        public string Phone2Number { get; set; }
        public int? Phone2Number_Weight { get; set; }
        public string Phone2Number_BusinessName { get; set; }
        public string Phone2Number_BusinessDescription { get; set; }
        #endregion

        #region Phone2Extension
        public string Phone2Extension { get; set; }
        public int? Phone2Extension_Weight { get; set; }
        public string Phone2Extension_BusinessName { get; set; }
        public string Phone2Extension_BusinessDescription { get; set; }
        #endregion

        #region Phone2CountryCode
        public string Phone2CountryCode { get; set; }
        public int? Phone2CountryCode_Weight { get; set; }
        public string Phone2CountryCode_BusinessName { get; set; }
        public string Phone2CountryCode_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public int? EmailAddress1_Weight { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public int? EmailAddress2_Weight { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        #endregion

        #region Address1
        public string Address1 { get; set; }
        public int? Address1_Weight { get; set; }
        public string Address1_BusinessName { get; set; }
        public string Address1_BusinessDescription { get; set; }
        #endregion

        # region City
        public string City { get; set; }
        public int? City_Weight { get; set; }
        public string City_BusinessName { get; set; }
        public string City_BusinessDescription { get; set; }
        #endregion 

        #region State
        public string State { get; set; }
        public int? State_Weight { get; set; }
        public string State_BusinessName { get; set; }
        public string State_BusinessDescription { get; set; }
        #endregion 

        #region PostalCode
        public string PostalCode { get; set; }
        public int? PostalCode_Weight { get; set; }
        public string PostalCode_BusinessName { get; set; }
        public string PostalCode_BusinessDescription { get; set; }
        #endregion 

        #region Country
        public string Country { get; set; }
        public int? Country_Weight { get; set; }
        public string Country_BusinessName { get; set; }
        public string Country_BusinessDescription { get; set; }
        #endregion 

        public List<StudentParentMatchSummaryViewModel> PossibleMatches { get; set; }

    }

    public class StudentParentMatchSummaryViewModel
    {
        public bool Selected { get; set; }
        public int MatchConfidence { get; set; }
        public string MasterId { get; set; }

        public string StudentParentTitle { get; set; }
        public string FirstName { get; set; }
        public string PreferredName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public string StudentId { get; set; }
        public string StudentFirstName { get; set; }
        public string StudentLastName { get; set; }
        public string Relationship { get; set; }
        public string Phone1Number { get; set; }
        public string Phone1Extension { get; set; }
        public string Phone1CountryCode { get; set; }
        public string Phone2Number { get; set; }
        public string Phone2Extension { get; set; }
        public string Phone2CountryCode { get; set; }
        public string EmailAddress1 { get; set; }
        public string EmailAddress2 { get; set; }

    }
}
