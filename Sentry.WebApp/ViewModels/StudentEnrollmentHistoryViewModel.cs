using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentEnrollmentHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        #region StudentId
        public string StudentId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_OriginalValue { get; set; }
        #endregion

        #region StudentMasterId
        public string StudentMasterId { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        #endregion

        #region TermCode
        public string TermCode { get; set; }
        public string TermCode_Status { get; set; }
        public string TermCode_OriginalValue { get; set; }
        #endregion

        #region TermName
        public string TermName { get; set; }
        public string TermName_Status { get; set; }
        public string TermName_OriginalValue { get; set; }
        #endregion

        #region TermMasterId
        public string TermMasterId { get; set; }
        public string TermMasterId_Status { get; set; }
        public string TermMasterId_OriginalValue { get; set; }
        #endregion

        #region CampusName
        public string CampusName { get; set; }
        public string CampusName_Status { get; set; }
        public string CampusName_OriginalValue { get; set; }
        #endregion

        #region CampusSourceSystemRecordId
        public string CampusSourceSystemRecordId { get; set; }
        public string CampusSourceSystemRecordId_Status { get; set; }
        public string CampusSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region CampusMasterId
        public string CampusMasterId { get; set; }
        public string CampusMasterId_Status { get; set; }
        public string CampusMasterId_OriginalValue { get; set; }
        #endregion

        #region AcademicCareerName
        public string AcademicCareerName { get; set; }
        public string AcademicCareerName_Status { get; set; }
        public string AcademicCareerName_OriginalValue { get; set; }
        #endregion

        #region AcademicCareerSourceSystemRecordId
        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }
        public string AcademicCareerSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region AcademicCareerMasterId
        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }
        public string AcademicCareerMasterId_OriginalValue { get; set; }
        #endregion

        #region AcademicLevelName
        public string AcademicLevelName { get; set; }
        public string AcademicLevelName_Status { get; set; }
        public string AcademicLevelName_OriginalValue { get; set; }
        #endregion

        #region AcademicLevelSourceSystemRecordId
        public string AcademicLevelSourceSystemRecordId { get; set; }
        public string AcademicLevelSourceSystemRecordId_Status { get; set; }
        public string AcademicLevelSourceSystemRecordId_OriginalValue { get; set; }
        #endregion

        #region AcademicLevelMasterId
        public string AcademicLevelMasterId { get; set; }
        public string AcademicLevelMasterId_Status { get; set; }
        public string AcademicLevelMasterId_OriginalValue { get; set; }
        #endregion

        #region TotalTransferUnits
        public string TotalTransferUnits { get; set; }
        public string TotalTransferUnits_Status { get; set; }
        public string TotalTransferUnits_OriginalValue { get; set; }
        #endregion

        #region TotalCumulativeUnits
        public string TotalCumulativeUnits { get; set; }
        public string TotalCumulativeUnits_Status { get; set; }
        public string TotalCumulativeUnits_OriginalValue { get; set; }
        #endregion
    }
}
