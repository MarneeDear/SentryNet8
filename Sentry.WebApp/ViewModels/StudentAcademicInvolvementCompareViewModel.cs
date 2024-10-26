using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicInvolvementCompareViewModel
    {
        public DateTime? IntegrationDate { get; set; }

        public long? Id { get; set; }
        public int IntegrationId { get; set; }
        public int SystemId { get; set; }
        public string MasterId { get; set; }

        public string StudentName { get; set; }
        public string StudentId { get; set; }

        public string System { get; set; }

        #region Integration Record

        public string SourceRecordId { get; set; }

        #region AcademicYear
        public string AcademicInvolvementAcademicYear { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessName { get; set; }
        public string AcademicInvolvementAcademicYear_BusinessDescription { get; set; }
        #endregion

        #region Term
        public string AcademicInvolvementTerm { get; set; }
        public string AcademicInvolvementTerm_BusinessName { get; set; }
        public string AcademicInvolvementTerm_BusinessDescription { get; set; }
        #endregion

        #region Type
        public string AcademicInvolvementType { get; set; }
        public string AcademicInvolvementType_BusinessName { get; set; }
        public string AcademicInvolvementType_BusinessDescription { get; set; }
        #endregion

        #region Name
        public string AcademicInvolvementName { get; set; }
        public string AcademicInvolvementName_BusinessName { get; set; }
        public string AcademicInvolvementName_BusinessDescription { get; set; }
        #endregion

        #endregion

        #region Compare Record

        public string SourceRecordId_Compare { get; set; }

        public string AcademicInvolvementAcademicYear_Compare { get; set; }
        public string AcademicInvolvementTerm_Compare { get; set; }
        public string AcademicInvolvementType_Compare { get; set; }
        public string AcademicInvolvementName_Compare { get; set; }
        #endregion
    }
}
