using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        #region Student Name

        public string StudentName { get; set; }
        public string StudentName_BusinessName { get; set; }
        public string StudentName_BusinessDescription { get; set; }
        public int StudentName_AttributeId { get; set; }
        public string StudentName_OriginalValue { get; set; }
        public string StudentName_Status { get; set; }
        public string StudentName_Source { get; set; }

        #endregion

        #region Student Id

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public int StudentId_AttributeId { get; set; }
        public string StudentId_OriginalValue { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_Source { get; set; }

        #endregion

        #region Student Master Id

        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public int StudentMasterId_AttributeId { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }

        #endregion

        #region AcademicYear

        public string AcademicInvolvementAcademicYear { get; set; }
        public string AcademicInvolvementAcademicYear_OriginalValue { get; set; }
        public string AcademicInvolvementAcademicYear_Status { get; set; }

        #endregion

        #region Term

        public string AcademicInvolvementTerm { get; set; }
        public string AcademicInvolvementTerm_OriginalValue { get; set; }
        public string AcademicInvolvementTerm_Status { get; set; }

        #endregion

        #region Type

        public string AcademicInvolvementType { get; set; }
        public string AcademicInvolvementType_OriginalValue { get; set; }
        public string AcademicInvolvementType_Status { get; set; }

        #endregion

        #region Name

        public string AcademicInvolvementName { get; set; }
        public string AcademicInvolvementName_OriginalValue { get; set; }
        public string AcademicInvolvementName_Status { get; set; }

        #endregion
    }
}
