using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    [Table("Students_Base", Schema = "Integration")]
    public class StudentHistory : BaseHistory
    {
        #region FirstName

        public string FirstName { get; set; }

        public string FirstName_Status { get; set; }

        public int? FirstName_AttributeId { get; set; }

        #endregion

        #region PreferredName

        public string PreferredName { get; set; }

        public string PreferredName_Status { get; set; }

        public int? PreferredName_AttributeId { get; set; }

        #endregion

        #region MiddleName

        public string MiddleName { get; set; }

        public string MiddleName_Status { get; set; }

        public int? MiddleName_AttributeId { get; set; }

        #endregion

        #region LastName

        public string LastName { get; set; }

        public string LastName_Status { get; set; }

        public int? LastName_AttributeId { get; set; }

        #endregion

        #region MaidenName

        public string MaidenName { get; set; }

        public string MaidenName_Status { get; set; }

        public int? MaidenName_AttributeId { get; set; }

        #endregion

        #region StudentId

        public string StudentId { get; set; }

        public string StudentId_Status { get; set; }

        public int? StudentId_AttributeId { get; set; }

        #endregion

        #region Title

        public string Title { get; set; }

        public string Title_Status { get; set; }

        public int? Title_AttributeId { get; set; }

        #endregion

        #region TitleSourceSystemRecordId

        public string TitleSourceSystemRecordId { get; set; }

        public string TitleSourceSystemRecordId_Status { get; set; }

        public int? TitleSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region TitleMasterId
        public string TitleMasterId { get; set; }

        public string TitleMasterId_Status { get; set; }

        public int? TitleMasterId_AttributeId { get; set; }

        #endregion

        #region Suffix

        public string Suffix { get; set; }

        public string Suffix_Status { get; set; }

        public int? Suffix_AttributeId { get; set; }

        #endregion

        #region SuffixSourceSystemRecordId

        public string SuffixSourceSystemRecordId { get; set; }

        public string SuffixSourceSystemRecordId_Status { get; set; }

        public int? SuffixSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region SuffixMasterId

        public string SuffixMasterId { get; set; }

        public string SuffixMasterId_Status { get; set; }

        public int? SuffixMasterId_AttributeId { get; set; }

        #endregion

        #region BirthDate

        public DateTime? BirthDate { get; set; }

        public string BirthDate_Status { get; set; }

        public int? BirthDate_AttributeId { get; set; }

        #endregion

        #region DeceasedDate

        public DateTime? DeceasedDate { get; set; }

        public string DeceasedDate_Status { get; set; }

        public int? DeceasedDate_AttributeId { get; set; }

        #endregion

        #region MaritalStatus

        public string MaritalStatus { get; set; }

        public string MaritalStatus_Status { get; set; }

        public int? MaritalStatus_AttributeId { get; set; }

        #endregion

        #region MaritalStatusCode

        public string MaritalStatusCode { get; set; }

        public string MaritalStatusCode_Status { get; set; }

        public int? MaritalStatusCode_AttributeId { get; set; }

        #endregion

        #region MartialStatusSourceSystemRecordId

        public string MaritalStatusSourceSystemRecordId { get; set; }

        public string MaritalStatusSourceSystemRecordId_Status { get; set; }

        public int? MaritalStatusSourceSystemRecordId_AttributeId { get; set; }

        #endregion

        #region MaritalStatusMasterId

        public string MaritalStatusMasterId { get; set; }

        public string MaritalStatusMasterId_Status { get; set; }

        public int? MaritalStatusMasterId_AttributeId { get; set; }

        #endregion

        #region FERPAInformationRelease

        public string FERPAInformationRelease { get; set; }

        public string FERPAInformationRelease_Status { get; set; }

        public int? FERPAInformationRelease_AttributeId { get; set; }

        #endregion

        #region CitizenshipCountryCode

        public string CitizenshipCountryCode { get; set; }

        public string CitizenshipCountryCode_Status { get; set; }

        public int? CitizenshipCountryCode_AttributeId { get; set; }

        #endregion

        #region CitizenshipCountryMasterId

        public string CitizenshipCountryMasterId { get; set; }

        public string CitizenshipCountryMasterId_Status { get; set; }

        public int? CitizenshipCountryMasterId_AttributeId { get; set; }

        #endregion

        #region Discharged

        public string Discharged { get; set; }

        public string Discharged_Status { get; set; }

        public int? Discharged_AttributeId { get; set; }

        #endregion

        #region DischargedYear

        public string DischargedYear { get; set; }

        public string DischargedYear_Status { get; set; }

        public int? DischargedYear_AttributeId { get; set; }

        #endregion

        #region DischargedTermCode

        public string DischargedTermCode { get; set; }

        public string DischargedTermCode_Status { get; set; }

        public int? DischargedTermCode_AttributeId { get; set; }

        #endregion

        #region DischargedTermMasterId

        public string DischargedTermMasterId { get; set; }

        public string DischargedTermMasterId_Status { get; set; }

        public int? DischargedTermMasterId_AttributeId { get; set; }

        #endregion
    }
}
