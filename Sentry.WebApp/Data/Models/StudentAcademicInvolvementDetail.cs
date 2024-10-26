using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Data.Models
{
    public class StudentAcademicInvolvementDetail : BaseDetail
    {
        #region Student

        #region Student Id

        public string StudentId { get; set; }
        public string StudentId_BusinessName { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public int StudentId_AttributeId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentId_Source { get; set; }

        #endregion

        #region Student Master Id
        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public int StudentMasterId_AttributeId { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }

        #endregion

        #endregion

        #region AcademicInvolvement

        #region AcademicYear

        public string AcademicYear { get; set; }
        public string AcademicYear_BusinessName { get; set; }
        public string AcademicYear_BusinessDescription { get; set; }
        public int AcademicYear_AttributeId { get; set; }
        public string AcademicYear_Status { get; set; }
        public string AcademicYear_Source { get; set; }

        #endregion

        #region Term

        public string Term { get; set; }
        public string Term_BusinessName { get; set; }
        public string Term_BusinessDescription { get; set; }
        public int Term_AttributeId { get; set; }
        public string Term_Status { get; set; }
        public string Term_Source { get; set; }
        public string TermCode { get; set; }

        #endregion

        #region Type

        public string AcademicInvolvementType { get; set; }
        public string AcademicInvolvementType_BusinessName { get; set; }
        public string AcademicInvolvementType_BusinessDescription { get; set; }
        public int AcademicInvolvementType_AttributeId { get; set; }
        public string AcademicInvolvementType_Status { get; set; }
        public string AcademicInvolvementType_Source { get; set; }

        #endregion

        #region Type Master Id

        public string AcademicInvolvementTypeMasterId { get; set; }
        public string AcademicInvolvementTypeMasterId_BusinessName { get; set; }
        public string AcademicInvolvementTypeMasterId_BusinessDescription { get; set; }
        public int AcademicInvolvementTypeMasterId_AttributeId { get; set; }
        public string AcademicInvolvementTypeMasterId_Status { get; set; }
        public string AcademicInvolvementTypeMasterId_Source { get; set; }

        #endregion

        #region Name

        public string AcademicInvolvementName { get; set; }
        public string AcademicInvolvementName_BusinessName { get; set; }
        public string AcademicInvolvementName_BusinessDescription { get; set; }
        public int AcademicInvolvementName_AttributeId { get; set; }
        public string AcademicInvolvementName_Status { get; set; }
        public string AcademicInvolvementName_Source { get; set; }

        #endregion

        #region Name Master Id

        public string AcademicInvolvementNameMasterId { get; set; }
        public string AcademicInvolvementNameMasterId_BusinessName { get; set; }
        public string AcademicInvolvementNameMasterId_BusinessDescription { get; set; }
        public int AcademicInvolvementNameMasterId_AttributeId { get; set; }
        public string AcademicInvolvementNameMasterId_Status { get; set; }
        public string AcademicInvolvementNameMasterId_Source { get; set; }

        #endregion

        #endregion
    }
}
