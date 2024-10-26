using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sentry.WebApp.Data.Models
{
    //[Table("Degrees_Detail", Schema = "Integration")]
    public class StudentDegreeDetail : BaseDetail
    {
        #region ClassOf
        public string ClassOf { get; set; }
        public string ClassOf_BusinessName { get; set; }
        public string ClassOf_BusinessDescription { get; set; }
        public string ClassOf_Source { get; set; }
        public string ClassOf_Category { get; set; }
        public string ClassOf_Status { get; set; }
        public int? ClassOf_AttributeId { get; set; }
        #endregion



        #region Student
        public string Student { get; set; }
        public string Student_BusinessName { get; set; }
        public string Student_BusinessDescription { get; set; }
        public int Student_AttributeId { get; set; }
        public string Student_Status { get; set; }
        public string Student_Source { get; set; }
        #endregion

        #region StudentSourceSystemRecordId
        public string StudentSourceSystemRecordId { get; set; }
        public string StudentSourceSystemRecordId_BusinessName { get; set; }
        public string StudentSourceSystemRecordId_BusinessDescription { get; set; }
        public int StudentSourceSystemRecordId_AttributeId { get; set; }
        public string StudentSourceSystemRecordId_Status { get; set; }
        public string StudentSourceSystemRecordId_Source { get; set; }
        #endregion

        #region StudentMasterId
        public string StudentMasterId { get; set; }
        public string StudentMasterId_BusinessName { get; set; }
        public string StudentMasterId_BusinessDescription { get; set; }
        public int StudentMasterId_AttributeId { get; set; }
        public string StudentMasterId_Status { get; set; }
        public string StudentMasterId_Source { get; set; }
        #endregion



        #region EducationalInstitution
        public string EducationalInstitution { get; set; }
        public string EducationalInstitution_BusinessName { get; set; }
        public string EducationalInstitution_BusinessDescription { get; set; }
        public string EducationalInstitution_Source { get; set; }
        public string EducationalInstitution_Category { get; set; }
        public string EducationalInstitution_Status { get; set; }
        public int? EducationalInstitution_AttributeId { get; set; }
        #endregion

        #region EducationalInstitutionSourceSystemRecordId
        public string EducationalInstitutionSourceSystemRecordId { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_BusinessName { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_BusinessDescription { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_Source { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_Category { get; set; }
        public string EducationalInstitutionSourceSystemRecordId_Status { get; set; }
        public int? EducationalInstitutionSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region EducationalInstitutionMasterId
        public string EducationalInstitutionMasterId { get; set; }
        public string EducationalInstitutionMasterId_BusinessName { get; set; }
        public string EducationalInstitutionMasterId_BusinessDescription { get; set; }
        public string EducationalInstitutionMasterId_Source { get; set; }
        public string EducationalInstitutionMasterId_Category { get; set; }
        public string EducationalInstitutionMasterId_Status { get; set; }
        public int? EducationalInstitutionMasterId_AttributeId { get; set; }
        #endregion




        #region PreferredClassOf
        public string PreferredClassOf { get; set; }
        public string PreferredClassOf_BusinessName { get; set; }
        public string PreferredClassOf_BusinessDescription { get; set; }
        public string PreferredClassOf_Source { get; set; }
        public string PreferredClassOf_Category { get; set; }
        public string PreferredClassOf_Status { get; set; }
        public int? PreferredClassOf_AttributeId { get; set; }
        #endregion

        #region AwardedDate
        public string AwardedDate { get; set; }
        public string AwardedDate_BusinessName { get; set; }
        public string AwardedDate_BusinessDescription { get; set; }
        public string AwardedDate_Source { get; set; }
        public string AwardedDate_Category { get; set; }
        public string AwardedDate_Status { get; set; }
        public int? AwardedDate_AttributeId { get; set; }
        #endregion


        

        #region DegreeHonorSourceSystemRecordId
        public string DegreeHonorSourceSystemRecordId { get; set; }
        public string DegreeHonorSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeHonorSourceSystemRecordId_BusinessDescription { get; set; }
        public string DegreeHonorSourceSystemRecordId_Source { get; set; }
        public string DegreeHonorSourceSystemRecordId_Category { get; set; }
        public string DegreeHonorSourceSystemRecordId_Status { get; set; }
        public int? DegreeHonorSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region DegreeHonorMasterId
        public string DegreeHonorMasterId { get; set; }
        public string DegreeHonorMasterId_BusinessName { get; set; }
        public string DegreeHonorMasterId_BusinessDescription { get; set; }
        public string DegreeHonorMasterId_Source { get; set; }
        public string DegreeHonorMasterId_Category { get; set; }
        public string DegreeHonorMasterId_Status { get; set; }
        public int? DegreeHonorMasterId_AttributeId { get; set; }
        #endregion



        #region DegreeStatus
        public string DegreeStatus { get; set; }
        public string DegreeStatus_BusinessName { get; set; }
        public string DegreeStatus_BusinessDescription { get; set; }
        public string DegreeStatus_Source { get; set; }
        public string DegreeStatus_Category { get; set; }
        public string DegreeStatus_Status { get; set; }
        public int? DegreeStatus_AttributeId { get; set; }
        #endregion

        #region DegreeStatusSourceSystemRecordId
        public string DegreeStatusSourceSystemRecordId { get; set; }
        public string DegreeStatusSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeStatusSourceSystemRecordId_BusinessDescription { get; set; }
        public string DegreeStatusSourceSystemRecordId_Source { get; set; }
        public string DegreeStatusSourceSystemRecordId_Category { get; set; }
        public string DegreeStatusSourceSystemRecordId_Status { get; set; }
        public int? DegreeStatusSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region DegreeStatusMasterId
        public string DegreeStatusMasterId { get; set; }
        public string DegreeStatusMasterId_BusinessName { get; set; }
        public string DegreeStatusMasterId_BusinessDescription { get; set; }
        public string DegreeStatusMasterId_Source { get; set; }
        public string DegreeStatusMasterId_Category { get; set; }
        public string DegreeStatusMasterId_Status { get; set; }
        public int? DegreeStatusMasterId_AttributeId { get; set; }
        #endregion


        
        #region AwardedTerm
        public string AwardedTerm { get; set; }
        public string AwardedTerm_BusinessName { get; set; }
        public string AwardedTerm_BusinessDescription { get; set; }
        public string AwardedTerm_Source { get; set; }
        public string AwardedTerm_Category { get; set; }
        public string AwardedTerm_Status { get; set; }
        public int? AwardedTerm_AttributeId { get; set; }
        #endregion

        #region AwardedTermMasterId
        public string AwardedTermMasterId { get; set; }
        public string AwardedTermMasterId_BusinessName { get; set; }
        public string AwardedTermMasterId_BusinessDescription { get; set; }
        public string AwardedTermMasterId_Source { get; set; }
        public string AwardedTermMasterId_Category { get; set; }
        public string AwardedTermMasterId_Status { get; set; }
        public int? AwardedTermMasterId_AttributeId { get; set; }
        #endregion



        #region DegreeType
        public string DegreeType { get; set; }
        public string DegreeType_BusinessName { get; set; }
        public string DegreeType_BusinessDescription { get; set; }
        public string DegreeType_Source { get; set; }
        public string DegreeType_Category { get; set; }
        public string DegreeType_Status { get; set; }
        public int? DegreeType_AttributeId { get; set; }
        #endregion

        #region DegreeTypeSourceSystemRecordId
        public string DegreeTypeSourceSystemRecordId { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessName { get; set; }
        public string DegreeTypeSourceSystemRecordId_BusinessDescription { get; set; }
        public string DegreeTypeSourceSystemRecordId_Source { get; set; }
        public string DegreeTypeSourceSystemRecordId_Category { get; set; }
        public string DegreeTypeSourceSystemRecordId_Status { get; set; }
        public int? DegreeTypeSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region DegreeTypeMasterId
        public string DegreeTypeMasterId { get; set; }
        public string DegreeTypeMasterId_BusinessName { get; set; }
        public string DegreeTypeMasterId_BusinessDescription { get; set; }
        public string DegreeTypeMasterId_Source { get; set; }
        public string DegreeTypeMasterId_Category { get; set; }
        public string DegreeTypeMasterId_Status { get; set; }
        public int? DegreeTypeMasterId_AttributeId { get; set; }
        #endregion



        #region AcademicCareer
        public string AcademicCareer { get; set; }
        public string AcademicCareer_BusinessName { get; set; }
        public string AcademicCareer_BusinessDescription { get; set; }
        public string AcademicCareer_Source { get; set; }
        public string AcademicCareer_Category { get; set; }
        public string AcademicCareer_Status { get; set; }
        public int? AcademicCareer_AttributeId { get; set; }
        #endregion

        #region AcademicCareerSourceSystemRecordId
        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessName { get; set; }
        public string AcademicCareerSourceSystemRecordId_BusinessDescription { get; set; }
        public string AcademicCareerSourceSystemRecordId_Source { get; set; }
        public string AcademicCareerSourceSystemRecordId_Category { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }
        public int? AcademicCareerSourceSystemRecordId_AttributeId { get; set; }
        #endregion

        #region AcademicCareerMasterId
        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_BusinessName { get; set; }
        public string AcademicCareerMasterId_BusinessDescription { get; set; }
        public string AcademicCareerMasterId_Source { get; set; }
        public string AcademicCareerMasterId_Category { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }
        public int? AcademicCareerMasterId_AttributeId { get; set; }
        #endregion

    }
}
