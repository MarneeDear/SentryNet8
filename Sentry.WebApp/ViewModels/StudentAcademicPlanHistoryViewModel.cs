using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.ViewModels
{
    public class StudentAcademicPlanHistoryViewModel
    {
        public DateTime? HistoryDate { get; set; }

        public string Student { get; set; }
        public string Student_OriginalValue { get; set; }
        public string Student_Status { get; set; }

        public string StudentMasterId { get; set; }
        public string StudentMasterId_OriginalValue { get; set; }
        public string StudentMasterId_Status { get; set; }



        public string Enrollment { get; set; }
        public string Enrollment_OriginalValue { get; set; }
        public string Enrollment_Status { get; set; }

        public string EnrollmentSourceSystemRecordId { get; set; }
        public string EnrollmentSourceSystemRecordId_OriginalValue { get; set; }
        public string EnrollmentSourceSystemRecordId_Status { get; set; }

        public string EnrollmentMasterId { get; set; }
        public string EnrollmentMasterId_OriginalValue { get; set; }
        public string EnrollmentMasterId_Status { get; set; }



        public string AcademicCareer { get; set; }
        public string AcademicCareer_OriginalValue { get; set; }
        public string AcademicCareer_Status { get; set; }

        public string AcademicCareerSourceSystemRecordId { get; set; }
        public string AcademicCareerSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicCareerSourceSystemRecordId_Status { get; set; }

        public string AcademicCareerMasterId { get; set; }
        public string AcademicCareerMasterId_OriginalValue { get; set; }
        public string AcademicCareerMasterId_Status { get; set; }



        public string TermName { get; set; }
        public string TermName_OriginalValue { get; set; }
        public string TermName_Status { get; set; }

        public string TermCode { get; set; }
        public string TermCode_OriginalValue { get; set; }
        public string TermCode_Status { get; set; }

        public string TermSourceSystemRecordId { get; set; }
        public string TermSourceSystemRecordId_OriginalValue { get; set; }
        public string TermSourceSystemRecordId_Status { get; set; }

        public string TermMasterId { get; set; }
        public string TermMasterId_OriginalValue { get; set; }
        public string TermMasterId_Status { get; set; }



        //public string CampusName { get; set; }
        //public string CampusName_OriginalValue { get; set; }
        //public string CampusName_Status { get; set; }

        public string CampusSourceSystemRecordId { get; set; }
        public string CampusSourceSystemRecordId_OriginalValue { get; set; }
        public string CampusSourceSystemRecordId_Status { get; set; }

        public string CampusMasterId { get; set; }
        public string CampusMasterId_OriginalValue { get; set; }
        public string CampusMasterId_Status { get; set; }




        public string Degree { get; set; }
        public string Degree_OriginalValue { get; set; }
        public string Degree_Status { get; set; }

        public string DegreeSourceSystemRecordId { get; set; }
        public string DegreeSourceSystemRecordId_OriginalValue { get; set; }
        public string DegreeSourceSystemRecordId_Status { get; set; }

        public string DegreeMasterId { get; set; }
        public string DegreeMasterId_OriginalValue { get; set; }
        public string DegreeMasterId_Status { get; set; }





        public string AcademicPlan { get; set; }
        public string AcademicPlan_OriginalValue { get; set; }
        public string AcademicPlan_Status { get; set; }

        public string AcademicPlanSourceSystemRecordId { get; set; }
        public string AcademicPlanSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicPlanSourceSystemRecordId_Status { get; set; }

        public string AcademicPlanMasterId { get; set; }
        public string AcademicPlanMasterId_OriginalValue { get; set; }
        public string AcademicPlanMasterId_Status { get; set; }



        public string AcademicCatalog { get; set; }
        public string AcademicCatalog_OriginalValue { get; set; }
        public string AcademicCatalog_Status { get; set; }

        public string AcademicCatalogDescription { get; set; }
        public string AcademicCatalogDescription_OriginalValue { get; set; }
        public string AcademicCatalogDescription_Status { get; set; }

        public string AcademicCatalogSourceSystemRecordId { get; set; }
        public string AcademicCatalogSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicCatalogSourceSystemRecordId_Status { get; set; }

        public string AcademicCatalogMasterId { get; set; }
        public string AcademicCatalogMasterId_OriginalValue { get; set; }
        public string AcademicCatalogMasterId_Status { get; set; }



        public string AcademicSubplan { get; set; }
        public string AcademicSubplan_OriginalValue { get; set; }
        public string AcademicSubplan_Status { get; set; }

        public string AcademicSubplanDescription { get; set; }
        public string AcademicSubplanDescription_OriginalValue { get; set; }
        public string AcademicSubplanDescription_Status { get; set; }

        public string AcademicSubplanSourceSystemRecordId { get; set; }
        public string AcademicSubplanSourceSystemRecordId_OriginalValue { get; set; }
        public string AcademicSubplanSourceSystemRecordId_Status { get; set; }

        public string AcademicSubplanMasterId { get; set; }
        public string AcademicSubplanMasterId_OriginalValue { get; set; }
        public string AcademicSubplanMasterId_Status { get; set; }



        public string StudentAcademicPlanStatus { get; set; }
        public string StudentAcademicPlanStatus_OriginalValue { get; set; }
        public string StudentAcademicPlanStatus_Status { get; set; }

        public string StudentAcademicPlanStatusSourceSystemRecordId { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_OriginalValue { get; set; }
        public string StudentAcademicPlanStatusSourceSystemRecordId_Status { get; set; }

        public string StudentAcademicPlanStatusMasterId { get; set; }
        public string StudentAcademicPlanStatusMasterId_OriginalValue { get; set; }
        public string StudentAcademicPlanStatusMasterId_Status { get; set; }



        public string ExpectedGraduationTerm { get; set; }
        public string ExpectedGraduationTerm_OriginalValue { get; set; }
        public string ExpectedGraduationTerm_Status { get; set; }

        public string ExpectedGraduationTermSourceSystemRecordId { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_OriginalValue { get; set; }
        public string ExpectedGraduationTermSourceSystemRecordId_Status { get; set; }

        public string ExpectedGraduationTermMasterId { get; set; }
        public string ExpectedGraduationTermMasterId_OriginalValue { get; set; }
        public string ExpectedGraduationTermMasterId_Status { get; set; }

    }
}
