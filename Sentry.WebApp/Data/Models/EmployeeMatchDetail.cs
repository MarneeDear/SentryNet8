using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class EmployeeMatchDetail : BaseDetail
    {
        #region FirstName
        public string FirstName { get; set; }
        public string FirstName_BusinessName { get; set; }
        public string FirstName_BusinessDescription { get; set; }
        public bool FirstName_IncludeInMatch { get; set; }
        public int FirstName_MatchWeight { get; set; }
        #endregion

        #region PreferredName
        public string PreferredName { get; set; }
        public string PreferredName_BusinessName { get; set; }
        public string PreferredName_BusinessDescription { get; set; }
        public bool PreferredName_IncludeInMatch { get; set; }
        public int PreferredName_MatchWeight { get; set; }
        #endregion

        #region MiddleName
        public string MiddleName { get; set; }
        public string MiddleName_BusinessName { get; set; }
        public string MiddleName_BusinessDescription { get; set; }
        public bool MiddleName_IncludeInMatch { get; set; }
        public int MiddleName_MatchWeight { get; set; }
        #endregion

        #region LastName
        public string LastName { get; set; }
        public string LastName_BusinessName { get; set; }
        public string LastName_BusinessDescription { get; set; }
        public bool LastName_IncludeInMatch { get; set; }
        public int LastName_MatchWeight { get; set; }
        #endregion

        #region MaidenName
        public string MaidenName { get; set; }
        public string MaidenName_BusinessName { get; set; }
        public string MaidenName_BusinessDescription { get; set; }
        public bool MaidenName_IncludeInMatch { get; set; }
        public int MaidenName_MatchWeight { get; set; }
        #endregion

        #region UAPersonId
        public string UAPersonId { get; set; }
        public string UAPersonId_BusinessName { get; set; }
        public string UAPersonId_BusinessDescription { get; set; }
        public bool UAPersonId_IncludeInMatch { get; set; }
        public int UAPersonId_MatchWeight { get; set; }
        #endregion

        #region Suffix
        public string Suffix { get; set; }
        public string Suffix_BusinessName { get; set; }
        public string Suffix_BusinessDescription { get; set; }
        public bool Suffix_IncludeInMatch { get; set; }
        public int Suffix_MatchWeight { get; set; }
        #endregion

        #region BirthDate
        public string BirthDate { get; set; }
        public string BirthDate_BusinessName { get; set; }
        public string BirthDate_BusinessDescription { get; set; }
        public bool BirthDate_IncludeInMatch { get; set; }
        public int BirthDate_MatchWeight { get; set; }
        #endregion

        #region DeceasedDate
        public string DeceasedDate { get; set; }
        public string DeceasedDate_BusinessName { get; set; }
        public string DeceasedDate_BusinessDescription { get; set; }
        public bool DeceasedDate_IncludeInMatch { get; set; }
        public int DeceasedDate_MatchWeight { get; set; }
        #endregion

        #region MaritalStatus
        public string MaritalStatus { get; set; }
        public string MaritalStatus_BusinessName { get; set; }
        public string MaritalStatus_BusinessDescription { get; set; }
        public bool MaritalStatus_IncludeInMatch { get; set; }
        public int MaritalStatus_MatchWeight { get; set; }
        #endregion

        #region EmailAddress1
        public string EmailAddress1 { get; set; }
        public string EmailAddress1_BusinessName { get; set; }
        public string EmailAddress1_BusinessDescription { get; set; }
        public bool EmailAddress1_IncludeInMatch { get; set; }
        public int EmailAddress1_MatchWeight { get; set; }
        #endregion

        #region EmailAddress2
        public string EmailAddress2 { get; set; }
        public string EmailAddress2_BusinessName { get; set; }
        public string EmailAddress2_BusinessDescription { get; set; }
        public bool EmailAddress2_IncludeInMatch { get; set; }
        public int EmailAddress2_MatchWeight { get; set; }
        #endregion

        #region NetId
        public string NetId { get; set; }
        public string NetId_BusinessName { get; set; }
        public string NetId_BusinessDescription { get; set; }
        public bool NetId_IncludeInMatch { get; set; }
        public int NetId_MatchWeight { get; set; }
        #endregion

        #region OrganizationName
        public string OrganizationName { get; set; }
        public string OrganizationName_BusinessName { get; set; }
        public string OrganizationName_BusinessDescription { get; set; }
        public bool OrganizationName_IncludeInMatch { get; set; }
        public int OrganizationName_MatchWeight { get; set; }
        #endregion

        #region HireDate
        public string HireDate { get; set; }
        public string HireDate_BusinessName { get; set; }
        public string HireDate_BusinessDescription { get; set; }
        public bool HireDate_IncludeInMatch { get; set; }
        public int HireDate_MatchWeight { get; set; }
        #endregion

        #region TerminationDate
        public string TerminationDate { get; set; }
        public string TerminationDate_BusinessName { get; set; }
        public string TerminationDate_BusinessDescription { get; set; }
        public bool TerminationDate_IncludeInMatch { get; set; }
        public int TerminationDate_MatchWeight { get; set; }
        #endregion

        #region EmployeeType
        public string EmployeeType { get; set; }
        public string EmployeeType_BusinessName { get; set; }
        public string EmployeeType_BusinessDescription { get; set; }
        public bool EmployeeType_IncludeInMatch { get; set; }
        public int EmployeeType_MatchWeight { get; set; }
        #endregion
    }
}
