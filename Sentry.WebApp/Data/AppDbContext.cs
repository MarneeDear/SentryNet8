using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sentry.WebApp.Data.Models;
using Microsoft.Data.SqlClient;

namespace Sentry.WebApp.Data
{
    public class AppDbContext : DbContext
    {

        // Dashboard
        public DbSet<IntegrationHealth> IntegrationHealth { get; set; }
        public DbSet<CategorySystemIntegration> CategorySystemIntegrations { get; set; }

        // Employee
        public DbSet<EmployeeRemediationList> EmployeeRemediationList { get; set; }
        public DbSet<EmployeeDetail> EmployeeDetails { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeComparisonDetail> EmployeeComparisonDetails { get; set; }
        public DbSet<EmployeePossibleMatch> EmployeePossibleMatches { get; set; }
        public DbSet<EmployeeHistory> EmployeeHistories { get; set; }
        public DbSet<EmployeeMatchDetail> EmployeeMatchDetails { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        //public DbSet<EmployeeManager> EmployeeManagers { get; set; }

        // Office Location
        public DbSet<OfficeLocationRemediationList> OfficeLocationRemediationList { get; set; }
		public DbSet<OfficeLocationDetail> OfficeLocationDetails { get; set; }
        public DbSet<OfficeLocationSourceDetail> OfficeLocationSourceDetails { get; set; }
        public DbSet<OfficeLocationHistory> OfficeLocationHistories { get; set; }
        public DbSet<OfficeLocationComparisonDetail> OfficeLocationComparisonDetails { get; set; }
        public DbSet<OfficeLocationPossibleMatch> OfficeLocationPossibleMatches { get; set; }

		// Organizational Unit
		public DbSet<OrganizationalUnitRemediationList> OrganizationalUnitsRemediationList { get; set; }
		public DbSet<OrganizationalUnitDetail> OrganizationalUnitDetails { get; set; }
		public DbSet<OrganizationalUnitSourceDetail> OrganizationalUnitSourceDetails { get; set; }
		public DbSet<OrganizationalUnitHistory> OrganizationalUnitHistories { get; set; }
		public DbSet<OrganizationalUnitComparisonDetail> OrganizationalUnitComparisonDetails { get; set; }
		public DbSet<OrganizationalUnitPossibleMatch> OrganizationalUnitPossibleMatches { get; set; }
		public DbSet<OrganizationalUnit> OrganizationalUnitParents { get; set; }
        public DbSet<OrganizationalUnit> OrganizationalUnits { get; set; }
        public DbSet<OrganizationalUnitType> OrganizationalUnitTypes { get; set; }
        public DbSet<OrganizationalUnitTreeItem> OrganizationalUnitTreeItems { get; set; }

        // Designation
        public DbSet<DesignationRemediationList> DesignationRemediationList { get; set; }
        public DbSet<DesignationDetail> DesignationDetails { get; set; }
        public DbSet<DesignationHistory> DesignationHistories { get; set; }
        public DbSet<DesignationMatchDetail> DesignationMatchDetails { get; set; }
        public DbSet<DesignationPossibleMatch> DesignationPossibleMatches { get; set; }
        public DbSet<DesignationComparisonDetail> DesignationComparisonDetails { get; set; }
        public DbSet<DesignationType> DesignationTypes { get; set; }
        public DbSet<DesignationSubtype> DesignationSubtypes { get; set; }
        public DbSet<DesignationStatus> DesignationStatus { get; set; }
        public DbSet<KFSAccount> KFSAccounts { get; set; }
        public DbSet<VSECategory> VSECategories { get; set; }

        // Student
        public DbSet<StudentRemediationList> StudentRemediationList { get; set; }
        public DbSet<StudentDetail> StudentDetails { get; set; }
        public DbSet<StudentHistory> StudentHistories { get; set; }
        public DbSet<StudentPossibleMatch> StudentPossibleMatches { get; set; }
        public DbSet<StudentComparisonDetail> StudentComparisonDetails { get; set; }
        public DbSet<StudentEnrollmentCampus> StudentEnrollmentCampus { get; set; }
        public DbSet<StudentEnrollmentLocation> StudentEnrollmentLocations { get; set; }
        public DbSet<StudentMatchDetail> StudentMatchDetails { get; set; }

        // Academic Catalog
        public DbSet<StudentAcademicCatalogRemediationList> StudentAcademicCatalogRemediationList { get; set; }
        public DbSet<StudentAcademicCatalogDetail> StudentAcademicCatalogDetails { get; set; }
        public DbSet<StudentAcademicCatalogDegreeType> StudentAcademicCatalogDegreeTypes { get; set; }
        public DbSet<StudentAcademicCatalogAcademicCareer> StudentAcademicCatalogAcademicCareers { get; set; }
        public DbSet<StudentAcademicCatalogAcademicProgram> StudentAcademicCatalogAcademicPrograms { get; set; }
        public DbSet<StudentAcademicCatalogAcademicPlan> StudentAcademicCatalogAcademicPlans { get; set; }
        public DbSet<StudentAcademicCatalogAcademicPlanType> StudentAcademicCatalogAcademicPlanTypes { get; set; }


        // Enrollment
        public DbSet<StudentEnrollmentRemediationList> StudentEnrollmentRemediationList { get; set; }
        public DbSet<StudentEnrollmentDetail> StudentEnrollmentDetails { get; set; }


        // Academic Involvement
        public DbSet<StudentAcademicInvolvementRemediationList> StudentAcademicInvolvementRemediationList { get; set; }
        public DbSet<StudentAcademicInvolvementDetail> StudentAcademicInvolvementDetails { get; set; }
        public DbSet<StudentAcademicInvolvementHistory> StudentAcademicInvolvementHistories { get; set; }
        public DbSet<StudentAcademicInvolvementTerm> StudentAcademicInvolvementTerms { get; set; }
        public DbSet<StudentAcademicInvolvementType> StudentAcademicInvolvementTypes { get; set; }
        public DbSet<StudentAcademicInvolvementName> StudentAcademicInvolvementNames { get; set; }

        // Degree
        public DbSet<DegreeRemediationList> DegreeRemediationList { get; set; }
        public DbSet<StudentDegreeDetail> StudentDegreeDetails { get; set; }
        public DbSet<DegreeHistory> DegreeHistories { get; set; }
        //public DbSet<DegreeType> DegreeTypes { get; set; }
        public DbSet<DegreeName> DegreeNames { get; set; }
        public DbSet<DegreePossibleMatch> DegreePossibleMatches { get; set; }
        public DbSet<DegreeComparisonDetail> DegreeComparisonDetails { get; set; }

        // Scholarship
        public DbSet<StudentScholarshipRemediationList> StudentScholarshipRemediationList { get; set; }
        public DbSet<StudentScholarshipDetail> StudentScholarshipDetails { get; set; }
        public DbSet<StudentScholarshipHistory> StudentScholarshipHistories { get; set; }
        public DbSet<StudentScholarshipMatchDetail> StudentScholarshipMatchDetails { get; set; }
        public DbSet<StudentScholarshipPossibleMatch> StudentScholarshipPossibleMatches { get; set; }
        public DbSet<StudentScholarshipComparisonDetail> StudentScholarshipComparisonDetails { get; set; }

        // Constituent
        public DbSet<ConstituentRemediationList> ConstituentsRemediationList { get; set; }
        public DbSet<ConstituentDetail> ConstituentDetails { get; set; }
        public DbSet<ConstituentMatchDetail> ConstituentMatchDetails { get; set; }
        public DbSet<ConstituentPossibleMatch> ConstituentPossibleMatches { get; set; }
        public DbSet<ConstituentComparisonDetail> ConstituentComparisonDetail { get; set; }

        // Constituent Phone
        public DbSet<ConstituentPhoneRemediationList> ConstituentPhoneRemediationList { get; set; }
        public DbSet<ConstituentPhoneDetail> ConstituentPhoneDetails { get; set; }
        public DbSet<ConstituentPhoneNumber> ConstituentPhoneNumbers { get; set; }

        // Constituent Email
        public DbSet<ConstituentEmailRemediationList> ConstituentEmailRemediationList { get; set; }
        public DbSet<ConstituentEmailDetail> ConstituentEmailDetails { get; set; }

        // Constituent Address
        public DbSet<ConstituentAddressRemediationList> ConstituentAddressRemediationList { get; set; }
        public DbSet<ConstituentAddressDetail> ConstituentAddressDetails { get; set; }

        // Student Academic Plan
        public DbSet<StudentAcademicPlanRemediationList> StudentAcademicPlanRemediationList { get; set; }
        public DbSet<StudentAcademicPlanDetail> StudentAcademicPlanDetails { get; set; }
        //public DbSet<StudentAcademicPlanHistory> StudentAcademicPlanHistories { get; set; }
        //public DbSet<StudentAcademicPlanPossibleMatch> StudentAcademicPlanPossibleMatches { get; set; }
        //public DbSet<StudentAcademicPlanComparisonDetail> StudentAcademicPlanComparisonDetails { get; set; }
        public DbSet<StudentAcademicPlanMatchDetail> StudentAcademicPlanMatchDetails { get; set; }

        // Student/Parent
        public DbSet<StudentParentRemediationList> StudentParentRemediationList { get; set; }
        public DbSet<StudentParentDetail> StudentParentDetails { get; set; }
        public DbSet<StudentParentMatchDetail> StudentParentMatchDetails { get; set; }
        public DbSet<StudentParentPossibleMatch> StudentParentPossibleMatches { get; set; }
        public DbSet<StudentParentComparisonDetail> StudentParentComparisonDetails { get; set; }


        public DbSet<RemediationCounts> RemediationCounts { get; set; }

        public DbSet<IntegrationStatus> IntegrationsStatuses { get; set; }
        public DbSet<LogEntry> LogEntries { get; set; }
        public DbSet<QueueEntryCountRow> QueueEntryCounts { get; set; }

        public DbSet<State> States { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<CountryDialingCode> CountryDialingCodes { get; set; }
        public DbSet<StudentName> StudentNames { get; set; }
        public DbSet<PersonName> PersonNames { get; set; }
        public DbSet<Suffix> Suffixes { get; set; }
        public DbSet<Title> Titles { get; set; }
        public DbSet<MaritalStatus> MaritalStatuses { get; set; }
        public DbSet<AcademicTerm> AcademicTerms { get; set; }
        public DbSet<StudentAcademicCatalogAcademicPlan> AcademicPlans { get; set; }
        public DbSet<StudentAcademicPlanStatus> AcademicPlanStatuses { get; set; }
        public DbSet<StudentAcademicSubplan> AcademicSubplans { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<Scholarship> Scholarships { get; set; }
        public DbSet<Organization> Organizations { get; set; }
        public DbSet<DesignationUseType> DesignationUseTypes { get; set; }
        public DbSet<StudentAcademicCatalogAcademicCareer> AcademicCareers { get; set; }
        public DbSet<AcademicLevel> AcademicLevels { get; set; }
        public DbSet<AcademicCalendarEntry> AcademicCalendarEntries { get; set; }
        public DbSet<StudentAcademicSubplan> StudentAcademicSubplans { get; set; }
        public DbSet<EducationalInstitution> EducationalInstitutions { get; set; }
        public DbSet<StudentName> Students { get; set; }
        public DbSet<StudentEnrollment> StudentEnrollments { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Campus> Campuses { get; set; }
        public DbSet<Honor> Honors { get; set; }
        public DbSet<DegreeStatus> DegreeStatuses { get; set; }
        public DbSet<DegreeType> DegreeTypes { get; set; }
        public DbSet<PhoneLineType> PhoneLineTypes { get; set; }
        public DbSet<PhoneUseType> PhoneUseTypes { get; set; }
        public DbSet<EmailUseType> EmailUseTypes { get; set; }
        public DbSet<AddressUseType> AddressUseTypes { get; set; }
        public DbSet<Relationship> Relationships { get; set; }
        public DbSet<SystemRecord> SystemRecords { get; set; }

        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<EmployeeRemediationList>()
                .HasKey(e => new { e.Id, e.SystemId });

            modelBuilder.Entity<DesignationRemediationList>()
                .HasKey(e => new { e.Id, e.SystemId });

            modelBuilder.Entity<IntegrationStatus>()
                .HasKey(e => new { e.SystemId, e.IntegrationId });

            modelBuilder.Entity<StudentRemediationList>()
                .HasKey(e => new { e.Id, e.SystemId });

            modelBuilder.Entity<IntegrationHealth>()
                .HasNoKey();

            modelBuilder.Entity<LogEntry>()
                .HasNoKey();

            modelBuilder.Entity<QueueEntryCountRow>()
                .HasNoKey();
        }

        #region Dashboard
        public IntegrationHealth GetIntegrationHealth(int systemId, int integrationId)
        {
            return IntegrationHealth.FromSqlInterpolated(
                $"SELECT * FROM [Integration].IntegrationHealth({systemId}, {integrationId})"
            ).Single();         
            
        }

        public IEnumerable<CategorySystemIntegration> GetCategorySystemIntegrations(string entity)
        {
            return CategorySystemIntegrations
                .FromSqlInterpolated($"Integration.CategorySystemIntegrations {entity}")
                .ToList();
        }

        #endregion

        #region Constituent

        public IEnumerable<ConstituentDetail> GetConstituentHistory(int SystemId, long Id)
        {
            return ConstituentDetails.FromSqlInterpolated($"SELECT * FROM Integration.ConstituentHistory({Id}, {SystemId})").ToList();
        }

        public ConstituentMatchDetail GetConstituentMatchDetails(int SystemId, long Id)
        {
            return ConstituentMatchDetails.FromSqlRaw(
                "EXEC [Integration].[ConstituentMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).AsEnumerable().Single();
        }
        public List<ConstituentPossibleMatch> GetConstituentPossibleMatches(int SystemId, long? Id)
        {
            return ConstituentPossibleMatches.FromSqlRaw(
                "EXEC Integration.ConstituentGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
        }

        public ConstituentComparisonDetail GetConstituentComparisonDetail(int SystemId, long Id, string CompareId)
        {
            var details = ConstituentComparisonDetail.FromSqlRaw(
                "EXEC Integration.ConstituentGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@CompareId", CompareId)
                    ).AsEnumerable().FirstOrDefault();
            details.SystemRecords = SystemRecords.FromSqlRaw(
                "EXEC [Integration].[PersonGetSystemRecords] @MasterRecordId",
                    new SqlParameter("@MasterRecordId", CompareId)
                    ).ToList();

            return details;
        }

        // Constituent Phone - Edit View Function
        public IEnumerable<ConstituentPhoneDetail> GetConstituentPhoneHistory(int SystemId, long Id)
        {
            var detail = ConstituentPhoneDetails.FromSqlInterpolated($"SELECT * FROM Integration.ConstituentPhoneHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Constituent Email - Edit View Function
        public IEnumerable<ConstituentEmailDetail> GetConstituentEmailHistory(int SystemId, long Id)
        {
            var detail = ConstituentEmailDetails.FromSqlInterpolated($"SELECT * FROM Integration.ConstituentEmailHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Constituent Address - Edit View Function
        public IEnumerable<ConstituentAddressDetail> GetConstituentAddressHistory(int SystemId, long Id)
        {
            var detail = ConstituentAddressDetails.FromSqlInterpolated($"SELECT * FROM Integration.ConstituentAddressHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        #endregion

        #region Student

        // Edit View Function
        public IEnumerable<StudentDetail> GetStudentHistory(int SystemId, long Id)
        {
            var detail = StudentDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Match View Sproc
        public StudentMatchDetail GetStudentMatchDetails(int SystemId, long Id)
        {
            return StudentMatchDetails.FromSqlRaw(
                "EXEC [Integration].[StudentMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).AsEnumerable().Single();
        }

        // Possible Match List Sproc
        public List<StudentPossibleMatch> GetStudentPossibleMatches(int SystemId, long? Id)
        {
            return StudentPossibleMatches.FromSqlRaw(
                "EXEC Integration.StudentGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
        }

        // Comparison View Sproc
        public StudentComparisonDetail GetStudentComparisonDetail(int SystemId, long Id, string CompareId)
        {
            StudentComparisonDetail studentComparisonDetails = StudentComparisonDetails.FromSqlRaw(
                "EXEC Integration.StudentGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@CompareId", CompareId)
                    ).AsEnumerable().FirstOrDefault();
            studentComparisonDetails.SystemRecords = SystemRecords.FromSqlRaw(
                "EXEC [Integration].[PersonGetSystemRecords] @MasterRecordId",
                    new SqlParameter("@MasterRecordId", CompareId)
                    ).ToList();

            return studentComparisonDetails;
        }

        #endregion

        #region Student Academic Plan

        // Edit View Function
        public IEnumerable<StudentAcademicPlanDetail> GetStudentAcademicPlanHistory(int SystemId, long Id)
        {
            var detail = StudentAcademicPlanDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentAcademicPlanHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Enrollment List for specific Student
        public IEnumerable<StudentEnrollment> GetStudentEnrollments(string StudentMasterId)
        {
            var detail = StudentEnrollments.FromSqlInterpolated($"SELECT * FROM MDS.StudentEnrollment({StudentMasterId})").ToList();
            return detail;
        }

        // Match View Sproc
        public StudentAcademicPlanMatchDetail GetStudentAcademicPlanMatchDetails(int SystemId, long Id)
        {
            return StudentAcademicPlanMatchDetails.FromSqlRaw(
                "EXEC [Integration].[StudentAcademicPlanMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).Single();
        }

        // Possible Match List Sproc
        //public List<StudentAcademicPlanPossibleMatch> GetStudentAcademicPlanPossibleMatches(int SystemId, long? Id)
        //{
        //    return StudentAcademicPlanPossibleMatches.FromSql(
        //        "EXEC Integration.StudentAcademicPlanGetPossibleMatches @SystemId, @RecordId",
        //            new SqlParameter("@SystemId", SystemId),
        //            new SqlParameter("@RecordId", Id)
        //            ).ToList();
        //}

        // Comparison View Sproc
        //public StudentAcademicPlanComparisonDetail GetStudentAcademicPlanComparisonDetail(int SystemId, long Id, string CompareId)
        //{
        //    StudentAcademicPlanComparisonDetail studentAcademicPlanComparisonDetails = StudentAcademicPlanComparisonDetails.FromSql(
        //        "EXEC Integration.StudentAcademicPlanGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
        //            new SqlParameter("@SystemId", SystemId),
        //            new SqlParameter("@RecordId", Id),
        //            new SqlParameter("@CompareId", CompareId)
        //            ).FirstOrDefault();
        //    studentAcademicPlanComparisonDetails.SystemRecords = SystemRecords.FromSql(
        //        "EXEC [Integration].[PersonGetSystemRecords] @MasterRecordId",
        //            new SqlParameter("@MasterRecordId", CompareId)
        //            ).ToList();

        //    return studentAcademicPlanComparisonDetails;
        //}

        #endregion

        #region StudentDegree

        public IEnumerable<StudentDegreeDetail> GetStudentDegreeHistory(int SystemId, long Id)
        {
            var detail = StudentDegreeDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentDegreeHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        public List<DegreePossibleMatch> GetDegreePossibleMatches(int SystemId, long? Id)
        {
            return DegreePossibleMatches.FromSqlRaw(
                "EXEC Integration.DegreeGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
        }

        public DegreeComparisonDetail GetDegreeComparisonDetail(int SystemId, long Id, string CompareId)
        {
            var details = DegreeComparisonDetails.FromSqlRaw(
                "EXEC Integration.DegreeGetPossibleMatchComparison @RecordId, @SystemId, @CompareId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@CompareId", CompareId)
                    );

            return details.FirstOrDefault();
        }

        #endregion

        #region StudentEnrollment
        public IEnumerable<StudentEnrollmentDetail> GetStudentEnrollmentHistory(int SystemId, long Id)
        {
            var detail = StudentEnrollmentDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentEnrollmentHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        //public StudentEnrollmentMatchDetail GetStudentEnrollmentMatchDetails(int SystemId, long Id)
        //{
        //    return StudentEnrollmentMatchDetails.FromSql(
        //        "EXEC [Integration].[StudentEnrollmentMatchDetail] @RecordId, @SystemId",
        //            new SqlParameter("@RecordId", Id),
        //            new SqlParameter("@SystemId", SystemId)
        //            ).Single();
        //}

        //public List<StudentEnrollmentPossibleMatch> GetStudentEnrollmentPossibleMatches(int SystemId, long? Id)
        //{
        //    return StudentEnrollmentPossibleMatches.FromSql(
        //        "EXEC Integration.StudentEnrollmentGetPossibleMatches @SystemId, @RecordId",
        //            new SqlParameter("@SystemId", SystemId),
        //            new SqlParameter("@RecordId", Id)
        //            ).ToList();
        //}

        //public StudentEnrollmentComparisonDetail GetStudentEnrollmentComparisonDetail(int SystemId, long Id, string CompareId)
        //{
        //    var details = StudentEnrollmentComparisonDetails.FromSql(
        //        "EXEC Integration.StudentEnrollmentGetPossibleMatchComparison @RecordId, @SystemId, @CompareId",
        //            new SqlParameter("@RecordId", Id),
        //            new SqlParameter("@SystemId", SystemId),
        //            new SqlParameter("@CompareId", CompareId)
        //            );

        //    return details.FirstOrDefault();
        //}
        #endregion

        #region Student Academic Involvement

        public StudentAcademicInvolvementDetail GetStudentAcademicInvolvementDetails(int SystemId, long Id)
        {
            return StudentAcademicInvolvementDetails.FromSqlInterpolated($"SELECT * FROM Integration.AcademicInvolvementDetail({Id}, {SystemId})").Single();
        }

        #endregion

        #region Student Academic Catalog

        // Edit View Function
        public IEnumerable<StudentAcademicCatalogDetail> GetStudentAcademicCatalogHistory(int SystemId, long Id)
        {
            return StudentAcademicCatalogDetails.FromSqlInterpolated($"SELECT * FROM Integration.AcademicCatalogHistory({Id}, {SystemId})").ToList();
        }

        #endregion

        #region Student Scholarship

        // Edit View Function
        public IEnumerable<StudentScholarshipDetail> GetStudentScholarshipHistory(int SystemId, long Id)
        {
            var detail = StudentScholarshipDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentScholarshipHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Match View Sproc
        public StudentScholarshipMatchDetail GetStudentScholarshipMatchDetails(int SystemId, long Id)
        {
            return StudentScholarshipMatchDetails.FromSqlInterpolated($"SELECT * FROM Integration.StudentScholarshipMatchDetail({Id}, {SystemId})").Single();
        }

        // Possible Match List Sproc
        public List<StudentScholarshipPossibleMatch> GetStudentScholarshipPossibleMatches(int SystemId, long? Id)
        {
            return StudentScholarshipPossibleMatches.FromSqlRaw(
                "EXEC Integration.StudentScholarshipGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    )
                .OrderByDescending(m => m.MatchConfidence)
                .ToList();
        }

        // Comparison View Sproc
        public StudentScholarshipComparisonDetail GetStudentScholarshipComparisonDetail(int SystemId, long Id, string CompareId)
        {
            return StudentScholarshipComparisonDetails.FromSqlRaw(
                "EXEC Integration.ScholarshipGetPossibleMatchComparison @RecordId, @SystemId, @CompareId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@CompareId", CompareId)
                    ).Single();
        }

        #endregion

        #region Student Parent

        // Student Parent - Edit View Function
        public IEnumerable<StudentParentDetail> GetStudentParentHistory(int SystemId, long Id)
        {
            var detail = StudentParentDetails.FromSqlInterpolated($"SELECT * FROM Integration.ParentHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        // Match View Sproc
        public StudentParentMatchDetail GetStudentParentMatchDetails(int SystemId, long Id)
        {
            return StudentParentMatchDetails.FromSqlRaw(
                "EXEC [Integration].[ParentMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).AsEnumerable().Single();
        }

        // Possible Match List Sproc
        public List<StudentParentPossibleMatch> GetStudentParentPossibleMatches(int SystemId, long? Id)
        {
            return StudentParentPossibleMatches.FromSqlRaw(
                "EXEC Integration.ParentGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
        }

        // Comparison View Sproc
        public StudentParentComparisonDetail GetStudentParentComparisonDetail(int SystemId, long Id, string CompareId)
        {
            StudentParentComparisonDetail studentParentComparisonDetails = StudentParentComparisonDetails.FromSqlRaw(
                "EXEC Integration.ParentGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@CompareId", CompareId)
                    ).AsEnumerable().FirstOrDefault();



            studentParentComparisonDetails.SystemRecords = SystemRecords.FromSqlRaw(
                "EXEC [Integration].[PersonGetSystemRecords] @MasterRecordId",
                    new SqlParameter("@MasterRecordId", CompareId)
                    ).ToList();

            return studentParentComparisonDetails;
        }

        #endregion

        #region Employee

        public IEnumerable<EmployeeDetail> GetEmployeeHistory(int SystemId, long Id)
        {
            var detail = EmployeeDetails.FromSqlInterpolated($"SELECT * FROM Integration.EmployeeHistory({Id}, {SystemId})").ToList();
            return detail;
        }

        public EmployeeMatchDetail GetEmployeeMatchDetails(int SystemId, long Id)
        {
            return EmployeeMatchDetails.FromSqlRaw(
                "EXEC [Integration].[EmployeeMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).AsEnumerable().Single();
        }

        public EmployeeComparisonDetail GetEmployeeComparisonDetail(int SystemId, long Id, string CompareId)
        {
            var details = EmployeeComparisonDetails.FromSqlRaw(
                "EXEC Integration.EmployeeGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@CompareId", CompareId)
                    ).AsEnumerable().FirstOrDefault();

            details.SystemRecords = SystemRecords.FromSqlRaw(
                "EXEC [Integration].[PersonGetSystemRecords] @MasterRecordId",
                    new SqlParameter("@MasterRecordId", CompareId)
                    ).ToList();

            return details;
        }

        public List<EmployeePossibleMatch> GetEmployeePossibleMatches(int SystemId, long? Id)
        {
            return EmployeePossibleMatches.FromSqlRaw(
                "EXEC Integration.EmployeeGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
            
        }

        #endregion

        #region Office Location
        public OfficeLocationSourceDetail OfficeLocationSourceData(int SystemId, long Id)
        {
            return this.OfficeLocationSourceDetails.FromSqlRaw(
                "EXEC Integration.OfficeLocationSourceDetails @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).Single();
        }

        public async Task<OfficeLocationComparisonDetail> GetOfficeLocationComparisonDetail(int SystemId, long Id, int CompareId)
		{
			// TODO: This can return more than 1 record.
			return await OfficeLocationComparisonDetails.FromSqlRaw(
				"EXEC Integration.OfficeLocationGetPossibleMatchComparison @RecordId, @SystemId, @CompareId",
					new SqlParameter("@RecordId", Id),
					new SqlParameter("@SystemId", SystemId),
					new SqlParameter("@CompareId", CompareId)
					).FirstOrDefaultAsync();
		}

		public List<OfficeLocationPossibleMatch> GetOfficeLocationPossibleMatches(int SystemId, long? Id)
		{
			return this.OfficeLocationPossibleMatches.FromSqlRaw(
				"EXEC Integration.OfficeLocationGetPossibleMatches @SystemId, @RecordId",
					new SqlParameter("@SystemId", SystemId),
					new SqlParameter("@RecordId", Id)
					).ToList();
		}
        #endregion

        #region OrganizationalUnit
        public OrganizationalUnitSourceDetail GetOrganizationalUnitSourceDetails(int SystemId, long Id)
        {
            return OrganizationalUnitSourceDetails.FromSqlRaw(
                "EXEC Integration.OrganizationalUnitSourceDetails @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).Single();
        }

        public async Task<OrganizationalUnitComparisonDetail> GetOrganizationalUnitComparisonDetail(int SystemId, long Id, int CompareId)
		{
			// TODO: This can return more than 1 record.
			return await OrganizationalUnitComparisonDetails.FromSqlRaw(
				"EXEC Integration.OrganizationalUnitGetPossibleMatchComparison @RecordId, @SystemId, @CompareId",
					new SqlParameter("@RecordId", Id),
					new SqlParameter("@SystemId", SystemId),
					new SqlParameter("@CompareId", CompareId)
					).FirstOrDefaultAsync();
		}

		public List<OrganizationalUnitPossibleMatch> GetOrganizationalUnitPossibleMatches(int SystemId, long? Id)
		{
			return OrganizationalUnitPossibleMatches.FromSqlRaw(
				"EXEC Integration.OrganizationalUnitGetPossibleMatches @SystemId, @RecordId",
					new SqlParameter("@SystemId", SystemId),
					new SqlParameter("@RecordId", Id)
					).ToList();
        }
        #endregion

        #region Designation
        public IEnumerable<DesignationDetail> GetDesignationHistory(int SystemId, long Id)
        {
            return DesignationDetails.FromSql($"SELECT * FROM Integration.DesignationHistory({Id}, {SystemId})").ToList();
        }

        public DesignationMatchDetail GetDesignationMatchDetails(int SystemId, long Id)
        {
            return DesignationMatchDetails.FromSqlRaw(
                "EXEC [Integration].[DesignationMatchDetail] @RecordId, @SystemId",
                    new SqlParameter("@RecordId", Id),
                    new SqlParameter("@SystemId", SystemId)
                    ).Single();
        }

        public List<DesignationPossibleMatch> GetDesignationPossibleMatches(int SystemId, long? Id)
        {
            return DesignationPossibleMatches.FromSqlRaw(
                "EXEC Integration.DesignationGetPossibleMatches @SystemId, @RecordId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id)
                    ).ToList();
        }

        public DesignationComparisonDetail GetDesignationComparisonDetail(int SystemId, long Id, string CompareId)
        {
            var details = DesignationComparisonDetails.FromSqlRaw(
                "EXEC Integration.DesignationGetPossibleMatchComparison @SystemId, @RecordId, @CompareId",
                    new SqlParameter("@SystemId", SystemId),
                    new SqlParameter("@RecordId", Id),                    
                    new SqlParameter("@CompareId", CompareId)
                    );

            return details.FirstOrDefault();
        }
        #endregion

        public RemediationCounts GetRemediationCounts()
        {
            return this.RemediationCounts.FromSqlRaw("EXEC Integration.GetRemediationCounts").AsEnumerable().Single();
        }

        public async Task<IntegrationStatus[]> LoadIntegrationStatusesBySystem(string System)
        {
            if (System == null)
                return await this.IntegrationsStatuses.Where(p => p.IsProducer.Equals(1)).ToArrayAsync();
            else
                return await this.IntegrationsStatuses.Where(p => p.SystemName.Equals(System, StringComparison.InvariantCultureIgnoreCase) && p.IsProducer.Equals(1)).ToArrayAsync();
        }

        public async Task<IntegrationStatus[]> LoadIntegrationStatusesByIntegration(string Integration)
        {
            if (Integration == null)
                return await this.IntegrationsStatuses.Where(p => p.IsProducer.Equals(1)).ToArrayAsync();
            else
                return await this.IntegrationsStatuses.Where(p => p.IntegrationName.Equals(Integration, StringComparison.InvariantCultureIgnoreCase) && p.IsProducer.Equals(1)).ToArrayAsync();
        }

        public async Task<LogEntry[]> LoadTopDrivers(DateTime? StartDate, DateTime? EndDate)
        {
            return await LogEntries.FromSqlRaw("SELECT * FROM [Log].[GetTopDrivers](@StartDate, @EndDate)",
                        new SqlParameter("@StartDate", (object)StartDate ?? DBNull.Value),
                        new SqlParameter("@EndDate", (object)EndDate ?? DBNull.Value)
                ).ToArrayAsync();
        }

        public async Task<QueueEntryCountRow[]> GetQueueEntryCounts()
        {
            return await QueueEntryCounts.FromSqlRaw("EXEC Integration.GetQueueEntryCounts").ToArrayAsync();
        }

        public int SetTriggerState(string Schema, string Table, bool ActionIsEnable)
        {
            try
            {
               Database.ExecuteSql($"{$"{(ActionIsEnable ? "ENABLE" : "DISABLE")}"} TRIGGER [{@Schema}].[{@Table}_Trigger] ON [{@Schema}].[{@Table}]",
                        new SqlParameter("@Schema", Schema),
                        new SqlParameter("@Table", Table)
                    );
                return 0;
            }
            catch
            {
                return -1;
            }
        }

        public int ChangeStudentIntegrationRecord(int SystemId, long RecordId, string FirstName, string PreferredName, string MiddleName, string LastName, string MaidenName, string StudentId, 
            string Suffix, string SuffixSourceSystemRecordId, string SuffixMasterId, 
            string BirthDate, string DeceasedDate, string MaritalStatus, string MaritalStatusCode, 
            string MaritalStatusSourceSystemRecordId, string MaritalStatusMasterID, string FERPAInformationRelease, string CitizenshipCountryCode, 
            string CitizenshipCountryMasterId, string DischargedTermCode, string DischargedAcademicCareerName, string DischargedAcademicCareerCode, string DischargedTermMasterId, string EmailAddress1, string EmailAddress1MasterRecordId, string EmailAddress2, string EmailAddress2MasterRecordId, string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.StudentChangeIntegrationRecord @SystemId, @RecordId, @FirstName, @PreferredName, @MiddleName, @LastName, @MaidenName, @StudentId, @Suffix, @SuffixSourceSystemRecordId, @SuffixMasterId, @BirthDate, @DeceasedDate, @MaritalStatus, @MaritalStatusCode, @MaritalStatusSourceSystemRecordId, @MaritalStatusMasterID, @FERPAInformationRelease, @CitizenshipCountryCode, @CitizenshipCountryMasterID, @DischargedTermCode, @DischargedAcademicCareerName, @DischargedAcademicCareerCode, @DischargedAcademicCalendarMasterId, @EmailAddress1, @EmailAddress1MasterRecordId, @EmailAddress2, @EmailAddress2MasterRecordId, @ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                        new SqlParameter("@PreferredName", (object)PreferredName ?? DBNull.Value),
                        new SqlParameter("@MiddleName", (object)MiddleName ?? DBNull.Value),
                        new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                        new SqlParameter("@MaidenName", (object)MaidenName ?? DBNull.Value),
                        new SqlParameter("@StudentId", (object)StudentId ?? DBNull.Value),
                        new SqlParameter("@Suffix", (object)Suffix ?? DBNull.Value),
                        new SqlParameter("@SuffixSourceSystemRecordId", (object)SuffixSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@SuffixMasterId", (object)SuffixMasterId ?? DBNull.Value),
                        new SqlParameter("@BirthDate", (object)BirthDate ?? DBNull.Value),
                        new SqlParameter("@DeceasedDate", (object)DeceasedDate ?? DBNull.Value),
                        new SqlParameter("@MaritalStatus", (object)MaritalStatus ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusCode", (object)MaritalStatusCode ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusSourceSystemRecordId", (object)MaritalStatusSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusMasterID", (object)MaritalStatusMasterID ?? DBNull.Value),
                        new SqlParameter("@FERPAInformationRelease", (object)FERPAInformationRelease ?? DBNull.Value),
                        new SqlParameter("@CitizenshipCountryCode", (object)CitizenshipCountryCode ?? DBNull.Value),
                        new SqlParameter("@CitizenshipCountryMasterID", (object)CitizenshipCountryMasterId ?? DBNull.Value),
                        new SqlParameter("@DischargedTermCode", (object)DischargedTermCode ?? DBNull.Value),
                        new SqlParameter("@DischargedAcademicCareerName", (object)DischargedAcademicCareerName ?? DBNull.Value),
                        new SqlParameter("@DischargedAcademicCareerCode", (object)DischargedAcademicCareerCode ?? DBNull.Value),
                        new SqlParameter("@DischargedAcademicCalendarMasterId", (object)DischargedTermMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress1", (object)EmailAddress1 ?? DBNull.Value),
                        new SqlParameter("@EmailAddress1MasterRecordId", (object)EmailAddress1MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress2", (object)EmailAddress2 ?? DBNull.Value),
                        new SqlParameter("@EmailAddress2MasterRecordId", (object)EmailAddress2MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeStudentAcademicInvolvementIntegrationRecord(int SystemId, long RecordId, string StudentId, string AcademicInvolvementAcademicYear, string AcademicInvolvementTerm, string AcademicInvolvementType, string AcademicInvolvementTypeMasterId, string AcademicInvolvementName, string AcademicInvolvementNameMasterId)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.StudentAcademicInvolvementChangeIntegrationRecord @SystemId, @RecordId, @StudentId, @AcademicInvolvementAcademicYear, @AcademicInvolvementTerm, @AcademicInvolvementType, @AcademicInvolvementTypeMasterId, @AcademicInvolvementName, @AcademicInvolvementNameMasterId",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@StudentId", (object)StudentId ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementAcademicYear", (object)AcademicInvolvementAcademicYear ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementTerm", (object)AcademicInvolvementTerm ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementType", (object)AcademicInvolvementType ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementTypeMasterId", (object)AcademicInvolvementTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementName", (object)AcademicInvolvementName ?? DBNull.Value),
                        new SqlParameter("@AcademicInvolvementNameMasterId", (object)AcademicInvolvementNameMasterId ?? DBNull.Value)
                );
        }

        public int ChangeStudentAcademicCatalogIntegrationRecord(int SystemId, long RecordId, string DegreeTypeName, string DegreeTypeSourceSystemRecordID, string DegreeTypeMasterId,
            string AcademicCareerName, string AcademicCareerSourceSystemRecordID, string AcademicCareerMasterId, string AcademicProgramName, string AcademicProgramSourceSystemRecordID, string AcademicProgramMasterId,
            string AcademicPlanName, string DepartmentName, string DepartmentCode, string DepartmentMasterId,
            string AcademicPlanTypeName, string AcademicPlanTypeSourceSystemRecordID, string AcademicPlanTypeMasterId, string TranscriptDescription, string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.AcademicCatalogChangeIntegrationRecord @SystemId, @RecordId, @DegreeTypeName, @DegreeTypeSourceSystemRecordID, @DegreeTypeMasterId, @AcademicCareerName, @AcademicCareerSourceSystemRecordID, @AcademicCareerMasterId, @AcademicProgramName, @AcademicProgramSourceSystemRecordID, @AcademicProgramMasterId, @AcademicPlanName, @DepartmentName, @DepartmentCode, @DepartmentMasterId, @AcademicPlanTypeName, @AcademicPlanTypeSourceSystemRecordID, @AcademicPlanTypeMasterId, @TranscriptDescription, @ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@DegreeTypeName", (object)DegreeTypeName ?? DBNull.Value),
                            new SqlParameter("@DegreeTypeSourceSystemRecordID", (object)DegreeTypeSourceSystemRecordID ?? DBNull.Value),
                            new SqlParameter("@DegreeTypeMasterId", (object)DegreeTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicCareerName", (object)AcademicCareerName ?? DBNull.Value),
                            new SqlParameter("@AcademicCareerSourceSystemRecordID", (object)AcademicCareerSourceSystemRecordID ?? DBNull.Value),
                            new SqlParameter("@AcademicCareerMasterId", (object)AcademicCareerMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicProgramName", (object)AcademicProgramName ?? DBNull.Value),
                            new SqlParameter("@AcademicProgramSourceSystemRecordID", (object)AcademicProgramSourceSystemRecordID ?? DBNull.Value),
                            new SqlParameter("@AcademicProgramMasterId", (object)AcademicProgramMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicPlanName", (object)AcademicPlanName ?? DBNull.Value),
                        new SqlParameter("@DepartmentName", (object)DepartmentName ?? DBNull.Value),
                            new SqlParameter("@DepartmentCode", (object)DepartmentCode ?? DBNull.Value),
                            new SqlParameter("@DepartmentMasterId", (object)DepartmentMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicPlanTypeName", (object)AcademicPlanTypeName ?? DBNull.Value),
                            new SqlParameter("@AcademicPlanTypeSourceSystemRecordID", (object)AcademicPlanTypeSourceSystemRecordID ?? DBNull.Value),
                            new SqlParameter("@AcademicPlanTypeMasterId", (object)AcademicPlanTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@TranscriptDescription", (object)TranscriptDescription ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeStudentAcademicPlanIntegrationRecord(int SystemId, long RecordId, string Student, string StudentMasterId,
            string AcademicCareer, string AcademicCareerSourceSystemRecordId, string AcademicCareerMasterId,
            string Degree, string DegreeSourceSystemRecordId, string DegreeMasterId,
            string CampusSourceSystemRecordId, string CampusMasterId,
            string Term, string TermSourceSystemRecordId, string TermMasterId,
            string AcademicPlan, string AcademicPlanSourceSystemRecordId, string AcademicPlanMasterId,
            string AcademicSubplan, string AcademicSubplanSourceSystemRecordId, string AcademicSubplanMasterId,
            string StudentAcademicPlanStatus, string StudentAcademicPlanStatusSourceSystemRecordId, string StudentAcademicPlanStatusMasterId,
            string ExpectedGraduationTerm, string ExpectedGraduationTermSourceSystemRecordId, string ExpectedGraduationTermMasterId,
            string ChangeAgent)
        {

            try
            {
                return this.Database.ExecuteSqlRaw(
                        "Integration.StudentAcademicPlanChangeIntegrationRecord @SystemId, @RecordId, @Student, @StudentMasterId," +
                        "@AcademicCareer, @AcademicCareerSourceSystemRecordID, @AcademicCareerMasterId," +
                        "@Degree, @DegreeSourceSystemRecordID, @DegreeMasterId," +
                        "@CampusSourceSystemRecordID, @CampusMasterId," +
                        "@Term, @TermSourceSystemRecordID, @TermMasterId," +
                        "@AcademicPlan, @AcademicPlanSourceSystemRecordID, @AcademicPlanMasterId," +
                        "@AcademicSubplan, @AcademicSubplanSourceSystemRecordID, @AcademicSubplanMasterId," +
                        "@StudentAcademicPlanStatus, @StudentAcademicPlanStatusSourceSystemRecordId, @StudentAcademicPlanStatusMasterId," +
                        "@ExpectedGraduationTerm, @ExpectedGraduationTermSourceSystemRecordId, @ExpectedGraduationTermMasterId," +
                        "@ChangeAgent",
                                new SqlParameter("@SystemId", SystemId),
                                new SqlParameter("@RecordId", RecordId),
                                new SqlParameter("@Student", (object)Student ?? DBNull.Value),
                                    new SqlParameter("@StudentMasterId", (object)StudentMasterId ?? DBNull.Value),
                                new SqlParameter("@AcademicCareer", (object)AcademicCareer ?? DBNull.Value),
                                    new SqlParameter("@AcademicCareerSourceSystemRecordID", (object)AcademicCareerSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@AcademicCareerMasterId", (object)AcademicCareerMasterId ?? DBNull.Value),
                                new SqlParameter("@Degree", (object)Degree ?? DBNull.Value),
                                    new SqlParameter("@DegreeSourceSystemRecordID", (object)DegreeSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@DegreeMasterId", (object)DegreeMasterId ?? DBNull.Value),
                                new SqlParameter("@CampusSourceSystemRecordID", (object)CampusSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@CampusMasterId", (object)CampusMasterId ?? DBNull.Value),
                                new SqlParameter("@Term", (object)Term ?? DBNull.Value),
                                    new SqlParameter("@TermSourceSystemRecordID", (object)TermSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@TermMasterId", (object)TermMasterId ?? DBNull.Value),
                                new SqlParameter("@AcademicPlan", (object)AcademicPlan ?? DBNull.Value),
                                    new SqlParameter("@AcademicPlanSourceSystemRecordID", (object)AcademicPlanSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@AcademicPlanMasterId", (object)AcademicPlanMasterId ?? DBNull.Value),
                                new SqlParameter("@AcademicSubplan", (object)AcademicSubplan ?? DBNull.Value),
                                    new SqlParameter("@AcademicSubplanSourceSystemRecordID", (object)AcademicSubplanSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@AcademicSubplanMasterId", (object)AcademicSubplanMasterId ?? DBNull.Value),
                                new SqlParameter("@StudentAcademicPlanStatus", (object)StudentAcademicPlanStatus ?? DBNull.Value),
                                    new SqlParameter("@StudentAcademicPlanStatusSourceSystemRecordID", (object)StudentAcademicPlanStatusSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@StudentAcademicPlanStatusMasterId", (object)StudentAcademicPlanStatusMasterId ?? DBNull.Value),
                                new SqlParameter("@ExpectedGraduationTerm", (object)ExpectedGraduationTerm ?? DBNull.Value),
                                    new SqlParameter("@ExpectedGraduationTermSourceSystemRecordID", (object)ExpectedGraduationTermSourceSystemRecordId ?? DBNull.Value),
                                    new SqlParameter("@ExpectedGraduationTermMasterId", (object)ExpectedGraduationTermMasterId ?? DBNull.Value),
                                new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                        );
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int ChangeStudentEnrollmentIntegrationRecord(int SystemId, long RecordId, string StudentId, string StudentMasterId,
            string TermCode, string TermName, string TermMasterId, string CampusName, string CampusSourceSystemRecordId, string CampusMasterId,
            string AcademicCareerName, string AcademicCareerSourceSystemRecordId, string AcademicCareerMasterId, string AcademicLevelName, string AcademicLevelSourceSystemRecordId, string AcademicLevelMasterId,
            string TotalTransferUnits, string TotalCumulativeUnits, string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.StudentEnrollmentChangeIntegrationRecord @SystemId, @RecordId, @StudentId, @StudentMasterId," +
                "@TermCode, @TermName, @TermMasterId, @CampusName, @CampusSourceSystemRecordId, @CampusMasterId," +
                "@AcademicCareerName, @AcademicCareerSourceSystemRecordId, @AcademicCareerMasterId, @AcademicLevelName, @AcademicLevelSourceSystemRecordId, @AcademicLevelMasterId," +
                "@TotalTransferUnits, @TotalCumulativeUnits, @ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@StudentId", (object)StudentId ?? DBNull.Value),
                            new SqlParameter("@StudentMasterId", (object)StudentMasterId ?? DBNull.Value),
                        new SqlParameter("@TermCode", (object)TermCode ?? DBNull.Value),
                            new SqlParameter("@TermName", (object)TermName ?? DBNull.Value),
                            new SqlParameter("@TermMasterId", (object)TermMasterId ?? DBNull.Value),
                        new SqlParameter("@CampusName", (object)CampusName ?? DBNull.Value),
                            new SqlParameter("@CampusSourceSystemRecordId", (object)CampusSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@CampusMasterId", (object)CampusMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicCareerName", (object)AcademicCareerName ?? DBNull.Value),
                            new SqlParameter("@AcademicCareerSourceSystemRecordId", (object)AcademicCareerSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@AcademicCareerMasterId", (object)AcademicCareerMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicLevelName", (object)AcademicLevelName ?? DBNull.Value),
                            new SqlParameter("@AcademicLevelSourceSystemRecordId", (object)AcademicLevelSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@AcademicLevelMasterId", (object)AcademicLevelMasterId ?? DBNull.Value),
                        new SqlParameter("@TotalTransferUnits", (object)TotalTransferUnits ?? DBNull.Value),
                        new SqlParameter("@TotalCumulativeUnits", (object)TotalCumulativeUnits ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeStudentDegreeIntegrationRecord(int SystemId, long RecordId, string ClassOf, string Student, string StudentMasterId,
            string EducationalInstitution, string EducationalInstitutionSourceSystemRecordId, string EducationalInstitutionMasterId,
            string PreferredClassOf, string AwardedDate,
            string HonorSourceSystemRecordId, string HonorMasterId,
            string DegreeStatus, string DegreeStatusSourceSystemRecordId, string DegreeStatusMasterId,
            string AwardedTerm, string AwardedTermMasterId,
            string DegreeType, string DegreeTypeSourceSystemRecordId, string DegreeTypeMasterId,
            string AcademicCareer, string AcademicCareerSourceSystemRecordId, string AcademicCareerMasterId,
            string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.StudentDegreeChangeIntegrationRecord @SystemId, @RecordId, @ClassOf, @Student, @StudentMasterId," +
                "@EducationalInstitution, @EducationalInstitutionSourceSystemRecordId, @EducationalInstitutionMasterId," +
                "@PreferredClassOf, @AwardedDate," +
                "@DegreeHonorSourceSystemRecordId, @DegreeHonorMasterId," +
                "@DegreeStatus, @DegreeStatusSourceSystemRecordId, @DegreeStatusMasterId," +
                "@AwardedTerm, @AwardedTermMasterId," +
                "@DegreeType, @DegreeTypeSourceSystemRecordId, @DegreeTypeMasterId," +
                "@AcademicCareer, @AcademicCareerSourceSystemRecordId, @AcademicCareerMasterId," +
                "@ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@ClassOf", (object)ClassOf ?? DBNull.Value),
                        new SqlParameter("@Student", (object)Student ?? DBNull.Value),
                        new SqlParameter("@StudentMasterId", (object)StudentMasterId ?? DBNull.Value),
                        new SqlParameter("@EducationalInstitution", (object)EducationalInstitution ?? DBNull.Value),
                        new SqlParameter("@EducationalInstitutionSourceSystemRecordId", (object)EducationalInstitutionSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@EducationalInstitutionMasterId", (object)EducationalInstitutionMasterId ?? DBNull.Value),
                        new SqlParameter("@PreferredClassOf", (object)PreferredClassOf ?? DBNull.Value),
                        new SqlParameter("@AwardedDate", (object)AwardedDate ?? DBNull.Value),
                        new SqlParameter("@DegreeHonorSourceSystemRecordId", (object)HonorSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@DegreeHonorMasterId", (object)HonorMasterId ?? DBNull.Value),
                        new SqlParameter("@DegreeStatus", (object)DegreeStatus ?? DBNull.Value),
                        new SqlParameter("@DegreeStatusSourceSystemRecordId", (object)DegreeStatusSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@DegreeStatusMasterId", (object)DegreeStatusMasterId ?? DBNull.Value),
                        new SqlParameter("@AwardedTerm", (object)AwardedTerm ?? DBNull.Value),
                        new SqlParameter("@AwardedTermMasterId", (object)AwardedTermMasterId ?? DBNull.Value),
                        new SqlParameter("@DegreeType", (object)DegreeType ?? DBNull.Value),
                        new SqlParameter("@DegreeTypeSourceSystemRecordId", (object)DegreeTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@DegreeTypeMasterId", (object)DegreeTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@AcademicCareer", (object)AcademicCareer ?? DBNull.Value),
                        new SqlParameter("@AcademicCareerSourceSystemRecordId", (object)AcademicCareerSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@AcademicCareerMasterId", (object)AcademicCareerMasterId ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeStudentScholarshipIntegrationRecord(int SystemId, long RecordId, string StudentId, string StudentMasterId, string TermCode, string TermMasterId, string KFSAccount, string DesignationMasterId, string ScholarshipCode, string ScholarshipName,
            string ScholarshipMasterId, decimal? Amount, string DepartmentCode, string DepartmentMasterId)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.ScholarshipChangeIntegrationRecord @SystemId, @RecordId, @StudentId, @StudentMasterId, @TermCode, @TermMasterId, @KFSAccount, @DesignationMasterId, @ScholarshipCode, @ScholarshipName, @ScholarshipMasterId, @Amount, @DepartmentCode, @DepartmentMasterId",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@StudentId", (object)StudentId ?? DBNull.Value),
                        new SqlParameter("@StudentMasterId", (object)StudentMasterId ?? DBNull.Value),
                        new SqlParameter("@TermCode", (object)TermCode ?? DBNull.Value),
                        new SqlParameter("@TermMasterId", (object)TermMasterId ?? DBNull.Value),
                        new SqlParameter("@KFSAccount", (object)KFSAccount ?? DBNull.Value),
                        new SqlParameter("@DesignationMasterId", (object)DesignationMasterId ?? DBNull.Value),
                        new SqlParameter("@ScholarshipCode", (object)ScholarshipCode ?? DBNull.Value),
                        new SqlParameter("@ScholarshipName", (object)ScholarshipName ?? DBNull.Value),
                        new SqlParameter("@ScholarshipMasterId", (object)ScholarshipMasterId ?? DBNull.Value),
                        new SqlParameter("@Amount", (object)Amount ?? DBNull.Value),
                        new SqlParameter("@DepartmentCode", (object)DepartmentCode ?? DBNull.Value),
                        new SqlParameter("@DepartmentMasterId", (object)DepartmentMasterId ?? DBNull.Value)
                );
        }

        public int ChangeEmployeeIntegrationRecord(int SystemId, long RecordId, string EmployeeConstituentId, string FirstName, string PreferredName, string MiddleName, string LastName, string MaidenName, string UAPersonId,
            string Suffix, string SuffixSourceSystemRecordId, string SuffixMasterId,
            string BirthDate, string DeceasedDate, string MaritalStatus, string MaritalStatusSourceSystemRecordId, string MaritalStatusMasterId,
            string OrganizationName, string OrganizationSourceSystemRecordId, string OrganizationMasterId, string HireDate, string TerminationDate, string EmployeeType, string EmployeeTypeSourceSystemRecordId, string EmployeeTypeMasterId, 
            string EmailAddress1, string EmailAddress1MasterRecordId, string EmailAddress2, string EmailAddress2MasterRecordId, string NetId, string ChangeAgent)
        {


            return this.Database.ExecuteSqlRaw(
                "Integration.EmployeeChangeIntegrationRecord @SystemId, @RecordId, @EmployeeConstituentId, @FirstName, @PreferredName, @MiddleName, @LastName, @MaidenName, @UAPersonId, @Suffix, @SuffixSourceSystemRecordId, @SuffixMasterId, @BirthDate, @DeceasedDate, @MaritalStatus, @MaritalStatusSourceSystemRecordId, @MaritalStatusMasterId, @OrganizationName, @OrganizationSourceSystemRecordId, @OrganizationMasterId, @HireDate, @TerminationDate, @EmployeeType, @EmployeeTypeSourceSystemRecordId, @EmployeeTypeMasterId, @EmailAddress1, @EmailAddress1MasterRecordId, @EmailAddress2, @EmailAddress2MasterRecordId, @NetId, @ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@EmployeeConstituentId", (object)EmployeeConstituentId ?? DBNull.Value),
                        new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                        new SqlParameter("@PreferredName", (object)PreferredName ?? DBNull.Value),
                        new SqlParameter("@MiddleName", (object)MiddleName ?? DBNull.Value),
                        new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                        new SqlParameter("@MaidenName", (object)MaidenName ?? DBNull.Value),
                        new SqlParameter("@UAPersonId", (object)UAPersonId ?? DBNull.Value),
                        new SqlParameter("@Suffix", (object)Suffix ?? DBNull.Value),
                        new SqlParameter("@SuffixSourceSystemRecordId", (object)SuffixSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@SuffixMasterId", (object)SuffixMasterId ?? DBNull.Value),
                        new SqlParameter("@BirthDate", (object)BirthDate ?? DBNull.Value),
                        new SqlParameter("@DeceasedDate", (object)DeceasedDate ?? DBNull.Value),
                        new SqlParameter("@MaritalStatus", (object)MaritalStatus ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusSourceSystemRecordId", (object)MaritalStatusSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusMasterId", (object)MaritalStatusMasterId ?? DBNull.Value),
                        new SqlParameter("@OrganizationName", (object)OrganizationName ?? DBNull.Value),
                        new SqlParameter("@OrganizationSourceSystemRecordId", (object)OrganizationSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@OrganizationMasterId", (object)OrganizationMasterId ?? DBNull.Value),
                        new SqlParameter("@HireDate", (object)HireDate ?? DBNull.Value),
                        new SqlParameter("@TerminationDate", (object)TerminationDate ?? DBNull.Value),
                        new SqlParameter("@EmployeeType", (object)EmployeeType ?? DBNull.Value),
                        new SqlParameter("@EmployeeTypeSourceSystemRecordId", (object)EmployeeTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@EmployeeTypeMasterId", (object)EmployeeTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress1", (object)EmailAddress1 ?? DBNull.Value),
                        new SqlParameter("@EmailAddress1MasterRecordId", (object)EmailAddress1MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress2", (object)EmailAddress2 ?? DBNull.Value),
                        new SqlParameter("@EmailAddress2MasterRecordId", (object)EmailAddress2MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@NetId", (object)NetId ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeConstituentIntegrationRecord(int SystemId, long RecordId, string FirstName, string PreferredName, string MiddleName, string LastName, string MaidenName, string UAPersonId, 
            string Suffix, string SuffixSourceSystemRecordId, string SuffixMasterId,
            string BirthDate, string DeceasedDate, string MaritalStatus, string MaritalStatusSourceSystemRecordId, string MaritalStatusMasterId, 
            string Address, string AddressSourceSystemRecordId, string AddressMasterId, string City, string PostalCode, string State, string StateSourceSystemRecordId, 
            string StateMasterId, string Country, string CountrySourceSystemRecordId, string CountryMasterId, string AddressUseType, string AddressUseTypeSourceSystemRecordId, string AddressUseTypeMasterId, bool? AddressIsPrimary,  
            string EmailAddress, string EmailAddressSourceSystemRecordId, string EmailAddressMasterId, string EmailAddressUseType, string EmailAddressUseTypeSourceSystemRecordId, string EmailAddressUseTypeMasterId, bool? EmailIsPrimary, string PhoneNumber, string PhoneNumberSourceSystemRecordId, string PhoneExtension,
            string PhoneMasterId, string PhoneCountryCode, string PhoneCountrySourceSystemRecordId, string PhoneCountryMasterRecordId, string PhoneLineType, string PhoneLineTypeSourceSystemRecordId, string PhoneLineTypeMasterRecordId, 
            string PhoneUseType, string PhoneUseTypeSourceSystemRecordId, string PhoneUseTypeMasterId,  bool? PhoneIsPrimary, string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw("Integration.ConstituentChangeIntegrationRecord @SystemId, @RecordId, @FirstName, @PreferredName, @MiddleName, @LastName, @MaidenName, @UAPersonId, @Suffix, @SuffixSourceSystemRecordId, @SuffixMasterId, @BirthDate, @DeceasedDate, @MaritalStatus, @MaritalStatusSourceSystemRecordId, @MaritalStatusMasterId, @Address, @AddressSourceSystemRecordId, @AddressMasterId, @City, @PostalCode, @State, @StateSourceSystemRecordId, @StateMasterId, @Country, @CountrySourceSystemRecordId, @CountryMasterId, @AddressUseType, @AddressUseTypeSourceSystemRecordId, @AddressUseTypeMasterId, @AddressIsPrimary, @EmailAddress, @EmailAddressSourceSystemRecordId, @EmailAddressMasterId, @EmailAddressUseType, @EmailAddressUseTypeSourceSystemRecordId, @EmailAddressUseTypeMasterId, @EmailIsPrimary, @PhoneNumber, @PhoneNumberSourceSystemRecordId, @PhoneExtension, @PhoneMasterId, @PhoneCountryCode, @PhoneCountrySourceSystemRecordId, @PhoneCountryMasterRecordId, @PhoneLineType, @PhoneLineTypeSourceSystemRecordId, @PhoneLineTypeMasterRecordId, @PhoneUseType, @PhoneUseTypeSourceSystemRecordId, @PhoneUseTypeMasterId, @PhoneIsPrimary, @ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                        new SqlParameter("@PreferredName", (object)PreferredName ?? DBNull.Value),
                        new SqlParameter("@MiddleName", (object)MiddleName ?? DBNull.Value),
                        new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                        new SqlParameter("@MaidenName", (object)MaidenName ?? DBNull.Value),
                        new SqlParameter("@UAPersonId", (object)UAPersonId ?? DBNull.Value),
                        new SqlParameter("@Suffix", (object)Suffix ?? DBNull.Value),
                        new SqlParameter("@SuffixSourceSystemRecordId", (object)SuffixSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@SuffixMasterId", (object)SuffixMasterId ?? DBNull.Value),
                        new SqlParameter("@BirthDate", (object)BirthDate ?? DBNull.Value),
                        new SqlParameter("@DeceasedDate", (object)DeceasedDate ?? DBNull.Value),
                        new SqlParameter("@MaritalStatus", (object)MaritalStatus ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusSourceSystemRecordId", (object)MaritalStatusSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@MaritalStatusMasterId", (object)MaritalStatusMasterId ?? DBNull.Value),
                        new SqlParameter("@Address", (object)Address ?? DBNull.Value),
                        new SqlParameter("@AddressSourceSystemRecordId", (object)AddressSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@AddressMasterId", (object)AddressMasterId ?? DBNull.Value),
                        new SqlParameter("@City", (object)City ?? DBNull.Value),
                        new SqlParameter("@PostalCode", (object)PostalCode ?? DBNull.Value),
                        new SqlParameter("@State", (object)State ?? DBNull.Value),
                        new SqlParameter("@StateSourceSystemRecordId", (object)StateSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@StateMasterId", (object)StateMasterId ?? DBNull.Value),
                        new SqlParameter("@Country", (object)Country ?? DBNull.Value),
                        new SqlParameter("@CountrySourceSystemRecordId", (object)CountrySourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@CountryMasterId", (object)CountryMasterId ?? DBNull.Value),
                        new SqlParameter("@AddressUseType", (object)AddressUseType ?? DBNull.Value),
                        new SqlParameter("@AddressUseTypeSourceSystemRecordId", (object)AddressUseTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@AddressUseTypeMasterId", (object)AddressUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@AddressIsPrimary", System.Data.SqlDbType.Bit) { Value = AddressIsPrimary.HasValue ? (object)AddressIsPrimary.Value : DBNull.Value },
                        new SqlParameter("@EmailAddress", (object)EmailAddress ?? DBNull.Value),
                        new SqlParameter("@EmailAddressSourceSystemRecordId", (object)EmailAddressSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddressMasterId", (object)EmailAddressMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailAddressUseType", (object)EmailAddressUseType ?? DBNull.Value),
                        new SqlParameter("@EmailAddressUseTypeSourceSystemRecordId", (object)EmailAddressUseTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddressUseTypeMasterId", (object)EmailAddressUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailIsPrimary", System.Data.SqlDbType.Bit) { Value = EmailIsPrimary.HasValue ? (object)EmailIsPrimary : DBNull.Value },
                        new SqlParameter("@PhoneNumber", (object)PhoneNumber ?? DBNull.Value),
                        new SqlParameter("@PhoneNumberSourceSystemRecordId", (object)PhoneNumberSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneExtension", (object)PhoneExtension ?? DBNull.Value),
                        new SqlParameter("@PhoneMasterId", (object)PhoneMasterId ?? DBNull.Value),
                        new SqlParameter("@PhoneCountryCode", (object)PhoneCountryCode ?? DBNull.Value),
                        new SqlParameter("@PhoneCountrySourceSystemRecordId", (object)PhoneCountrySourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneCountryMasterRecordId", (object)PhoneCountryMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneLineType", (object)PhoneLineType ?? DBNull.Value),
                        new SqlParameter("@PhoneLineTypeSourceSystemRecordId", (object)PhoneLineTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneLineTypeMasterRecordId", (object)PhoneLineTypeMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneUseType", (object)PhoneUseType ?? DBNull.Value),
                        new SqlParameter("@PhoneUseTypeSourceSystemRecordId", (object)PhoneUseTypeSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneUseTypeMasterId", (object)PhoneUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@PhoneIsPrimary", System.Data.SqlDbType.Bit) { Value = PhoneIsPrimary.HasValue ? (object)PhoneIsPrimary : DBNull.Value},
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ConstituentPhoneChangeIntegrationRecord(int SystemId, long RecordId,
            string ConstituentSourceSystemRecordId, string FirstName, string LastName, string UAPersonId, string ConstituentMasterId,
            string PhoneNumber, string PhoneExtension, string PhoneMasterId,
            string CountryCode, string CountrySourceSystemRecordId, string CountryMasterRecordId,
            string PhoneLineType, string PhoneLineTypeSourceSystemRecordId, string PhoneLineTypeMasterRecordId,
            string PhoneUseType, string PhoneUseTypeSourceSystemRecordId, string PhoneUseTypeMasterId, bool? PhoneIsPrimary,
            string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.ConstituentPhoneChangeIntegrationRecord @SystemId, @RecordId," +
                "@ConstituentSourceSystemRecordId, @FirstName, @LastName, @UAPersonId, @ConstituentMasterId," +
                "@PhoneNumber, @PhoneExtension, @PhoneMasterId," +
                "@CountryCode, @CountrySourceSystemRecordId, @CountryMasterRecordId," +
                "@PhoneLineType, @PhoneLineTypeSourceSystemRecordId, @PhoneLineTypeMasterRecordId," +
                "@PhoneUseType, @PhoneUseTypeSourceSystemRecordId, @PhoneUseTypeMasterId, @PhoneIsPrimary," +
                "@ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@ConstituentSourceSystemRecordId", (object)ConstituentSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                            new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                            new SqlParameter("@UAPersonId", (object)UAPersonId ?? DBNull.Value),
                            new SqlParameter("@ConstituentMasterId", (object)ConstituentMasterId ?? DBNull.Value),
                        new SqlParameter("@PhoneNumber", (object)PhoneNumber ?? DBNull.Value),
                            new SqlParameter("@PhoneExtension", (object)PhoneExtension ?? DBNull.Value),
                            new SqlParameter("@PhoneMasterId", (object)PhoneMasterId ?? DBNull.Value),
                        new SqlParameter("@CountryCode", (object)CountryCode ?? DBNull.Value),
                            new SqlParameter("@CountrySourceSystemRecordId", (object)CountrySourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@CountryMasterRecordId", (object)CountryMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneLineType", (object)PhoneLineType ?? DBNull.Value),
                            new SqlParameter("@PhoneLineTypeSourceSystemRecordId", (object)PhoneLineTypeSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@PhoneLineTypeMasterRecordId", (object)PhoneLineTypeMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@PhoneUseType", (object)PhoneUseType ?? DBNull.Value),
                            new SqlParameter("@PhoneUseTypeSourceSystemRecordId", (object)PhoneUseTypeSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@PhoneUseTypeMasterId", (object)PhoneUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@PhoneIsPrimary", (object)PhoneIsPrimary ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ConstituentEmailChangeIntegrationRecord(int SystemId, long RecordId,
            string ConstituentSourceSystemRecordId, string ConstituentMasterId, string FirstName, string LastName, string UAPersonId,
            string EmailAddress, string EmailAddressMasterId,
            string EmailAddressUseType, string EmailAddressUseTypeSourceSystemRecordId, string EmailAddressUseTypeMasterId, bool? EmailIsPrimary,
            string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.ConstituentEmailChangeIntegrationRecord @SystemId, @RecordId," +
                "@ConstituentSourceSystemRecordId, @ConstituentMasterId, @FirstName, @LastName, @UAPersonId," +
                "@EmailAddress, @EmailAddressMasterId," +
                "@EmailAddressUseType, @EmailAddressUseTypeSourceSystemRecordId, @EmailAddressUseTypeMasterId, @EmailIsPrimary," +
                "@ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@ConstituentSourceSystemRecordId", (object)ConstituentSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@ConstituentMasterId", (object)ConstituentMasterId ?? DBNull.Value),
                            new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                            new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                            new SqlParameter("@UAPersonId", (object)UAPersonId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress", (object)EmailAddress ?? DBNull.Value),
                            new SqlParameter("@EmailAddressMasterId", (object)EmailAddressMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailAddressUseType", (object)EmailAddressUseType ?? DBNull.Value),
                            new SqlParameter("@EmailAddressUseTypeSourceSystemRecordId", (object)EmailAddressUseTypeSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@EmailAddressUseTypeMasterId", (object)EmailAddressUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@EmailIsPrimary", System.Data.SqlDbType.Bit) { Value = EmailIsPrimary.HasValue ? (object)EmailIsPrimary : DBNull.Value },
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ConstituentAddressChangeIntegrationRecord(int SystemId, long RecordId,
            string ConstituentSourceSystemRecordId, string ConstituentMasterId, string FirstName, string LastName, string UAPersonId,
            string Address, string AddressMasterId,
            string City, string PostalCode, string DeliveryPointCode, 
            string State, string StateSourceSystemRecordId, string StateMasterId,
            string Country, string CountrySourceSystemRecordId, string CountryMasterId,
            string AddressUseType, string AddressUseTypeSourceSystemRecordId, string AddressUseTypeMasterId, bool? AddressIsPrimary,
            string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.ConstituentAddressChangeIntegrationRecord @SystemId, @RecordId," +
                "@ConstituentSourceSystemRecordId, @ConstituentMasterId, @FirstName, @LastName, @UAPersonId," +
                "@Address, @AddressMasterId," +
                "@City, @PostalCode, @DeliveryPointCode," +
                "@State, @StateSourceSystemRecordId, @StateMasterId," +
                "@Country, @CountrySourceSystemRecordId, @CountryMasterId," +
                "@AddressUseType, @AddressUseTypeSourceSystemRecordId, @AddressUseTypeMasterId, @AddressIsPrimary," +
                "@ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@ConstituentSourceSystemRecordId", (object)ConstituentSourceSystemRecordId ?? DBNull.Value),
                        new SqlParameter("@ConstituentMasterId", (object)ConstituentMasterId ?? DBNull.Value),
                        new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                        new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                        new SqlParameter("@UAPersonId", (object)UAPersonId ?? DBNull.Value),
                        new SqlParameter("@Address", (object)Address ?? DBNull.Value),
                            new SqlParameter("@AddressMasterId", (object)AddressMasterId ?? DBNull.Value),
                        new SqlParameter("@City", (object)City ?? DBNull.Value),
                        new SqlParameter("@PostalCode", (object)PostalCode ?? DBNull.Value),
                        new SqlParameter("@DeliveryPointCode", (object)DeliveryPointCode ?? DBNull.Value),
                        new SqlParameter("@State", (object)State ?? DBNull.Value),
                            new SqlParameter("@StateSourceSystemRecordId", (object)StateSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@StateMasterId", (object)StateMasterId ?? DBNull.Value),
                        new SqlParameter("@Country", (object)Country ?? DBNull.Value),
                            new SqlParameter("@CountrySourceSystemRecordId", (object)CountrySourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@CountryMasterId", (object)CountryMasterId ?? DBNull.Value),
                        new SqlParameter("@AddressUseType", (object)AddressUseType ?? DBNull.Value),
                            new SqlParameter("@AddressUseTypeSourceSystemRecordId", (object)AddressUseTypeSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@AddressUseTypeMasterId", (object)AddressUseTypeMasterId ?? DBNull.Value),
                        new SqlParameter("@AddressIsPrimary", System.Data.SqlDbType.Bit) { Value = AddressIsPrimary.HasValue ? (object)AddressIsPrimary : DBNull.Value },
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }


        public int StudentParentChangeIntegrationRecord(int SystemId, long RecordId,
            string FirstName, string PreferredName, string LastName,
            string ParentConstituentSourceSystemRecordId, string ParentMasterRecordId,
            string Suffix, string SuffixSourceSystemRecordId, string SuffixMasterRecordId,
            string StudentId, string StudentConstituentSourceSystemRecordId, string StudentFirstName, string StudentLastName, string StudentMasterRecordId,
            string Relationship, string RelationshipSourceSystemRecordId, string RelationshipMasterRecordId,
            string Phone1Number, string Phone1MasterRecordId, string Phone1Extension, string Phone1CountryCode, string Phone1CountryMasterRecordId,
            string Phone2Number, string Phone2MasterRecordId, string Phone2Extension, string Phone2CountryCode, string Phone2CountryMasterRecordId,
            string EmailAddress1, string EmailAddress1MasterRecordId,
            string EmailAddress2, string EmailAddress2MasterRecordId,
            string AddressMasterId, string Address1, string Address2,
            string City,
            string State, string StateMasterId,
            string PostalCode, string DeliveryPointCode,
            string Country, string CountryMasterId,
            string ChangeAgent)

        {
            return this.Database.ExecuteSqlRaw(
                "Integration.ParentChangeIntegrationRecord @SystemId, @RecordId," +
                "@FirstName, @PreferredName, @LastName," +
                "@ParentConstituentSourceSystemRecordId, @ParentMasterRecordId," +
                "@Suffix, @SuffixSourceSystemRecordId, @SuffixMasterRecordId," +
                "@StudentId, @StudentConstituentSourceSystemRecordId, @StudentFirstName, @StudentLastName, @StudentMasterRecordId," +
                "@Relationship, @RelationshipSourceSystemRecordId, @RelationshipMasterRecordId," +
                "@Phone1Number, @Phone1MasterRecordId, @Phone1Extension, @Phone1CountryCode, @Phone1CountryMasterRecordId," +
                "@Phone2Number, @Phone2MasterRecordId, @Phone2Extension, @Phone2CountryCode, @Phone2CountryMasterRecordId," +
                "@EmailAddress1, @EmailAddress1MasterRecordId," +
                "@EmailAddress2, @EmailAddress2MasterRecordId," +
                "@AddressMasterId, @Address1, @Address2," + 
                "@City," +
                "@State, @StateMasterId," +
                "@PostalCode," +
                "@DeliveryPointCode, " +
                "@Country, @CountryMasterId," +
                "@ChangeAgent",
                        new SqlParameter("@SystemId", SystemId),
                        new SqlParameter("@RecordId", RecordId),
                        new SqlParameter("@FirstName", (object)FirstName ?? DBNull.Value),
                        new SqlParameter("@PreferredName", (object)PreferredName ?? DBNull.Value),
                        new SqlParameter("@LastName", (object)LastName ?? DBNull.Value),
                        new SqlParameter("@ParentConstituentSourceSystemRecordId", (object)ParentConstituentSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@ParentMasterRecordId", (object)ParentMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@Suffix", (object)Suffix ?? DBNull.Value),
                            new SqlParameter("@SuffixSourceSystemRecordId", (object)SuffixSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@SuffixMasterRecordId", (object)SuffixMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@StudentId", (object)StudentId ?? DBNull.Value),
                            new SqlParameter("@StudentConstituentSourceSystemRecordId", (object)StudentConstituentSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@StudentFirstName", (object)StudentFirstName ?? DBNull.Value),
                            new SqlParameter("@StudentLastName", (object)StudentLastName ?? DBNull.Value),
                            new SqlParameter("@StudentMasterRecordId", (object)StudentMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@Relationship", (object)Relationship ?? DBNull.Value),
                            new SqlParameter("@RelationshipSourceSystemRecordId", (object)RelationshipSourceSystemRecordId ?? DBNull.Value),
                            new SqlParameter("@RelationshipMasterRecordId", (object)RelationshipMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@Phone1Number", (object)Phone1Number ?? DBNull.Value),
                            new SqlParameter("@Phone1MasterRecordId", (object)Phone1MasterRecordId ?? DBNull.Value),
                            new SqlParameter("@Phone1Extension", (object)Phone1Extension ?? DBNull.Value),
                            new SqlParameter("@Phone1CountryCode", (object)Phone1CountryCode ?? DBNull.Value),
                            new SqlParameter("@Phone1CountryMasterRecordId", (object)Phone1CountryMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@Phone2Number", (object)Phone2Number ?? DBNull.Value),
                            new SqlParameter("@Phone2MasterRecordId", (object)Phone2MasterRecordId ?? DBNull.Value),
                            new SqlParameter("@Phone2Extension", (object)Phone2Extension ?? DBNull.Value),
                            new SqlParameter("@Phone2CountryCode", (object)Phone2CountryCode ?? DBNull.Value),
                            new SqlParameter("@Phone2CountryMasterRecordId", (object)Phone2CountryMasterRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress1", (object)EmailAddress1 ?? DBNull.Value),
                            new SqlParameter("@EmailAddress1MasterRecordId", (object)EmailAddress1MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@EmailAddress2", (object)EmailAddress2 ?? DBNull.Value),
                            new SqlParameter("@EmailAddress2MasterRecordId", (object)EmailAddress2MasterRecordId ?? DBNull.Value),
                        new SqlParameter("@AddressMasterId", (object)AddressMasterId ?? DBNull.Value),
                            new SqlParameter("@Address1", (object)Address1 ?? DBNull.Value),
                            new SqlParameter("@Address2", (object)Address2 ?? DBNull.Value),
                        new SqlParameter("@City", (object)City ?? DBNull.Value),
                        new SqlParameter("@State", (object)State ?? DBNull.Value),
                            new SqlParameter("@StateMasterId", (object)StateMasterId ?? DBNull.Value),
                        new SqlParameter("@PostalCode", (object)PostalCode ?? DBNull.Value),
                        new SqlParameter("@DeliveryPointCode", (object)DeliveryPointCode ?? DBNull.Value),
                        new SqlParameter("@Country", (object)Country ?? DBNull.Value),
                            new SqlParameter("@CountryMasterId", (object)Country ?? DBNull.Value),
                        new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeOrganizationalUnitIntegrationRecord(int SystemId, long? Id, string OrganizationalUnitName, string OrganizationalUnitCode, string OrganizationalUnitType, string ParentOrganizationalUnitName, 
            string ParentOrganizationalUnitCode, string ParentOrganizationalUnitType, string ParentOrganizationalUnitMasterId, string OrganizationName, string OrganizationCode, string OrganizationMasterId)
		{
            return this.Database.ExecuteSqlRaw(
                "Integration.OrganizationalUnitChangeIntegrationRecord @SystemId, @RecordId, @OrganizationalUnitName, @OrganizationalUnitCode, @OrganizationalUnitType, @ParentOrganizationalUnitName, @ParentOrganizationalUnitCode, @ParentOrganizationalUnitType, @ParentOrganizationalUnitMasterId, @OrganizationName, @OrganizationCode, @OrganizationMasterId",
                    new SqlParameter("@SystemId",                               (object)SystemId ?? DBNull.Value),
					new SqlParameter("@RecordId",                               (object)Id ?? DBNull.Value),
					new SqlParameter("@OrganizationalUnitName",                 (object)OrganizationalUnitName ?? DBNull.Value),
					new SqlParameter("@OrganizationalUnitCode",                 (object)OrganizationalUnitCode ?? DBNull.Value),
                    new SqlParameter("@OrganizationalUnitType",                 (object)OrganizationalUnitType ?? DBNull.Value),
                    new SqlParameter("@ParentOrganizationalUnitName",           (object)ParentOrganizationalUnitName ?? DBNull.Value),
                    new SqlParameter("@ParentOrganizationalUnitCode",           (object)ParentOrganizationalUnitCode ?? DBNull.Value),
                    new SqlParameter("@ParentOrganizationalUnitType",           (object)ParentOrganizationalUnitType ?? DBNull.Value),
                    new SqlParameter("@ParentOrganizationalUnitMasterId",       (object)ParentOrganizationalUnitMasterId ?? DBNull.Value),
                    new SqlParameter("@OrganizationName",                       (object)OrganizationName ?? DBNull.Value),
                    new SqlParameter("@OrganizationCode",                       (object)OrganizationCode ?? DBNull.Value),
                    new SqlParameter("@OrganizationMasterId",                   (object)OrganizationMasterId ?? DBNull.Value)
                );
		}

        public int ChangeDesignationIntegrationRecord(int SystemId, long? Id, 
            string DesignationId, string DesignationName, 
            string Description, string StartDate, string EndDate,
            string OrganizationalUnit, string OrganizationalUnitCode, string OrganizationalUnitMasterId, 
            string DesignationTypeName, string DesignationTypeSourceSystemRecordId, string DesignationTypeMasterId, 
            string KFSAccountCode, string VSECategoryName, string VSECategorySourceSystemRecordId, string VSECategoryMasterId, 
            string GLOrganizationName, string GLOrganizationCode, string GLOrganizationMasterId, 
            string DesignationUseTypeName, string DesignationUseTypeMasterId, string DesignationStatus, string DesignationStatusMasterId, string ChangeAgent)
        {
            return this.Database.ExecuteSqlRaw(
                "Integration.DesignationChangeIntegrationRecord @SystemId, @RecordId, @DesignationId, @DesignationName, " +
                "@Description, @StartDate, @EndDate, " +
                "@OrganizationalUnit, @OrganizationalUnitCode, @OrganizationalUnitMasterId, " +
                "@DesignationTypeName, @DesignationTypeSourceSystemRecordId, @DesignationTypeMasterId, " +
                "@KFSAccountCode, @VSECategoryName, @VSECategorySourceSystemRecordId, @VSECategoryMasterId, " +
                "@GLOrganizationName, @GLOrganizationCode, @GLOrganizationMasterId, @DesignationUseTypeName, " +
                "@DesignationUseTypeMasterId, @DesignationStatus, @DesignationStatusMasterId, @ChangeAgent",
                    new SqlParameter("@SystemId", (object)SystemId ?? DBNull.Value),
                    new SqlParameter("@RecordId", (object)Id ?? DBNull.Value),
                    new SqlParameter("@DesignationId", (object)DesignationId ?? DBNull.Value),
                    new SqlParameter("@DesignationName", (object)DesignationName ?? DBNull.Value),
                    new SqlParameter("@Description", (object)Description ?? DBNull.Value),
                    new SqlParameter("@StartDate", (object)StartDate ?? DBNull.Value),
                    new SqlParameter("@EndDate", (object)EndDate ?? DBNull.Value),
                    new SqlParameter("@OrganizationalUnit", (object)OrganizationalUnit ?? DBNull.Value),
                    new SqlParameter("@OrganizationalUnitCode", (object)OrganizationalUnitCode ?? DBNull.Value),
                    new SqlParameter("@OrganizationalUnitMasterId", (object)OrganizationalUnitMasterId ?? DBNull.Value),
                    new SqlParameter("@DesignationTypeName", (object)DesignationTypeName ?? DBNull.Value),
                    new SqlParameter("@DesignationTypeSourceSystemRecordId", (object)DesignationTypeSourceSystemRecordId ?? DBNull.Value),
                    new SqlParameter("@DesignationTypeMasterId", (object)DesignationTypeMasterId ?? DBNull.Value),
                    new SqlParameter("@KFSAccountCode", (object)KFSAccountCode ?? DBNull.Value),
                    new SqlParameter("@VSECategoryName", (object)VSECategoryName ?? DBNull.Value),
                    new SqlParameter("@VSECategorySourceSystemRecordId", (object)VSECategorySourceSystemRecordId ?? DBNull.Value),
                    new SqlParameter("@VSECategoryMasterId", (object)VSECategoryMasterId ?? DBNull.Value),
                    new SqlParameter("@GLOrganizationName", (object)GLOrganizationName ?? DBNull.Value),
                    new SqlParameter("@GLOrganizationCode", (object)GLOrganizationCode ?? DBNull.Value),
                    new SqlParameter("@GLOrganizationMasterId", (object)GLOrganizationMasterId ?? DBNull.Value),
                    new SqlParameter("@DesignationUseTypeName", (object)DesignationUseTypeName ?? DBNull.Value),
                    new SqlParameter("@DesignationUseTypeMasterId", (object)DesignationUseTypeMasterId ?? DBNull.Value),
                    new SqlParameter("@DesignationStatus", (object)DesignationStatus ?? DBNull.Value),
                    new SqlParameter("@DesignationStatusMasterId", (object)DesignationStatusMasterId ?? DBNull.Value),
                    new SqlParameter("@ChangeAgent", (object)ChangeAgent ?? DBNull.Value)
                );
        }

        public int ChangeOfficeLocationIntegrationRecord(int SystemId, long? Id, string Name, string BuildingCode, string Address1, string Address2, string City, string State, int? StateMasterId, string PostalCode, string Country, int? CountryMasterId)
		{
			return this.Database.ExecuteSqlRaw(
				"Integration.ChangeOfficeLocationIntegrationRecord @SystemId, @RecordId, @Name, @BuildingCode, @Address1, @Address2, @City, @State, @StateMasterId, @PostalCode, @Country, @CountryMasterId",
					new SqlParameter("@SystemId", (object)SystemId ?? DBNull.Value),
					new SqlParameter("@RecordId", (object)Id ?? DBNull.Value),
					new SqlParameter("@Name", (object)Name ?? DBNull.Value),
					new SqlParameter("@BuildingCode", (object)BuildingCode ?? DBNull.Value),
					new SqlParameter("@Address1", (object)Address1 ?? DBNull.Value),
					new SqlParameter("@Address2", (object)Address2 ?? DBNull.Value),
					new SqlParameter("@City", (object)City ?? DBNull.Value),
					new SqlParameter("@State", (object)State ?? DBNull.Value),
					new SqlParameter("@StateMasterId", (object)StateMasterId ?? DBNull.Value),
					new SqlParameter("@PostalCode", (object)PostalCode ?? DBNull.Value),
					new SqlParameter("@Country", (object)Country ?? DBNull.Value),
					new SqlParameter("@CountryMasterId", (object)CountryMasterId ?? DBNull.Value)
				);
		}

		public int RevalidateRecord(int? SystemId, int? IntegrationId, long? Id)
		{
            return this.Database.ExecuteSqlRaw(
                "Integration.IntegrationRecordRevalidate @SystemId, @IntegrationId, @RecordId",
				new SqlParameter("@SystemId", SystemId),
				new SqlParameter("@IntegrationId", IntegrationId),
				new SqlParameter("@RecordId", Id));
		}

		public async Task<int> ManuallyMatchIntegrationRecord(int systemId, int integrationId, long id, string masterId, string changeAgent)
		{
            return await Database.ExecuteSqlRawAsync(
                "Integration.IntegrationRecordManuallyMatch @SystemId, @IntegrationId, @RecordId, @MatchRecordId",
				new SqlParameter("@SystemId", systemId),
                new SqlParameter("@IntegrationId", integrationId),
                new SqlParameter("@RecordId", id),
                new SqlParameter("@MatchRecordId", (object)masterId ?? DBNull.Value),
                new SqlParameter("@ChangeAgent", (object)changeAgent ?? DBNull.Value));
        }

        public int RemoveIntegrationRecord(int SystemId, int IntegrationId, long RecordId)
        {
            return Database.ExecuteSqlRaw(
                "Integration.IntegrationRecordManualRemoval @RecordId, @SystemId, @IntegrationId",
                       new SqlParameter("@RecordId", RecordId),
                       new SqlParameter("@SystemId", SystemId),
                       new SqlParameter("@IntegrationId", IntegrationId)
                );
        }

        public int RemoveIntegrationPossibleMatchRecord(int SystemId, int IntegrationId, long RecordId)
        {
            return Database.ExecuteSqlRaw(
                "Integration.IntegrationRecordManualMerge @RecordId, @SystemId, @IntegrationId",
                       new SqlParameter("@RecordId", RecordId),
                       new SqlParameter("@SystemId", SystemId),
                       new SqlParameter("@IntegrationId", IntegrationId)
                );
        }
    }
}
