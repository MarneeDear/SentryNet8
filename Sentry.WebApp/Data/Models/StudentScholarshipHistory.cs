using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Sentry.WebApp.Data.Models
{
    [Table("Scholarships_Base", Schema = "Integration")]
    public class StudentScholarshipHistory : BaseHistory
    {
        #region Student

        [Column("StudentId")]
        public string StudentId { get; set; }
        [Column("StudentId_Status")]
        public string StudentId_Status { get; set; }

        [Column("StudentMasterId")]
        public string StudentMasterId { get; set; }
        [Column("StudentMasterId_Status")]
        public string StudentMasterId_Status { get; set; }

        #endregion

        #region Academic Year

        [Column("AcademicYear")]
        public string AcademicYear { get; set; }
        [Column("AcademicYear_Status")]
        public string AcademicYear_Status { get; set; }

        #endregion

        #region Awarded Term

        [Column("TermCode")]
        public string ScholarshipTermCode { get; set; }
        [Column("TermCode_Status")]
        public string ScholarshipTermCode_Status { get; set; }

        [Column("TermMasterId")]
        public string ScholarshipTermMasterId { get; set; }
        [Column("TermMasterId_Status")]
        public string ScholarshipTermMasterId_Status { get; set; }

        #endregion

        #region Designation

        [Column("KFSAccount")]
        public string ScholarshipKFSAccount { get; set; }
        [Column("KFSAccount_Status")]
        public string ScholarshipKFSAccount_Status { get; set; }
        [Column("DesignationMasterId")]
        public string ScholarshipDesignationMasterId { get; set; }
        [Column("DesignationMasterId_Status")]
        public string ScholarshipDesignationMasterId_Status { get; set; }

        #endregion

        #region Department

        [Column("DepartmentCode")]
        public string ScholarshipDepartmentCode { get; set; }
        [Column("DepartmentCode_Status")]
        public string ScholarshipDepartmentCode_Status { get; set; }

        [Column("DepartmentMasterId")]
        public string ScholarshipDepartmentMasterId { get; set; }
        [Column("DepartmentMasterId_Status")]
        public string ScholarshipDepartmentMasterId_Status { get; set; }

        #endregion

        #region Scholarship

        [Column("ScholarshipCode")]
        public string ScholarshipCode { get; set; }
        [Column("ScholarshipCode_Status")]
        public string ScholarshipCode_Status { get; set; }

        [Column("ScholarshipName")]
        public string ScholarshipName { get; set; }
        [Column("ScholarshipName_Status")]
        public string ScholarshipName_Status { get; set; }

        [Column("ScholarshipMasterId")]
        public string ScholarshipMasterId { get; set; }
        [Column("ScholarshipMasterId_Status")]
        public string ScholarshipMasterId_Status { get; set; }

        #endregion

        #region Amount

        [Column("Amount", TypeName = "decimal(18,2)")]
        public decimal? ScholarshipAmount { get; set; }
        [Column("Amount_Status")]
        public string ScholarshipAmount_Status { get; set; }
        
        #endregion
    }
}
