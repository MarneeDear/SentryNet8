using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class EmployeeMatchViewModel : BaseIntegrationViewModel
    {

        public EmployeeMatchViewModel() : base() { }

        #region Integration Record

            public DateTime? IntegrationDate { get; set; }

            #region Bio/Dem Details

                //FirstName
                public string FirstName { get; set; }
                public int FirstName_Weight { get; set; }
                public string FirstName_BusinessName { get; set; }
                public string FirstName_BusinessDescription { get; set; }

                //PreferredName
                public string PreferredName { get; set; }
                public int PreferredName_Weight { get; set; }
                public string PreferredName_BusinessName { get; set; }
                public string PreferredName_BusinessDescription { get; set; }

                //MiddleName
                public string MiddleName { get; set; }
                public int MiddleName_Weight { get; set; }
                public string MiddleName_BusinessName { get; set; }
                public string MiddleName_BusinessDescription { get; set; }

                //LastName
                public string LastName { get; set; }
                public int LastName_Weight { get; set; }
                public string LastName_BusinessName { get; set; }
                public string LastName_BusinessDescription { get; set; }

                //MaidenName
                public string MaidenName { get; set; }
                public int MaidenName_Weight { get; set; }
                public string MaidenName_BusinessName { get; set; }
                public string MaidenName_BusinessDescription { get; set; }

                //Suffix
                public string Suffix { get; set; }
                public int Suffix_Weight { get; set; }
                public string Suffix_BusinessName { get; set; }
                public string Suffix_BusinessDescription { get; set; }

                //DateOfBirth
                public string DateOfBirth { get; set; }
                public int DateOfBirth_Weight { get; set; }
                public string DateOfBirth_BusinessName { get; set; }
                public string DateOfBirth_BusinessDescription { get; set; }

                //DeceasedDate
                public string DeceasedDate { get; set; }
                public int DeceasedDate_Weight { get; set; }
                public string DeceasedDate_BusinessName { get; set; }
                public string DeceasedDate_BusinessDescription { get; set; }

                //MaritalStatus
                public string MaritalStatus { get; set; }
                public int MaritalStatus_Weight { get; set; }
                public string MaritalStatus_BusinessName { get; set; }
                public string MaritalStatus_BusinessDescription { get; set; }

                //EmailAddress1
                public string EmailAddress1 { get; set; }
                public int EmailAddress1_Weight { get; set; }
                public string EmailAddress1_BusinessName { get; set; }
                public string EmailAddress1_BusinessDescription { get; set; }

                //EmailAddress1MasterRecordId
                public string EmailAddress1MasterRecordId { get; set; }
                public int EmailAddress1MasterRecordId_Weight { get; set; }
                public string EmailAddress1MasterRecordId_BusinessName { get; set; }
                public string EmailAddress1MasterRecordId_BusinessDescription { get; set; }

                //EmailAddress2
                public string EmailAddress2 { get; set; }
                public int EmailAddress2_Weight { get; set; }
                public string EmailAddress2_BusinessName { get; set; }
                public string EmailAddress2_BusinessDescription { get; set; }

                //EmailAddress2MasterRecordId
                public string EmailAddress2MasterRecordId { get; set; }
                public int EmailAddress2MasterRecordId_Weight { get; set; }
                public string EmailAddress2MasterRecordId_BusinessName { get; set; }
                public string EmailAddress2MasterRecordId_BusinessDescription { get; set; }

                //NetId
                public string NetId { get; set; }
                public int NetId_Weight { get; set; }
                public string NetId_BusinessName { get; set; }
                public string NetId_BusinessDescription { get; set; }


        #endregion

        #region Employee Details

        //UAPersonId
        public string UAPersonId { get; set; }
                public int UAPersonId_Weight { get; set; }
                public string UAPersonId_BusinessName { get; set; }
                public string UAPersonId_BusinessDescription { get; set; }

                //EmployeeTitle
                public string EmployeeTitle { get; set; }
                public int EmployeeTitle_Weight { get; set; }
                public string EmployeeTitle_BusinessName { get; set; }
                public string EmployeeTitle_BusinessDescription { get; set; }

                //HireDate
                public string HireDate { get; set; }
                public int HireDate_Weight { get; set; }
                public string HireDate_BusinessName { get; set; }
                public string HireDate_BusinessDescription { get; set; }

                //TerminationDate
                public string TerminationDate { get; set; }
                public int TerminationDate_Weight { get; set; }
                public string TerminationDate_BusinessName { get; set; }
                public string TerminationDate_BusinessDescription { get; set; }

                

        #endregion

        #region Employment Details

        //OrganizationName
        public string OrganizationName { get; set; }
                public int OrganizationName_Weight { get; set; }
                public string OrganizationName_BusinessName { get; set; }
                public string OrganizationName_BusinessDescription { get; set; }

                //EmployeeType
                public string EmployeeType { get; set; }
                public int EmployeeType_Weight { get; set; }
                public string EmployeeType_BusinessName { get; set; }
                public string EmployeeType_BusinessDescription { get; set; }

            #endregion

        #endregion
        
    }
}
