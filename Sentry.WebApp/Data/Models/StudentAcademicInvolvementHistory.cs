using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
    [Table("AcademicInvolvements_Base", Schema = "Integration")]
    public class StudentAcademicInvolvementHistory : BaseHistory
    {
        //public string StudentName { get; set; }
        //public string StudentName_Status { get; set; }

        public string StudentId { get; set; }
        public string StudentId_Status { get; set; }
        public string StudentMasterId { get; set; }
        public string StudentMasterId_Status { get; set; }

        public string AcademicYear { get; set; }
        public string AcademicYear_Status { get; set; }

        public string AcademicInvolvementName { get; set; }
        public string AcademicInvolvementName_Status { get; set; }

        public string AcademicInvolvementNameMasterId { get; set; }
        public string AcademicInvolvementNameMasterId_Status { get; set; }

        public string AcademicInvolvementType { get; set; }
        public string AcademicInvolvementType_Status { get; set; }

        public string AcademicInvolvementTypeMasterId { get; set; }
        public string AcademicInvolvementTypeMasterId_Status { get; set; }

        public string TermCode { get; set; }
        public string TermCode_Status { get; set; }

        public string Term { get; set; }
        public string Term_Status { get; set; }

    }
}
