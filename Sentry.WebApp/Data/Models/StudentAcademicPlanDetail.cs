using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    public class StudentAcademicPlanDetail : BaseDetail
    {

        #region Student
        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        public string Student_Status { get; set; }
        public string Student_Source { get; set; }
        public string Student_Category { get; set; }
        public int? Student_AttributeId { get; set; }
        #endregion

        #region StudentMasterId
        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }
        public string StudentMasterId_Category { get; set; }
        public int? StudentMasterId_AttributeId { get; set; }
        #endregion



        //#region Enrollment
        //public string Enrollment { get; set; }
        //public string Enrollment_BusinessName { get; set; }
        //public string Enrollment_BusinessDescription { get; set; }
        //public string Enrollment_Status { get; set; }
        //public string Enrollment_Source { get; set; }
        //public string Enrollment_Category { get; set; }
        //public int? Enrollment_AttributeId { get; set; }
        //#endregion

        //#region EnrollmentSourceSystemRecordId
        //public string EnrollmentSourceSystemRecordId { get; set; }
        //public string EnrollmentSourceSystemRecordId_BusinessName { get; set; }
        //public string EnrollmentSourceSystemRecordId_BusinessDescription { get; set; }
        //public string EnrollmentSourceSystemRecordId_Status { get; set; }
        //public string EnrollmentSourceSystemRecordId_Source { get; set; }
        //public string EnrollmentSourceSystemRecordId_Category { get; set; }
        //public int? EnrollmentSourceSystemRecordId_AttributeId { get; set; }
        //#endregion

        //#region EnrollmentMasterId
        //public string EnrollmentMasterId { get; set; }
        //public string EnrollmentMasterId_BusinessName { get; set; }
        //public string EnrollmentMasterId_BusinessDescription { get; set; }
        //public string EnrollmentMasterId_Status { get; set; }
        //public string EnrollmentMasterId_Source { get; set; }
        //public string EnrollmentMasterId_Category { get; set; }
        //public int? EnrollmentMasterId_AttributeId { get; set; }
        //#endregion



        #region AcademicCareer
        public string AcademicCareer { get; set; }
        public string AcademicCareer_BusinessName { get; set; }
        public string AcademicCareer_BusinessDescription { get; set; }
        public string AcademicCareer_Status { get; set; }
        public string AcademicCareer_Source { get; set; }
        public string AcademicCareer_Category { get; set; }
        public int? AcademicCareer_AttributeId { get; set; }
        #endregion

        #region AcademicCareerSourceSystemRecordId
        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }
        public string AcademicCareerSourceSystemRecordId_Source { get; set; }
        public string AcademicCareerSourceSystemRecordId_Category { get; set; }
        public int? AcademicCareerSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AcademicCareerMasterId
        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_BusinessName { get; set; }
        public string AcademicCareerMasterId_BusinessDescription { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }
        public string AcademicCareerMasterId_Source { get; set; }
        public string AcademicCareerMasterId_Category { get; set; }
        public int? AcademicCareerMasterId_AttributeId { get; set; }
        #endregion



        #region Term
        public string Term { get; set; }
        public string Term_BusinessName { get; set; }
        public string Term_BusinessDescription { get; set; }
        public string Term_Status { get; set; }
        public string Term_Source { get; set; }
        public string Term_Category { get; set; }
        public int? Term_AttributeId { get; set; }
        #endregion

        #region TermSourceSystemRecordId
        public string TermSourceSystemRecordId { get; set; }
        public string TermSourceSystemRecordId_BusinessName { get; set; }
        public string TermSourceSystemRecordId_BusinessDescription { get; set; }
        public string TermSourceSystemRecordId_Status { get; set; }
        public string TermSourceSystemRecordId_Source { get; set; }
        public string TermSourceSystemRecordId_Category { get; set; }
        public int? TermSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region TermMasterId
        public string TermMasterId { get; set; }
        public string TermMasterId_BusinessName { get; set; }
        public string TermMasterId_BusinessDescription { get; set; }
        public string TermMasterId_Status { get; set; }
        public string TermMasterId_Source { get; set; }
        public string TermMasterId_Category { get; set; }
        public int? TermMasterId_AttributeId { get; set; }
        #endregion



        //#region CampusName
        //public string CampusName { get; set; }
        //public string CampusName_BusinessName { get; set; }
        //public string CampusName_BusinessDescription { get; set; }
        //public string CampusName_Status { get; set; }
        //public string CampusName_Source { get; set; }
        //public string CampusName_Category { get; set; }
        //public int? CampusName_AttributeId { get; set; }
        //#endregion

        #region CampusSourceSystemRecordId
        public string CampusSourceSystemRecordId { get; set; }
        public string CampusSourceSystemRecordId_BusinessName { get; set; }
        public string CampusSourceSystemRecordId_BusinessDescription { get; set; }
        public string CampusSourceSystemRecordId_Status { get; set; }
        public string CampusSourceSystemRecordId_Source { get; set; }
        public string CampusSourceSystemRecordId_Category { get; set; }
        public int? CampusSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region CampusMasterId
        public string CampusMasterId { get; set; }
        public string CampusMasterId_BusinessName { get; set; }
        public string CampusMasterId_BusinessDescription { get; set; }
        public string CampusMasterId_Status { get; set; }
        public string CampusMasterId_Source { get; set; }
        public string CampusMasterId_Category { get; set; }
        public int? CampusMasterId_AttributeId { get; set; }
        #endregion



        #region Degree
        public string Degree { get; set; }
        public string Degree_BusinessName { get; set; }
        public string Degree_BusinessDescription { get; set; }
        public string Degree_Status { get; set; }
        public string Degree_Source { get; set; }
        public string Degree_Category { get; set; }
        public int? Degree_AttributeId { get; set; }
        #endregion

        #region DegreeSourceSystemRecordId
        public string DegreeSourceSystemRecordId { get; set; }
        public string DegreeSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeSourceSystemRecordId_BusinessDescription { get; set; }
        public string DegreeSourceSystemRecordId_Status { get; set; }
        public string DegreeSourceSystemRecordId_Source { get; set; }
        public string DegreeSourceSystemRecordId_Category { get; set; }
        public int? DegreeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region DegreeMasterId
        public string DegreeMasterId { get; set; }
        public string DegreeMasterId_BusinessName { get; set; }
        public string DegreeMasterId_BusinessDescription { get; set; }
        public string DegreeMasterId_Status { get; set; }
        public string DegreeMasterId_Source { get; set; }
        public string DegreeMasterId_Category { get; set; }
        public int? DegreeMasterId_AttributeId { get; set; }
        #endregion



        #region AcademicPlan
        public string AcademicPlan { get; set; }
        public string AcademicPlan_BusinessName { get; set; }
        public string AcademicPlan_BusinessDescription { get; set; }
        public string AcademicPlan_Status { get; set; }
        public string AcademicPlan_Source { get; set; }
        public string AcademicPlan_Category { get; set; }
        public int? AcademicPlan_AttributeId { get; set; }
        #endregion

        #region AcademicPlanSourceSystemRecordId
        public string AcademicPlanSourceSystemRecordId { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicPlanSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicPlanSourceSystemRecordId_Status { get; set; }
        public string AcademicPlanSourceSystemRecordId_Source { get; set; }
        public string AcademicPlanSourceSystemRecordId_Category { get; set; }
        public int? AcademicPlanSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AcademicPlanMasterId
        public string AcademicPlanMasterId { get; set; }
        public string AcademicPlanMasterId_BusinessName { get; set; }
        public string AcademicPlanMasterId_BusinessDescription { get; set; }
        public string AcademicPlanMasterId_Status { get; set; }
        public string AcademicPlanMasterId_Source { get; set; }
        public string AcademicPlanMasterId_Category { get; set; }
        public int? AcademicPlanMasterId_AttributeId { get; set; }
        #endregion



        #region AcademicCatalog
        public string AcademicCatalog { get; set; }
        public string AcademicCatalog_BusinessName { get; set; }
        public string AcademicCatalog_BusinessDescription { get; set; }
        public string AcademicCatalog_Status { get; set; }
        public string AcademicCatalog_Source { get; set; }
        public string AcademicCatalog_Category { get; set; }
        public int? AcademicCatalog_AttributeId { get; set; }
        #endregion

        #region AcademicCatalogSourceSystemRecordId
        public string AcademicCatalogSourceSystemRecordId { get; set; }
        public string AcademicCatalogSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCatalogSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Status { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Source { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Category { get; set; }
        public int? AcademicCatalogSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AcademicCatalogMasterId
        public string AcademicCatalogMasterId { get; set; }
        public string AcademicCatalogMasterId_BusinessName { get; set; }
        public string AcademicCatalogMasterId_BusinessDescription { get; set; }
        public string AcademicCatalogMasterId_Status { get; set; }
        public string AcademicCatalogMasterId_Source { get; set; }
        public string AcademicCatalogMasterId_Category { get; set; }
        public int? AcademicCatalogMasterId_AttributeId { get; set; }
        #endregion



        #region AcademicSubplan
        public string AcademicSubplan { get; set; }
        public string AcademicSubplan_BusinessName { get; set; }
        public string AcademicSubplan_BusinessDescription { get; set; }
        public string AcademicSubplan_Status { get; set; }
        public string AcademicSubplan_Source { get; set; }
        public string AcademicSubplan_Category { get; set; }
        public int? AcademicSubplan_AttributeId { get; set; }
        #endregion

        #region AcademicSubplanSourceSystemRecordId
        public string AcademicSubplanSourceSystemRecordId { get; set; }
        public string AcademicSubplanSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicSubplanSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Status { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Source { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Category { get; set; }
        public int? AcademicSubplanSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AcademicSubplanMasterId
        public string AcademicSubplanMasterId { get; set; }
        public string AcademicSubplanMasterId_BusinessName { get; set; }
        public string AcademicSubplanMasterId_BusinessDescription { get; set; }
        public string AcademicSubplanMasterId_Status { get; set; }
        public string AcademicSubplanMasterId_Source { get; set; }
        public string AcademicSubplanMasterId_Category { get; set; }
        public int? AcademicSubplanMasterId_AttributeId { get; set; }
        #endregion



        #region StudentAcademicPlanStatus
        public string StudentAcademicPlanStatus { get; set; }
        public string StudentAcademicPlanStatus_BusinessName { get; set; }
        public string StudentAcademicPlanStatus_BusinessDescription { get; set; }
        public string StudentAcademicPlanStatus_Status { get; set; }
        public string StudentAcademicPlanStatus_Source { get; set; }
        public string StudentAcademicPlanStatus_Category { get; set; }
        public int? StudentAcademicPlanStatus_AttributeId { get; set; }
        #endregion

        #region StudentAcademicPlanStatusSourceSystemRecordId
        public string StudentAcademicPlanStatusSourceSystemRecordId { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_BusinessName { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Status { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Source { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Category { get; set; }
        public int? StudentAcademicPlanStatusSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region StudentAcademicPlanStatusMasterId
        public string StudentAcademicPlanStatusMasterId { get; set; }
        public string StudentAcademicPlanStatusMasterId_BusinessName { get; set; }
        public string StudentAcademicPlanStatusMasterId_BusinessDescription { get; set; }
        public string StudentAcademicPlanStatusMasterId_Status { get; set; }
        public string StudentAcademicPlanStatusMasterId_Source { get; set; }
        public string StudentAcademicPlanStatusMasterId_Category { get; set; }
        public int? StudentAcademicPlanStatusMasterId_AttributeId { get; set; }
        #endregion



        #region ExpectedGraduationTerm
        public string ExpectedGraduationTerm { get; set; }
        public string ExpectedGraduationTerm_BusinessName { get; set; }
        public string ExpectedGraduationTerm_BusinessDescription { get; set; }
        public string ExpectedGraduationTerm_Status { get; set; }
        public string ExpectedGraduationTerm_Source { get; set; }
        public string ExpectedGraduationTerm_Category { get; set; }
        public int? ExpectedGraduationTerm_AttributeId { get; set; }
        #endregion

        #region ExpectedGraduationTermSourceSystemRecordId
        public string ExpectedGraduationTermSourceSystemRecordId { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_BusinessName { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_BusinessDescription { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Status { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Source { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Category { get; set; }
        public int? ExpectedGraduationTermSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region ExpectedGraduationTermMasterId
        public string ExpectedGraduationTermMasterId { get; set; }
        public string ExpectedGraduationTermMasterId_BusinessName { get; set; }
        public string ExpectedGraduationTermMasterId_BusinessDescription { get; set; }
        public string ExpectedGraduationTermMasterId_Status { get; set; }
        public string ExpectedGraduationTermMasterId_Source { get; set; }
        public string ExpectedGraduationTermMasterId_Category { get; set; }
        public int? ExpectedGraduationTermMasterId_AttributeId { get; set; }
        #endregion

    }
}
