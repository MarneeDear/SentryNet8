using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class StudentScholarshipDetail : BaseDetail
    {
        #region StudentId

        public string StudentId { get; set; }
        public string StudentId_Name { get; set; }
        public string StudentId_BusinessDescription { get; set; }
        public string StudentId_Source { get; set; }
        public string StudentId_Category { get; set; }
        public string StudentId_Status { get; set; }
        public int? StudentId_AttributeId { get; set; }

        #endregion

        #region StudentMasterId
        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public string StudentMasterId_Source { get; set; }
        public string StudentMasterId_Category { get; set; }
        public string StudentMasterId_Status { get; set; }
        public int? StudentMasterId_AttributeId { get; set; }

        #endregion

        #region AcademicCareerCode

        public string AcademicCareerCode { get; set; }
        public string AcademicCareerCode_Name { get; set; }
        public string AcademicCareerCode_BusinessDescription { get; set; }
        public string AcademicCareerCode_Source { get; set; }
        public string AcademicCareerCode_Category { get; set; }
        public string AcademicCareerCode_Status { get; set; }
        public int? AcademicCareerCode_AttributeId { get; set; }

        #endregion

        //#region TermCode

        //public string TermCode { get; set; }
        //public string TermCode_BusinessName { get; set; }
        //public string TermCode_BusinessDescription { get; set; }
        //public string TermCode_Source { get; set; }
        //public string TermCode_Category { get; set; }
        //public string TermCode_Status { get; set; }
        //public int? TermCode_AttributeId { get; set; }

        //#endregion

        //#region TermMasterId
        //public string TermMasterId { get; set; }
        //public string TermMasterId_BusinessName { get; set; }
        //public string TermMasterId_BusinessDescription { get; set; }
        //public string TermMasterId_Source { get; set; }
        //public string TermMasterId_Category { get; set; }
        //public string TermMasterId_Status { get; set; }
        //public int? TermMasterId_AttributeId { get; set; }
        //#endregion

        #region KFSAccount

        public string KFSAccount { get; set; }
        public string KFSAccount_BusinessName { get; set; }
        public string KFSAccount_BusinessDescription { get; set; }
        public string KFSAccount_Source { get; set; }
        public string KFSAccount_Category { get; set; }
        public string KFSAccount_Status { get; set; }
        public int? KFSAccount_AttributeId { get; set; }

        #endregion

        #region DesignationMasterId
        
        public string DesignationMasterId { get; set; }
        public string DesignationMasterId_BusinessName { get; set; }
        public string DesignationMasterId_BusinessDescription { get; set; }
        public string DesignationMasterId_Source { get; set; }
        public string DesignationMasterId_Category { get; set; }
        public string DesignationMasterId_Status { get; set; }
        public int? DesignationMasterId_AttributeId { get; set; }

        #endregion

        #region DepartmentCode
        
        public string DepartmentCode { get; set; }
        public string DepartmentCode_BusinessName { get; set; }
        public string DepartmentCode_BusinessDescription { get; set; }
        public string DepartmentCode_Source { get; set; }
        public string DepartmentCode_Category { get; set; }
        public string DepartmentCode_Status { get; set; }
        public int? DepartmentCode_AttributeId { get; set; }

        #endregion

        #region DepartmentMasterId
        
        public string DepartmentMasterId { get; set; }
        public string DepartmentMasterId_BusinessName { get; set; }
        public string DepartmentMasterId_BusinessDescription { get; set; }
        public string DepartmentMasterId_Source { get; set; }
        public string DepartmentMasterId_Category { get; set; }
        public string DepartmentMasterId_Status { get; set; }
        public int? DepartmentMasterId_AttributeId { get; set; }

        #endregion

        #region ScholarshipCode
        
        public string ScholarshipCode { get; set; }
        public string ScholarshipCode_BusinessName { get; set; }
        public string ScholarshipCode_BusinessDescription { get; set; }
        public string ScholarshipCode_Source { get; set; }
        public string ScholarshipCode_Category { get; set; }
        public string ScholarshipCode_Status { get; set; }
        public int? ScholarshipCode_AttributeId { get; set; }

        #endregion

        #region ScholarshipName
        
        public string ScholarshipName { get; set; }
        public string ScholarshipName_BusinessName { get; set; }
        public string ScholarshipName_BusinessDescription { get; set; }
        public string ScholarshipName_Source { get; set; }
        public string ScholarshipName_Category { get; set; }
        public string ScholarshipName_Status { get; set; }
        public int? ScholarshipName_AttributeId { get; set; }

        #endregion

        #region ScholarshipMasterId
        
        public string ScholarshipMasterId { get; set; }
        public string ScholarshipMasterId_BusinessName { get; set; }
        public string ScholarshipMasterId_BusinessDescription { get; set; }
        public string ScholarshipMasterId_Source { get; set; }
        public string ScholarshipMasterId_Category { get; set; }
        public string ScholarshipMasterId_Status { get; set; }
        public int? ScholarshipMasterId_AttributeId { get; set; }

        #endregion

        #region Amount

        [Column("Amount", TypeName = "decimal(18,2)")]
        public decimal? Amount { get; set; }
        public string Amount_Name { get; set; }
        public string Amount_BusinessDescription { get; set; }
        public string Amount_Source { get; set; }
        public string Amount_Category { get; set; }
        public string Amount_Status { get; set; }
        public int? Amount_AttributeId { get; set; }

        #endregion
    }
}
