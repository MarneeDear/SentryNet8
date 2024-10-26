using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentScholarshipHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        #region Student

        public string StudentId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_OriginalValue { get; set; }

        public string StudentMasterId { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }

        #endregion

        #region Academic Year

        public string ScholarshipAcademicYear { get; set; }
        public string ScholarshipAcademicYear_Status { get; set; }
        public string ScholarshipAcademicYear_OriginalValue { get; set; }

        #endregion

        #region Awarded Term

        public string TermCode { get; set; }
        public string TermCode_Status { get; set; }
        public string TermCode_OriginalValue { get; set; }

        public string TermMasterId { get; set; }
        public string TermMasterId_Status { get; set; }
        public string TermMasterId_OriginalValue { get; set; }

        #endregion

        #region Designation

        public string ScholarshipKFSAccount { get; set; }
        public string ScholarshipKFSAccount_Status { get; set; }
        public string ScholarshipKFSAccount_OriginalValue { get; set; }

        public string ScholarshipDesignationMasterId { get; set; }
        public string ScholarshipDesignationMasterId_Status { get; set; }
        public string ScholarshipDesignationMasterId_OriginalValue { get; set; }

        #endregion

        #region Department

        public string ScholarshipDepartmentCode { get; set; }
        public string ScholarshipDepartmentCode_Status { get; set; }
        public string ScholarshipDepartmentCode_OriginalValue { get; set; }

        public string ScholarshipDepartmentMasterId { get; set; }
        public string ScholarshipDepartmentMasterId_Status { get; set; }
        public string ScholarshipDepartmentMasterId_OriginalValue { get; set; }

        #endregion

        #region Scholarship

        public string ScholarshipCode { get; set; }
        public string ScholarshipCode_Status { get; set; }
        public string ScholarshipCode_OriginalValue { get; set; }

        public string ScholarshipName { get; set; }
        public string ScholarshipName_Status { get; set; }
        public string ScholarshipName_OriginalValue { get; set; }

        public string ScholarshipMasterId { get; set; }
        public string ScholarshipMasterId_Status { get; set; }
        public string ScholarshipMasterId_OriginalValue { get; set; }

        #endregion

        #region Amount

        public decimal? ScholarshipAmount { get; set; }
        public string ScholarshipAmount_Status { get; set; }
        public decimal? ScholarshipAmount_OriginalValue { get; set; }

        #endregion
    }
}
