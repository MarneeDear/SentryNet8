using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementViewModel : BaseIntegrationViewModel
    {
        public StudentAcademicInvolvementViewModel() : base() { }

        public DateTime? IntegrationDate { get; set; }

        #region Student

        #region Student Name

        public string StudentName { get; set; }
        public string StudentName_BusinessName { get; set; }
        public string StudentName_BusinessDescription { get; set; }
        public int StudentName_AttributeId { get; set; }
        public string StudentName_OriginalValue { get; set; }
        public string StudentName_Status { get; set; }
        public string StudentName_Source { get; set; }
        public string StudentNameName { get; set; }
        public string StudentNameCode { get; set; }

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

        #endregion

        #region AcademicInvolvement

        #region AcademicYear

        public string AcademicInvolvementAcademicYear { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessName { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessDescription { get; set; }
        public int AcademicInvolvementAcademicYear_AttributeId { get; set; }
        public string AcademicInvolvementAcademicYear_OriginalValue { get; set; }
        public string AcademicInvolvementAcademicYear_Status { get; set; }
        public string AcademicInvolvementAcademicYear_Source { get; set; }

        #endregion

        #region Term

        public string AcademicInvolvementTerm { get; set; }
        public string AcademicInvolvementTerm_BusinessName { get; set; }
        public string AcademicInvolvementTerm_BusinessDescription { get; set; }
        public int AcademicInvolvementTerm_AttributeId { get; set; }
        public string AcademicInvolvementTerm_OriginalValue { get; set; }
        public string AcademicInvolvementTerm_Status { get; set; }
        public string AcademicInvolvementTerm_Source { get; set; }
        public string AcademicInvolvementTermName { get; set; }
        public string AcademicInvolvementTermCode { get; set; }

        #endregion

        #region Type

        public string AcademicInvolvementType { get; set; }
        public string AcademicInvolvementType_BusinessName { get; set; }
        public string AcademicInvolvementType_BusinessDescription { get; set; }
        public int AcademicInvolvementType_AttributeId { get; set; }
        public string AcademicInvolvementType_OriginalValue { get; set; }
        public string AcademicInvolvementType_Status { get; set; }
        public string AcademicInvolvementType_Source { get; set; }
        public string AcademicInvolvementTypeName { get; set; }
        public string AcademicInvolvementTypeCode { get; set; }

        #endregion

        #region Type Master Id

        public string AcademicInvolvementTypeMasterId { get; set; }
        public string AcademicInvolvementTypeMasterId_BusinessName { get; set; }
        public string AcademicInvolvementTypeMasterId_BusinessDescription { get; set; }
        public int AcademicInvolvementTypeMasterId_AttributeId { get; set; }
        public string AcademicInvolvementTypeMasterId_OriginalValue { get; set; }
        public string AcademicInvolvementTypeMasterId_Status { get; set; }
        public string AcademicInvolvementTypeMasterId_Source { get; set; }

        #endregion

        #region Name

        public string AcademicInvolvementName { get; set; }
        public string AcademicInvolvementName_BusinessName { get; set; }
        public string AcademicInvolvementName_BusinessDescription { get; set; }
        public int AcademicInvolvementName_AttributeId { get; set; }
        public string AcademicInvolvementName_OriginalValue { get; set; }
        public string AcademicInvolvementName_Status { get; set; }
        public string AcademicInvolvementName_Source { get; set; }
        public string AcademicInvolvementNameName { get; set; }
        public string AcademicInvolvementNameCode { get; set; }

        #endregion

        #region Name Master Id

        public string AcademicInvolvementNameMasterId { get; set; }
        public string AcademicInvolvementNameMasterId_BusinessName { get; set; }
        public string AcademicInvolvementNameMasterId_BusinessDescription { get; set; }
        public int AcademicInvolvementNameMasterId_AttributeId { get; set; }
        public string AcademicInvolvementNameMasterId_OriginalValue { get; set; }
        public string AcademicInvolvementNameMasterId_Status { get; set; }
        public string AcademicInvolvementNameMasterId_Source { get; set; }

        #endregion

        #region DropDowns

        public List<SelectListItem> StudentAcademicInvolvementTermList { get; set; }
        public List<SelectListItem> StudentAcademicInvolvementTypeList { get; set; }
        public List<SelectListItem> StudentAcademicInvolvementNameList { get; set; }

        #endregion

        public List<StudentAcademicInvolvementHistoryViewModel> HistoryData { get; set; }

        #endregion
    }
}
