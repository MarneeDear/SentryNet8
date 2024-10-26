using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Sentry.WebApp.Data.Models
{
    public class RemediationCounts
    {
        [Key]
        public int Key { get; set; }

        // Organization
        public int OrganizationalUnitCount { get; set; }
        public int OfficeLocationCount { get; set; }
        public int AcademicCatalogCount { get; set; }

        public int EmployeeCount { get; set; }

        // Student
        public int StudentBioDemCount { get; set; }
        public int StudentEnrollmentCount { get; set; }
        public int StudentDegreeCount { get; set; }
        public int StudentAcademicInvolvementCount { get; set; }
        public int StudentAcademicPlanCount { get; set; }
        //public int ScholarshipCount { get; set; }
        public int StudentScholarshipCount { get; set; }
        public int StudentParentCount { get; set; }

        // Constituent
        public int ConstituentIndividualCount { get; set; }
        public int ConstituentPhoneCount { get; set; }
        public int ConstituentEmailCount { get; set; }
        public int ConstituentAddressCount { get; set; }

        // Finance
        public int DesignationCount { get; set; }
    }
}
