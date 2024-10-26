using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeRecordsQuality]
    public class StudentController : IntegrationController
    {
        public const string READONLY = "Not Provided";
        public StudentController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<StudentController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        /*******************************************************************************************************************************
		*** Student ***
		*******************************************************************************************************************************/

        #region Dashboard

        // GET: Student Dashboard
        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Student",
                PageId = "studentDashboardPage",
                ActiveClass = "StudentDashboard",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()                
            };

            return View(viewModel);
        }
        #endregion

        #region ContactList
        // GET: Student Contact List
        public IActionResult StudentContactList()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Student Contact",
                PageId = "studentContactPage",
                ActiveClass = "StudentContact",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
            };

            return View(viewModel);
        }
        #endregion

        #region Student - Bio/Dem

        /*  *** List View ***
        ******************************************************************/
        #region List
        // GET: Student/StudentList
        public IActionResult StudentList()
        {
            var model = new StudentListViewModel()
            {
                Title = "Student",
                PageId = "studentBioDemPage",
                ActiveClass = "Student",
                Message = "Your Student Page",
                Integration = "Student",
                IntegrationId = 5,
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<StudentRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentList(AjaxDataTableRequest request)
        {
            try
            {
                var students = _context.StudentRemediationList.AsQueryable();

                int recordsTotal = students.Count();

                var studentList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? students
                                                : students.Where(s => s.FirstName.Contains(request.searchValue) ||
                                                                      s.MiddleName.Contains(request.searchValue) ||
                                                                      s.LastName.Contains(request.searchValue) ||
                                                                      s.StudentId.Contains(request.searchValue) ||
                                                                      s.ErrorCategories.Contains(request.searchValue) ||
                                                                      s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = studentList.Count();

                var studentRemediationList = new List<StudentRemediationListItemViewModel>();

                foreach (var item in studentList.Skip(request.start).Take(request.length))
                {
                    studentRemediationList.Add(new StudentRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,
                        Name = string.Format("{0} {1} {2}", item.FirstName, item.MiddleName, item.LastName),
                        StudentId = item.StudentId,
                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = studentRemediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        // GET: Student/StudentEdit
        public IActionResult StudentEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var studentDetail = history.First();
                var studentSource = history.Last();

                var viewModel = new StudentViewModel()
                {
                    Title = "Student",
                    PageId = "studentBioDemPage",
                    ActiveClass = "Student",
                    Message = "Your Student Page",
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = studentDetail.Id,
                    System = studentDetail.SystemName,
                    SystemId = studentDetail.SystemId,
                    Integration = studentDetail.IntegrationName,
                    IntegrationId = studentDetail.IntegrationId,
                    IntegrationDate = studentDetail.IntegrationDate,
                    CreatedDate = studentDetail.RecordDate,
                    SourceRecordId = studentDetail.SourceRecordId,
                    CreatedOnDT = studentDetail.RecordDate,
                    
                    HistoryData = new List<StudentHistoryViewModel>(),
                    RecordStatus = studentDetail.RecordStatus,

                    #region Detail Attributes

                    FirstName = studentDetail.FirstName,
                        FirstName_BusinessName = "First Name",
                        FirstName_BusinessDescription = studentDetail.FirstName_BusinessDescription,
                        FirstName_AttributeId = studentDetail.FirstName_AttributeId,
                        FirstName_OriginalValue = studentSource.FirstName,
                        FirstName_Status = studentDetail.FirstName_Status,
                        FirstName_Source = studentDetail.FirstName_Source,
                        FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.FirstName_Source }),
                    PreferredName = studentDetail.PreferredName,
                        PreferredName_BusinessName = "Preferred Name",
                        PreferredName_BusinessDescription = "The student's preferred name",
                        PreferredName_AttributeId = studentDetail.PreferredName_AttributeId,
                        PreferredName_OriginalValue = studentSource.PreferredName,
                        PreferredName_Status = studentDetail.PreferredName_Status,
                        PreferredName_Source = studentDetail.PreferredName_Source,
                        PreferredName_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.PreferredName_Source }),
                    MiddleName = studentDetail.MiddleName,
                        MiddleName_BusinessName = "Middle Name",
                        MiddleName_BusinessDescription = "The student's middle name",
                        MiddleName_AttributeId = studentDetail.MiddleName_AttributeId,
                        MiddleName_OriginalValue = studentSource.MiddleName,
                        MiddleName_Status = studentDetail.MiddleName_Status,
                        MiddleName_Source = studentDetail.MiddleName_Source,
                        MiddleName_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.MiddleName_Source }),
                    LastName = studentDetail.LastName,
                        LastName_BusinessName = "Last Name",
                        LastName_BusinessDescription = "The student's last name",
                        LastName_AttributeId = studentDetail.LastName_AttributeId,
                        LastName_OriginalValue = studentSource.LastName,
                        LastName_Status = studentDetail.LastName_Status,
                        LastName_Source = studentDetail.LastName_Source,
                        LastName_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.LastName_Source }),
                    MaidenName = studentDetail.MaidenName,
                        MaidenName_BusinessName = "Maiden Name",
                        MaidenName_BusinessDescription = "The student's maiden name",
                        MaidenName_AttributeId = studentDetail.MaidenName_AttributeId,
                        MaidenName_OriginalValue = studentSource.MaidenName,
                        MaidenName_Status = studentDetail.MaidenName_Status,
                        MaidenName_Source = studentDetail.MaidenName_Source,
                        MaidenName_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.MaidenName_Source }),
                    StudentId = studentDetail.StudentId,
                        StudentId_BusinessName = studentDetail.StudentId_BusinessName,
                        StudentId_BusinessDescription = studentDetail.StudentId_BusinessDescription,
                        StudentId_AttributeId = studentDetail.StudentId_AttributeId,
                        StudentId_OriginalValue = studentSource.StudentId,
                        StudentId_Status = studentDetail.StudentId_Status,
                        StudentId_Source = studentDetail.StudentId_Source,
                        StudentId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.StudentId_Source }),
                    //StudentTitle = studentDetail.Title,
                    //    StudentTitle_BusinessName = studentDetail.Title_BusinessName,
                    //    StudentTitle_BusinessDescription = studentDetail.Title_BusinessDescription,
                    //    StudentTitle_AttributeId = studentDetail.Title_AttributeId,
                    //    StudentTitle_OriginalValue = studentSource.Title,
                    //    StudentTitle_Status = studentDetail.Title_Status,
                    //    StudentTitle_Source = studentDetail.Title_Source,
                    //TitleSourceSystemRecordId = studentDetail.TitleSourceSystemRecordId,
                    //    TitleSourceSystemRecordId_BusinessName = studentDetail.TitleSourceSystemRecordId_BusinessName,
                    //    TitleSourceSystemRecordId_BusinessDescription = studentDetail.TitleSourceSystemRecordId_BusinessDescription,
                    //    TitleSourceSystemRecordId_AttributeId = studentDetail.TitleSourceSystemRecordId_AttributeId,
                    //    TitleSourceSystemRecordId_OriginalValue = studentSource.TitleSourceSystemRecordId,
                    //    TitleSourceSystemRecordId_Status = studentDetail.TitleSourceSystemRecordId_Status,
                    //    TitleSourceSystemRecordId_Source = studentDetail.TitleSourceSystemRecordId_Source,
                    //TitleMasterId = studentDetail.TitleMasterId,
                    //    TitleMasterId_BusinessName = studentDetail.TitleMasterId_BusinessName,
                    //    TitleMasterId_BusinessDescription = studentDetail.TitleMasterId_BusinessDescription,
                    //    TitleMasterId_AttributeId = studentDetail.TitleMasterId_AttributeId,
                    //    TitleMasterId_OriginalValue = studentSource.TitleMasterId,
                    //    TitleMasterId_Status = studentDetail.TitleMasterId_Status,
                    //    TitleMasterId_Source = studentDetail.TitleMasterId_Source,
                    //    TitleMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.TitleMasterId_Source, studentDetail.TitleSourceSystemRecordId_Source, studentDetail.Title_Source }),
                    Suffix = studentDetail.Suffix,
                        Suffix_BusinessName = studentDetail.Suffix_BusinessName,
                        Suffix_BusinessDescription = studentDetail.Suffix_BusinessDescription,
                        Suffix_AttributeId = studentDetail.Suffix_AttributeId,
                        Suffix_OriginalValue = studentSource.Suffix,
                        Suffix_Status = studentDetail.Suffix_Status,
                        Suffix_Source = studentDetail.Suffix_Source,
                    SuffixSourceSystemRecordId = studentDetail.SuffixSourceSystemRecordId,
                        SuffixSourceSystemRecordId_BusinessName = studentDetail.SuffixSourceSystemRecordId_BusinessName,
                        SuffixSourceSystemRecordId_BusinessDescription = studentDetail.SuffixSourceSystemRecordId_BusinessDescription,
                        SuffixSourceSystemRecordId_AttributeId = studentDetail.SuffixSourceSystemRecordId_AttributeId,
                        SuffixSourceSystemRecordId_OriginalValue = studentSource.SuffixSourceSystemRecordId,
                        SuffixSourceSystemRecordId_Status = studentDetail.SuffixSourceSystemRecordId_Status,
                        SuffixSourceSystemRecordId_Source = studentDetail.SuffixSourceSystemRecordId_Source,
                    SuffixMasterId = studentDetail.SuffixMasterId,
                        SuffixMasterId_BusinessName = studentDetail.SuffixMasterId_BusinessName,
                        SuffixMasterId_BusinessDescription = studentDetail.SuffixMasterId_BusinessDescription,
                        SuffixMasterId_AttributeId = studentDetail.SuffixMasterId_AttributeId,
                        SuffixMasterId_OriginalValue = studentSource.SuffixMasterId,
                        SuffixMasterId_Status = studentDetail.SuffixMasterId_Status,
                        SuffixMasterId_Source = studentDetail.SuffixMasterId_Source,
                        SuffixMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.SuffixMasterId_Source, studentDetail.SuffixSourceSystemRecordId_Source }),
                    BirthDate = studentDetail.BirthDate,
                        BirthDate_BusinessName = studentDetail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = studentDetail.BirthDate_BusinessDescription,
                        BirthDate_AttributeId = studentDetail.BirthDate_AttributeId,
                        BirthDate_OriginalValue = studentSource.BirthDate,
                        BirthDate_Status = studentDetail.BirthDate_Status,
                        BirthDate_Source = studentDetail.BirthDate_Source,
                        BirthDate_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.BirthDate_Source }),
                    DeceasedDate = studentDetail.DeceasedDate,
                        DeceasedDate_BusinessName = studentDetail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = studentDetail.DeceasedDate_BusinessDescription,
                        DeceasedDate_AttributeId = studentDetail.DeceasedDate_AttributeId,
                        DeceasedDate_OriginalValue = studentSource.DeceasedDate,
                        DeceasedDate_Status = studentDetail.DeceasedDate_Status,
                        DeceasedDate_Source = studentDetail.DeceasedDate_Source,
                        DeceasedDate_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.DeceasedDate_Source }),
                    MaritalStatus = studentDetail.MaritalStatus,
                        MaritalStatus_BusinessName = studentDetail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = studentDetail.MaritalStatus_BusinessDescription,
                        MaritalStatus_AttributeId = studentDetail.MaritalStatus_AttributeId,
                        MaritalStatus_OriginalValue = studentSource.MaritalStatus,
                        MaritalStatus_Status = studentDetail.MaritalStatus_Status,
                        MaritalStatus_Source = studentDetail.MaritalStatus_Source,
                    MaritalStatusCode = studentDetail.MaritalStatusCode,
                        MaritalStatusCode_BusinessName = studentDetail.MaritalStatusCode_BusinessName,
                        MaritalStatusCode_BusinessDescription = studentDetail.MaritalStatusCode_BusinessDescription,
                        MaritalStatusCode_AttributeId = studentDetail.MaritalStatusCode_AttributeId,
                        MaritalStatusCode_OriginalValue = studentSource.MaritalStatusCode,
                        MaritalStatusCode_Status = studentDetail.MaritalStatusCode_Status,
                        MaritalStatusCode_Source = studentDetail.MaritalStatusCode_Source,
                    MaritalStatusSourceSystemRecordId = studentDetail.MaritalStatusSourceSystemRecordId,
                        MaritalStatusSourceSystemRecordId_BusinessName = studentDetail.MaritalStatusSourceSystemRecordId_BusinessName,
                        MaritalStatusSourceSystemRecordId_BusinessDescription = studentDetail.MaritalStatusSourceSystemRecordId_BusinessDescription,
                        MaritalStatusSourceSystemRecordId_AttributeId = studentDetail.MaritalStatusSourceSystemRecordId_AttributeId,
                        MaritalStatusSourceSystemRecordId_OriginalValue = studentSource.MaritalStatusSourceSystemRecordId,
                        MaritalStatusSourceSystemRecordId_Status = studentDetail.MaritalStatusSourceSystemRecordId_Status,
                        MaritalStatusSourceSystemRecordId_Source = studentDetail.MaritalStatusSourceSystemRecordId_Source,
                    MaritalStatusMasterId = studentDetail.MaritalStatusMasterId,
                        MaritalStatusMasterId_BusinessName = studentDetail.MaritalStatusMasterId_BusinessName,
                        MaritalStatusMasterId_BusinessDescription = studentDetail.MaritalStatusMasterId_BusinessDescription,
                        MaritalStatusMasterId_AttributeId = studentDetail.MaritalStatusMasterId_AttributeId,
                        MaritalStatusMasterId_OriginalValue = studentSource.MaritalStatusMasterId,
                        MaritalStatusMasterId_Status = studentDetail.MaritalStatusMasterId_Status,
                        MaritalStatusMasterId_Source = studentDetail.MaritalStatusMasterId_Source,
                        MaritalStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.MaritalStatusMasterId_Source, studentDetail.MaritalStatusSourceSystemRecordId_Source, studentDetail.MaritalStatusCode_Source }),
                    CitizenshipCountryCode = studentDetail.CitizenshipCountryCode,
                        CitizenshipCountryCode_BusinessName = studentDetail.CitizenshipCountryCode_BusinessName,
                        CitizenshipCountryCode_BusinessDescription = studentDetail.CitizenshipCountryCode_BusinessDescription,
                        CitizenshipCountryCode_AttributeId = studentDetail.CitizenshipCountryCode_AttributeId,
                        CitizenshipCountryCode_OriginalValue = studentSource.CitizenshipCountryCode,
                        CitizenshipCountryCode_Status = studentDetail.CitizenshipCountryCode_Status,
                        CitizenshipCountryCode_Source = studentDetail.CitizenshipCountryCode_Source,
                    CitizenshipCountryMasterId = studentDetail.CitizenshipCountryMasterId,
                        CitizenshipCountryMasterId_BusinessName = studentDetail.CitizenshipCountryMasterId_BusinessName,
                        CitizenshipCountryMasterId_BusinessDescription = studentDetail.CitizenshipCountryMasterId_BusinessDescription,
                        CitizenshipCountryMasterId_AttributeId = studentDetail.CitizenshipCountryMasterId_AttributeId,
                        CitizenshipCountryMasterId_OriginalValue = studentSource.CitizenshipCountryMasterId,
                        CitizenshipCountryMasterId_Status = studentDetail.CitizenshipCountryMasterId_Status,
                        CitizenshipCountryMasterId_Source = studentDetail.CitizenshipCountryMasterId_Source,
                        CitizenshipCountryMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.CitizenshipCountryMasterId_Source, studentDetail.CitizenshipCountryCode_Source }),
                    FERPAInformationRelease = studentDetail.FERPAInformationRelease,
                        FERPAInformationRelease_BusinessName = studentDetail.FERPAInformationRelease_BusinessName,
                        FERPAInformationRelease_BusinessDescription = studentDetail.FERPAInformationRelease_BusinessDescription,
                        FERPAInformationRelease_AttributeId = studentDetail.FERPAInformationRelease_AttributeId,
                        FERPAInformationRelease_OriginalValue = studentSource.FERPAInformationRelease,
                        FERPAInformationRelease_Status = studentDetail.FERPAInformationRelease_Status,
                        FERPAInformationRelease_Source = studentDetail.FERPAInformationRelease_Source,
                        FERPAInformationRelease_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.FERPAInformationRelease_Source }),
                    AcademicCareerName = studentDetail.DischargedAcademicCareerName,
                        AcademicCareerName_BusinessName = studentDetail.DischargedAcademicCareerName_BusinessName,
                        AcademicCareerName_BusinessDescription = studentDetail.DischargedAcademicCareerName_BusinessDescription,
                        AcademicCareerName_AttributeId = studentDetail.DischargedAcademicCareerName_AttributeId,
                        AcademicCareerName_OriginalValue = studentSource.DischargedAcademicCareerName,
                        AcademicCareerName_Status = studentDetail.DischargedAcademicCareerName_Status,
                        AcademicCareerName_Source = studentDetail.DischargedAcademicCareerName_Source,
                    AcademicCareerCode = studentDetail.DischargedAcademicCareerCode,
                        AcademicCareerCode_BusinessName = studentDetail.DischargedAcademicCareerCode_BusinessName,
                        AcademicCareerCode_BusinessDescription = studentDetail.DischargedAcademicCareerCode_BusinessDescription,
                        AcademicCareerCode_AttributeId = studentDetail.DischargedAcademicCareerCode_AttributeId,
                        AcademicCareerCode_OriginalValue = studentSource.DischargedAcademicCareerCode,
                        AcademicCareerCode_Status = studentDetail.DischargedAcademicCareerCode_Status,
                        AcademicCareerCode_Source = studentDetail.DischargedAcademicCareerCode_Source,
                    DischargedTermCode = studentDetail.DischargedTermCode,
                        DischargedTermCode_BusinessName = studentDetail.DischargedTermCode_BusinessName,
                        DischargedTermCode_BusinessDescription = studentDetail.DischargedTermCode_BusinessDescription,
                        DischargedTermCode_AttributeId = studentDetail.DischargedTermCode_AttributeId,
                        DischargedTermCode_OriginalValue = studentSource.DischargedTermCode,
                        DischargedTermCode_Status = studentDetail.DischargedTermCode_Status,
                        DischargedTermCode_Source = studentDetail.DischargedTermCode_Source,
                    AcademicCalendarMasterId = studentDetail.DischargedAcademicCalendarMasterId,
                        AcademicCalendarMasterId_BusinessName = studentDetail.DischargedAcademicCalendarMasterId_BusinessName,
                        AcademicCalendarMasterId_BusinessDescription = studentDetail.DischargedAcademicCalendarMasterId_BusinessDescription,
                        AcademicCalendarMasterId_AttributeId = studentDetail.DischargedAcademicCalendarMasterId_AttributeId,
                        AcademicCalendarMasterId_OriginalValue = studentSource.DischargedAcademicCalendarMasterId,
                        AcademicCalendarMasterId_Status = studentDetail.DischargedAcademicCalendarMasterId_Status,
                        AcademicCalendarMasterId_Source = studentDetail.DischargedAcademicCalendarMasterId_Source,
                        AcademicCalendarMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.DischargedAcademicCalendarMasterId_Source, studentDetail.DischargedTermCode_Source }),
                    EmailAddress1 = studentDetail.EmailAddress1,
                        EmailAddress1_BusinessName = studentDetail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = studentDetail.EmailAddress1_BusinessDescription,
                        EmailAddress1_AttributeId = studentDetail.EmailAddress1_AttributeId,
                        EmailAddress1_OriginalValue = studentSource.EmailAddress1,
                        EmailAddress1_Status = studentDetail.EmailAddress1_Status,
                        EmailAddress1_Source = studentDetail.EmailAddress1_Source,
                    EmailAddress1MasterRecordId = studentDetail.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_BusinessName = studentDetail.EmailAddress1MasterRecordId_BusinessName,
                        EmailAddress1MasterRecordId_BusinessDescription = studentDetail.EmailAddress1MasterRecordId_BusinessDescription,
                        EmailAddress1MasterRecordId_AttributeId = studentDetail.EmailAddress1MasterRecordId_AttributeId,
                        EmailAddress1MasterRecordId_OriginalValue = studentSource.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_Status = studentDetail.EmailAddress1MasterRecordId_Status,
                        EmailAddress1MasterRecordId_Source = studentDetail.EmailAddress1MasterRecordId_Source,
                        EmailAddress1MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.EmailAddress1MasterRecordId_Source, studentDetail.EmailAddress1_Source }),
                    EmailAddress2 = studentDetail.EmailAddress2,
                        EmailAddress2_BusinessName = studentDetail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = studentDetail.EmailAddress2_BusinessDescription,
                        EmailAddress2_AttributeId = studentDetail.EmailAddress2_AttributeId,
                        EmailAddress2_OriginalValue = studentSource.EmailAddress2,
                        EmailAddress2_Status = studentDetail.EmailAddress2_Status,
                        EmailAddress2_Source = studentDetail.EmailAddress2_Source,
                    EmailAddress2MasterRecordId = studentDetail.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_BusinessName = studentDetail.EmailAddress2MasterRecordId_BusinessName,
                        EmailAddress2MasterRecordId_BusinessDescription = studentDetail.EmailAddress2MasterRecordId_BusinessDescription,
                        EmailAddress2MasterRecordId_AttributeId = studentDetail.EmailAddress2MasterRecordId_AttributeId,
                        EmailAddress2MasterRecordId_OriginalValue = studentSource.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_Status = studentDetail.EmailAddress2MasterRecordId_Status,
                        EmailAddress2MasterRecordId_Source = studentDetail.EmailAddress2MasterRecordId_Source,
                        EmailAddress2MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDetail.EmailAddress2MasterRecordId_Source, studentDetail.EmailAddress2_Source }),

                    TitleList = GetTitleList(),
                    SuffixList = GetSuffixList(),
                    MaritalStatusList = GetMaritalStatusList(),
                    CountryList = GetCountryList()

                    #endregion

                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new StudentHistoryViewModel()
                    {
                        FirstName = item.FirstName,
                            FirstName_Status = item.FirstName_Status,
                            FirstName_OriginalValue = previousitem.FirstName,
                        PreferredName = item.PreferredName,
                            PreferredName_Status = item.PreferredName_Status,
                            PreferredName_OriginalValue = previousitem.PreferredName,
                        MiddleName = item.MiddleName,
                            MiddleName_Status = item.MiddleName_Status,
                            MiddleName_OriginalValue = previousitem.MiddleName,
                        LastName = item.LastName,
                            LastName_Status = item.LastName_Status,
                            LastName_OriginalValue = previousitem.LastName,
                        MaidenName = item.MaidenName,
                            MaidenName_Status = item.MaidenName_Status,
                            MaidenName_OriginalValue = previousitem.MaidenName,
                        StudentId = item.StudentId,
                            StudentId_Status = item.StudentId_Status,
                            StudentId_OriginalValue = previousitem.StudentId,
                        Suffix = item.Suffix,
                            Suffix_Status = item.Suffix_Status,
                            Suffix_OriginalValue = previousitem.Suffix,
                        SuffixSourceSystemRecordId = item.SuffixSourceSystemRecordId,
                            SuffixSourceSystemRecordId_Status = item.SuffixSourceSystemRecordId_Status,
                            SuffixSourceSystemRecordId_OriginalValue = previousitem.SuffixSourceSystemRecordId,
                        SuffixMasterId = item.SuffixMasterId,
                            SuffixMasterId_Status = item.SuffixMasterId_Status,
                            SuffixMasterId_OriginalValue = previousitem.SuffixMasterId,
                        BirthDate = item.BirthDate,
                            BirthDate_Status = item.BirthDate_Status,
                            BirthDate_OriginalValue = previousitem.BirthDate,
                        DeceasedDate = item.DeceasedDate,
                            DeceasedDate_Status = item.DeceasedDate_Status,
                            DeceasedDate_OriginalValue = previousitem.DeceasedDate,
                        MaritalStatus = item.MaritalStatus,
                            MaritalStatus_Status = item.MaritalStatus_Status,
                            MaritalStatus_OriginalValue = previousitem.MaritalStatus,
                        MaritalStatusCode = item.MaritalStatusCode,
                            MaritalStatusCode_Status = item.MaritalStatusCode_Status,
                            MaritalStatusCode_OriginalValue = previousitem.MaritalStatusCode,
                        MaritalStatusSourceSystemRecordId = item.MaritalStatusSourceSystemRecordId,
                            MaritalStatusSourceSystemRecordId_Status = item.MaritalStatusSourceSystemRecordId_Status,
                            MaritalStatusSourceSystemRecordId_OriginalValue = previousitem.MaritalStatusSourceSystemRecordId,
                        MaritalStatusMasterId = item.MaritalStatusMasterId,
                            MaritalStatusMasterId_Status = item.MaritalStatusMasterId_Status,
                            MaritalStatusMasterId_OriginalValue = previousitem.MaritalStatusMasterId,
                        CitizenshipCountryCode = item.CitizenshipCountryCode,
                            CitizenshipCountryCode_Status = item.CitizenshipCountryCode_Status,
                            CitizenshipCountryCode_OriginalValue = previousitem.CitizenshipCountryCode,
                        CitizenshipCountryMasterId = item.CitizenshipCountryMasterId,
                            CitizenshipCountryMasterId_Status = item.CitizenshipCountryMasterId_Status,
                            CitizenshipCountryMasterId_OriginalValue = previousitem.CitizenshipCountryMasterId,
                        FERPAInformationRelease = item.FERPAInformationRelease,
                            FERPAInformationRelease_Status = item.FERPAInformationRelease_Status,
                            FERPAInformationRelease_OriginalValue = previousitem.FERPAInformationRelease,
                        AcademicCareerName = item.DischargedAcademicCareerName,
                            AcademicCareerName_Status = item.DischargedAcademicCareerName_Status,
                            AcademicCareerName_OriginalValue = previousitem.DischargedAcademicCareerName,
                        AcademicCareerCode = item.DischargedAcademicCareerCode,
                            AcademicCareerCode_Status = item.DischargedAcademicCareerCode_Status,
                            AcademicCareerCode_OriginalValue = previousitem.DischargedAcademicCareerCode,
                        DischargedTermCode = item.DischargedTermCode,
                            DischargedTermCode_Status = item.DischargedTermCode_Status,
                            DischargedTermCode_OriginalValue = previousitem.DischargedTermCode,
                        AcademicCalendarMasterId = item.DischargedAcademicCalendarMasterId,
                            AcademicCalendarMasterId_Status = item.DischargedAcademicCalendarMasterId_Status,
                            AcademicCalendarMasterId_OriginalValue = previousitem.DischargedAcademicCalendarMasterId,
                        EmailAddress1 = item.EmailAddress1,
                            EmailAddress1_Status = item.EmailAddress1_Status,
                            EmailAddress1_OriginalValue = previousitem.EmailAddress1,
                        EmailAddress1MasterRecordId = item.EmailAddress1MasterRecordId,
                            EmailAddress1MasterRecordId_Status = item.EmailAddress1MasterRecordId_Status,
                            EmailAddress1MasterRecordId_OriginalValue = previousitem.EmailAddress1MasterRecordId,
                        EmailAddress2 = item.EmailAddress2,
                            EmailAddress2_Status = item.EmailAddress2_Status,
                            EmailAddress2_OriginalValue = previousitem.EmailAddress2,
                        EmailAddress2MasterRecordId = item.EmailAddress2MasterRecordId,
                            EmailAddress2MasterRecordId_Status = item.EmailAddress2MasterRecordId_Status,
                            EmailAddress2MasterRecordId_OriginalValue = previousitem.EmailAddress2MasterRecordId,

                        HistoryDate = item.RecordDate
                    });
                }
                #endregion

                if (!string.IsNullOrEmpty(viewModel.AcademicCalendarMasterId))
                {
                    viewModel.DischargedTermName = _context.AcademicCalendarEntries.Single(e => e.Id == viewModel.AcademicCalendarMasterId).Name;
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve student edit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Match ***
        ******************************************************************/
        #region Match
        // GET: Student/StudentMatch
        public IActionResult StudentMatch(long Id, int SystemId)
        {
            try
            {
                var detail = _context.GetStudentMatchDetails(SystemId, Id);

                var viewModel = new StudentMatchViewModel()
                {
                    PageId = "studentBioDemPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Student",
                    Title = "Student Matching",
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    Id = detail.Id,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    SourceRecordId = detail.SourceRecordId,
                    CreatedOnDT = detail.RecordDate,

                    FirstName = detail.FirstName,
                        FirstName_BusinessName = "First Name", //detail.FirstName_BusinessName
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Weight = detail.FirstName_MatchWeight,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = "Preferred Name", //detail.PreferredName_BusinessName
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Weight = detail.PreferredName_MatchWeight,
                    MiddleName = detail.MiddleName,
                        MiddleName_BusinessName = "Middle Name", //detail.MiddleName_BusinessName
                        MiddleName_BusinessDescription = detail.MiddleName_BusinessDescription,
                        MiddleName_Weight = detail.MiddleName_MatchWeight,
                    LastName = detail.LastName,
                        LastName_BusinessName = "Last Name", //detail.LastName_BusinessName
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Weight = detail.LastName_MatchWeight,
                    MaidenName = detail.MaidenName,
                        MaidenName_BusinessName = "Maiden Name", //detail.MaidenName_BusinessName
                        MaidenName_BusinessDescription = detail.MaidenName_BusinessDescription,
                        MaidenName_Weight = detail.MaidenName_MatchWeight,
                    StudentId = detail.StudentId,
                        StudentId_BusinessName = detail.StudentId_BusinessName,
                        StudentId_BusinessDescription = detail.StudentId_BusinessDescription,
                        StudentId_Weight = detail.StudentId_MatchWeight,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Weight = detail.Suffix_MatchWeight,
                    BirthDate = detail.BirthDate,
                        BirthDate_BusinessName = detail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = detail.BirthDate_BusinessDescription,
                        BirthDate_Weight = detail.BirthDate_MatchWeight,
                    DeceasedDate = detail.DeceasedDate,
                        DeceasedDate_BusinessName = detail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = detail.DeceasedDate_BusinessDescription,
                        DeceasedDate_Weight = detail.DeceasedDate_MatchWeight,
                    MaritalStatus = detail.MaritalStatus,
                        MaritalStatus_BusinessName = detail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = detail.MaritalStatus_BusinessDescription,
                        MaritalStatus_Weight = detail.MaritalStatus_MatchWeight,
                    InformationReleaseStatus = detail.FERPAInformationRelease,
                        InformationReleaseStatus_BusinessName = detail.FERPAInformationRelease_BusinessName,
                        InformationReleaseStatus_BusinessDescription = detail.FERPAInformationRelease_BusinessDescription,
                        InformationReleaseStatus_Weight = detail.FERPAInformationRelease_MatchWeight,
                    CitizenshipCountry = detail.CitizenshipCountry,
                        CitizenshipCountry_BusinessName = detail.CitizenshipCountry_BusinessName,
                        CitizenshipCountry_BusinessDescription = detail.CitizenshipCountry_BusinessDescription,
                        CitizenshipCountry_Weight = detail.CitizenshipCountry_MatchWeight,
                    Discharged = detail.Discharged,
                        Discharged_BusinessName = detail.Discharged_BusinessName,
                        Discharged_BusinessDescription = detail.Discharged_BusinessDescription,
                        Discharged_Weight = detail.Discharged_MatchWeight,
                    AcademicCareerName = detail.DischargedAcademicCareerName,
                        AcademicCareerName_BusinessName = detail.DischargedAcademicCareerName_BusinessName,
                        AcademicCareerName_BusinessDescription = detail.DischargedAcademicCareerName_BusinessDescription,
                        AcademicCareerName_Weight = detail.DischargedAcademicCareerName_MatchWeight,
                    DischargedTerm = detail.DischargedAcademicCareerCode,
                        DischargedTerm_BusinessName = detail.DischargedAcademicCareerCode_BusinessName,
                        DischargedTerm_BusinessDescription = detail.DischargedAcademicCareerCode_BusinessDescription,
                        DischargedTerm_Weight = detail.DischargedAcademicCareerCode_MatchWeight,
                    EmailAddress1 = detail.EmailAddress1,
                        EmailAddress1_BusinessName = detail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = detail.EmailAddress1_BusinessDescription,
                        EmailAddress1_Weight = detail.EmailAddress1_MatchWeight,
                    EmailAddress2 = detail.EmailAddress2,
                        EmailAddress2_BusinessName = detail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = detail.EmailAddress2_BusinessDescription,
                        EmailAddress2_Weight = detail.EmailAddress2_MatchWeight,
                };

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        public IActionResult GetPossibleMatchList(long Id, int SystemId)
        {
            try
            {
                var viewModel = new StudentPossibleMatchViewModel()
                {
                    PossibleMatches = new List<StudentPossibleMatchViewModel.StudentMatchSummaryViewModel>()
                };
                foreach (var possibleMatch in _context.GetStudentPossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new StudentPossibleMatchViewModel.StudentMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,

                        Name = $"{possibleMatch.FirstName} {possibleMatch.MiddleName} {possibleMatch.LastName}",
                        FirstName = possibleMatch.FirstName,
                        MiddleName = possibleMatch.MiddleName,
                        LastName = possibleMatch.LastName,
                        StudentId = possibleMatch.StudentId,
                        BirthDate = possibleMatch.BirthDate
                    });
                }
                return PartialView("StudentPossibleMatchList", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve Student GetPossibleMatchList");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Compare ***
        ******************************************************************/
        #region Compare
        // GET: Student/StudentCompare
        // Mike: public async Task<IActionResult> StudentCompare(int SystemId, long Id, string MasterId)
        public IActionResult StudentCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetStudentComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new StudentCompareViewModel()
                {
                    Id = Id,
                    SystemId = detail.SystemId,
                    MasterId = MasterId,

                    System = detail.SystemName,
                    IntegrationName = detail.IntegrationName,
                    SystemName = detail.SystemName,
                    IntegrationId = detail.IntegrationId,
                    SourceRecordId = detail.SourceRecordId,
                    IntegrationDate = detail.IntegrationDate,

                    FirstName = detail.FirstName,
                        FirstName_BusinessName = detail.FirstName_BusinessName,
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Compare = detail.FirstName_Compare,
                        FirstName_IsDifferent = detail.FirstName != detail.FirstName_Compare,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = detail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Compare = detail.PreferredName_Compare,
                        PreferredName_IsDifferent = detail.PreferredName != detail.PreferredName_Compare,
                    MiddleName = detail.MiddleName,
                        MiddleName_BusinessName = detail.MiddleName_BusinessName,
                        MiddleName_BusinessDescription = detail.MiddleName_BusinessDescription,
                        MiddleName_Compare = detail.MiddleName_Compare,
                        MiddleName_IsDifferent = detail.MiddleName != detail.MiddleName_Compare,
                    LastName = detail.LastName,
                        LastName_BusinessName = detail.LastName_BusinessName,
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Compare = detail.LastName_Compare,
                        LastName_IsDifferent = detail.LastName != detail.LastName_Compare,
                    MaidenName = detail.MaidenName,
                        MaidenName_BusinessName = detail.MaidenName_BusinessName,
                        MaidenName_BusinessDescription = detail.MaidenName_BusinessDescription,
                        MaidenName_Compare = detail.MaidenName_Compare,
                        MaidenName_IsDifferent = detail.MaidenName != detail.MaidenName_Compare,
                    StudentId = detail.StudentId,
                        StudentId_BusinessName = detail.StudentId_BusinessName,
                        StudentId_BusinessDescription = detail.StudentId_BusinessDescription,
                        StudentId_Compare = detail.StudentId_Compare,
                        StudentId_IsDifferent = detail.StudentId != detail.StudentId_Compare,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Compare = detail.Suffix_Compare,
                        Suffix_IsDifferent = detail.Suffix != detail.Suffix_Compare,
                    BirthDate = detail.BirthDate,
                        BirthDate_BusinessName = detail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = detail.BirthDate_BusinessDescription,
                        BirthDate_Compare = detail.BirthDate_Compare,
                        BirthDate_IsDifferent = detail.BirthDate != detail.BirthDate_Compare,
                    DeceasedDate = detail.DeceasedDate,
                        DeceasedDate_BusinessName = detail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = detail.DeceasedDate_BusinessDescription,
                        DeceasedDate_Compare = detail.DeceasedDate_Compare,
                        DeceasedDate_IsDifferent = detail.DeceasedDate != detail.DeceasedDate_Compare,
                    MaritalStatus = detail.MaritalStatus,
                        MaritalStatus_BusinessName = detail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = detail.MaritalStatus_BusinessDescription,
                        MaritalStatus_Compare = detail.MaritalStatus_Compare,
                        MaritalStatus_IsDifferent = detail.MaritalStatus != detail.MaritalStatus_Compare,
                    InformationReleaseStatus = detail.FERPAInformationRelease,
                        InformationReleaseStatus_BusinessName = detail.FERPAInformationRelease_BusinessName,
                        InformationReleaseStatus_BusinessDescription = detail.FERPAInformationRelease_BusinessDescription,
                        InformationReleaseStatus_Compare = detail.FERPAInformationRelease_Compare,
                        InformationReleaseStatus_IsDifferent = detail.FERPAInformationRelease != detail.FERPAInformationRelease_Compare,
                    CitizenshipCountry = detail.CitizenshipCountry,
                        CitizenshipCountry_BusinessName = detail.CitizenshipCountry_BusinessName,
                        CitizenshipCountry_BusinessDescription = detail.CitizenshipCountry_BusinessDescription,
                        CitizenshipCountry_Compare = detail.CitizenshipCountry_Compare,
                        CitizenshipCountry_IsDifferent = detail.CitizenshipCountry != detail.CitizenshipCountry_Compare,
                    Discharged = detail.Discharged,
                        Discharged_BusinessName = detail.Discharged_BusinessName,
                        Discharged_BusinessDescription = detail.Discharged_BusinessDescription,
                        Discharged_Compare = detail.Discharged_Compare,
                        Discharged_IsDifferent = detail.Discharged != detail.Discharged_Compare,
                    EmailAddress1 = detail.EmailAddress1,
                        EmailAddress1_BusinessName = detail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = detail.EmailAddress1_BusinessDescription,
                        EmailAddress1_Compare = detail.EmailAddress1_Compare,
                        EmailAddress1_IsDifferent = detail.EmailAddress1 != detail.EmailAddress1_Compare,
                    EmailAddress2 = detail.EmailAddress2,
                        EmailAddress2_BusinessName = detail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = detail.EmailAddress2_BusinessDescription,
                        EmailAddress2_Compare = detail.EmailAddress2_Compare,
                        EmailAddress2_IsDifferent = detail.EmailAddress2 != detail.EmailAddress2_Compare,
                    SystemRecords = detail.SystemRecords.ToList()
                };

                return PartialView("StudentCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Manual Match ***
        ******************************************************************/
        #region ManualMatch
        // GET: Student/StudentManualMatch
        public async Task<IActionResult> StudentManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentManualMatch");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentList));
        }
        #endregion

        /*  *** Save ***
        ******************************************************************/
        #region Save
        // POST: Student/StudentSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentSave(StudentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentIntegrationRecord(model.SystemId, model.Id, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.StudentId,                        
                        model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId,
                        model.BirthDate, model.DeceasedDate,
                        model.MaritalStatus, model.MaritalStatusCode, model.MaritalStatusSourceSystemRecordId, model.MaritalStatusMasterId,
                        model.FERPAInformationRelease,
                        model.CitizenshipCountryCode, model.CitizenshipCountryMasterId,
                        model.DischargedTermCode, model.AcademicCareerName, model.AcademicCareerCode, model.AcademicCalendarMasterId,
                        model.EmailAddress1, model.EmailAddress1MasterRecordId, model.EmailAddress2, model.EmailAddress2MasterRecordId,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentList));
            }
            else
            {
                model.Title = "Student";
                model.PageId = "studentBioDemPage";
                model.ActiveClass = "Student";
                model.Message = "Your Student Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(StudentEdit), model);
            }

            
        }
        #endregion

        /*  *** Revalidate ***
        ******************************************************************/
        #region Revalidate
        // POST: Student/StudentRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentRevalidate(StudentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // Title
                    //if (string.IsNullOrEmpty(model.TitleMasterId) && (!string.IsNullOrEmpty(model.TitleSourceSystemRecordId) || !string.IsNullOrEmpty(model.Title)))
                    //{
                    //    model.TitleSourceSystemRecordId = null;
                    //    model.Title = null;

                    //    model.IsChanged = true;
                    //}

                    // Suffix
                    if (string.IsNullOrEmpty(model.SuffixMasterId) && (!string.IsNullOrEmpty(model.SuffixSourceSystemRecordId) || !string.IsNullOrEmpty(model.Suffix)))
                    {
                        model.SuffixSourceSystemRecordId = null;
                        model.Suffix = null;

                        model.IsChanged = true;
                    }

                    // MaritalStatus
                    if (string.IsNullOrEmpty(model.MaritalStatusMasterId) && (!string.IsNullOrEmpty(model.MaritalStatusSourceSystemRecordId) || !string.IsNullOrEmpty(model.MaritalStatusCode) || !string.IsNullOrEmpty(model.MaritalStatus)))
                    {
                        model.MaritalStatusSourceSystemRecordId = null;
                        model.MaritalStatusCode = null;
                        model.MaritalStatus = null;

                        model.IsChanged = true;
                    }

                    // Citizenship Country
                    if (string.IsNullOrEmpty(model.CitizenshipCountryMasterId) && (!string.IsNullOrEmpty(model.CitizenshipCountryCode)))
                    {
                        model.CitizenshipCountryCode = null;

                        model.IsChanged = true;
                    }

                    // Discharged Term
                    if (string.IsNullOrEmpty(model.AcademicCalendarMasterId) && (!string.IsNullOrEmpty(model.AcademicCareerCode) || !string.IsNullOrEmpty(model.AcademicCareerName) || !string.IsNullOrEmpty(model.DischargedTermCode)))
                    {
                        model.AcademicCareerCode = null;
                        model.AcademicCareerName = null;
                        model.DischargedTermCode = null;

                        model.IsChanged = true;
                    }

                    _context.ChangeStudentIntegrationRecord(model.SystemId, model.Id, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.StudentId,                         
                        model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId,
                        model.BirthDate, model.DeceasedDate,
                        model.MaritalStatus, model.MaritalStatusCode, model.MaritalStatusSourceSystemRecordId, model.MaritalStatusMasterId,
                        model.FERPAInformationRelease,
                        model.CitizenshipCountryCode, model.CitizenshipCountryMasterId,
                        model.DischargedTermCode, model.AcademicCareerName, model.AcademicCareerCode, model.AcademicCalendarMasterId,
                        model.EmailAddress1, model.EmailAddress1MasterRecordId, model.EmailAddress2, model.EmailAddress2MasterRecordId,
                        User.Identity.Name);

                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore ***
        ******************************************************************/
        #region Ignore
        public IActionResult StudentIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load StudentIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentList));
        }
        #endregion

        #endregion

        #region Student - Enrollment

        /*  *** Enrollment List View ***
        ******************************************************************/
        #region Student - Enrollment List
        // GET: Student/StudentEnrollmentList
        public IActionResult StudentEnrollmentList()
        {
            try
            {
                var model = new StudentEnrollmentListViewModel()
                {
                    Title = "Student Enrollment",
                    PageId = "studentEnrollmentPage",
                    ActiveClass = "StudentEnrollment",
                    Message = "Your Student Enrollment Page",
                    Integration = "Student Enrollment",
                    IntegrationId = 9,
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<StudentEnrollmentRemediationListItemViewModel>()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentList details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentEnrollmentList(AjaxDataTableRequest request)
        {
            try
            {
                var enrollments = _context.StudentEnrollmentRemediationList.AsQueryable();

                int recordsTotal = enrollments.Count();

                var studentEnrollmentList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? enrollments
                                                : enrollments.Where(s => s.StudentId.Contains(request.searchValue) ||
                                                                      s.EnrollmentTerm.Contains(request.searchValue) ||
                                                                      s.EnrollmentCampus.Contains(request.searchValue) ||
                                                                      s.ErrorCategories.Contains(request.searchValue) ||
                                                                      s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = studentEnrollmentList.Count();

                var studentEnrollmentRemediationList = new List<StudentEnrollmentRemediationListItemViewModel>();

                foreach (var item in studentEnrollmentList.Skip(request.start).Take(request.length))
                {
                    studentEnrollmentRemediationList.Add(new StudentEnrollmentRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,
                        
                        StudentId = item.StudentId,
                        EnrollmentTerm = item.EnrollmentTerm,
                        EnrollmentCampus = item.EnrollmentCampus,

                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = studentEnrollmentRemediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Enrollment Edit View ***
        ******************************************************************/
        #region Student - Enrollment Edit
        // GET: Student/StudentEnrollmentEdit
        public IActionResult StudentEnrollmentEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentEnrollmentHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var enrollmentDetail = history.First();
                var enrollmentSource = history.Last();

                var viewModel = new StudentEnrollmentViewModel()
                {
                    Title = "Student Enrollment",
                    PageId = "studentEnrollmentPage",
                    ActiveClass = "StudentEnrollment",
                    Message = "Your Student Enrollment Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = enrollmentDetail.Id,
                    System = enrollmentDetail.SystemName,
                    SystemId = enrollmentDetail.SystemId,
                    Integration = enrollmentDetail.IntegrationName,
                    IntegrationId = enrollmentDetail.IntegrationId,
                    IntegrationDate = enrollmentDetail.IntegrationDate,
                    CreatedDate = enrollmentDetail.RecordDate,
                    SourceRecordId = enrollmentDetail.SourceRecordId,
                    CreatedOnDT = enrollmentDetail.RecordDate,

                    HistoryData = new List<StudentEnrollmentHistoryViewModel>(),
                    RecordStatus = enrollmentDetail.RecordStatus,

                    #region Enrollment Detail

                    StudentId = enrollmentDetail.StudentId,
                        StudentId_BusinessName = enrollmentDetail.StudentId_BusinessName,
                        StudentId_BusinessDescription = enrollmentDetail.StudentId_BusinessDescription,
                        StudentId_AttributeId = enrollmentDetail.StudentId_AttributeId,
                        StudentId_OriginalValue = enrollmentSource.StudentId,
                        StudentId_Status = enrollmentDetail.StudentId_Status,
                        StudentId_Source = enrollmentDetail.StudentId_Source,
                        StudentId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.StudentId_Source }),
                    StudentMasterId = enrollmentDetail.StudentMasterId,
                        StudentMasterId_BusinessName = enrollmentDetail.StudentMasterId_BusinessName,
                        StudentMasterId_BusinessDescription = enrollmentDetail.StudentMasterId_BusinessDescription,
                        StudentMasterId_AttributeId = enrollmentDetail.StudentMasterId_AttributeId,
                        StudentMasterId_OriginalValue = enrollmentSource.StudentMasterId,
                        StudentMasterId_Status = enrollmentDetail.StudentMasterId_Status,
                        StudentMasterId_Source = enrollmentDetail.StudentMasterId_Source,
                        StudentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.StudentMasterId_Source, enrollmentDetail.StudentId_Source }),

                    TermCode = enrollmentDetail.TermCode,
                        TermCode_BusinessName = enrollmentDetail.TermCode_BusinessName,
                        TermCode_BusinessDescription = enrollmentDetail.TermCode_BusinessDescription,
                        TermCode_AttributeId = enrollmentDetail.TermCode_AttributeId,
                        TermCode_OriginalValue = enrollmentSource.TermCode,
                        TermCode_Status = enrollmentDetail.TermCode_Status,
                        TermCode_Source = enrollmentDetail.TermCode_Source,
                        TermCode_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.TermCode_Source }),
                    TermName = enrollmentDetail.TermName,
                        TermName_BusinessName = enrollmentDetail.TermName_BusinessName,
                        TermName_BusinessDescription = enrollmentDetail.TermName_BusinessDescription,
                        TermName_AttributeId = enrollmentDetail.TermName_AttributeId,
                        TermName_OriginalValue = enrollmentSource.TermName,
                        TermName_Status = enrollmentDetail.TermName_Status,
                        TermName_Source = enrollmentDetail.TermName_Source,
                        TermName_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.TermName_Source }),
                    TermMasterId = enrollmentDetail.TermMasterId,
                        TermMasterId_BusinessName = enrollmentDetail.TermMasterId_BusinessName,
                        TermMasterId_BusinessDescription = enrollmentDetail.TermMasterId_BusinessDescription,
                        TermMasterId_AttributeId = enrollmentDetail.TermMasterId_AttributeId,
                        TermMasterId_OriginalValue = enrollmentSource.TermMasterId,
                        TermMasterId_Status = enrollmentDetail.TermMasterId_Status,
                        TermMasterId_Source = enrollmentDetail.TermMasterId_Source,
                        TermMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.TermMasterId_Source, enrollmentDetail.TermName_Source, enrollmentDetail.TermCode_Source }),

                    CampusName = enrollmentDetail.CampusName,
                        CampusName_BusinessName = enrollmentDetail.CampusName_BusinessName,
                        CampusName_BusinessDescription = enrollmentDetail.CampusName_BusinessDescription,
                        CampusName_AttributeId = enrollmentDetail.CampusName_AttributeId,
                        CampusName_OriginalValue = enrollmentSource.CampusName,
                        CampusName_Status = enrollmentDetail.CampusName_Status,
                        CampusName_Source = enrollmentDetail.CampusName_Source,
                        CampusName_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.CampusName_Source }),
                    CampusSourceSystemRecordId = enrollmentDetail.CampusSourceSystemRecordId,
                        CampusSourceSystemRecordId_BusinessName = enrollmentDetail.CampusSourceSystemRecordId_BusinessName,
                        CampusSourceSystemRecordId_BusinessDescription = enrollmentDetail.CampusSourceSystemRecordId_BusinessDescription,
                        CampusSourceSystemRecordId_AttributeId = enrollmentDetail.CampusSourceSystemRecordId_AttributeId,
                        CampusSourceSystemRecordId_OriginalValue = enrollmentSource.CampusSourceSystemRecordId,
                        CampusSourceSystemRecordId_Status = enrollmentDetail.CampusSourceSystemRecordId_Status,
                        CampusSourceSystemRecordId_Source = enrollmentDetail.CampusSourceSystemRecordId_Source,
                        CampusSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.CampusSourceSystemRecordId_Source }),
                    CampusMasterId = enrollmentDetail.CampusMasterId,
                        CampusMasterId_BusinessName = enrollmentDetail.CampusMasterId_BusinessName,
                        CampusMasterId_BusinessDescription = enrollmentDetail.CampusMasterId_BusinessDescription,
                        CampusMasterId_AttributeId = enrollmentDetail.CampusMasterId_AttributeId,
                        CampusMasterId_OriginalValue = enrollmentSource.CampusMasterId,
                        CampusMasterId_Status = enrollmentDetail.CampusMasterId_Status,
                        CampusMasterId_Source = enrollmentDetail.CampusMasterId_Source,
                        CampusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.CampusMasterId_Source, enrollmentDetail.CampusSourceSystemRecordId_Source, enrollmentDetail.CampusName_Source }),

                    AcademicCareerName = enrollmentDetail.AcademicCareerName,
                        AcademicCareerName_BusinessName = enrollmentDetail.AcademicCareerName_BusinessName,
                        AcademicCareerName_BusinessDescription = enrollmentDetail.AcademicCareerName_BusinessDescription,
                        AcademicCareerName_AttributeId = enrollmentDetail.AcademicCareerName_AttributeId,
                        AcademicCareerName_OriginalValue = enrollmentSource.AcademicCareerName,
                        AcademicCareerName_Status = enrollmentDetail.AcademicCareerName_Status,
                        AcademicCareerName_Source = enrollmentDetail.AcademicCareerName_Source,
                        AcademicCareerName_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicCareerName_Source }),
                    AcademicCareerSourceSystemRecordId = enrollmentDetail.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_BusinessName = enrollmentDetail.AcademicCareerSourceSystemRecordId_BusinessName,
                        AcademicCareerSourceSystemRecordId_BusinessDescription = enrollmentDetail.AcademicCareerSourceSystemRecordId_BusinessDescription,
                        AcademicCareerSourceSystemRecordId_AttributeId = enrollmentDetail.AcademicCareerSourceSystemRecordId_AttributeId,
                        AcademicCareerSourceSystemRecordId_OriginalValue = enrollmentSource.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_Status = enrollmentDetail.AcademicCareerSourceSystemRecordId_Status,
                        AcademicCareerSourceSystemRecordId_Source = enrollmentDetail.AcademicCareerSourceSystemRecordId_Source,
                        AcademicCareerSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicCareerSourceSystemRecordId_Source }),
                    AcademicCareerMasterId = enrollmentDetail.AcademicCareerMasterId,
                        AcademicCareerMasterId_BusinessName = enrollmentDetail.AcademicCareerMasterId_BusinessName,
                        AcademicCareerMasterId_BusinessDescription = enrollmentDetail.AcademicCareerMasterId_BusinessDescription,
                        AcademicCareerMasterId_AttributeId = enrollmentDetail.AcademicCareerMasterId_AttributeId,
                        AcademicCareerMasterId_OriginalValue = enrollmentSource.AcademicCareerMasterId,
                        AcademicCareerMasterId_Status = enrollmentDetail.AcademicCareerMasterId_Status,
                        AcademicCareerMasterId_Source = enrollmentDetail.AcademicCareerMasterId_Source,
                        AcademicCareerMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicCareerMasterId_Source, enrollmentDetail.AcademicCareerSourceSystemRecordId_Source, enrollmentDetail.AcademicCareerName_Source }),

                    AcademicLevelName = enrollmentDetail.AcademicLevelName,
                        AcademicLevelName_BusinessName = enrollmentDetail.AcademicLevelName_BusinessName,
                        AcademicLevelName_BusinessDescription = enrollmentDetail.AcademicLevelName_BusinessDescription,
                        AcademicLevelName_AttributeId = enrollmentDetail.AcademicLevelName_AttributeId,
                        AcademicLevelName_OriginalValue = enrollmentSource.AcademicLevelName,
                        AcademicLevelName_Status = enrollmentDetail.AcademicLevelName_Status,
                        AcademicLevelName_Source = enrollmentDetail.AcademicLevelName_Source,
                        AcademicLevelName_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicLevelName_Source }),
                    AcademicLevelSourceSystemRecordId = enrollmentDetail.AcademicLevelSourceSystemRecordId,
                        AcademicLevelSourceSystemRecordId_BusinessName = enrollmentDetail.AcademicLevelSourceSystemRecordId_BusinessName,
                        AcademicLevelSourceSystemRecordId_BusinessDescription = enrollmentDetail.AcademicLevelSourceSystemRecordId_BusinessDescription,
                        AcademicLevelSourceSystemRecordId_AttributeId = enrollmentDetail.AcademicLevelSourceSystemRecordId_AttributeId,
                        AcademicLevelSourceSystemRecordId_OriginalValue = enrollmentSource.AcademicLevelSourceSystemRecordId,
                        AcademicLevelSourceSystemRecordId_Status = enrollmentDetail.AcademicLevelSourceSystemRecordId_Status,
                        AcademicLevelSourceSystemRecordId_Source = enrollmentDetail.AcademicLevelSourceSystemRecordId_Source,
                        AcademicLevelSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicLevelSourceSystemRecordId_Source }),
                    AcademicLevelMasterId = enrollmentDetail.AcademicLevelMasterId,
                        AcademicLevelMasterId_BusinessName = enrollmentDetail.AcademicLevelMasterId_BusinessName,
                        AcademicLevelMasterId_BusinessDescription = enrollmentDetail.AcademicLevelMasterId_BusinessDescription,
                        AcademicLevelMasterId_AttributeId = enrollmentDetail.AcademicLevelMasterId_AttributeId,
                        AcademicLevelMasterId_OriginalValue = enrollmentSource.AcademicLevelMasterId,
                        AcademicLevelMasterId_Status = enrollmentDetail.AcademicLevelMasterId_Status,
                        AcademicLevelMasterId_Source = enrollmentDetail.AcademicLevelMasterId_Source,
                        AcademicLevelMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.AcademicLevelMasterId_Source, enrollmentDetail.AcademicLevelSourceSystemRecordId_Source, enrollmentDetail.AcademicLevelName_Source }),

                    TotalTransferUnits = enrollmentDetail.TotalTransferUnits,
                        TotalTransferUnits_BusinessName = enrollmentDetail.TotalTransferUnits_BusinessName,
                        TotalTransferUnits_BusinessDescription = enrollmentDetail.TotalTransferUnits_BusinessDescription,
                        TotalTransferUnits_AttributeId = enrollmentDetail.TotalTransferUnits_AttributeId,
                        TotalTransferUnits_OriginalValue = enrollmentSource.TotalTransferUnits,
                        TotalTransferUnits_Status = enrollmentDetail.TotalTransferUnits_Status,
                        TotalTransferUnits_Source = enrollmentDetail.TotalTransferUnits_Source,
                        TotalTransferUnits_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.TotalTransferUnits_Source }),

                    TotalCumulativeUnits = enrollmentDetail.TotalCumulativeUnits,
                        TotalCumulativeUnits_BusinessName = enrollmentDetail.TotalCumulativeUnits_BusinessName,
                        TotalCumulativeUnits_BusinessDescription = enrollmentDetail.TotalCumulativeUnits_BusinessDescription,
                        TotalCumulativeUnits_AttributeId = enrollmentDetail.TotalCumulativeUnits_AttributeId,
                        TotalCumulativeUnits_OriginalValue = enrollmentSource.TotalCumulativeUnits,
                        TotalCumulativeUnits_Status = enrollmentDetail.TotalCumulativeUnits_Status,
                        TotalCumulativeUnits_Source = enrollmentDetail.TotalCumulativeUnits_Source,
                        TotalCumulativeUnits_IsReadOnly = AttributeIsReadOnly(new string[] { enrollmentDetail.TotalCumulativeUnits_Source }),

                    StudentList = GetStudentList(),
                    CampusList = GetCampusList(),
                    AcademicCareerList = GetDischargedAcademicCareerList(),
                    AcademicLevelList = GetAcademicLevelList(),

                    #endregion

                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new StudentEnrollmentHistoryViewModel()
                    {
                        StudentId = item.StudentId,
                            StudentId_Status = item.StudentId_Status,
                            StudentId_OriginalValue = previousitem.StudentId,
                        StudentMasterId = item.StudentMasterId,
                            StudentMasterId_Status = item.StudentMasterId_Status,
                            StudentMasterId_OriginalValue = previousitem.StudentMasterId,
                        TermCode = item.TermCode,
                            TermCode_Status = item.TermCode_Status,
                            TermCode_OriginalValue = previousitem.TermCode,
                        TermName = item.TermName,
                            TermName_Status = item.TermName_Status,
                            TermName_OriginalValue = previousitem.TermName,
                        TermMasterId = item.TermMasterId,
                            TermMasterId_Status = item.TermMasterId_Status,
                            TermMasterId_OriginalValue = previousitem.TermMasterId,
                        CampusName = item.CampusName,
                            CampusName_Status = item.CampusName_Status,
                            CampusName_OriginalValue = previousitem.CampusName,
                        CampusSourceSystemRecordId = item.CampusSourceSystemRecordId,
                            CampusSourceSystemRecordId_Status = item.CampusSourceSystemRecordId_Status,
                            CampusSourceSystemRecordId_OriginalValue = previousitem.CampusSourceSystemRecordId,
                        CampusMasterId = item.CampusMasterId,
                            CampusMasterId_Status = item.CampusMasterId_Status,
                            CampusMasterId_OriginalValue = previousitem.CampusMasterId,
                        AcademicCareerName = item.AcademicCareerName,
                            AcademicCareerName_Status = item.AcademicCareerName_Status,
                            AcademicCareerName_OriginalValue = previousitem.AcademicCareerName,
                        AcademicCareerSourceSystemRecordId = item.AcademicCareerSourceSystemRecordId,
                            AcademicCareerSourceSystemRecordId_Status = item.AcademicCareerSourceSystemRecordId_Status,
                            AcademicCareerSourceSystemRecordId_OriginalValue = previousitem.AcademicCareerSourceSystemRecordId,
                        AcademicCareerMasterId = item.AcademicCareerMasterId,
                            AcademicCareerMasterId_Status = item.AcademicCareerMasterId_Status,
                            AcademicCareerMasterId_OriginalValue = previousitem.AcademicCareerMasterId,
                        AcademicLevelName = item.AcademicLevelName,
                            AcademicLevelName_Status = item.AcademicLevelName_Status,
                            AcademicLevelName_OriginalValue = previousitem.AcademicLevelName,
                        AcademicLevelSourceSystemRecordId = item.AcademicLevelSourceSystemRecordId,
                            AcademicLevelSourceSystemRecordId_Status = item.AcademicLevelSourceSystemRecordId_Status,
                            AcademicLevelSourceSystemRecordId_OriginalValue = previousitem.AcademicLevelSourceSystemRecordId,
                        AcademicLevelMasterId = item.AcademicLevelMasterId,
                            AcademicLevelMasterId_Status = item.AcademicLevelMasterId_Status,
                            AcademicLevelMasterId_OriginalValue = previousitem.AcademicLevelMasterId,
                        TotalTransferUnits = item.TotalTransferUnits,
                            TotalTransferUnits_Status = item.TotalTransferUnits_Status,
                            TotalTransferUnits_OriginalValue = previousitem.TotalTransferUnits,
                        TotalCumulativeUnits = item.TotalCumulativeUnits,
                            TotalCumulativeUnits_Status = item.TotalCumulativeUnits_Status,
                            TotalCumulativeUnits_OriginalValue = previousitem.TotalCumulativeUnits,

                        HistoryDate = item.RecordDate

                    });
                }
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Enrollment Save ***
        ******************************************************************/
        #region Student - Enrollment Save
        // POST: Student/StudentEnrollmentSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentEnrollmentSave(StudentEnrollmentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentEnrollmentIntegrationRecord(model.SystemId, model.Id, model.StudentId, model.StudentMasterId,
                        model.TermCode, model.TermName, model.TermMasterId,
                        model.CampusName, model.CampusSourceSystemRecordId, model.CampusMasterId,
                        model.AcademicCareerName, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                        model.AcademicLevelName, model.AcademicLevelSourceSystemRecordId, model.AcademicLevelMasterId,
                        model.TotalTransferUnits, model.TotalCumulativeUnits,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentEnrollmentSave details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentEnrollmentList));
            }
            return View(model);
        }
        #endregion

        /*  *** Enrollment Revalidate ***
        ******************************************************************/
        #region Student - Enrollment Revalidate
        // POST: Student/StudentEnrollmentRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentEnrollmentRevalidate(StudentEnrollmentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // StudentMasterId
                    if (string.IsNullOrEmpty(model.StudentMasterId) && (!string.IsNullOrEmpty(model.StudentMasterId)))
                    {
                        model.StudentMasterId = null;

                        model.IsChanged = true;
                    }

                    // TermMasterId
                    if (string.IsNullOrEmpty(model.TermMasterId) && (!string.IsNullOrEmpty(model.TermName) || !string.IsNullOrEmpty(model.TermCode)))
                    {
                        model.TermName = null;
                        model.TermCode = null;

                        model.IsChanged = true;
                    }

                    // CampusMasterId
                    if (string.IsNullOrEmpty(model.CampusMasterId) && (!string.IsNullOrEmpty(model.CampusSourceSystemRecordId) || !string.IsNullOrEmpty(model.CampusName)))
                    {
                        model.CampusSourceSystemRecordId = null;
                        model.CampusName = null;

                        model.IsChanged = true;
                    }

                    // AcademicCareerMasterId
                    if (string.IsNullOrEmpty(model.AcademicCareerMasterId) && (!string.IsNullOrEmpty(model.AcademicCareerSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicCareerName)))
                    {
                        model.AcademicCareerSourceSystemRecordId = null;
                        model.AcademicCareerName = null;

                        model.IsChanged = true;
                    }

                    // AcademicLevelMasterId
                    if (string.IsNullOrEmpty(model.AcademicLevelMasterId) && (!string.IsNullOrEmpty(model.AcademicLevelSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicLevelName)))
                    {
                        model.AcademicLevelSourceSystemRecordId = null;
                        model.AcademicLevelName = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ChangeStudentEnrollmentIntegrationRecord(model.SystemId, model.Id, model.StudentId, model.StudentMasterId,
                            model.TermCode, model.TermName, model.TermMasterId,
                            model.CampusName, model.CampusSourceSystemRecordId, model.CampusMasterId,
                            model.AcademicCareerName, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                            model.AcademicLevelName, model.AcademicLevelSourceSystemRecordId, model.AcademicLevelMasterId,
                            model.TotalTransferUnits, model.TotalCumulativeUnits,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentEnrollmentRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentEnrollmentList));
            }
            return View(model);
        }
        #endregion

        /*  *** Enrollment Match ***
        ******************************************************************/
        #region Student - Enrollment Match
        // GET: Student/StudentEnrollmentMatch
        public IActionResult StudentEnrollmentMatch(long Id, int SystemId)
        {
            try
            {
                //var studentDetail = await _context.StudentDetails.Where(e => e.Id == Id && e.SystemId == SystemId).SingleAsync();

                var viewModel = new StudentEnrollmentMatchViewModel()
                {
                    Title = "Student Enrollment Matching",
                    PageId = "studentEnrollmentPage",
                    ActiveClass = "StudentEnrollment",
                    Message = "Your Student Enrollment Page",
                    //PageWrapperClass = "toggled",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    Id = -798461354651321684,
                    System = "UAIR",
                    SystemId = 5,
                    Integration = "Enrollment",
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 11, 06),
                    SourceRecordId = "kj234h2guihg34hg234",
                    CreatedOnDT = new DateTime(2019, 11, 06),

                    StudentName = "Tom Cotton",
                    StudentId = "4984651354351",
                    EnrollmentAcademicYear = "1971",
                        EnrollmentAcademicYear_BusinessName = "Academic Year",
                        EnrollmentAcademicYear_BusinessDescription = "Year of Academic Enrollment",
                    EnrollmentTerm = "2171",
                        EnrollmentTerm_BusinessName = "Term",
                        EnrollmentTerm_BusinessDescription = "Term of Academic Enrollment",
                    EnrollmentCampus = "UA South",
                        EnrollmentCampus_BusinessName = "Campus",
                        EnrollmentCampus_BusinessDescription = "Campus Description",
                    EnrollmentLocation = "Sierra Vista",
                        EnrollmentLocation_BusinessName = "Location",
                        EnrollmentLocation_BusinessDescription = "Location Description",
                    EnrollmentTotalCumulativeUnits = 149,
                        EnrollmentTotalCumulativeUnits_BusinessName = "Total Cumulative Units",
                        EnrollmentTotalCumulativeUnits_BusinessDescription = "Total Cumulative Units Description",
                    EnrollmentAcademicCareer = "Undergraduate",
                        EnrollmentAcademicCareer_BusinessName = "Academic Career",
                        EnrollmentAcademicCareer_BusinessDescription = "Academic Career Description",
                        EnrollmentAcademicCareer_Weight = 33,
                    EnrollmentAcademicLevel = "Sophomore",
                        EnrollmentAcademicLevel_BusinessName = "Academic Level",
                        EnrollmentAcademicLevel_BusinessDescription = "Academic Level Description",
                        EnrollmentAcademicLevel_Weight = 33,
                    EnrollmentAcademicProgram = "College of Medicine",
                        EnrollmentAcademicProgram_BusinessName = "Academic Program",
                        EnrollmentAcademicProgram_BusinessDescription = "Academic Program Description",
                        EnrollmentAcademicProgram_Weight = 33,
                    EnrollmentAcademicPlan = "Creative Writing",
                        EnrollmentAcademicPlan_BusinessName = "Academic Plan",
                        EnrollmentAcademicPlan_BusinessDescription = "Academic Plan Description",
                    EnrollmentAcademicPlanType = "Major",
                        EnrollmentAcademicPlanType_BusinessName = "Academic Plan Type",
                        EnrollmentAcademicPlanType_BusinessDescription = "Academic Plan Type Description",
                    EnrollmentAcademicSubplan = "Japanese Culture",
                        EnrollmentAcademicSubplan_BusinessName = "Academic Subplan",
                        EnrollmentAcademicSubplan_BusinessDescription = "Academic Subplan Description",
                    EnrollmentAcademicSubplanType = "Emphasis",
                        EnrollmentAcademicSubplanType_BusinessName = "Academic Subplan Type",
                        EnrollmentAcademicSubplanType_BusinessDescription = "Academic Subplan Type Description",

                    PossibleMatches = new List<StudentEnrollmentMatchViewModel.StudentEnrollmentMatchSummaryViewModel>()
                };

                viewModel.PossibleMatches.Add(new StudentEnrollmentMatchViewModel.StudentEnrollmentMatchSummaryViewModel()
                {
                    MatchConfidence = 75,
                    MasterId = "4864313546846",

                    StudentName = "Tom Cotton",
                    StudentId = "4984651354351",
                    EnrollmentAcademicYear = "1971",
                    EnrollmentTerm = "2172",
                    EnrollmentAcademicCareer = "Undergraduate",
                    EnrollmentAcademicLevel = "Sophomore",
                    EnrollmentAcademicProgram = "College of Medicine"
                });

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Enrollment Compare ***
        ******************************************************************/
        #region Student - Enrollment Compare
        // GET: Student/StudentEnrollmentCompare
        public IActionResult StudentEnrollmentCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                //var comparison = _context.GetStudentComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new StudentEnrollmentCompareViewModel()
                {
                    Id = -798461354651321684,
                    System = "UAIR",
                    SystemId = 5,
                    MasterId = MasterId,
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 11, 06),
                    SourceRecordId = "kj234h2guihg34hg234",
                    SourceRecordId_Compare = "kj234h2guihg34hg234",

                    StudentName = "Tom Cotton",
                    StudentId = "4984651354351",
                    EnrollmentAcademicYear = "1971",
                        EnrollmentAcademicYear_BusinessName = "Academic Year",
                        EnrollmentAcademicYear_BusinessDescription = "Year of Academic Enrollment",
                        EnrollmentAcademicYear_Compare = "1971",
                    EnrollmentTerm = "2171",
                        EnrollmentTerm_BusinessName = "Term",
                        EnrollmentTerm_BusinessDescription = "Term of Academic Enrollment",
                        EnrollmentTerm_Compare = "2172",
                    EnrollmentCampus = "UA South",
                        EnrollmentCampus_BusinessName = "Campus",
                        EnrollmentCampus_BusinessDescription = "Campus Description",
                        EnrollmentCampus_Compare = "UA South",
                    EnrollmentLocation = "Sierra Vista",
                        EnrollmentLocation_BusinessName = "Location",
                        EnrollmentLocation_BusinessDescription = "Location Description",
                        EnrollmentLocation_Compare = "Sierra Vista",
                    EnrollmentTotalCumulativeUnits = 149,
                        EnrollmentTotalCumulativeUnits_BusinessName = "Total Cumulative Units",
                        EnrollmentTotalCumulativeUnits_BusinessDescription = "Total Cumulative Units Description",
                        EnrollmentTotalCumulativeUnits_Compare = 149,
                    EnrollmentAcademicCareer = "Undergraduate",
                        EnrollmentAcademicCareer_BusinessName = "Academic Career",
                        EnrollmentAcademicCareer_BusinessDescription = "Academic Career Description",
                        EnrollmentAcademicCareer_Compare = "Undergraduate",
                    EnrollmentAcademicLevel = "Sophomore",
                        EnrollmentAcademicLevel_BusinessName = "Academic Level",
                        EnrollmentAcademicLevel_BusinessDescription = "Academic Level Description",
                        EnrollmentAcademicLevel_Compare = "Sophomore",
                    EnrollmentAcademicProgram = "College of Medicine",
                        EnrollmentAcademicProgram_BusinessName = "Academic Program",
                        EnrollmentAcademicProgram_BusinessDescription = "Academic Program Description",
                        EnrollmentAcademicProgram_Compare = "College of Medicine",
                    EnrollmentAcademicPlan = "Creative Writing",
                        EnrollmentAcademicPlan_BusinessName = "Academic Plan",
                        EnrollmentAcademicPlan_BusinessDescription = "Academic Plan Description",
                        EnrollmentAcademicPlan_Compare = "Creative Writing",
                    EnrollmentAcademicPlanType = "Major",
                        EnrollmentAcademicPlanType_BusinessName = "Academic Plan Type",
                        EnrollmentAcademicPlanType_BusinessDescription = "Academic Plan Type Description",
                        EnrollmentAcademicPlanType_Compare = "Major",
                    EnrollmentAcademicSubplan = "Japanese Culture",
                        EnrollmentAcademicSubplan_BusinessName = "Academic Subplan",
                        EnrollmentAcademicSubplan_BusinessDescription = "Academic Subplan Description",
                        EnrollmentAcademicSubplan_Compare = "Japanese Culture",
                    EnrollmentAcademicSubplanType = "Emphasis",
                        EnrollmentAcademicSubplanType_BusinessName = "Academic Subplan Type",
                        EnrollmentAcademicSubplanType_BusinessDescription = "Academic Subplan Type Description",
                        EnrollmentAcademicSubplanType_Compare = "Emphasis",
                };

                return PartialView("StudentEnrollmentCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Enrollment Manual Match ***
        ******************************************************************/
        #region Student - Enrollment ManualMatch
        // GET: Student/StudentEnrollmentManualMatch
        public async Task<IActionResult> StudentEnrollmentManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentEnrollmentManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentEnrollmentList));
        }
        #endregion

        /*  *** Remove/Ignore ***
        ******************************************************************/
        #region Remove/Ignore
        public IActionResult StudentEnrollmentIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load StudentEnrollmentIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentEnrollmentList));
        }
        #endregion

        #endregion

        #region Student - Degree

        /*  *** Degree List View ***
        ******************************************************************/
        #region Student - Degree List
        // GET: Student/StudentDegreeList
        public IActionResult StudentDegreeList()
        {
            var model = new DegreeListViewModel()
            {
                Title = "Student Degree",
                PageId = "studentDegreePage",
                ActiveClass = "StudentDegree",
                Message = "Your Student Degree Page",
                Integration = "Student Degree",
                IntegrationId = 8,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<DegreeRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentDegreeList(AjaxDataTableRequest request)
        {
            try
            {
                var degrees = _context.DegreeRemediationList.AsQueryable();

                int recordsTotal = degrees.Count();

                var degreeList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? degrees
                                                : degrees.Where(s => s.Student.Contains(request.searchValue) ||
                                                                     s.EducationalInstitution.Contains(request.searchValue) ||
                                                                     s.PreferredClassOf.Contains(request.searchValue) ||
                                                                     //s.AwardedDate.Contains(request.searchValue) ||
                                                                     s.AwardedTerm.Contains(request.searchValue) ||
                                                                     s.ErrorCategories.Contains(request.searchValue) ||
                                                                     s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = degreeList.Count();

                var degreeRemediationList = new List<DegreeRemediationListItemViewModel>();

                foreach (var item in degreeList.Skip(request.start).Take(request.length))
                {
                    degreeRemediationList.Add(new DegreeRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,

                        Student = item.Student,
                        //StudentName = "Coming Soon...",
                        EducationalInstitution = item.EducationalInstitution,
                        PreferredClassOf = item.PreferredClassOf,
                        //AwardedDate = item.AwardedDate,
                        AwardedTerm = item.AwardedTerm,

                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = degreeRemediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentDegreeList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Degree Edit View ***
        ******************************************************************/
        #region Student - Degree Edit
        // GET: Student/StudentDegreeEdit
        public IActionResult StudentDegreeEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentDegreeHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var studentDegreeDetail = history.First();
                var studentDegreeSource = history.Last();

                var viewModel = new DegreeViewModel()
                {
                    Title = "Student Degree",
                    PageId = "studentDegreePage",
                    ActiveClass = "StudentDegree",
                    Message = "Your Student Degree Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = studentDegreeDetail.Id,
                    System = studentDegreeDetail.SystemName,
                    SystemId = studentDegreeDetail.SystemId,
                    Integration = studentDegreeDetail.IntegrationName,
                    IntegrationId = studentDegreeDetail.IntegrationId,
                    IntegrationDate = studentDegreeDetail.IntegrationDate,
                    CreatedDate = studentDegreeDetail.RecordDate,
                    SourceRecordId = studentDegreeDetail.SourceRecordId,
                    CreatedOnDT = studentDegreeDetail.RecordDate,

                    HistoryData = new List<StudentDegreeHistoryViewModel>(),

                    #region Degree Details
                        
                    ClassOf = studentDegreeDetail.ClassOf,
                        ClassOf_BusinessName = studentDegreeDetail.ClassOf_BusinessName,
                        ClassOf_BusinessDescription = studentDegreeDetail.ClassOf_BusinessDescription,
                        ClassOf_OriginalValue = studentDegreeSource.ClassOf,
                        ClassOf_Status = studentDegreeDetail.ClassOf_Status,
                        ClassOf_Source = studentDegreeDetail.ClassOf_Source,
                        ClassOf_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.ClassOf_Source }),

                    Student = studentDegreeDetail.Student,
                        Student_BusinessName = studentDegreeDetail.Student_BusinessName,
                        Student_BusinessDescription = studentDegreeDetail.Student_BusinessDescription,
                        Student_OriginalValue = studentDegreeSource.Student,
                        Student_Status = studentDegreeDetail.Student_Status,
                        Student_Source = studentDegreeDetail.Student_Source,
                        Student_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.Student_Source }),
                    StudentSourceSystemRecordId = studentDegreeDetail.StudentSourceSystemRecordId,
                        StudentSourceSystemRecordId_BusinessName = studentDegreeDetail.StudentSourceSystemRecordId_BusinessName,
                        StudentSourceSystemRecordId_BusinessDescription = studentDegreeDetail.StudentSourceSystemRecordId_BusinessDescription,
                        StudentSourceSystemRecordId_OriginalValue = studentDegreeSource.StudentSourceSystemRecordId,
                        StudentSourceSystemRecordId_Status = studentDegreeDetail.StudentSourceSystemRecordId_Status,
                        StudentSourceSystemRecordId_Source = studentDegreeDetail.StudentSourceSystemRecordId_Source,
                        StudentSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.StudentSourceSystemRecordId_Source }),
                    StudentMasterId = studentDegreeDetail.StudentMasterId,
                        StudentMasterId_BusinessName = studentDegreeDetail.StudentMasterId_BusinessName,
                        StudentMasterId_BusinessDescription = studentDegreeDetail.StudentMasterId_BusinessDescription,
                        StudentMasterId_OriginalValue = studentDegreeSource.StudentMasterId,
                        StudentMasterId_Status = studentDegreeDetail.StudentMasterId_Status,
                        StudentMasterId_Source = studentDegreeDetail.StudentMasterId_Source,
                        StudentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.StudentMasterId_Source, studentDegreeDetail.StudentSourceSystemRecordId_Source, studentDegreeDetail.Student_Source }),

                    EducationalInstitution = studentDegreeDetail.EducationalInstitution,
                        EducationalInstitution_BusinessName = studentDegreeDetail.EducationalInstitution_BusinessName,
                        EducationalInstitution_BusinessDescription = studentDegreeDetail.EducationalInstitution_BusinessDescription,
                        EducationalInstitution_OriginalValue = studentDegreeSource.EducationalInstitution,
                        EducationalInstitution_Status = studentDegreeDetail.EducationalInstitution_Status,
                        EducationalInstitution_Source = studentDegreeDetail.EducationalInstitution_Source,
                        EducationalInstitution_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.EducationalInstitution_Source }),
                    EducationalInstitutionSourceSystemRecordId = studentDegreeDetail.EducationalInstitutionSourceSystemRecordId,
                        EducationalInstitutionSourceSystemRecordId_BusinessName = studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_BusinessName,
                        EducationalInstitutionSourceSystemRecordId_BusinessDescription = studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_BusinessDescription,
                        EducationalInstitutionSourceSystemRecordId_OriginalValue = studentDegreeSource.EducationalInstitutionSourceSystemRecordId,
                        EducationalInstitutionSourceSystemRecordId_Status = studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_Status,
                        EducationalInstitutionSourceSystemRecordId_Source = studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_Source,
                        EducationalInstitutionSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_Source }),
                    EducationalInstitutionMasterId = studentDegreeDetail.EducationalInstitutionMasterId,
                        EducationalInstitutionMasterId_BusinessName = studentDegreeDetail.EducationalInstitutionMasterId_BusinessName,
                        EducationalInstitutionMasterId_BusinessDescription = studentDegreeDetail.EducationalInstitutionMasterId_BusinessDescription,
                        EducationalInstitutionMasterId_OriginalValue = studentDegreeSource.EducationalInstitutionMasterId,
                        EducationalInstitutionMasterId_Status = studentDegreeDetail.EducationalInstitutionMasterId_Status,
                        EducationalInstitutionMasterId_Source = studentDegreeDetail.EducationalInstitutionMasterId_Source,
                        EducationalInstitutionMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.EducationalInstitutionMasterId_Source, studentDegreeDetail.EducationalInstitutionSourceSystemRecordId_Source, studentDegreeDetail.EducationalInstitution_Source }),

                    PreferredClassOf = studentDegreeDetail.PreferredClassOf,
                        PreferredClassOf_BusinessName = studentDegreeDetail.PreferredClassOf_BusinessName,
                        PreferredClassOf_BusinessDescription = studentDegreeDetail.PreferredClassOf_BusinessDescription,
                        PreferredClassOf_OriginalValue = studentDegreeSource.PreferredClassOf,
                        PreferredClassOf_Status = studentDegreeDetail.PreferredClassOf_Status,
                        PreferredClassOf_Source = studentDegreeDetail.PreferredClassOf_Source,
                        PreferredClassOf_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.PreferredClassOf_Source }),

                    AwardedDate = studentDegreeDetail.AwardedDate,
                        AwardedDate_BusinessName = studentDegreeDetail.AwardedDate_BusinessName,
                        AwardedDate_BusinessDescription = studentDegreeDetail.AwardedDate_BusinessDescription,
                        AwardedDate_OriginalValue = studentDegreeSource.AwardedDate,
                        AwardedDate_Status = studentDegreeDetail.AwardedDate_Status,
                        AwardedDate_Source = studentDegreeDetail.AwardedDate_Source,
                        AwardedDate_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AwardedDate_Source }),
                        
                    HonorSourceSystemRecordId = studentDegreeDetail.DegreeHonorSourceSystemRecordId,
                        HonorSourceSystemRecordId_BusinessName = studentDegreeDetail.DegreeHonorSourceSystemRecordId_BusinessName,
                        HonorSourceSystemRecordId_BusinessDescription = studentDegreeDetail.DegreeHonorSourceSystemRecordId_BusinessDescription,
                        HonorSourceSystemRecordId_OriginalValue = studentDegreeSource.DegreeHonorSourceSystemRecordId,
                        HonorSourceSystemRecordId_Status = studentDegreeDetail.DegreeHonorSourceSystemRecordId_Status,
                        HonorSourceSystemRecordId_Source = studentDegreeDetail.DegreeHonorSourceSystemRecordId_Source,
                        HonorSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeHonorSourceSystemRecordId_Source }),
                    HonorMasterId = studentDegreeDetail.DegreeHonorMasterId,
                        HonorMasterId_BusinessName = studentDegreeDetail.DegreeHonorMasterId_BusinessName,
                        HonorMasterId_BusinessDescription = studentDegreeDetail.DegreeHonorMasterId_BusinessDescription,
                        HonorMasterId_OriginalValue = studentDegreeSource.DegreeHonorMasterId,
                        HonorMasterId_Status = studentDegreeDetail.DegreeHonorMasterId_Status,
                        HonorMasterId_Source = studentDegreeDetail.DegreeHonorMasterId_Source,
                        HonorMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeHonorMasterId_Source, studentDegreeDetail.DegreeHonorSourceSystemRecordId_Source }),

                    DegreeStatus = studentDegreeDetail.DegreeStatus,
                        DegreeStatus_BusinessName = studentDegreeDetail.DegreeStatus_BusinessName,
                        DegreeStatus_BusinessDescription = studentDegreeDetail.DegreeStatus_BusinessDescription,
                        DegreeStatus_OriginalValue = studentDegreeSource.DegreeStatus,
                        DegreeStatus_Status = studentDegreeDetail.DegreeStatus_Status,
                        DegreeStatus_Source = studentDegreeDetail.DegreeStatus_Source,
                        DegreeStatus_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeStatus_Source }),
                    DegreeStatusSourceSystemRecordId = studentDegreeDetail.DegreeStatusSourceSystemRecordId,
                        DegreeStatusSourceSystemRecordId_BusinessName = studentDegreeDetail.DegreeStatusSourceSystemRecordId_BusinessName,
                        DegreeStatusSourceSystemRecordId_BusinessDescription = studentDegreeDetail.DegreeStatusSourceSystemRecordId_BusinessDescription,
                        DegreeStatusSourceSystemRecordId_OriginalValue = studentDegreeSource.DegreeStatusSourceSystemRecordId,
                        DegreeStatusSourceSystemRecordId_Status = studentDegreeDetail.DegreeStatusSourceSystemRecordId_Status,
                        DegreeStatusSourceSystemRecordId_Source = studentDegreeDetail.DegreeStatusSourceSystemRecordId_Source,
                        DegreeStatusSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeStatusSourceSystemRecordId_Source }),
                    DegreeStatusMasterId = studentDegreeDetail.DegreeStatusMasterId,
                        DegreeStatusMasterId_BusinessName = studentDegreeDetail.DegreeStatusMasterId_BusinessName,
                        DegreeStatusMasterId_BusinessDescription = studentDegreeDetail.DegreeStatusMasterId_BusinessDescription,
                        DegreeStatusMasterId_OriginalValue = studentDegreeSource.DegreeStatusMasterId,
                        DegreeStatusMasterId_Status = studentDegreeDetail.DegreeStatusMasterId_Status,
                        DegreeStatusMasterId_Source = studentDegreeDetail.DegreeStatusMasterId_Source,
                        DegreeStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeStatusMasterId_Source, studentDegreeDetail.DegreeStatusSourceSystemRecordId_Source, studentDegreeDetail.DegreeStatus_Source }),

                    AwardedTerm = studentDegreeDetail.AwardedTerm,
                        AwardedTerm_BusinessName = studentDegreeDetail.AwardedTerm_BusinessName,
                        AwardedTerm_BusinessDescription = studentDegreeDetail.AwardedTerm_BusinessDescription,
                        AwardedTerm_OriginalValue = studentDegreeSource.AwardedTerm,
                        AwardedTerm_Status = studentDegreeDetail.AwardedTerm_Status,
                        AwardedTerm_Source = studentDegreeDetail.AwardedTerm_Source,
                        AwardedTerm_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AwardedTerm_Source }),
                    AwardedTermMasterId = studentDegreeDetail.AwardedTermMasterId,
                        AwardedTermMasterId_BusinessName = studentDegreeDetail.AwardedTermMasterId_BusinessName,
                        AwardedTermMasterId_BusinessDescription = studentDegreeDetail.AwardedTermMasterId_BusinessDescription,
                        AwardedTermMasterId_OriginalValue = studentDegreeSource.AwardedTermMasterId,
                        AwardedTermMasterId_Status = studentDegreeDetail.AwardedTermMasterId_Status,
                        AwardedTermMasterId_Source = studentDegreeDetail.AwardedTermMasterId_Source,
                        AwardedTermMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AwardedTermMasterId_Source, studentDegreeDetail.AwardedTerm_Source }),

                    DegreeType = studentDegreeDetail.DegreeType,
                        DegreeType_BusinessName = studentDegreeDetail.DegreeType_BusinessName,
                        DegreeType_BusinessDescription = studentDegreeDetail.DegreeType_BusinessDescription,
                        DegreeType_OriginalValue = studentDegreeSource.DegreeType,
                        DegreeType_Status = studentDegreeDetail.DegreeType_Status,
                        DegreeType_Source = studentDegreeDetail.DegreeType_Source,
                        DegreeType_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeType_Source }),
                    DegreeTypeSourceSystemRecordId = studentDegreeDetail.DegreeTypeSourceSystemRecordId,
                        DegreeTypeSourceSystemRecordId_BusinessName = studentDegreeDetail.DegreeTypeSourceSystemRecordId_BusinessName,
                        DegreeTypeSourceSystemRecordId_BusinessDescription = studentDegreeDetail.DegreeTypeSourceSystemRecordId_BusinessDescription,
                        DegreeTypeSourceSystemRecordId_OriginalValue = studentDegreeSource.DegreeTypeSourceSystemRecordId,
                        DegreeTypeSourceSystemRecordId_Status = studentDegreeDetail.DegreeTypeSourceSystemRecordId_Status,
                        DegreeTypeSourceSystemRecordId_Source = studentDegreeDetail.DegreeTypeSourceSystemRecordId_Source,
                        DegreeTypeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeTypeSourceSystemRecordId_Source }),
                    DegreeTypeMasterId = studentDegreeDetail.DegreeTypeMasterId,
                        DegreeTypeMasterId_BusinessName = studentDegreeDetail.DegreeTypeMasterId_BusinessName,
                        DegreeTypeMasterId_BusinessDescription = studentDegreeDetail.DegreeTypeMasterId_BusinessDescription,
                        DegreeTypeMasterId_OriginalValue = studentDegreeSource.DegreeTypeMasterId,
                        DegreeTypeMasterId_Status = studentDegreeDetail.DegreeTypeMasterId_Status,
                        DegreeTypeMasterId_Source = studentDegreeDetail.DegreeTypeMasterId_Source,
                        DegreeTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.DegreeTypeMasterId_Source, studentDegreeDetail.DegreeTypeSourceSystemRecordId_Source, studentDegreeDetail.DegreeType_Source }),

                    AcademicCareer = studentDegreeDetail.AcademicCareer,
                        AcademicCareer_BusinessName = studentDegreeDetail.AcademicCareer_BusinessName,
                        AcademicCareer_BusinessDescription = studentDegreeDetail.AcademicCareer_BusinessDescription,
                        AcademicCareer_OriginalValue = studentDegreeSource.AcademicCareer,
                        AcademicCareer_Status = studentDegreeDetail.AcademicCareer_Status,
                        AcademicCareer_Source = studentDegreeDetail.AcademicCareer_Source,
                        AcademicCareer_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AcademicCareer_Source }),
                    AcademicCareerSourceSystemRecordId = studentDegreeDetail.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_BusinessName = studentDegreeDetail.AcademicCareerSourceSystemRecordId_BusinessName,
                        AcademicCareerSourceSystemRecordId_BusinessDescription = studentDegreeDetail.AcademicCareerSourceSystemRecordId_BusinessDescription,
                        AcademicCareerSourceSystemRecordId_OriginalValue = studentDegreeSource.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_Status = studentDegreeDetail.AcademicCareerSourceSystemRecordId_Status,
                        AcademicCareerSourceSystemRecordId_Source = studentDegreeDetail.AcademicCareerSourceSystemRecordId_Source,
                        AcademicCareerSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AcademicCareerSourceSystemRecordId_Source }),
                    AcademicCareerMasterId = studentDegreeDetail.AcademicCareerMasterId,
                        AcademicCareerMasterId_BusinessName = studentDegreeDetail.AcademicCareerMasterId_BusinessName,
                        AcademicCareerMasterId_BusinessDescription = studentDegreeDetail.AcademicCareerMasterId_BusinessDescription,
                        AcademicCareerMasterId_OriginalValue = studentDegreeSource.AcademicCareerMasterId,
                        AcademicCareerMasterId_Status = studentDegreeDetail.AcademicCareerMasterId_Status,
                        AcademicCareerMasterId_Source = studentDegreeDetail.AcademicCareerMasterId_Source,
                        AcademicCareerMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentDegreeDetail.AcademicCareerMasterId_Source, studentDegreeDetail.AcademicCareerSourceSystemRecordId_Source, studentDegreeDetail.AcademicCareer_Source }),
                        
                    #endregion

                    #region Dropdowns
                    StudentList = GetStudentList(),
                    EducationalInstitutionList = GetEducationalInstitutionList(),
                    HonorList = GetHonorList(),
                    DegreeStatusList = GetDegreeStatusList(),
                    DegreeTypeList = GetDegreeTypeList(),
                    AwardedTermList = GetStudentAcademicCatalogAcademicProgramList(),
                    AcademicCareerList = GetDischargedAcademicCareerList(),
                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);
                    viewModel.HistoryData.Add(new StudentDegreeHistoryViewModel()
                    {
                        ClassOf = item.ClassOf,
                            ClassOf_Status = item.ClassOf_Status,
                            ClassOf_OriginalValue = previousitem.ClassOf,

                        Student = item.Student,
                            Student_Status = item.Student_Status,
                            Student_OriginalValue = previousitem.Student,
                        StudentSourceSystemRecordId = item.StudentSourceSystemRecordId,
                            StudentSourceSystemRecordId_Status = item.StudentSourceSystemRecordId_Status,
                            StudentSourceSystemRecordId_OriginalValue = previousitem.StudentSourceSystemRecordId,
                        StudentMasterId = item.StudentMasterId,
                            StudentMasterId_Status = item.StudentMasterId_Status,
                            StudentMasterId_OriginalValue = previousitem.StudentMasterId,

                        EducationalInstitution = item.EducationalInstitution,
                            EducationalInstitution_Status = item.EducationalInstitution_Status,
                            EducationalInstitution_OriginalValue = previousitem.EducationalInstitution,
                        EducationalInstitutionSourceSystemRecordId = item.EducationalInstitutionSourceSystemRecordId,
                            EducationalInstitutionSourceSystemRecordId_Status = item.EducationalInstitutionSourceSystemRecordId_Status,
                            EducationalInstitutionSourceSystemRecordId_OriginalValue = previousitem.EducationalInstitutionSourceSystemRecordId,
                        EducationalInstitutionMasterId = item.EducationalInstitutionMasterId,
                            EducationalInstitutionMasterId_Status = item.EducationalInstitutionMasterId_Status,
                            EducationalInstitutionMasterId_OriginalValue = previousitem.EducationalInstitutionMasterId,

                        PreferredClassOf = item.PreferredClassOf,
                            PreferredClassOf_Status = item.PreferredClassOf_Status,
                            PreferredClassOf_OriginalValue = previousitem.PreferredClassOf,

                        AwardedDate = item.AwardedDate,
                            AwardedDate_Status = item.AwardedDate_Status,
                            AwardedDate_OriginalValue = previousitem.AwardedDate,
                            
                        HonorSourceSystemRecordId = item.DegreeHonorSourceSystemRecordId,
                            HonorSourceSystemRecordId_Status = item.DegreeHonorSourceSystemRecordId_Status,
                            HonorSourceSystemRecordId_OriginalValue = previousitem.DegreeHonorSourceSystemRecordId,
                        HonorMasterId = item.DegreeHonorMasterId,
                            HonorMasterId_Status = item.DegreeHonorMasterId_Status,
                            HonorMasterId_OriginalValue = previousitem.DegreeHonorMasterId,

                        DegreeStatus = item.DegreeStatus,
                            DegreeStatus_Status = item.DegreeStatus_Status,
                            DegreeStatus_OriginalValue = previousitem.DegreeStatus,
                        DegreeStatusSourceSystemRecordId = item.DegreeStatusSourceSystemRecordId,
                            DegreeStatusSourceSystemRecordId_Status = item.DegreeStatusSourceSystemRecordId_Status,
                            DegreeStatusSourceSystemRecordId_OriginalValue = previousitem.DegreeStatusSourceSystemRecordId,
                        DegreeStatusMasterId = item.DegreeStatusMasterId,
                            DegreeStatusMasterId_Status = item.DegreeStatusMasterId_Status,
                            DegreeStatusMasterId_OriginalValue = previousitem.DegreeStatusMasterId,

                        AwardedTerm = item.AwardedTerm,
                            AwardedTerm_Status = item.AwardedTerm_Status,
                            AwardedTerm_OriginalValue = previousitem.AwardedTerm,

                        DegreeType = item.DegreeType,
                            DegreeType_Status = item.DegreeType_Status,
                            DegreeType_OriginalValue = previousitem.DegreeType,
                        DegreeTypeSourceSystemRecordId = item.DegreeTypeSourceSystemRecordId,
                            DegreeTypeSourceSystemRecordId_Status = item.DegreeTypeSourceSystemRecordId_Status,
                            DegreeTypeSourceSystemRecordId_OriginalValue = previousitem.DegreeTypeSourceSystemRecordId,
                        DegreeTypeMasterId = item.DegreeTypeMasterId,
                            DegreeTypeMasterId_Status = item.DegreeTypeMasterId_Status,
                            DegreeTypeMasterId_OriginalValue = previousitem.DegreeTypeMasterId,

                        AcademicCareer = item.AcademicCareer,
                            AcademicCareer_Status = item.AcademicCareer_Status,
                            AcademicCareer_OriginalValue = previousitem.AcademicCareer,
                        AcademicCareerSourceSystemRecordId = item.AcademicCareerSourceSystemRecordId,
                            AcademicCareerSourceSystemRecordId_Status = item.AcademicCareerSourceSystemRecordId_Status,
                            AcademicCareerSourceSystemRecordId_OriginalValue = previousitem.AcademicCareerSourceSystemRecordId,
                        AcademicCareerMasterId = item.AcademicCareerMasterId,
                            AcademicCareerMasterId_Status = item.AcademicCareerMasterId_Status,
                            AcademicCareerMasterId_OriginalValue = previousitem.AcademicCareerMasterId,

                        HistoryDate = item.RecordDate
                    });
                };
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentDegreeEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Degree Save ***
        ******************************************************************/
        #region Student - Degree Save
        // POST: Student/StudentDegreeSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentDegreeSave(DegreeViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentDegreeIntegrationRecord(model.SystemId, model.Id, model.ClassOf, model.Student, model.StudentMasterId,
                        model.EducationalInstitution, model.EducationalInstitutionSourceSystemRecordId, model.EducationalInstitutionMasterId,
                        model.PreferredClassOf, model.AwardedDate,
                        model.HonorSourceSystemRecordId, model.HonorMasterId,
                        model.DegreeStatus, model.DegreeStatusSourceSystemRecordId, model.DegreeStatusMasterId,
                        model.AwardedTerm, model.AwardedTermMasterId,
                        model.DegreeType, model.DegreeTypeSourceSystemRecordId, model.DegreeTypeMasterId,
                        model.AcademicCareer, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentDegreeSave details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentDegreeList));
            }
            return View(model);
        }
        #endregion

        /*  *** Degree Revalidate ***
        ******************************************************************/
        #region Student - Degree Revalidate
        // POST: Student/StudentDegreeRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentDegreeRevalidate(DegreeViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // StudentMasterId
                    if (string.IsNullOrEmpty(model.StudentMasterId) && (!string.IsNullOrEmpty(model.StudentMasterId)))
                    {
                        model.StudentMasterId = null;

                        model.IsChanged = true;
                    }

                    // EducationalInstitutionMasterId
                    if (string.IsNullOrEmpty(model.EducationalInstitutionMasterId) && (!string.IsNullOrEmpty(model.EducationalInstitutionSourceSystemRecordId) || !string.IsNullOrEmpty(model.EducationalInstitution)))
                    {
                        model.EducationalInstitutionSourceSystemRecordId = null;
                        model.EducationalInstitution = null;

                        model.IsChanged = true;
                    }

                    // HonorMasterId
                    if (string.IsNullOrEmpty(model.HonorMasterId) && (!string.IsNullOrEmpty(model.HonorSourceSystemRecordId)))
                    {
                        model.HonorSourceSystemRecordId = null;

                        model.IsChanged = true;
                    }

                    // DegreeStatusMasterId
                    if (string.IsNullOrEmpty(model.DegreeStatusMasterId) && (!string.IsNullOrEmpty(model.DegreeStatusSourceSystemRecordId) || !string.IsNullOrEmpty(model.DegreeStatus)))
                    {
                        model.DegreeStatusSourceSystemRecordId = null;
                        model.DegreeStatus = null;

                        model.IsChanged = true;
                    }

                    // AwardedTermMasterId
                    if (string.IsNullOrEmpty(model.AwardedTermMasterId) && (!string.IsNullOrEmpty(model.AwardedTerm)))
                    {
                        model.AwardedTerm = null;

                        model.IsChanged = true;
                    }

                    // DegreeTypeMasterId
                    if (string.IsNullOrEmpty(model.DegreeTypeMasterId) && (!string.IsNullOrEmpty(model.DegreeTypeSourceSystemRecordId) || !string.IsNullOrEmpty(model.DegreeType)))
                    {
                        model.DegreeTypeSourceSystemRecordId = null;
                        model.DegreeType = null;

                        model.IsChanged = true;
                    }

                    // AcademicCareerMasterId
                    if (string.IsNullOrEmpty(model.AcademicCareerMasterId) && (!string.IsNullOrEmpty(model.AcademicCareerSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicCareer)))
                    {
                        model.AcademicCareerSourceSystemRecordId = null;
                        model.AcademicCareer = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ChangeStudentDegreeIntegrationRecord(model.SystemId, model.Id, model.ClassOf, model.Student, model.StudentMasterId,
                            model.EducationalInstitution, model.EducationalInstitutionSourceSystemRecordId, model.EducationalInstitutionMasterId,
                            model.PreferredClassOf, model.AwardedDate,
                            model.HonorSourceSystemRecordId, model.HonorMasterId,
                            model.DegreeStatus, model.DegreeStatusSourceSystemRecordId, model.DegreeStatusMasterId,
                            model.AwardedTerm, model.AwardedTermMasterId,
                            model.DegreeType, model.DegreeTypeSourceSystemRecordId, model.DegreeTypeMasterId,
                            model.AcademicCareer, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                            User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentDegreeRevalidate details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentDegreeList));
            }
            return View(model);
        }
        #endregion

        /*  *** Degree Match ***
        ******************************************************************/
        #region Student - Degree Match
        // GET: Student/StudentDegreeMatch
        public async Task<IActionResult> StudentDegreeMatch(long Id, int SystemId)
        {
            try
            {
                var details = await _context.StudentDegreeDetails.Where(e => e.Id == Id && e.SystemId == SystemId).SingleAsync();

                var viewModel = new DegreeMatchViewModel()
                {
                    Title = "Student Degree Matching",
                    PageId = "studentDegreePage",
                    ActiveClass = "StudentDegree",
                    Message = "Your Student Degree Page",
                    PageWrapperClass = "toggled",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    Id = details.Id,
                    System = details.SystemName,
                    SystemId = details.SystemId,
                    Integration = details.IntegrationName,
                    IntegrationId = details.IntegrationId,
                    IntegrationDate = details.IntegrationDate,
                    SourceRecordId = details.SourceRecordId,
                    CreatedOnDT = details.RecordDate,

                    StudentName = "Ted",
                    Student = details.Student,
                    DegreeEducationalInstitution = details.EducationalInstitutionMasterId,
                        DegreeEducationalInstitution_BusinessName = details.EducationalInstitutionMasterId_BusinessName,
                        DegreeEducationalInstitution_BusinessDescription = details.EducationalInstitutionMasterId_BusinessDescription,
                        DegreeEducationalInstitution_Weight = 25,
                    // More to come later...

                    PossibleMatches = new List<DegreeMatchViewModel.DegreeMatchSummaryViewModel>()
                };

                foreach (var possibleMatch in _context.GetDegreePossibleMatches(details.SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new DegreeMatchViewModel.DegreeMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,

                        StudentName = "Ted",
                        Student = details.Student,
                        DegreeTerm = possibleMatch.DegreeTerm
                    });
                }

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentDegreeMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Degree Compare ***
        ******************************************************************/
        #region Student - Degree Compare
        // GET: Student/StudentDegreeCompare
        public IActionResult StudentDegreeCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var comparison = _context.GetDegreeComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new DegreeCompareViewModel()
                {
                    Id = Id,
                    System = comparison.System,
                    SystemId = SystemId,
                    MasterId = MasterId,
                    IntegrationId = comparison.IntegrationId,
                    IntegrationDate = comparison.IntegrationDate,
                    SourceRecordId = comparison.SourceRecordId,
                    SourceRecordId_Compare = comparison.SourceRecordId_Compare,

                    StudentName = comparison.StudentName,
                    // More to come later...
                };

                return PartialView("StudentDegreeCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentDegreeCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Degree Manual Match ***
        ******************************************************************/
        #region Student - Degree ManualMatch
        // GET: Student/StudentDegreeManualMatch
        public async Task<IActionResult> StudentDegreeManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentDegreeManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentDegreeList));
        }
        #endregion

        /*  *** Ignore/Remove ***
        ******************************************************************/
        #region Ignore/Remove
        public IActionResult StudentDegreeIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load StudentDegreeIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentDegreeList));
        }
        #endregion

        #endregion

        #region Student - Academic Involvement

        /*  *** Academic Involvement List View ***
        ******************************************************************/
        #region Student - Academic Involvement List
        // GET: Student/StudentAcademicInvolvementList
        public async Task<IActionResult> StudentAcademicInvolvementList()
        {
            try
            {
                var model = new StudentAcademicInvolvementListViewModel()
                {
                    Title = "Student Academic Involvement",
                    PageId = "studentAcademicInvolvementPage",
                    ActiveClass = "StudentAcademicInvolvement",
                    Message = "Your Student Academic Involvement Page",
                    Integration = "Student Academic Involvement",
                    IntegrationId = 9,
                    User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<StudentAcademicInvolvementRemediationListItemViewModel>()
                };

                var list = await _context.StudentAcademicInvolvementRemediationList.ToListAsync();

                foreach (var item in list)
                {
                    model.RemediationList.Add(new StudentAcademicInvolvementRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        IntegrationDate = item.IntegrationDate,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate,
                        RecordStatus = item.RecordStatus,

                        StudentId = item.StudentId,
                        AcademicInvolvementAcademicYear = item.AcademicInvolvementAcademicYear,
                        AcademicInvolvementTerm = item.AcademicInvolvementTerm,
                        AcademicInvolvementType = item.AcademicInvolvementType,
                        AcademicInvolvementName = item.AcademicInvolvementName
                    });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting StudentAcademicInvolvementList");

                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Degree Edit View ***
        ******************************************************************/
        #region Student - Academic Involvement Edit
        // GET: Student/StudentAcademicInvolvementEdit
        public IActionResult StudentAcademicInvolvementEdit(long Id, int SystemId)
        {
            try
            {
                var detail = _context.GetStudentAcademicInvolvementDetails(SystemId, Id);
                var history = _context.StudentAcademicInvolvementHistories.Where(e => e.RecordId == Id && e.SystemId == SystemId).OrderByDescending(o => o.RecordDate);
                var source = history.Last();

                var viewModel = new StudentAcademicInvolvementViewModel()
                {
                    Title = "Student Academic Involvement",
                    PageId = "studentAcademicInvolvementPage",
                    ActiveClass = "StudentAcademicInvolvement",
                    Message = "Your Student AcademicInvolvement Page",
                    User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = detail.Id,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    CreatedOnDT = detail.RecordDate,
                    SourceRecordId = detail.SourceRecordId,                    
                    RecordStatus = detail.RecordStatus,

                    StudentName = "Owen Benjamin Smith",
                        StudentName_BusinessName = "Student Name",
                        StudentName_BusinessDescription = "StudentName Business Description",
                        StudentName_AttributeId = 0001,
                        StudentName_OriginalValue = "Owen Benjamin Smith",
                        StudentName_Status = "Good",
                        StudentName_Source = "UAIR.StudentName",
                    StudentId = detail.StudentId,
                        StudentId_BusinessName = detail.StudentId_BusinessName,
                        StudentId_BusinessDescription = detail.StudentId_BusinessDescription,
                        StudentId_AttributeId = detail.StudentId_AttributeId,
                        StudentId_OriginalValue = source.StudentId,
                        StudentId_Status = detail.StudentId_Status,
                        StudentId_Source = detail.StudentId_Source,
                    StudentMasterId = detail.StudentMasterId,
                        StudentMasterId_BusinessName = detail.StudentMasterId_BusinessName,
                        StudentMasterId_BusinessDescription = detail.StudentMasterId_BusinessDescription,
                        StudentMasterId_AttributeId = detail.StudentMasterId_AttributeId,
                        StudentMasterId_OriginalValue = source.StudentMasterId,
                        StudentMasterId_Status = detail.StudentMasterId_Status,
                        StudentMasterId_Source = detail.StudentMasterId_Source,
                    AcademicInvolvementAcademicYear = detail.AcademicYear,
                        AcademicInvolvementAcademicYear_BusinessName = detail.AcademicYear_BusinessName,
                        AcademicInvolvementAcademicYear_BusinessDescription = detail.AcademicYear_BusinessDescription,
                        AcademicInvolvementAcademicYear_AttributeId = detail.AcademicYear_AttributeId,
                        AcademicInvolvementAcademicYear_OriginalValue = source.AcademicYear,
                        AcademicInvolvementAcademicYear_Status = detail.AcademicYear_Status,
                        AcademicInvolvementAcademicYear_Source = detail.AcademicYear_Source,
                    AcademicInvolvementTerm = detail.TermCode,
                        AcademicInvolvementTerm_BusinessName = detail.Term_BusinessName,
                        AcademicInvolvementTerm_BusinessDescription = detail.Term_BusinessDescription,
                        AcademicInvolvementTerm_AttributeId = detail.Term_AttributeId,
                        AcademicInvolvementTerm_OriginalValue = source.Term,
                        AcademicInvolvementTerm_Status = detail.Term_Status,
                        AcademicInvolvementTerm_Source = detail.Term_Source,
                        AcademicInvolvementTermName = detail.Term,
                        AcademicInvolvementTermCode = detail.TermCode,
                    AcademicInvolvementType = detail.AcademicInvolvementType,
                        AcademicInvolvementType_BusinessName = detail.AcademicInvolvementType_BusinessName,
                        AcademicInvolvementType_BusinessDescription = detail.AcademicInvolvementType_BusinessDescription,
                        AcademicInvolvementType_AttributeId = detail.AcademicInvolvementType_AttributeId,
                        AcademicInvolvementType_OriginalValue = source.AcademicInvolvementType,
                        AcademicInvolvementType_Status = detail.AcademicInvolvementType_Status,
                        AcademicInvolvementType_Source = detail.AcademicInvolvementType_Source,
                    AcademicInvolvementTypeMasterId = detail.AcademicInvolvementTypeMasterId,
                        AcademicInvolvementTypeMasterId_BusinessName = detail.AcademicInvolvementTypeMasterId_BusinessName,
                        AcademicInvolvementTypeMasterId_BusinessDescription = detail.AcademicInvolvementTypeMasterId_BusinessDescription,
                        AcademicInvolvementTypeMasterId_AttributeId = detail.AcademicInvolvementTypeMasterId_AttributeId,
                        AcademicInvolvementTypeMasterId_OriginalValue = source.AcademicInvolvementTypeMasterId,
                        AcademicInvolvementTypeMasterId_Status = detail.AcademicInvolvementTypeMasterId_Status,
                        AcademicInvolvementTypeMasterId_Source = detail.AcademicInvolvementTypeMasterId_Source,
                    AcademicInvolvementName = detail.AcademicInvolvementName,
                        AcademicInvolvementName_BusinessName = detail.AcademicInvolvementName_BusinessName,
                        AcademicInvolvementName_BusinessDescription = detail.AcademicInvolvementName_BusinessDescription,
                        AcademicInvolvementName_AttributeId = detail.AcademicInvolvementName_AttributeId,
                        AcademicInvolvementName_OriginalValue = source.AcademicInvolvementName,
                        AcademicInvolvementName_Status = detail.AcademicInvolvementName_Status,
                        AcademicInvolvementName_Source = detail.AcademicInvolvementName_Source,
                        AcademicInvolvementNameName = detail.AcademicInvolvementName,
                    AcademicInvolvementNameMasterId = detail.AcademicInvolvementNameMasterId,
                        AcademicInvolvementNameMasterId_BusinessName = detail.AcademicInvolvementNameMasterId_BusinessName,
                        AcademicInvolvementNameMasterId_BusinessDescription = detail.AcademicInvolvementNameMasterId_BusinessDescription,
                        AcademicInvolvementNameMasterId_AttributeId = detail.AcademicInvolvementNameMasterId_AttributeId,
                        AcademicInvolvementNameMasterId_OriginalValue = source.AcademicInvolvementNameMasterId,
                        AcademicInvolvementNameMasterId_Status = detail.AcademicInvolvementNameMasterId_Status,
                        AcademicInvolvementNameMasterId_Source = detail.AcademicInvolvementNameMasterId_Source,

                    HistoryData = new List<StudentAcademicInvolvementHistoryViewModel>(),

                    #region DropDowns
                    StudentAcademicInvolvementTermList = GetStudentAcademicInvolvementTermList().ToList(),
                    StudentAcademicInvolvementTypeList = GetStudentAcademicInvolvementTypeList().ToList(),
                    StudentAcademicInvolvementNameList = GetStudentAcademicInvolvementNameList().ToList()
                    #endregion
                };

                foreach(var item in history)
                {
                    viewModel.HistoryData.Add(new StudentAcademicInvolvementHistoryViewModel()
                    {
                        StudentName = "Owen Benjamin Smith",
                            StudentName_OriginalValue = "Owen Benjamin Smith",
                            StudentName_Status = "Good",
                        StudentId = item.StudentId,
                            StudentId_OriginalValue = source.StudentId,
                            StudentId_Status = item.StudentId_Status,
                        StudentMasterId = item.StudentMasterId,
                            StudentMasterId_OriginalValue = source.StudentMasterId,
                            StudentMasterId_Status = item.StudentMasterId_Status,
                        AcademicInvolvementAcademicYear = item.AcademicYear,
                            AcademicInvolvementAcademicYear_OriginalValue = source.AcademicYear,
                            AcademicInvolvementAcademicYear_Status = item.AcademicYear_Status,
                        AcademicInvolvementTerm = item.Term,
                            AcademicInvolvementTerm_OriginalValue = source.Term,
                            AcademicInvolvementTerm_Status = item.Term_Status,
                        AcademicInvolvementType = item.AcademicInvolvementType,
                            AcademicInvolvementType_OriginalValue = source.AcademicInvolvementType,
                            AcademicInvolvementType_Status = item.AcademicInvolvementType_Status,
                        AcademicInvolvementName = item.AcademicInvolvementName,
                            AcademicInvolvementName_OriginalValue = source.AcademicInvolvementName,
                            AcademicInvolvementName_Status = item.AcademicInvolvementName_Status,

                        HistoryDate = item.RecordDate
                    });
                }                

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Academic Involvement Save ***
        ******************************************************************/
        #region Student - Academic Involvement Save
        // POST: Student/StudentAcademicInvolvementSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentAcademicInvolvementSave(StudentAcademicInvolvementViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    // Ted tried to make this work (even tried creating a new stored procedure), no luck... Commenting out for now.
                    //_context.ChangeStudentAcademicInvolvementIntegrationRecord(model.SystemId, model.Id, model.StudentId, model.AcademicInvolvementAcademicYear, model.AcademicInvolvementTerm, model.AcademicInvolvementType, model.AcademicInvolvementTypeMasterId, model.AcademicInvolvementName, model.AcademicInvolvementNameMasterId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementSave details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentAcademicInvolvementList));
            }
            return View(model);
        }
        #endregion

        /*  *** Academic Involvement Revalidate ***
        ******************************************************************/
        #region Student - Academic Involvement Revalidate
        // POST: Student/StudentAcademicInvolvementRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentAcademicInvolvementRevalidate(StudentAcademicInvolvementViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    if (model.IsChanged)
                    {
                        //_context.ChangeStudentIntegrationRecord(model.SystemId, model.Id, model.FirstName, model.MiddleName, model.LastName, model.StudentId, model.CommonTitle, model.ProfessionalTitle, model.Suffix, model.BirthDate, model.DeceasedDate, model.MaritalStatus, model.HighestDegreeAwarded, model.AcademicLevel, model.EnrollmentStatus, model.LastEnrollmentTerm, model.InformationReleaseStatus);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementRevalidate details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentAcademicInvolvementList));
            }
            return View(model);
        }
        #endregion

        /*  *** Academic Involvement Match ***
        ******************************************************************/
        #region Student - Academic Involvement Match
        // GET: Student/StudentAcademicInvolvementMatch
        public IActionResult StudentAcademicInvolvementMatch(long Id, int SystemId)
        {
            try
            {
                //var studentDetail = await _context.StudentDetails.Where(e => e.Id == Id && e.SystemId == SystemId).SingleAsync();

                var viewModel = new StudentAcademicInvolvementMatchViewModel()
                {
                    Title = "Student Academic Involvement Matching",
                    PageId = "studentAcademicInvolvementPage",
                    ActiveClass = "StudentAcademicInvolvement",
                    Message = "Your Student Academic Involvement Page",
                    //PageWrapperClass = "toggled",
                    User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),

                    Id = -798461354651321684,
                    System = "UAIR",
                    SystemId = 5,
                    Integration = "Academic Involvement",
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 11, 06),
                    SourceRecordId = "kj234h2guihg34hg234",
                    CreatedOnDT = new DateTime(2019, 11, 06),

                    StudentName = "Owen Benjamin Smith",
                    StudentId = "4984651354351",
                    AcademicInvolvementAcademicYear = "1958",
                        AcademicInvolvementAcademicYear_BusinessName = "Academic Year",
                        AcademicInvolvementAcademicYear_BusinessDescription = "AcademicInvolvementAcademicYear Business Description",
                        AcademicInvolvementAcademicYear_Weight = 25,
                    AcademicInvolvementTerm = "Spring",
                        AcademicInvolvementTerm_BusinessName = "Term",
                        AcademicInvolvementTerm_BusinessDescription = "AcademicInvolvementTerm Business Description",
                        AcademicInvolvementTerm_Weight = 25,
                    AcademicInvolvementType = "Greek Life",
                        AcademicInvolvementType_BusinessName = "Academic Involvement Type",
                        AcademicInvolvementType_BusinessDescription = "AcademicInvolvementType Business Description",
                        AcademicInvolvementType_Weight = 25,
                    AcademicInvolvementName = "Kappa Alpha Psi",
                        AcademicInvolvementName_BusinessName = "Academic Involvement Name",
                        AcademicInvolvementName_BusinessDescription = "AcademicInvolvementName Business Description",
                        AcademicInvolvementName_Weight = 25,

                    PossibleMatches = new List<StudentAcademicInvolvementMatchViewModel.StudentAcademicInvolvementMatchSummaryViewModel>()
                };

                viewModel.PossibleMatches.Add(new StudentAcademicInvolvementMatchViewModel.StudentAcademicInvolvementMatchSummaryViewModel()
                {
                    MatchConfidence = 75,
                    MasterId = "4864313546846",

                    StudentName = "Tom Cotton",
                    StudentId = "4984651354351",
                    AcademicInvolvementAcademicYear = "2172",
                    AcademicInvolvementTerm = "Spring",
                    AcademicInvolvementType = "Greek Life",
                    AcademicInvolvementName = "Kappa Alpha Psi"
                });

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Academic Involvement Compare ***
        ******************************************************************/
        #region Student - Academic Involvement Compare
        // GET: Student/StudentAcademicInvolvementCompare
        public IActionResult StudentAcademicInvolvementCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                //var comparison = _context.GetStudentComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new StudentAcademicInvolvementCompareViewModel()
                {
                    Id = -798461354651321684,
                    System = "UAIR",
                    SystemId = 5,
                    MasterId = MasterId,
                    IntegrationId = 9,
                    IntegrationDate = new DateTime(2019, 11, 06),
                    SourceRecordId = "kj234h2guihg34hg234",
                    SourceRecordId_Compare = "kj234h2guihg34hg234",

                    StudentName = "Owen Benjamin Smith",
                    StudentId = "4984651354351",
                    AcademicInvolvementAcademicYear = "1958",
                        AcademicInvolvementAcademicYear_BusinessName = "Academic Year",
                        AcademicInvolvementAcademicYear_BusinessDescription = "AcademicInvolvementAcademicYear Business Description",
                        AcademicInvolvementAcademicYear_Compare = "1958",
                    AcademicInvolvementTerm = "Spring",
                        AcademicInvolvementTerm_BusinessName = "Term",
                        AcademicInvolvementTerm_BusinessDescription = "AcademicInvolvementTerm Business Description",
                        AcademicInvolvementTerm_Compare = "Spring",
                    AcademicInvolvementType = "Greek Life",
                        AcademicInvolvementType_BusinessName = "Academic Involvement Type",
                        AcademicInvolvementType_BusinessDescription = "AcademicInvolvementType Business Description",
                        AcademicInvolvementType_Compare = "Greek Life",
                    AcademicInvolvementName = "Kappa Alpha Psi",
                        AcademicInvolvementName_BusinessName = "Academic Involvement Name",
                        AcademicInvolvementName_BusinessDescription = "AcademicInvolvementName Business Description",
                        AcademicInvolvementName_Compare = "Kappa Alpha Psi",
                };

                return PartialView("StudentAcademicInvolvementCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Academic Involvement Manual Match ***
        ******************************************************************/
        #region Student - Academic Involvement ManualMatch
        // GET: Student/StudentAcademicInvolvementManualMatch
        public async Task<IActionResult> StudentAcademicInvolvementManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicInvolvementManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentAcademicInvolvementList));
        }
        #endregion

        #endregion

        #region Student - Scholarship

        /*  *** Scholarship List View ***
        ******************************************************************/
        #region Student - Scholarship List
        // GET: Student/StudentScholarshipList
        public async Task<IActionResult> StudentScholarshipList()
        {
            try
            {
                var model = new StudentScholarshipListViewModel()
                {
                    Title = "Student Scholarship",
                    PageId = "studentScholarshipPage",
                    ActiveClass = "StudentScholarship",
                    Message = "Your Student Scholarship Page",
                    Integration = "Student Scholarship",
                    IntegrationId = 9,
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),
                    RemediationList = new List<StudentScholarshipRemediationListItemViewModel>()
                };

                var list = await _context.StudentScholarshipRemediationList.ToListAsync();

                foreach(var item in list)
                {
                    model.RemediationList.Add(new StudentScholarshipRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,
                        IntegrationDate = item.IntegrationDate,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate,
                        RecordStatus = item.RecordStatus,

                        StudentId = item.StudentId,
                        ScholarshipAmount = item.ScholarshipAmount
                    });
                }

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting StudentScholarshipList");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Scholarship Edit View ***
        ******************************************************************/
        #region Student - Scholarship Edit
        // GET: Student/StudentScholarshipEdit
        public IActionResult StudentScholarshipEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentScholarshipHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var scholarshipDetail = history.First();
                var scholarshipSource = history.Last();

                var viewModel = new StudentScholarshipViewModel()
                {
                    Title = "Student Scholarship",
                    PageId = "studentScholarshipPage",
                    ActiveClass = "StudentScholarship",
                    Message = "Your Student Scholarship Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = scholarshipDetail.Id,
                    System = scholarshipDetail.SystemName,
                    SystemId = scholarshipDetail.SystemId,
                    Integration = scholarshipDetail.IntegrationName,
                    IntegrationId = scholarshipDetail.IntegrationId,
                    IntegrationDate = scholarshipDetail.IntegrationDate,
                    CreatedDate = scholarshipDetail.RecordDate,
                    SourceRecordId = scholarshipDetail.SourceRecordId,
                    CreatedOnDT = scholarshipDetail.RecordDate,
                    RecordStatus = scholarshipDetail.RecordStatus,
                    HistoryData = new List<StudentScholarshipHistoryViewModel>(),
                    

                    StudentId = scholarshipDetail.StudentId,
                        StudentId_BusinessName = scholarshipDetail.StudentId_Name,
                        StudentId_BusinessDescription = scholarshipDetail.StudentId_BusinessDescription,
                        StudentId_OriginalValue = scholarshipSource.StudentId,
                        StudentId_Status = scholarshipDetail.StudentId_Status,
                        StudentId_Source = scholarshipDetail.StudentId_Source,
                        //StudentId_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.StudentId_Source }),
                    StudentMasterId = scholarshipDetail.StudentMasterId,
                        StudentMasterId_BusinessName = scholarshipDetail.StudentMasterId_BusinessName,
                        StudentMasterId_BusinessDescription = scholarshipDetail.StudentMasterId_BusinessDescription,
                        StudentMasterId_OriginalValue = scholarshipSource.StudentMasterId,
                        StudentMasterId_Status = scholarshipDetail.StudentMasterId_Status,
                        StudentMasterId_Source = scholarshipDetail.StudentMasterId_Source,
                        StudentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.StudentMasterId_Source, scholarshipDetail.StudentId_Source }),
                    AcademicCareerCode = scholarshipDetail.AcademicCareerCode,
                        AcademicCareerCode_BusinessName = scholarshipDetail.AcademicCareerCode_Name,
                        AcademicCareerCode_BusinessDescription = scholarshipDetail.AcademicCareerCode_BusinessDescription,
                        AcademicCareerCode_OriginalValue = scholarshipSource.AcademicCareerCode,
                        AcademicCareerCode_Status = scholarshipDetail.AcademicCareerCode_Status,
                        AcademicCareerCode_Source = scholarshipDetail.AcademicCareerCode_Source,
                    //TermCode = scholarshipDetail.TermCode,
                    //    TermCode_BusinessName = scholarshipDetail.TermCode_BusinessName,
                    //    TermCode_BusinessDescription = scholarshipDetail.TermCode_BusinessDescription,
                    //    TermCode_OriginalValue = scholarshipSource.TermCode,
                    //    TermCode_Status = scholarshipDetail.TermCode_Status,
                    //    TermCode_Source = scholarshipDetail.TermCode_Source,
                    //TermMasterId = scholarshipDetail.TermMasterId,
                    //    TermMasterId_BusinessName = scholarshipDetail.TermMasterId_BusinessName,
                    //    TermMasterId_BusinessDescription = scholarshipDetail.TermMasterId_BusinessDescription,
                    //    TermMasterId_OriginalValue = scholarshipSource.TermMasterId,
                    //    TermMasterId_Status = scholarshipDetail.TermMasterId_Status,
                    //    TermMasterId_Source = scholarshipDetail.TermMasterId_Source,
                    //    TermMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.TermMasterId_Source, scholarshipDetail.AcademicCareerCode_Source }),
                    ScholarshipKFSAccount = scholarshipDetail.KFSAccount,
                        ScholarshipKFSAccount_BusinessName = scholarshipDetail.KFSAccount_BusinessName,
                        ScholarshipKFSAccount_BusinessDescription = scholarshipDetail.KFSAccount_BusinessDescription,
                        ScholarshipKFSAccount_OriginalValue = scholarshipSource.KFSAccount,
                        ScholarshipKFSAccount_Status = scholarshipDetail.KFSAccount_Status,
                        ScholarshipKFSAccount_Source = scholarshipDetail.KFSAccount_Source,
                    ScholarshipDesignationMasterId = scholarshipDetail.DesignationMasterId,
                        ScholarshipDesignationMasterId_BusinessName = scholarshipDetail.DesignationMasterId_BusinessName,
                        ScholarshipDesignationMasterId_BusinessDescription = scholarshipDetail.DesignationMasterId_BusinessDescription,
                        ScholarshipDesignationMasterId_OriginalValue = scholarshipSource.DesignationMasterId,
                        ScholarshipDesignationMasterId_Status = scholarshipDetail.DesignationMasterId_Status,
                        ScholarshipDesignationMasterId_Source = scholarshipDetail.DesignationMasterId_Source,
                    ScholarshipDepartmentCode = scholarshipDetail.DepartmentCode,
                        ScholarshipDepartmentCode_BusinessName = scholarshipDetail.DepartmentCode_BusinessName,
                        ScholarshipDepartmentCode_BusinessDescription = scholarshipDetail.DepartmentCode_BusinessDescription,
                        ScholarshipDepartmentCode_OriginalValue = scholarshipSource.DepartmentCode,
                        ScholarshipDepartmentCode_Status = scholarshipDetail.DepartmentCode_Status,
                        ScholarshipDepartmentCode_Source = scholarshipDetail.DepartmentCode_Source,
                    ScholarshipDepartmentMasterId = scholarshipDetail.DepartmentMasterId,
                        ScholarshipDepartmentMasterId_BusinessName = scholarshipDetail.DepartmentMasterId_BusinessName,
                        ScholarshipDepartmentMasterId_BusinessDescription = scholarshipDetail.DepartmentMasterId_BusinessDescription,
                        ScholarshipDepartmentMasterId_OriginalValue = scholarshipSource.DepartmentMasterId,
                        ScholarshipDepartmentMasterId_Status = scholarshipDetail.DepartmentMasterId_Status,
                        ScholarshipDepartmentMasterId_Source = scholarshipDetail.DepartmentMasterId_Source,
                        ScholarshipDepartmentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.DepartmentMasterId_Source, scholarshipDetail.DepartmentCode_Source, scholarshipDetail.DesignationMasterId_Source, scholarshipDetail.KFSAccount_Source }),
                    ScholarshipCode = scholarshipDetail.ScholarshipCode,
                        ScholarshipCode_BusinessName = scholarshipDetail.ScholarshipCode_BusinessName,
                        ScholarshipCode_BusinessDescription = scholarshipDetail.ScholarshipCode_BusinessDescription,
                        ScholarshipCode_OriginalValue = scholarshipSource.ScholarshipCode,
                        ScholarshipCode_Status = scholarshipDetail.ScholarshipCode_Status,
                        ScholarshipCode_Source = scholarshipDetail.ScholarshipCode_Source,
                    ScholarshipName = scholarshipDetail.ScholarshipName,
                        ScholarshipName_BusinessName = scholarshipDetail.ScholarshipName_BusinessName,
                        ScholarshipName_BusinessDescription = scholarshipDetail.ScholarshipName_BusinessDescription,
                        ScholarshipName_OriginalValue = scholarshipSource.ScholarshipName,
                        ScholarshipName_Status = scholarshipDetail.ScholarshipName_Status,
                        ScholarshipName_Source = scholarshipDetail.ScholarshipName_Source,
                    ScholarshipMasterId = scholarshipDetail.ScholarshipMasterId,
                        ScholarshipMasterId_BusinessName = scholarshipDetail.ScholarshipMasterId_BusinessName,
                        ScholarshipMasterId_BusinessDescription = scholarshipDetail.ScholarshipMasterId_BusinessDescription,
                        ScholarshipMasterId_OriginalValue = scholarshipSource.ScholarshipMasterId,
                        ScholarshipMasterId_Status = scholarshipDetail.ScholarshipMasterId_Status,
                        ScholarshipMasterId_Source = scholarshipDetail.ScholarshipMasterId_Source,
                        ScholarshipMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.ScholarshipMasterId_Source, scholarshipDetail.ScholarshipName_Source, scholarshipDetail.ScholarshipCode_Source }),
                    ScholarshipAmount = scholarshipDetail.Amount,
                        ScholarshipAmount_BusinessName = scholarshipDetail.Amount_Name,
                        ScholarshipAmount_BusinessDescription = scholarshipDetail.Amount_BusinessDescription,
                        ScholarshipAmount_OriginalValue = scholarshipSource.Amount,
                        ScholarshipAmount_Status = scholarshipDetail.Amount_Status,
                        ScholarshipAmount_Source = scholarshipDetail.Amount_Source,
                        ScholarshipAmount_IsReadOnly = AttributeIsReadOnly(new string[] { scholarshipDetail.Amount_Source }),

                    //Students = GetStudentList(),
                    Students = new List<SelectListItem>(),
                    //AcademicTerms = GetAcademicTermList("Term"),
                    Designations = GetDesignationList(),
                    Scholarships = GetScholarshipList(),
                    Departments = GetUADepartmentList()
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new StudentScholarshipHistoryViewModel()
                    {
                        StudentId = item.StudentId,
                            StudentId_Status = item.StudentId_Status,
                            //StudentId_OriginalValue = previousitem == null ? null : previousitem.StudentId,
                            StudentId_OriginalValue = previousitem.StudentId,
                        StudentMasterId = item.StudentMasterId,
                            StudentMasterId_Status = item.StudentMasterId_Status,
                            //StudentMasterId_OriginalValue = previousitem == null ? null : previousitem.StudentMasterId,
                            StudentMasterId_OriginalValue = previousitem.StudentMasterId,
                        ScholarshipAcademicYear = item.AcademicCareerCode,
                            ScholarshipAcademicYear_Status = item.AcademicCareerCode_Status,
                            //ScholarshipAcademicYear_OriginalValue = previousitem == null ? null : previousitem.AcademicCareerCode,
                            ScholarshipAcademicYear_OriginalValue = previousitem.AcademicCareerCode,
                        //TermCode = item.TermCode,
                        //    TermCode_Status = item.TermCode_Status,
                        //    //TermCode_OriginalValue = previousitem == null ? null : previousitem.TermCode,
                        //    TermCode_OriginalValue = previousitem.TermCode,
                        //TermMasterId = item.TermMasterId,
                        //    TermMasterId_Status = item.TermMasterId_Status,
                        //    //TermMasterId_OriginalValue = previousitem == null ? null : previousitem.TermMasterId,
                        //    TermMasterId_OriginalValue = previousitem.TermMasterId,
                        ScholarshipKFSAccount = item.KFSAccount,
                            ScholarshipKFSAccount_Status = item.KFSAccount_Status,
                            //ScholarshipKFSAccount_OriginalValue = previousitem == null ? null : previousitem.KFSAccount,
                            ScholarshipKFSAccount_OriginalValue = previousitem.KFSAccount,
                        ScholarshipDesignationMasterId = item.DesignationMasterId,
                            ScholarshipDesignationMasterId_Status = item.DesignationMasterId_Status,
                            //ScholarshipDesignationMasterId_OriginalValue = previousitem == null ? null : previousitem.DesignationMasterId,
                            ScholarshipDesignationMasterId_OriginalValue = previousitem.DesignationMasterId,
                        ScholarshipDepartmentCode = item.DepartmentCode,
                            ScholarshipDepartmentCode_Status = item.DepartmentCode_Status,
                            //ScholarshipDepartmentCode_OriginalValue = previousitem == null ? null : previousitem.DepartmentCode,
                            ScholarshipDepartmentCode_OriginalValue = previousitem.DepartmentCode,
                        ScholarshipDepartmentMasterId = item.DepartmentMasterId,
                            ScholarshipDepartmentMasterId_Status = item.DepartmentMasterId_Status,
                            //ScholarshipDepartmentMasterId_OriginalValue = previousitem == null ? null : previousitem.DepartmentMasterId,
                            ScholarshipDepartmentMasterId_OriginalValue = previousitem.DepartmentMasterId,
                        ScholarshipCode = item.ScholarshipCode,
                            ScholarshipCode_Status = item.ScholarshipCode_Status,
                            //ScholarshipCode_OriginalValue = previousitem == null ? null : previousitem.ScholarshipCode,
                            ScholarshipCode_OriginalValue = previousitem.ScholarshipCode,
                        ScholarshipName = item.ScholarshipName,
                            ScholarshipName_Status = item.ScholarshipName_Status,
                            //ScholarshipName_OriginalValue = previousitem == null ? null : previousitem.ScholarshipName,
                            ScholarshipName_OriginalValue = previousitem.ScholarshipName,
                        ScholarshipMasterId = item.ScholarshipMasterId,
                            ScholarshipMasterId_Status = item.ScholarshipMasterId_Status,
                            //ScholarshipMasterId_OriginalValue = previousitem == null ? null : previousitem.ScholarshipMasterId,
                            ScholarshipMasterId_OriginalValue = previousitem.ScholarshipMasterId,
                        ScholarshipAmount = item.Amount,
                            ScholarshipAmount_Status = item.Amount_Status,
                            //ScholarshipAmount_OriginalValue = previousitem == null ? null : previousitem.Amount,
                            ScholarshipAmount_OriginalValue = previousitem.Amount,
                        HistoryDate = item.RecordDate
                    });
                }
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentScholarshipEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Scholarship Save ***
        ******************************************************************/
        #region Student - Scholarship Save
        // POST: Student/StudentDegreeSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentScholarshipSave(StudentScholarshipViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentScholarshipIntegrationRecord(model.SystemId, model.Id, model.StudentId, model.StudentMasterId, model.TermCode, model.TermMasterId, model.ScholarshipKFSAccount, model.ScholarshipDesignationMasterId,
                        model.ScholarshipCode, model.ScholarshipName, model.ScholarshipMasterId, model.ScholarshipAmount, model.ScholarshipDepartmentCode, model.ScholarshipDepartmentMasterId);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentScholarshipSave details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentScholarshipList));
            }
            return View(model);
        }
        #endregion

        /*  *** Scholarship Revalidate ***
        ******************************************************************/
        #region Student - Scholarship Revalidate
        // POST: Student/StudentDegreeRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentScholarshipRevalidate(StudentScholarshipViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    if (model.IsChanged)
                    {
                        _context.ChangeStudentScholarshipIntegrationRecord(model.SystemId, model.Id, model.StudentId, model.StudentMasterId, model.TermCode, model.TermMasterId, model.ScholarshipKFSAccount, model.ScholarshipDesignationMasterId, 
                            model.ScholarshipCode, model.ScholarshipName, model.ScholarshipMasterId, model.ScholarshipAmount, model.ScholarshipDepartmentCode, model.ScholarshipDepartmentMasterId);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentScholarshipRevalidate details");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentScholarshipList));
            }
            return View(model);
        }
        #endregion

        /*  *** Scholarship Match ***
        ******************************************************************/
        #region Student - Scholarship Match
        // GET: Student/StudentScholarshipMatch
        public IActionResult StudentScholarshipMatch(long Id, int SystemId)
        {
            try
            {
                var detail = _context.GetStudentScholarshipMatchDetails(SystemId, Id);

                var viewModel = new StudentScholarshipMatchViewModel()
                {
                    Title = "Student Scholarship Matching",
                    PageId = "studentScholarshipPage",
                    ActiveClass = "StudentScholarship",
                    Message = "Your Student Scholarship Page",
                    //PageWrapperClass = "toggled",
                    User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),

                    Id = detail.Id,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    SourceRecordId = detail.SourceRecordId,
                    CreatedOnDT = detail.RecordDate,


                    Student = detail.Student,
                        Student_BusinessName = detail.Student_BusinessName,
                        Student_BusinessDescription = detail.AcademicYear_BusinessDescription,
                        Student_Weight = detail.Student_MatchWeight,
                    ScholarshipAcademicYear = detail.AcademicYear,
                        ScholarshipAcademicYear_BusinessName = detail.AcademicYear_BusinessName,
                        ScholarshipAcademicYear_BusinessDescription = detail.AcademicYear_BusinessDescription,
                        ScholarshipAcademicYear_Weight = detail.AcademicYear_MatchWeight,
                    ScholarshipTerm = detail.ScholarshipTerm,
                        ScholarshipTerm_BusinessName = detail.ScholarshipTerm_BusinessName,
                        ScholarshipTerm_BusinessDescription = detail.ScholarshipTerm_BusinessDescription,
                        ScholarshipTerm_Weight = detail.ScholarshipTerm_MatchWeight,
                    ScholarshipDesignation = detail.Designation,
                        ScholarshipDesignation_BusinessName = detail.Designation_BusinessName,
                        ScholarshipDesignation_BusinessDescription = detail.Designation_BusinessDescription,
                        ScholarshipDesignation_Weight = detail.Designation_MatchWeight,
                    Scholarship = detail.Scholarship,
                        Scholarship_BusinessName = detail.Scholarship_BusinessName,
                        Scholarship_BusinessDescription = detail.Scholarship_BusinessDescription,
                        Scholarship_Weight = detail.Scholarship_MatchWeight,
                    ScholarshipAmount = detail.Amount,
                        ScholarshipAmount_BusinessName = detail.Amount_BusinessName,
                        ScholarshipAmount_BusinessDescription = detail.Amount_BusinessDescription,
                        ScholarshipAmount_Weight = detail.Amount_MatchWeight,
                    ScholarshipDepartment = detail.Department,
                        ScholarshipDepartment_BusinessName = detail.Department_BusinessName,
                        ScholarshipDepartment_BusinessDescription = detail.Department_BusinessDescription,
                        ScholarshipDepartment_Weight = detail.Department_MatchWeight,

                    PossibleMatches = new List<StudentScholarshipMatchViewModel.StudentScholarshipMatchSummaryViewModel>()
                };

                foreach (var possibleMatch in _context.GetStudentScholarshipPossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new StudentScholarshipMatchViewModel.StudentScholarshipMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,

                        Student = possibleMatch.Student,
                        ScholarshipAcademicYear = possibleMatch.AcademicYear,
                        ScholarshipTerm = possibleMatch.ScholarshipTerm,
                        ScholarshipDesignation = possibleMatch.Designation,
                        ScholarshipAmount = possibleMatch.Amount,
                        ScholarshipDepartment = possibleMatch.Department,
                        Scholarship = possibleMatch.Scholarship
                    });
                }                

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentScholarshipMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Scholarship Compare ***
        ******************************************************************/
        #region Student - Scholarship Compare
        // GET: Student/StudentScholarshipCompare
        public IActionResult StudentScholarshipCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetStudentScholarshipMatchDetails(SystemId, Id);
                var comparison = _context.GetStudentScholarshipComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new StudentScholarshipCompareViewModel()
                {
                    Id = detail.Id,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    MasterId = MasterId,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    SourceRecordId = detail.SourceRecordId,

                    Student = detail.Student,
                        Student_BusinessName = detail.Student_BusinessName,
                        Student_BusinessDescription = detail.Student_BusinessDescription,
                        Student_Compare = comparison.Student_Compare,
                    ScholarshipAcademicYear = detail.AcademicYear,
                        ScholarshipAcademicYear_BusinessName = detail.AcademicYear_BusinessName,
                        ScholarshipAcademicYear_BusinessDescription = detail.AcademicYear_BusinessDescription,
                        ScholarshipAcademicYear_Compare = comparison.AcademicYear_Compare,
                    ScholarshipTerm = detail.ScholarshipTerm,
                        ScholarshipTerm_BusinessName = detail.ScholarshipTerm_BusinessName,
                        ScholarshipTerm_BusinessDescription = detail.ScholarshipTerm_BusinessDescription,
                        ScholarshipTerm_Compare = comparison.ScholarshipTerm_Compare,
                    ScholarshipDesignation = detail.Designation,
                        ScholarshipDesignation_BusinessName = detail.Designation_BusinessName,
                        ScholarshipDesignation_BusinessDescription = detail.Designation_BusinessDescription,
                        ScholarshipDesignation_Compare = comparison.Designation_Compare,
                    ScholarshipAmount = detail.Amount,
                        ScholarshipAmount_BusinessName = detail.Amount_BusinessName,
                        ScholarshipAmount_BusinessDescription = detail.Amount_BusinessDescription,
                        ScholarshipAmount_Compare = comparison.Amount_Compare,
                    ScholarshipDepartment = detail.Department,
                        ScholarshipDepartment_BusinessName = detail.Department_BusinessName,
                        ScholarshipDepartment_BusinessDescription = detail.Department_BusinessDescription,
                        ScholarshipDepartment_Compare = comparison.Department_Compare,
                    Scholarship = detail.Scholarship,
                        Scholarship_BusinessName = detail.Scholarship_BusinessName,
                        Scholarship_BusinessDescription = detail.Scholarship_BusinessDescription,
                        Scholarship_Compare = comparison.Scholarship_Compare,
                };

                return PartialView("StudentScholarshipCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentScholarshipCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Scholarship Manual Match ***
        ******************************************************************/
        #region Student - Scholarship ManualMatch
        // GET: Student/StudentScholarshipManualMatch
        public async Task<IActionResult> StudentScholarshipManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentScholarshipManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentScholarshipList));
        }
        #endregion

        #endregion

        #region Student - Academic Plan

        /*  *** Academic Plan List View ***
        ******************************************************************/
        #region Academic Plan List
        // GET: Student/StudentAcademicPlanList
        public IActionResult StudentAcademicPlanList()
        {
            var model = new StudentAcademicPlanListViewModel()
            {
                Title = "Student Academic Plan",
                PageId = "studentAcademicPlanPage",
                ActiveClass = "StudentAcademicPlan",
                Message = "Your Student Academic Plan Page",
                Integration = "Academic Plan",
                IntegrationId = 15,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<StudentAcademicPlanRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentAcademicPlanList(AjaxDataTableRequest request)
        {
            try
            {
                var studentAcademicPlans = _context.StudentAcademicPlanRemediationList.AsQueryable();

                int recordsTotal = studentAcademicPlans.Count();

                var studentAcademicPlanList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? studentAcademicPlans
                                                : studentAcademicPlans.Where(s => s.Student.Contains(request.searchValue) ||
                                                                                    s.Term.Contains(request.searchValue) ||
                                                                                    s.AcademicCareer.Contains(request.searchValue) ||
                                                                                    s.AcademicPlan.Contains(request.searchValue) ||
                                                                                    s.AcademicSubplan.Contains(request.searchValue) ||
                                                                                    s.ErrorCategories.Contains(request.searchValue) ||
                                                                                    s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = studentAcademicPlanList.Count();

                var studentAcademicPlanRemediationList = new List<StudentAcademicPlanRemediationListItemViewModel>();

                foreach (var item in studentAcademicPlanList.Skip(request.start).Take(request.length))
                {
                    studentAcademicPlanRemediationList.Add(new StudentAcademicPlanRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,

                        Student = item.Student,
                        Term = item.Term,
                        AcademicCareer = item.AcademicCareer,
                        AcademicPlan = item.AcademicPlan,
                        AcademicSubplan = item.AcademicSubplan,

                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = studentAcademicPlanRemediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicPlanList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Academic Plan Edit View ***
        ******************************************************************/
        #region Academic Plan Edit
        // GET: Student/StudentAcademicPlanEdit
        public IActionResult StudentAcademicPlanEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentAcademicPlanHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var studentAcademicPlanDetail = history.First();
                var studentAcademicPlanSource = history.Last();

                var viewModel = new AcademicPlanViewModel()
                {
                    Title = "Student Academic Plan",
                    PageId = "studentAcademicPlanPage",
                    ActiveClass = "StudentAcademicPlan",
                    Message = "Your Student Academic Plan Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = studentAcademicPlanDetail.Id,
                    System = studentAcademicPlanDetail.SystemName,
                    SystemId = studentAcademicPlanDetail.SystemId,
                    Integration = studentAcademicPlanDetail.IntegrationName,
                    IntegrationId = studentAcademicPlanDetail.IntegrationId,
                    IntegrationDate = studentAcademicPlanDetail.IntegrationDate,
                    CreatedDate = studentAcademicPlanDetail.RecordDate,
                    SourceRecordId = studentAcademicPlanDetail.SourceRecordId,
                    CreatedOnDT = studentAcademicPlanDetail.RecordDate,

                    HistoryData = new List<StudentAcademicPlanHistoryViewModel>(),

                    #region Academic Plan Details
                    
                    Student = studentAcademicPlanDetail.Student,
                        Student_BusinessName = studentAcademicPlanDetail.Student_BusinessName,
                        Student_BusinessDescription = studentAcademicPlanDetail.Student_BusinessDescription,
                        Student_OriginalValue = studentAcademicPlanSource.Student,
                        Student_Status = studentAcademicPlanDetail.Student_Status,
                        Student_Source = studentAcademicPlanDetail.Student_Source,
                        Student_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.Student_Source }),
                    StudentMasterId = studentAcademicPlanDetail.StudentMasterId,
                        StudentMasterId_BusinessName = studentAcademicPlanDetail.StudentMasterId_BusinessName,
                        StudentMasterId_BusinessDescription = studentAcademicPlanDetail.StudentMasterId_BusinessDescription,
                        StudentMasterId_OriginalValue = studentAcademicPlanSource.StudentMasterId,
                        StudentMasterId_Status = studentAcademicPlanDetail.StudentMasterId_Status,
                        StudentMasterId_Source = studentAcademicPlanDetail.StudentMasterId_Source,
                        StudentMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.StudentMasterId_Source, studentAcademicPlanDetail.Student_Source }),
                        
                    Enrollment = (!string.IsNullOrEmpty(studentAcademicPlanDetail.TermMasterId) && !string.IsNullOrEmpty(studentAcademicPlanDetail.CampusMasterId)) ? $"{studentAcademicPlanDetail.TermMasterId}|{studentAcademicPlanDetail.CampusMasterId}" : null,
                        Enrollment_BusinessName = "Enrollment",
                        Enrollment_BusinessDescription = "Student enrollment",
                        Enrollment_Status = 
                                        ((studentAcademicPlanDetail.TermMasterId_Status == "Bad" || studentAcademicPlanDetail.CampusMasterId_Status == "Bad") ? "Bad" : 
                                        ((studentAcademicPlanDetail.TermMasterId_Status == "Enriched" || studentAcademicPlanDetail.CampusMasterId_Status == "Enriched") ? "Enriched" : 
                                        ((studentAcademicPlanDetail.TermMasterId_Status == "Changed" || studentAcademicPlanDetail.CampusMasterId_Status == "Changed") ? "Changed" : "Good"))),
                        Enrollment_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.CampusMasterId_Source, studentAcademicPlanDetail.CampusSourceSystemRecordId_Source, studentAcademicPlanDetail.TermMasterId_Source, studentAcademicPlanDetail.TermSourceSystemRecordId_Source, studentAcademicPlanDetail.Term_Source }),
                    
                    AcademicCareer = studentAcademicPlanDetail.AcademicCareer,
                        AcademicCareer_BusinessName = studentAcademicPlanDetail.AcademicCareer_BusinessName,
                        AcademicCareer_BusinessDescription = studentAcademicPlanDetail.AcademicCareer_BusinessDescription,
                        AcademicCareer_OriginalValue = studentAcademicPlanSource.AcademicCareer,
                        AcademicCareer_Status = studentAcademicPlanDetail.AcademicCareer_Status,
                        AcademicCareer_Source = studentAcademicPlanDetail.AcademicCareer_Source,
                        AcademicCareer_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCareer_Source }),
                    AcademicCareerSourceSystemRecordId = studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_BusinessName,
                        AcademicCareerSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_BusinessDescription,
                        AcademicCareerSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.AcademicCareerSourceSystemRecordId,
                        AcademicCareerSourceSystemRecordId_Status = studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_Status,
                        AcademicCareerSourceSystemRecordId_Source = studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_Source,
                        AcademicCareerSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_Source }),
                    AcademicCareerMasterId = studentAcademicPlanDetail.AcademicCareerMasterId,
                        AcademicCareerMasterId_BusinessName = studentAcademicPlanDetail.AcademicCareerMasterId_BusinessName,
                        AcademicCareerMasterId_BusinessDescription = studentAcademicPlanDetail.AcademicCareerMasterId_BusinessDescription,
                        AcademicCareerMasterId_OriginalValue = studentAcademicPlanSource.AcademicCareerMasterId,
                        AcademicCareerMasterId_Status = studentAcademicPlanDetail.AcademicCareerMasterId_Status,
                        AcademicCareerMasterId_Source = studentAcademicPlanDetail.AcademicCareerMasterId_Source,
                        AcademicCareerMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCareerMasterId_Source, studentAcademicPlanDetail.AcademicCareerSourceSystemRecordId_Source, studentAcademicPlanDetail.AcademicCareer_Source }),

                    TermName = studentAcademicPlanDetail.Term,
                        TermName_BusinessName = studentAcademicPlanDetail.Term_BusinessName,
                        TermName_BusinessDescription = studentAcademicPlanDetail.Term_BusinessDescription,
                        TermName_OriginalValue = studentAcademicPlanSource.Term,
                        TermName_Status = studentAcademicPlanDetail.Term_Status,
                        TermName_Source = studentAcademicPlanDetail.Term_Source,
                        TermName_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.Term_Source }),
                    TermSourceSystemRecordId = studentAcademicPlanDetail.TermSourceSystemRecordId,
                        TermSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.TermSourceSystemRecordId_BusinessName,
                        TermSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.TermSourceSystemRecordId_BusinessDescription,
                        TermSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.TermSourceSystemRecordId,
                        TermSourceSystemRecordId_Status = studentAcademicPlanDetail.TermSourceSystemRecordId_Status,
                        TermSourceSystemRecordId_Source = studentAcademicPlanDetail.TermSourceSystemRecordId_Source,
                        TermSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.TermSourceSystemRecordId_Source }),
                    TermMasterId = studentAcademicPlanDetail.TermMasterId,
                        TermMasterId_BusinessName = studentAcademicPlanDetail.TermMasterId_BusinessName,
                        TermMasterId_BusinessDescription = studentAcademicPlanDetail.TermMasterId_BusinessDescription,
                        TermMasterId_OriginalValue = studentAcademicPlanSource.TermMasterId,
                        TermMasterId_Status = studentAcademicPlanDetail.TermMasterId_Status,
                        TermMasterId_Source = studentAcademicPlanDetail.TermMasterId_Source,
                        TermMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.TermMasterId_Source, studentAcademicPlanDetail.TermSourceSystemRecordId_Source, studentAcademicPlanDetail.Term_Source }),

                    Degree = studentAcademicPlanDetail.Degree,
                        Degree_BusinessName = studentAcademicPlanDetail.Degree_BusinessName,
                        Degree_BusinessDescription = studentAcademicPlanDetail.Degree_BusinessDescription,
                        Degree_OriginalValue = studentAcademicPlanSource.Degree,
                        Degree_Status = studentAcademicPlanDetail.Degree_Status,
                        Degree_Source = studentAcademicPlanDetail.Degree_Source,
                        Degree_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.Degree_Source }),
                    DegreeSourceSystemRecordId = studentAcademicPlanDetail.DegreeSourceSystemRecordId,
                        DegreeSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.DegreeSourceSystemRecordId_BusinessName,
                        DegreeSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.DegreeSourceSystemRecordId_BusinessDescription,
                        DegreeSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.DegreeSourceSystemRecordId,
                        DegreeSourceSystemRecordId_Status = studentAcademicPlanDetail.DegreeSourceSystemRecordId_Status,
                        DegreeSourceSystemRecordId_Source = studentAcademicPlanDetail.DegreeSourceSystemRecordId_Source,
                        DegreeSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.DegreeSourceSystemRecordId_Source }),
                    DegreeMasterId = studentAcademicPlanDetail.DegreeMasterId,
                        DegreeMasterId_BusinessName = studentAcademicPlanDetail.DegreeMasterId_BusinessName,
                        DegreeMasterId_BusinessDescription = studentAcademicPlanDetail.DegreeMasterId_BusinessDescription,
                        DegreeMasterId_OriginalValue = studentAcademicPlanSource.DegreeMasterId,
                        DegreeMasterId_Status = studentAcademicPlanDetail.DegreeMasterId_Status,
                        DegreeMasterId_Source = studentAcademicPlanDetail.DegreeMasterId_Source,
                        DegreeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.DegreeMasterId_Source, studentAcademicPlanDetail.DegreeSourceSystemRecordId_Source, studentAcademicPlanDetail.Degree_Source }),
                        
                    AcademicPlan = studentAcademicPlanDetail.AcademicPlan,
                        AcademicPlan_BusinessName = studentAcademicPlanDetail.AcademicPlan_BusinessName,
                        AcademicPlan_BusinessDescription = studentAcademicPlanDetail.AcademicPlan_BusinessDescription,
                        AcademicPlan_OriginalValue = studentAcademicPlanSource.AcademicPlan,
                        AcademicPlan_Status = studentAcademicPlanDetail.AcademicPlan_Status,
                        AcademicPlan_Source = studentAcademicPlanDetail.AcademicPlan_Source,
                        AcademicPlan_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicPlan_Source }),
                    AcademicPlanSourceSystemRecordId = studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId,
                        AcademicPlanSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_BusinessName,
                        AcademicPlanSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_BusinessDescription,
                        AcademicPlanSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.AcademicPlanSourceSystemRecordId,
                        AcademicPlanSourceSystemRecordId_Status = studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_Status,
                        AcademicPlanSourceSystemRecordId_Source = studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_Source,
                        AcademicPlanSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_Source }),
                    AcademicPlanMasterId = studentAcademicPlanDetail.AcademicPlanMasterId,
                        AcademicPlanMasterId_BusinessName = studentAcademicPlanDetail.AcademicPlanMasterId_BusinessName,
                        AcademicPlanMasterId_BusinessDescription = studentAcademicPlanDetail.AcademicPlanMasterId_BusinessDescription,
                        AcademicPlanMasterId_OriginalValue = studentAcademicPlanSource.AcademicPlanMasterId,
                        AcademicPlanMasterId_Status = studentAcademicPlanDetail.AcademicPlanMasterId_Status,
                        AcademicPlanMasterId_Source = studentAcademicPlanDetail.AcademicPlanMasterId_Source,
                        AcademicPlanMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicPlanMasterId_Source, studentAcademicPlanDetail.AcademicPlanSourceSystemRecordId_Source, studentAcademicPlanDetail.AcademicPlan_Source }),
                        
                    AcademicCatalog = studentAcademicPlanDetail.AcademicCatalog,
                        AcademicCatalog_BusinessName = studentAcademicPlanDetail.AcademicCatalog_BusinessName,
                        AcademicCatalog_BusinessDescription = studentAcademicPlanDetail.AcademicCatalog_BusinessDescription,
                        AcademicCatalog_OriginalValue = studentAcademicPlanSource.AcademicCatalog,
                        AcademicCatalog_Status = studentAcademicPlanDetail.AcademicCatalog_Status,
                        AcademicCatalog_Source = studentAcademicPlanDetail.AcademicCatalog_Source,
                        AcademicCatalog_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCatalog_Source }),
                    AcademicCatalogSourceSystemRecordId = studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId,
                        AcademicCatalogSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_BusinessName,
                        AcademicCatalogSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_BusinessDescription,
                        AcademicCatalogSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.AcademicCatalogSourceSystemRecordId,
                        AcademicCatalogSourceSystemRecordId_Status = studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_Status,
                        AcademicCatalogSourceSystemRecordId_Source = studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_Source,
                        AcademicCatalogSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_Source }),
                    AcademicCatalogMasterId = studentAcademicPlanDetail.AcademicCatalogMasterId,
                        AcademicCatalogMasterId_BusinessName = studentAcademicPlanDetail.AcademicCatalogMasterId_BusinessName,
                        AcademicCatalogMasterId_BusinessDescription = studentAcademicPlanDetail.AcademicCatalogMasterId_BusinessDescription,
                        AcademicCatalogMasterId_OriginalValue = studentAcademicPlanSource.AcademicCatalogMasterId,
                        AcademicCatalogMasterId_Status = studentAcademicPlanDetail.AcademicCatalogMasterId_Status,
                        AcademicCatalogMasterId_Source = studentAcademicPlanDetail.AcademicCatalogMasterId_Source,
                        AcademicCatalogMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicCatalogMasterId_Source, studentAcademicPlanDetail.AcademicCatalogSourceSystemRecordId_Source, studentAcademicPlanDetail.AcademicCareer_Source }),

                    AcademicSubplan = studentAcademicPlanDetail.AcademicSubplan,
                        AcademicSubplan_BusinessName = studentAcademicPlanDetail.AcademicSubplan_BusinessName,
                        AcademicSubplan_BusinessDescription = studentAcademicPlanDetail.AcademicSubplan_BusinessDescription,
                        AcademicSubplan_OriginalValue = studentAcademicPlanSource.AcademicSubplan,
                        AcademicSubplan_Status = studentAcademicPlanDetail.AcademicSubplan_Status,
                        AcademicSubplan_Source = studentAcademicPlanDetail.AcademicSubplan_Source,
                        AcademicSubplan_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicSubplan_Source }),
                    AcademicSubplanSourceSystemRecordId = studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId,
                        AcademicSubplanSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_BusinessName,
                        AcademicSubplanSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_BusinessDescription,
                        AcademicSubplanSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.AcademicSubplanSourceSystemRecordId,
                        AcademicSubplanSourceSystemRecordId_Status = studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_Status,
                        AcademicSubplanSourceSystemRecordId_Source = studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_Source,
                        AcademicSubplanSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_Source }),
                    AcademicSubplanMasterId = studentAcademicPlanDetail.AcademicSubplanMasterId,
                        AcademicSubplanMasterId_BusinessName = studentAcademicPlanDetail.AcademicSubplanMasterId_BusinessName,
                        AcademicSubplanMasterId_BusinessDescription = studentAcademicPlanDetail.AcademicSubplanMasterId_BusinessDescription,
                        AcademicSubplanMasterId_OriginalValue = studentAcademicPlanSource.AcademicSubplanMasterId,
                        AcademicSubplanMasterId_Status = studentAcademicPlanDetail.AcademicSubplanMasterId_Status,
                        AcademicSubplanMasterId_Source = studentAcademicPlanDetail.AcademicSubplanMasterId_Source,
                        AcademicSubplanMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.AcademicSubplanMasterId_Source, studentAcademicPlanDetail.AcademicSubplanSourceSystemRecordId_Source, studentAcademicPlanDetail.AcademicSubplan_Source }),

                    StudentAcademicPlanStatus = studentAcademicPlanDetail.StudentAcademicPlanStatus,
                        StudentAcademicPlanStatus_BusinessName = studentAcademicPlanDetail.StudentAcademicPlanStatus_BusinessName,
                        StudentAcademicPlanStatus_BusinessDescription = studentAcademicPlanDetail.StudentAcademicPlanStatus_BusinessDescription,
                        StudentAcademicPlanStatus_OriginalValue = studentAcademicPlanSource.StudentAcademicPlanStatus,
                        StudentAcademicPlanStatus_Status = studentAcademicPlanDetail.StudentAcademicPlanStatus_Status,
                        StudentAcademicPlanStatus_Source = studentAcademicPlanDetail.StudentAcademicPlanStatus_Source,
                        StudentAcademicPlanStatus_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.StudentAcademicPlanStatus_Source }),
                    StudentAcademicPlanStatusSourceSystemRecordId = studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId,
                        StudentAcademicPlanStatusSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_BusinessName,
                        StudentAcademicPlanStatusSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_BusinessDescription,
                        StudentAcademicPlanStatusSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.StudentAcademicPlanStatusSourceSystemRecordId,
                        StudentAcademicPlanStatusSourceSystemRecordId_Status = studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_Status,
                        StudentAcademicPlanStatusSourceSystemRecordId_Source = studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_Source,
                        StudentAcademicPlanStatusSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_Source }),
                    StudentAcademicPlanStatusMasterId = studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId,
                        StudentAcademicPlanStatusMasterId_BusinessName = studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId_BusinessName,
                        StudentAcademicPlanStatusMasterId_BusinessDescription = studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId_BusinessDescription,
                        StudentAcademicPlanStatusMasterId_OriginalValue = studentAcademicPlanSource.StudentAcademicPlanStatusMasterId,
                        StudentAcademicPlanStatusMasterId_Status = studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId_Status,
                        StudentAcademicPlanStatusMasterId_Source = studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId_Source,
                        StudentAcademicPlanStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.StudentAcademicPlanStatusMasterId_Source, studentAcademicPlanDetail.StudentAcademicPlanStatusSourceSystemRecordId_Source, studentAcademicPlanDetail.StudentAcademicPlanStatus_Source }),

                    ExpectedGraduationTerm = studentAcademicPlanDetail.ExpectedGraduationTerm,
                        ExpectedGraduationTerm_BusinessName = studentAcademicPlanDetail.ExpectedGraduationTerm_BusinessName,
                        ExpectedGraduationTerm_BusinessDescription = studentAcademicPlanDetail.ExpectedGraduationTerm_BusinessDescription,
                        ExpectedGraduationTerm_OriginalValue = studentAcademicPlanSource.ExpectedGraduationTerm,
                        ExpectedGraduationTerm_Status = studentAcademicPlanDetail.ExpectedGraduationTerm_Status,
                        ExpectedGraduationTerm_Source = studentAcademicPlanDetail.ExpectedGraduationTerm_Source,
                        ExpectedGraduationTerm_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.ExpectedGraduationTerm_Source }),
                    ExpectedGraduationTermSourceSystemRecordId = studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId,
                        ExpectedGraduationTermSourceSystemRecordId_BusinessName = studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_BusinessName,
                        ExpectedGraduationTermSourceSystemRecordId_BusinessDescription = studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_BusinessDescription,
                        ExpectedGraduationTermSourceSystemRecordId_OriginalValue = studentAcademicPlanSource.ExpectedGraduationTermSourceSystemRecordId,
                        ExpectedGraduationTermSourceSystemRecordId_Status = studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_Status,
                        ExpectedGraduationTermSourceSystemRecordId_Source = studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_Source,
                        ExpectedGraduationTermSourceSystemRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_Source }),
                    ExpectedGraduationTermMasterId = studentAcademicPlanDetail.ExpectedGraduationTermMasterId,
                        ExpectedGraduationTermMasterId_BusinessName = studentAcademicPlanDetail.ExpectedGraduationTermMasterId_BusinessName,
                        ExpectedGraduationTermMasterId_BusinessDescription = studentAcademicPlanDetail.ExpectedGraduationTermMasterId_BusinessDescription,
                        ExpectedGraduationTermMasterId_OriginalValue = studentAcademicPlanSource.ExpectedGraduationTermMasterId,
                        ExpectedGraduationTermMasterId_Status = studentAcademicPlanDetail.ExpectedGraduationTermMasterId_Status,
                        ExpectedGraduationTermMasterId_Source = studentAcademicPlanDetail.ExpectedGraduationTermMasterId_Source,
                        ExpectedGraduationTermMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { studentAcademicPlanDetail.ExpectedGraduationTermMasterId_Source, studentAcademicPlanDetail.ExpectedGraduationTermSourceSystemRecordId_Source, studentAcademicPlanDetail.ExpectedGraduationTerm_Source }),
                        
                    #endregion

                    #region Dropdowns
                    StudentList = GetStudentList(),
                    EnrollmentList = GetStudentEnrollmentDropdownList(studentAcademicPlanDetail.StudentMasterId),
                    DegreeList = GetDegreeTypeList(),
                    AcademicPlanList = GetStudentAcademicCatalogAcademicPlanList(),
                    AcademicSubplanList = GetAcademicSubplanList(),
                    AcademicPlanStatusList = GetAcademicPlanStatusList(),
                    AcademicCareerList = GetDischargedAcademicCareerList(),
                    ExpectedGraduationTermList = GetAcademicTermList(),
                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);
                    viewModel.HistoryData.Add(new StudentAcademicPlanHistoryViewModel()
                    {
                        Student = item.Student,
                            Student_Status = item.Student_Status,
                            Student_OriginalValue = previousitem.Student,
                        StudentMasterId = item.StudentMasterId,
                            StudentMasterId_Status = item.StudentMasterId_Status,
                            StudentMasterId_OriginalValue = previousitem.StudentMasterId,
                            
                        CampusSourceSystemRecordId = item.CampusSourceSystemRecordId,
                            CampusSourceSystemRecordId_Status = item.CampusSourceSystemRecordId_Status,
                            CampusSourceSystemRecordId_OriginalValue = previousitem.CampusSourceSystemRecordId,
                        CampusMasterId = item.CampusMasterId,
                            CampusMasterId_Status = item.CampusMasterId_Status,
                            CampusMasterId_OriginalValue = previousitem.CampusMasterId,

                        AcademicCareer = item.AcademicCareer,
                            AcademicCareer_Status = item.AcademicCareer_Status,
                            AcademicCareer_OriginalValue = previousitem.AcademicCareer,
                        AcademicCareerSourceSystemRecordId = item.AcademicCareerSourceSystemRecordId,
                            AcademicCareerSourceSystemRecordId_Status = item.AcademicCareerSourceSystemRecordId_Status,
                            AcademicCareerSourceSystemRecordId_OriginalValue = previousitem.AcademicCareerSourceSystemRecordId,
                        AcademicCareerMasterId = item.AcademicCareerMasterId,
                            AcademicCareerMasterId_Status = item.AcademicCareerMasterId_Status,
                            AcademicCareerMasterId_OriginalValue = previousitem.AcademicCareerMasterId,

                        TermName = item.Term,
                            TermName_Status = item.Term_Status,
                            TermName_OriginalValue = previousitem.Term,
                        TermSourceSystemRecordId = item.TermSourceSystemRecordId,
                            TermSourceSystemRecordId_Status = item.TermSourceSystemRecordId_Status,
                            TermSourceSystemRecordId_OriginalValue = previousitem.TermSourceSystemRecordId,
                        TermMasterId = item.TermMasterId,
                            TermMasterId_Status = item.TermMasterId_Status,
                            TermMasterId_OriginalValue = previousitem.TermMasterId,

                        Degree = item.Degree,
                            Degree_Status = item.Degree_Status,
                            Degree_OriginalValue = previousitem.Degree,
                        DegreeSourceSystemRecordId = item.DegreeSourceSystemRecordId,
                            DegreeSourceSystemRecordId_Status = item.DegreeSourceSystemRecordId_Status,
                            DegreeSourceSystemRecordId_OriginalValue = previousitem.DegreeSourceSystemRecordId,
                        DegreeMasterId = item.DegreeMasterId,
                            DegreeMasterId_Status = item.DegreeMasterId_Status,
                            DegreeMasterId_OriginalValue = previousitem.DegreeMasterId,
                            
                        AcademicPlan = item.AcademicPlan,
                            AcademicPlan_Status = item.AcademicPlan_Status,
                            AcademicPlan_OriginalValue = previousitem.AcademicPlan,
                        AcademicPlanSourceSystemRecordId = item.AcademicPlanSourceSystemRecordId,
                            AcademicPlanSourceSystemRecordId_Status = item.AcademicPlanSourceSystemRecordId_Status,
                            AcademicPlanSourceSystemRecordId_OriginalValue = previousitem.AcademicPlanSourceSystemRecordId,
                        AcademicPlanMasterId = item.AcademicPlanMasterId,
                            AcademicPlanMasterId_Status = item.AcademicPlanMasterId_Status,
                            AcademicPlanMasterId_OriginalValue = previousitem.AcademicPlanMasterId,

                        AcademicCatalog = item.AcademicCatalog,
                            AcademicCatalog_Status = item.AcademicCatalog_Status,
                            AcademicCatalog_OriginalValue = previousitem.AcademicCatalog,
                        AcademicCatalogSourceSystemRecordId = item.AcademicCatalogSourceSystemRecordId,
                            AcademicCatalogSourceSystemRecordId_Status = item.AcademicCatalogSourceSystemRecordId_Status,
                            AcademicCatalogSourceSystemRecordId_OriginalValue = previousitem.AcademicCatalogSourceSystemRecordId,
                        AcademicCatalogMasterId = item.AcademicCatalogMasterId,
                            AcademicCatalogMasterId_Status = item.AcademicCatalogMasterId_Status,
                            AcademicCatalogMasterId_OriginalValue = previousitem.AcademicCatalogMasterId,

                        AcademicSubplan = item.AcademicSubplan,
                            AcademicSubplan_Status = item.AcademicSubplan_Status,
                            AcademicSubplan_OriginalValue = previousitem.AcademicSubplan,
                        AcademicSubplanSourceSystemRecordId = item.AcademicSubplanSourceSystemRecordId,
                            AcademicSubplanSourceSystemRecordId_Status = item.AcademicSubplanSourceSystemRecordId_Status,
                            AcademicSubplanSourceSystemRecordId_OriginalValue = previousitem.AcademicSubplanSourceSystemRecordId,
                        AcademicSubplanMasterId = item.AcademicSubplanMasterId,
                            AcademicSubplanMasterId_Status = item.AcademicSubplanMasterId_Status,
                            AcademicSubplanMasterId_OriginalValue = previousitem.AcademicSubplanMasterId,

                        StudentAcademicPlanStatus = item.StudentAcademicPlanStatus,
                            StudentAcademicPlanStatus_Status = item.StudentAcademicPlanStatus_Status,
                            StudentAcademicPlanStatus_OriginalValue = previousitem.StudentAcademicPlanStatus,
                        StudentAcademicPlanStatusSourceSystemRecordId = item.StudentAcademicPlanStatusSourceSystemRecordId,
                            StudentAcademicPlanStatusSourceSystemRecordId_Status = item.StudentAcademicPlanStatusSourceSystemRecordId_Status,
                            StudentAcademicPlanStatusSourceSystemRecordId_OriginalValue = previousitem.StudentAcademicPlanStatusSourceSystemRecordId,
                        StudentAcademicPlanStatusMasterId = item.StudentAcademicPlanStatusMasterId,
                            StudentAcademicPlanStatusMasterId_Status = item.StudentAcademicPlanStatusMasterId_Status,
                            StudentAcademicPlanStatusMasterId_OriginalValue = previousitem.StudentAcademicPlanStatusMasterId,

                        ExpectedGraduationTerm = item.ExpectedGraduationTerm,
                            ExpectedGraduationTerm_Status = item.ExpectedGraduationTerm_Status,
                            ExpectedGraduationTerm_OriginalValue = previousitem.ExpectedGraduationTerm,
                        ExpectedGraduationTermSourceSystemRecordId = item.ExpectedGraduationTermSourceSystemRecordId,
                            ExpectedGraduationTermSourceSystemRecordId_Status = item.ExpectedGraduationTermSourceSystemRecordId_Status,
                            ExpectedGraduationTermSourceSystemRecordId_OriginalValue = previousitem.ExpectedGraduationTermSourceSystemRecordId,
                        ExpectedGraduationTermMasterId = item.ExpectedGraduationTermMasterId,
                            ExpectedGraduationTermMasterId_Status = item.ExpectedGraduationTermMasterId_Status,
                            ExpectedGraduationTermMasterId_OriginalValue = previousitem.ExpectedGraduationTermMasterId,

                        HistoryDate = item.RecordDate
                    });
                };
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentAcademicPlanEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion


        /* No Match/Compare for Academic Plan at this time (05/05/2020) */


        /*  *** Academic Plan Save ***
        ******************************************************************/
        #region Academic Plan Save
        // POST: Student/StudentAcademicPlanSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentAcademicPlanSave(AcademicPlanViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeStudentAcademicPlanIntegrationRecord(model.SystemId, model.Id, model.Student, model.StudentMasterId,
                        model.AcademicCareer, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                        model.Degree, model.DegreeSourceSystemRecordId, model.DegreeMasterId,
                        model.CampusSourceSystemRecordId, model.CampusMasterId,
                        model.TermName, model.TermSourceSystemRecordId, model.TermMasterId,
                        model.AcademicPlan, model.AcademicPlanSourceSystemRecordId, model.AcademicPlanMasterId,
                        model.AcademicSubplan, model.AcademicSubplanSourceSystemRecordId, model.AcademicSubplanMasterId,
                        model.StudentAcademicPlanStatus, model.StudentAcademicPlanStatusSourceSystemRecordId, model.StudentAcademicPlanStatusMasterId,
                        model.ExpectedGraduationTerm, model.ExpectedGraduationTermSourceSystemRecordId, model.ExpectedGraduationTermMasterId,
                        User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentAcademicPlanSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentAcademicPlanList));
            }
            else
            {
                model.Title = "Student Academic Plan";
                model.PageId = "studentAcademicPlanPage";
                model.ActiveClass = "StudentAcademicPlan";
                model.Message = "Your Student Academic Plan Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(StudentAcademicPlanEdit), model);
            }


        }
        #endregion

        /*  *** Academic Plan Revalidate ***
        ******************************************************************/
        #region Academic Plan Revalidate
        // POST: Student/StudentAcademicPlanRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentAcademicPlanRevalidate(AcademicPlanViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // StudentMasterId
                    if (string.IsNullOrEmpty(model.StudentMasterId) && (!string.IsNullOrEmpty(model.StudentMasterId)))
                    {
                        model.StudentMasterId = null;

                        model.IsChanged = true;
                    }

                    // Enrollment
                    if (string.IsNullOrEmpty(model.Enrollment) && (!string.IsNullOrEmpty(model.Enrollment)))
                    {
                        model.Enrollment = null;

                        model.IsChanged = true;
                    }

                    // @DegreeMasterId
                    if (string.IsNullOrEmpty(model.DegreeMasterId) && (!string.IsNullOrEmpty(model.DegreeSourceSystemRecordId) || !string.IsNullOrEmpty(model.Degree)))
                    {
                        model.DegreeSourceSystemRecordId = null;
                        model.Degree = null;

                        model.IsChanged = true;
                    }

                    // AcademicPlanMasterId
                    if (string.IsNullOrEmpty(model.AcademicPlanMasterId) && (!string.IsNullOrEmpty(model.AcademicPlanSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicPlan)))
                    {
                        model.AcademicPlanSourceSystemRecordId = null;
                        model.AcademicPlan = null;

                        model.IsChanged = true;
                    }

                    // AcademicSubplanMasterId
                    if (string.IsNullOrEmpty(model.AcademicSubplanMasterId) && (!string.IsNullOrEmpty(model.AcademicSubplanSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicSubplan)))
                    {
                        model.AcademicSubplanSourceSystemRecordId = null;
                        model.AcademicSubplan = null;

                        model.IsChanged = true;
                    }

                    // AcademicPlanStatusMasterId
                    if (string.IsNullOrEmpty(model.StudentAcademicPlanStatusMasterId) && (!string.IsNullOrEmpty(model.StudentAcademicPlanStatusSourceSystemRecordId) || !string.IsNullOrEmpty(model.StudentAcademicPlanStatus)))
                    {
                        model.StudentAcademicPlanStatusSourceSystemRecordId = null;
                        model.StudentAcademicPlanStatus = null;

                        model.IsChanged = true;
                    }

                    // ExpectedGraduationTermMasterId
                    if (string.IsNullOrEmpty(model.ExpectedGraduationTermMasterId) && (!string.IsNullOrEmpty(model.ExpectedGraduationTermSourceSystemRecordId) || !string.IsNullOrEmpty(model.ExpectedGraduationTerm)))
                    {
                        model.ExpectedGraduationTermSourceSystemRecordId = null;
                        model.ExpectedGraduationTerm = null;

                        model.IsChanged = true;
                    }

                    // AcademicCareerMasterId
                    if (string.IsNullOrEmpty(model.AcademicCareerMasterId) && (!string.IsNullOrEmpty(model.AcademicCareerSourceSystemRecordId) || !string.IsNullOrEmpty(model.AcademicCareer)))
                    {
                        model.AcademicCareerSourceSystemRecordId = null;
                        model.AcademicCareer = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _context.ChangeStudentAcademicPlanIntegrationRecord(model.SystemId, model.Id, model.Student, model.StudentMasterId,
                           model.AcademicCareer, model.AcademicCareerSourceSystemRecordId, model.AcademicCareerMasterId,
                           model.Degree, model.DegreeSourceSystemRecordId, model.DegreeMasterId,
                           model.CampusSourceSystemRecordId, model.CampusMasterId,
                           model.TermName, model.TermSourceSystemRecordId, model.TermMasterId,
                           model.AcademicPlan, model.AcademicPlanSourceSystemRecordId, model.AcademicPlanMasterId,
                           model.AcademicSubplan, model.AcademicSubplanSourceSystemRecordId, model.AcademicSubplanMasterId,
                           model.StudentAcademicPlanStatus, model.StudentAcademicPlanStatusSourceSystemRecordId, model.StudentAcademicPlanStatusMasterId,
                           model.ExpectedGraduationTerm, model.ExpectedGraduationTermSourceSystemRecordId, model.ExpectedGraduationTermMasterId,
                           User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentAcademicPlanRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentAcademicPlanList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore/Remove ***
        ******************************************************************/
        #region Ignore/Remove
        public IActionResult StudentAcademicPlanIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load StudentAcademicPlanIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentAcademicPlanList));
        }
        #endregion

        #endregion

        #region Parent

        /*  *** Parent List View ***
        ******************************************************************/
        #region Parent List
        // GET: Student/StudentParentList
        public IActionResult StudentParentList()
        {
            var model = new StudentParentListViewModel()
            {
                Title = "Parent",
                PageId = "studentParentPage",
                ActiveClass = "StudentParent",
                Message = "Your Parent Page",
                Integration = "Parent",
                IntegrationId = 16,
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<StudentParentRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetStudentParentList(AjaxDataTableRequest request)
        {
            try
            {
                var studentParents = _context.StudentParentRemediationList.AsQueryable();

                int recordsTotal = studentParents.Count();
                if (!String.IsNullOrWhiteSpace(request.sortColumn))
                {
                    if (request.sortColumn == "studentName")
                    {
                        request.sortColumn = "studentFirstName";
                    }
                    else if (request.sortColumn == "parentName")
                    {
                        request.sortColumn = "firstName";
                    }
                }
                var studentParentList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? studentParents
                                                : studentParents.Where(s => s.FirstName.Contains(request.searchValue) ||
                                                                                    s.LastName.Contains(request.searchValue) ||
                                                                                    s.StudentFirstName.Contains(request.searchValue) ||
                                                                                    s.StudentLastName.Contains(request.searchValue) ||
                                                                                    s.Relationship.Contains(request.searchValue) ||
                                                                                    s.ErrorCategories.Contains(request.searchValue) ||
                                                                                    s.SystemName.Contains(request.searchValue))
                                        )
                                        .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                        .ToListAsync();

                int recordsFiltered = studentParentList.Count();

                var studentParentRemediationList = new List<StudentParentRemediationListItemViewModel>();

                foreach (var item in studentParentList.Skip(request.start).Take(request.length))
                {
                    studentParentRemediationList.Add(new StudentParentRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        RecordStatus = item.RecordStatus,

                        ParentName = string.Format("{0} {1}", item.FirstName, item.LastName),
                        StudentName = string.Format("{0} {1}", item.StudentFirstName, item.StudentLastName),
                        Relationship = item.Relationship,

                        ErrorCategories = item.ErrorCategories,
                        SystemName = item.SystemName,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCount = item.ErrorCount,
                        IntegrationId = item.IntegrationId,
                        CreatedDate = item.CreatedDate
                    });
                }

                var data = studentParentRemediationList.ToList();

                return Json(
                    new
                    {
                        request.draw,
                        recordsFiltered,
                        recordsTotal,
                        data
                    });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Parent Edit View ***
        ******************************************************************/
        #region Parent Edit
        // GET: Student/StudentParentEdit
        public IActionResult StudentParentEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetStudentParentHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var integrationDetail = history.First();
                var integrationSource = history.Last();

                var viewModel = new StudentParentViewModel()
                {
                    Title = "Student Parent",
                    PageId = "studentParentPage",
                    ActiveClass = "StudentParent",
                    Message = "Your Student Parent Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),

                    IsChanged = false,
                    Id = integrationDetail.Id,
                    System = integrationDetail.SystemName,
                    SystemId = integrationDetail.SystemId,
                    Integration = integrationDetail.IntegrationName,
                    IntegrationId = integrationDetail.IntegrationId,
                    IntegrationDate = integrationDetail.IntegrationDate,
                    CreatedDate = integrationDetail.RecordDate,
                    SourceRecordId = integrationDetail.SourceRecordId,
                    CreatedOnDT = integrationDetail.RecordDate,

                    HistoryData = new List<StudentParentHistoryViewModel>(),

                    #region Parent Details
                        
                    FirstName = integrationDetail.FirstName,
                        FirstName_BusinessName = integrationDetail.FirstName_BusinessName,
                        FirstName_BusinessDescription = integrationDetail.FirstName_BusinessDescription,
                        FirstName_OriginalValue = integrationSource.FirstName,
                        FirstName_Status = integrationDetail.FirstName_Status,
                        FirstName_Source = integrationDetail.FirstName_Source,
                        FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.FirstName_Source }),
                        
                    PreferredName = integrationDetail.PreferredName,
                        PreferredName_BusinessName = integrationDetail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = integrationDetail.PreferredName_BusinessDescription,
                        PreferredName_OriginalValue = integrationSource.PreferredName,
                        PreferredName_Status = integrationDetail.PreferredName_Status,
                        PreferredName_Source = integrationDetail.PreferredName_Source,
                        PreferredName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PreferredName_Source }),
                        
                    LastName = integrationDetail.LastName,
                        LastName_BusinessName = integrationDetail.LastName_BusinessName,
                        LastName_BusinessDescription = integrationDetail.LastName_BusinessDescription,
                        LastName_OriginalValue = integrationSource.LastName,
                        LastName_Status = integrationDetail.LastName_Status,
                        LastName_Source = integrationDetail.LastName_Source,
                        LastName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.LastName_Source }),
                        
                    Suffix = integrationDetail.Suffix,
                        Suffix_BusinessName = integrationDetail.Suffix_BusinessName,
                        Suffix_BusinessDescription = integrationDetail.Suffix_BusinessDescription,
                        Suffix_OriginalValue = integrationSource.Suffix,
                        Suffix_Status = integrationDetail.Suffix_Status,
                        Suffix_Source = integrationDetail.Suffix_Source,
                        Suffix_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Suffix_Source }),
                    SuffixMasterRecordId = integrationDetail.SuffixMasterRecordId,
                        SuffixMasterRecordId_BusinessName = integrationDetail.SuffixMasterRecordId_BusinessName,
                        SuffixMasterRecordId_BusinessDescription = integrationDetail.SuffixMasterRecordId_BusinessDescription,
                        SuffixMasterRecordId_OriginalValue = integrationSource.SuffixMasterRecordId,
                        SuffixMasterRecordId_Status = integrationDetail.SuffixMasterRecordId_Status,
                        SuffixMasterRecordId_Source = integrationDetail.SuffixMasterRecordId_Source,
                        SuffixMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.SuffixMasterRecordId_Source, integrationDetail.Suffix_Source }),
                        
                    StudentId = integrationDetail.StudentId,
                        StudentId_BusinessName = integrationDetail.StudentId_BusinessName,
                        StudentId_BusinessDescription = integrationDetail.StudentId_BusinessDescription,
                        StudentId_OriginalValue = integrationSource.StudentId,
                        StudentId_Status = integrationDetail.StudentId_Status,
                        StudentId_Source = integrationDetail.StudentId_Source,
                        StudentId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.StudentId_Source }),
                    StudentFirstName = integrationDetail.StudentFirstName,
                        StudentFirstName_BusinessName = integrationDetail.StudentFirstName_BusinessName,
                        StudentFirstName_BusinessDescription = integrationDetail.StudentFirstName_BusinessDescription,
                        StudentFirstName_OriginalValue = integrationSource.StudentFirstName,
                        StudentFirstName_Status = integrationDetail.StudentFirstName_Status,
                        StudentFirstName_Source = integrationDetail.StudentFirstName_Source,
                        StudentFirstName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.StudentFirstName_Source }),
                    StudentLastName = integrationDetail.StudentLastName,
                        StudentLastName_BusinessName = integrationDetail.StudentLastName_BusinessName,
                        StudentLastName_BusinessDescription = integrationDetail.StudentLastName_BusinessDescription,
                        StudentLastName_OriginalValue = integrationSource.StudentLastName,
                        StudentLastName_Status = integrationDetail.StudentLastName_Status,
                        StudentLastName_Source = integrationDetail.StudentLastName_Source,
                        StudentLastName_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.StudentLastName_Source }),
                    StudentMasterRecordId = integrationDetail.StudentMasterRecordId,
                        StudentMasterRecordId_BusinessName = integrationDetail.StudentMasterRecordId_BusinessName,
                        StudentMasterRecordId_BusinessDescription = integrationDetail.StudentMasterRecordId_BusinessDescription,
                        StudentMasterRecordId_OriginalValue = integrationSource.StudentMasterRecordId,
                        StudentMasterRecordId_Status = integrationDetail.StudentMasterRecordId_Status,
                        StudentMasterRecordId_Source = integrationDetail.StudentMasterRecordId_Source,
                        StudentMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.StudentMasterRecordId_Source, integrationDetail.StudentLastName_Source, integrationDetail.StudentFirstName_Source, integrationDetail.StudentId_Source }),

                    Relationship = integrationDetail.Relationship,
                        Relationship_BusinessName = integrationDetail.Relationship_BusinessName,
                        Relationship_BusinessDescription = integrationDetail.Relationship_BusinessDescription,
                        Relationship_OriginalValue = integrationSource.Relationship,
                        Relationship_Status = integrationDetail.Relationship_Status,
                        Relationship_Source = integrationDetail.Relationship_Source,
                        Relationship_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Relationship_Source }),
                    RelationshipMasterRecordId = integrationDetail.RelationshipMasterRecordId,
                        RelationshipMasterRecordId_BusinessName = integrationDetail.RelationshipMasterRecordId_BusinessName,
                        RelationshipMasterRecordId_BusinessDescription = integrationDetail.RelationshipMasterRecordId_BusinessDescription,
                        RelationshipMasterRecordId_OriginalValue = integrationSource.RelationshipMasterRecordId,
                        RelationshipMasterRecordId_Status = integrationDetail.RelationshipMasterRecordId_Status,
                        RelationshipMasterRecordId_Source = integrationDetail.RelationshipMasterRecordId_Source,
                        RelationshipMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.RelationshipMasterRecordId_Source, integrationDetail.Relationship_Source }),

                    Phone1Number = integrationDetail.Phone1Number,
                        Phone1Number_BusinessName = integrationDetail.Phone1Number_BusinessName,
                        Phone1Number_BusinessDescription = integrationDetail.Phone1Number_BusinessDescription,
                        Phone1Number_OriginalValue = integrationSource.Phone1Number,
                        Phone1Number_Status = integrationDetail.Phone1Number_Status,
                        Phone1Number_Source = integrationDetail.Phone1Number_Source,
                        Phone1Number_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone1Number_Source }),
                    Phone1MasterRecordId = integrationDetail.Phone1MasterRecordId,
                        Phone1MasterRecordId_BusinessName = integrationDetail.Phone1MasterRecordId_BusinessName,
                        Phone1MasterRecordId_BusinessDescription = integrationDetail.Phone1MasterRecordId_BusinessDescription,
                        Phone1MasterRecordId_OriginalValue = integrationSource.Phone1MasterRecordId,
                        Phone1MasterRecordId_Status = integrationDetail.Phone1MasterRecordId_Status,
                        Phone1MasterRecordId_Source = integrationDetail.Phone1MasterRecordId_Source,
                        Phone1MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone1MasterRecordId_Source, integrationDetail.Phone1Number_Source }),

                    Phone1Extension = integrationDetail.Phone1Extension,
                        Phone1Extension_BusinessName = integrationDetail.Phone1Extension_BusinessName,
                        Phone1Extension_BusinessDescription = integrationDetail.Phone1Extension_BusinessDescription,
                        Phone1Extension_OriginalValue = integrationSource.Phone1Extension,
                        Phone1Extension_Status = integrationDetail.Phone1Extension_Status,
                        Phone1Extension_Source = integrationDetail.Phone1Extension_Source,
                        Phone1Extension_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone1Extension_Source }),

                    Phone1CountryCode = integrationDetail.Phone1CountryCode,
                        Phone1CountryCode_BusinessName = integrationDetail.Phone1CountryCode_BusinessName,
                        Phone1CountryCode_BusinessDescription = integrationDetail.Phone1CountryCode_BusinessDescription,
                        Phone1CountryCode_OriginalValue = integrationSource.Phone1CountryCode,
                        Phone1CountryCode_Status = integrationDetail.Phone1CountryCode_Status,
                        Phone1CountryCode_Source = integrationDetail.Phone1CountryCode_Source,
                        Phone1CountryCode_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone1CountryCode_Source }),
                    Phone1CountryMasterRecordId = integrationDetail.Phone1CountryMasterRecordId,
                        Phone1CountryMasterRecordId_BusinessName = integrationDetail.Phone1CountryMasterRecordId_BusinessName,
                        Phone1CountryMasterRecordId_BusinessDescription = integrationDetail.Phone1CountryMasterRecordId_BusinessDescription,
                        Phone1CountryMasterRecordId_OriginalValue = integrationSource.Phone1CountryMasterRecordId,
                        Phone1CountryMasterRecordId_Status = integrationDetail.Phone1CountryMasterRecordId_Status,
                        Phone1CountryMasterRecordId_Source = integrationDetail.Phone1CountryMasterRecordId_Source,
                        Phone1CountryMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone1CountryMasterRecordId_Source, integrationDetail.Phone1CountryCode_Source }),

                    Phone2Number = integrationDetail.Phone2Number,
                        Phone2Number_BusinessName = integrationDetail.Phone2Number_BusinessName,
                        Phone2Number_BusinessDescription = integrationDetail.Phone2Number_BusinessDescription,
                        Phone2Number_OriginalValue = integrationSource.Phone2Number,
                        Phone2Number_Status = integrationDetail.Phone2Number_Status,
                        Phone2Number_Source = integrationDetail.Phone2Number_Source,
                        Phone2Number_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone2Number_Source }),
                    Phone2MasterRecordId = integrationDetail.Phone2MasterRecordId,
                        Phone2MasterRecordId_BusinessName = integrationDetail.Phone2MasterRecordId_BusinessName,
                        Phone2MasterRecordId_BusinessDescription = integrationDetail.Phone2MasterRecordId_BusinessDescription,
                        Phone2MasterRecordId_OriginalValue = integrationSource.Phone2MasterRecordId,
                        Phone2MasterRecordId_Status = integrationDetail.Phone2MasterRecordId_Status,
                        Phone2MasterRecordId_Source = integrationDetail.Phone2MasterRecordId_Source,
                        Phone2MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone2MasterRecordId_Source, integrationDetail.Phone2Number_Source }),

                    Phone2Extension = integrationDetail.Phone2Extension,
                        Phone2Extension_BusinessName = integrationDetail.Phone2Extension_BusinessName,
                        Phone2Extension_BusinessDescription = integrationDetail.Phone2Extension_BusinessDescription,
                        Phone2Extension_OriginalValue = integrationSource.Phone2Extension,
                        Phone2Extension_Status = integrationDetail.Phone2Extension_Status,
                        Phone2Extension_Source = integrationDetail.Phone2Extension_Source,
                        Phone2Extension_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone2Extension_Source }),

                    Phone2CountryCode = integrationDetail.Phone2CountryCode,
                        Phone2CountryCode_BusinessName = integrationDetail.Phone2CountryCode_BusinessName,
                        Phone2CountryCode_BusinessDescription = integrationDetail.Phone2CountryCode_BusinessDescription,
                        Phone2CountryCode_OriginalValue = integrationSource.Phone2CountryCode,
                        Phone2CountryCode_Status = integrationDetail.Phone2CountryCode_Status,
                        Phone2CountryCode_Source = integrationDetail.Phone2CountryCode_Source,
                        Phone2CountryCode_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone2CountryCode_Source }),
                    Phone2CountryMasterRecordId = integrationDetail.Phone2CountryMasterRecordId,
                        Phone2CountryMasterRecordId_BusinessName = integrationDetail.Phone2CountryMasterRecordId_BusinessName,
                        Phone2CountryMasterRecordId_BusinessDescription = integrationDetail.Phone2CountryMasterRecordId_BusinessDescription,
                        Phone2CountryMasterRecordId_OriginalValue = integrationSource.Phone2CountryMasterRecordId,
                        Phone2CountryMasterRecordId_Status = integrationDetail.Phone2CountryMasterRecordId_Status,
                        Phone2CountryMasterRecordId_Source = integrationDetail.Phone2CountryMasterRecordId_Source,
                        Phone2CountryMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Phone2CountryMasterRecordId_Source, integrationDetail.Phone2CountryCode_Source }),

                    EmailAddress1 = integrationDetail.EmailAddress1,
                        EmailAddress1_BusinessName = integrationDetail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = integrationDetail.EmailAddress1_BusinessDescription,
                        EmailAddress1_OriginalValue = integrationSource.EmailAddress1,
                        EmailAddress1_Status = integrationDetail.EmailAddress1_Status,
                        EmailAddress1_Source = integrationDetail.EmailAddress1_Source,
                        EmailAddress1_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddress1_Source }),
                    EmailAddress1MasterRecordId = integrationDetail.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_BusinessName = integrationDetail.EmailAddress1MasterRecordId_BusinessName,
                        EmailAddress1MasterRecordId_BusinessDescription = integrationDetail.EmailAddress1MasterRecordId_BusinessDescription,
                        EmailAddress1MasterRecordId_OriginalValue = integrationSource.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_Status = integrationDetail.EmailAddress1MasterRecordId_Status,
                        EmailAddress1MasterRecordId_Source = integrationDetail.EmailAddress1MasterRecordId_Source,
                        EmailAddress1MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddress1MasterRecordId_Source, integrationDetail.EmailAddress1_Source }),

                    EmailAddress2 = integrationDetail.EmailAddress2,
                        EmailAddress2_BusinessName = integrationDetail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = integrationDetail.EmailAddress2_BusinessDescription,
                        EmailAddress2_OriginalValue = integrationSource.EmailAddress2,
                        EmailAddress2_Status = integrationDetail.EmailAddress2_Status,
                        EmailAddress2_Source = integrationDetail.EmailAddress2_Source,
                        EmailAddress2_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddress2_Source }),
                    EmailAddress2MasterRecordId = integrationDetail.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_BusinessName = integrationDetail.EmailAddress2MasterRecordId_BusinessName,
                        EmailAddress2MasterRecordId_BusinessDescription = integrationDetail.EmailAddress2MasterRecordId_BusinessDescription,
                        EmailAddress2MasterRecordId_OriginalValue = integrationSource.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_Status = integrationDetail.EmailAddress2MasterRecordId_Status,
                        EmailAddress2MasterRecordId_Source = integrationDetail.EmailAddress2MasterRecordId_Source,
                        EmailAddress2MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.EmailAddress2MasterRecordId_Source, integrationDetail.EmailAddress2_Source }),
                                            
                    Address1 = integrationDetail.Address1,
                        Address1_BusinessName = integrationDetail.Address1_BusinessName,
                        Address1_BusinessDescription = integrationDetail.Address1_BusinessDescription,
                        Address1_OriginalValue = integrationSource.Address1,
                        Address1_Status = integrationDetail.Address1_Status,
                        Address1_Source = integrationDetail.Address1_Source,
                        Address1_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Address1_Source }),

                    AddressMasterId = integrationDetail.AddressMasterId,
                        AddressMasterId_BusinessName = integrationDetail.AddressMasterId_BusinessName,
                        AddressMasterdId_BusinessDescription = integrationDetail.AddressMasterId_BusinessDescription,
                        AddressMasterId_OriginalValue = integrationSource.AddressMasterId,
                        AddressMasterId_Status = integrationDetail.AddressMasterId_Status,
                        AddressMasterId_Source = integrationDetail.AddressMasterId_Source,
                        AddressMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.AddressMasterId_Source, integrationDetail.Address1_Source }),

                    City = integrationDetail.City,
                        City_BusinessName = integrationDetail.City_BusinessName,
                        City_BusinessDescription = integrationDetail.City_BusinessDescription,
                        City_OriginalValue = integrationSource.City,
                        City_Status = integrationDetail.City_Status,
                        City_Source = integrationDetail.City_Source,
                        City_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.City_Source }),
                    
                    State = integrationDetail.State,
                        State_BusinessName = integrationDetail.State_BusinessName,
                        State_BusinessDescription = integrationDetail.State_BusinessDescription,
                        State_OriginalValue = integrationSource.State,
                        State_Status = integrationDetail.State_Status,
                        State_Source = integrationDetail.State_Source,
                        State_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.State_Source }),

                    StateMasterId = integrationDetail.State,
                        StateMasterId_BusinessName = integrationDetail.StateMasterId_BusinessName,
                        StateMasterId_BusinessDescription = integrationDetail.StateMasterId_BusinessDescription,
                        StateMasterId_OriginalValue = integrationSource.StateMasterId,
                        StateMasterId_Status = integrationDetail.StateMasterId_Status,
                        StateMasterId_Source = integrationDetail.StateMasterId_Source,
                        StateMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.StateMasterId_Source, integrationDetail.State_Source }),

                    PostalCode = integrationDetail.PostalCode,
                        PostalCode_BusinessName = integrationDetail.PostalCode_BusinessName,
                        PostalCode_BusinessDescription = integrationDetail.PostalCode_BusinessDescription,
                        PostalCode_OriginalValue = integrationSource.PostalCode,
                        PostalCode_Status = integrationDetail.PostalCode_Status,
                        PostalCode_Source = integrationDetail.PostalCode_Source,
                        PostalCode_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.PostalCode_Source }),


                    //DeliveryPointCode = integrationDetail.DeliveryPointCode,
                    //    DeliveryPointCode_BusinessName = integrationDetail.DeliveryPointCode_BusinessName,
                    //    DeliveryPointCode_BusinessDescription = integrationDetail.DeliveryPointCode_BusinessDescription,
                    //    DeliveryPointCode_OriginalValue = integrationSource.DeliveryPointCode,
                    //    DeliveryPointCode_Status = integrationDetail.DeliveryPointCode_Status,
                    //    DeliveryPointCode_Source = integrationDetail.DeliveryPointCode_Source,
                    //    DeliveryPointCode_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.DeliveryPointCode_Source }),

                    Country = integrationDetail.Country,
                        Country_BusinessName = integrationDetail.Country_BusinessName,
                        Country_BusinessDescription = integrationDetail.Country_BusinessDescription,
                        Country_OriginalValue = integrationSource.Country,
                        Country_Status = integrationDetail.Country_Status,
                        Country_Source = integrationDetail.Country_Source,
                        Country_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.Country_Source }),

                    CountryMasterRecordId = integrationDetail.Country,
                        CountryMasterRecordId_BusinessName = integrationDetail.CountryMasterRecordId_BusinessName,
                        CountryMasterRecordId_BusinessDescription = integrationDetail.CountryMasterRecordId_BusinessDescription,
                        CountryMasterRecordId_OriginalValue = integrationSource.CountryMasterRecordId,
                        CountryMasterRecordId_Status = integrationDetail.CountryMasterRecordId_Status,
                        CountryMasterRecordId_Source = integrationDetail.CountryMasterRecordId_Source,
                        CountryMasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { integrationDetail.CountryMasterRecordId_Source, integrationDetail.Country_Source }),

                    #endregion

                    #region Dropdowns
                    TitleList = GetTitleList(),
                    SuffixList = GetSuffixList(),
                    StudentList = GetStudentList(),
                    RelationshipList = GetRelationshipList(),
                    CountryCodeList = GetCountryDialingCodeList(),
                    StateList = GetStateList(),
                    CountryList = GetCountryList()
                    #endregion
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);
                    viewModel.HistoryData.Add(new StudentParentHistoryViewModel()
                    {
                       
                        FirstName = item.FirstName,
                            FirstName_Status = item.FirstName_Status,
                            FirstName_OriginalValue = previousitem.FirstName,
                        PreferredName = item.PreferredName,
                            PreferredName_Status = item.PreferredName_Status,
                            PreferredName_OriginalValue = previousitem.PreferredName,
                        LastName = item.LastName,
                            LastName_Status = item.LastName_Status,
                            LastName_OriginalValue = previousitem.LastName,

                        Suffix = item.Suffix,
                            Suffix_Status = item.Suffix_Status,
                            Suffix_OriginalValue = previousitem.Suffix,
                        SuffixMasterRecordId = item.SuffixMasterRecordId,
                            SuffixMasterRecordId_Status = item.SuffixMasterRecordId_Status,
                            SuffixMasterRecordId_OriginalValue = previousitem.SuffixMasterRecordId,

                        StudentId = item.StudentId,
                            StudentId_Status = item.StudentId_Status,
                            StudentId_OriginalValue = previousitem.StudentId,

                        StudentFirstName = item.StudentFirstName,
                            StudentFirstName_Status = item.StudentFirstName_Status,
                            StudentFirstName_OriginalValue = previousitem.StudentFirstName,
                        StudentLastName = item.StudentLastName,
                            StudentLastName_Status = item.StudentLastName_Status,
                            StudentLastName_OriginalValue = previousitem.StudentLastName,
                        StudentMasterRecordId = item.StudentMasterRecordId,
                            StudentMasterRecordId_Status = item.StudentMasterRecordId_Status,
                            StudentMasterRecordId_OriginalValue = previousitem.StudentMasterRecordId,

                        Relationship = item.Relationship,
                            Relationship_Status = item.Relationship_Status,
                            Relationship_OriginalValue = previousitem.Relationship,
                        RelationshipMasterRecordId = item.RelationshipMasterRecordId,
                            RelationshipMasterRecordId_Status = item.RelationshipMasterRecordId_Status,
                            RelationshipMasterRecordId_OriginalValue = previousitem.RelationshipMasterRecordId,

                        Phone1Number = item.Phone1Number,
                            Phone1Number_Status = item.Phone1Number_Status,
                            Phone1Number_OriginalValue = previousitem.Phone1Number,
                        Phone1MasterRecordId = item.Phone1MasterRecordId,
                            Phone1MasterRecordId_Status = item.Phone1MasterRecordId_Status,
                            Phone1MasterRecordId_OriginalValue = previousitem.Phone1MasterRecordId,

                        Phone1Extension = item.Phone1Extension,
                            Phone1Extension_Status = item.Phone1Extension_Status,
                            Phone1Extension_OriginalValue = previousitem.Phone1Extension,

                        Phone1CountryCode = item.Phone1CountryCode,
                            Phone1CountryCode_Status = item.Phone1CountryCode_Status,
                            Phone1CountryCode_OriginalValue = previousitem.Phone1CountryCode,
                        Phone1CountryMasterRecordId = item.Phone1CountryMasterRecordId,
                            Phone1CountryMasterRecordId_Status = item.Phone1CountryMasterRecordId_Status,
                            Phone1CountryMasterRecordId_OriginalValue = previousitem.Phone1CountryMasterRecordId,

                        Phone2Number = item.Phone2Number,
                            Phone2Number_Status = item.Phone2Number_Status,
                            Phone2Number_OriginalValue = previousitem.Phone2Number,
                        Phone2MasterRecordId = item.Phone2MasterRecordId,
                            Phone2MasterRecordId_Status = item.Phone2MasterRecordId_Status,
                            Phone2MasterRecordId_OriginalValue = previousitem.Phone2MasterRecordId,

                        Phone2Extension = item.Phone2Extension,
                            Phone2Extension_Status = item.Phone2Extension_Status,
                            Phone2Extension_OriginalValue = previousitem.Phone2Extension,

                        Phone2CountryCode = item.Phone2CountryCode,
                            Phone2CountryCode_Status = item.Phone2CountryCode_Status,
                            Phone2CountryCode_OriginalValue = previousitem.Phone2CountryCode,
                        Phone2CountryMasterRecordId = item.Phone2CountryMasterRecordId,
                            Phone2CountryMasterRecordId_Status = item.Phone2CountryMasterRecordId_Status,
                            Phone2CountryMasterRecordId_OriginalValue = previousitem.Phone2CountryMasterRecordId,

                        EmailAddress1 = item.EmailAddress1,
                            EmailAddress1_Status = item.EmailAddress1_Status,
                            EmailAddress1_OriginalValue = previousitem.EmailAddress1,
                        EmailAddress1MasterRecordId = item.EmailAddress1MasterRecordId,
                            EmailAddress1MasterRecordId_Status = item.EmailAddress1MasterRecordId_Status,
                            EmailAddress1MasterRecordId_OriginalValue = previousitem.EmailAddress1MasterRecordId,

                        EmailAddress2 = item.EmailAddress2,
                            EmailAddress2_Status = item.EmailAddress2_Status,
                            EmailAddress2_OriginalValue = previousitem.EmailAddress2,
                        EmailAddress2MasterRecordId = item.EmailAddress2MasterRecordId,
                            EmailAddress2MasterRecordId_Status = item.EmailAddress2MasterRecordId_Status,
                            EmailAddress2MasterRecordId_OriginalValue = previousitem.EmailAddress2MasterRecordId,

                        Address1 = item.Address1,
                            Address1_Status = item.Address1_Status,
                            Address1_OriginalValue = previousitem.Address1,
                        AddressMasterId = item.AddressMasterId,
                            AddressMasterId_Status = item.AddressMasterId_Status,
                            AddressMasterId_OriginalValue = previousitem.AddressMasterId,

                        PostalCode = item.PostalCode,
                            PostalCode_Status = item.PostalCode_Status,
                            PostalCode_OriginalValue = previousitem.PostalCode,

                        City = item.City,
                            City_Status = item.City_Status,
                            City_OriginalValue = previousitem.City,
                            
                        State = item.State,
                            State_Status = item.State_Status,
                            State_OriginalValue = previousitem.State,
                        StateMasterId = item.StateMasterId,
                            StateMasterId_Status = item.StateMasterId_Status,
                            StateMasterId_OriginalValue = previousitem.StateMasterId,

                        Country = item.Country,
                            Country_Status = item.Country_Status,
                            Country_OriginalValue = previousitem.Country,
                        CountryMasterRecordId = item.CountryMasterRecordId,
                            CountryMasterRecordId_Status = item.CountryMasterRecordId_Status,
                            CountryMasterRecordId_OriginalValue = previousitem.CountryMasterRecordId,

                        HistoryDate = item.RecordDate
                    });
                };
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Parent Match ***
        ******************************************************************/
        #region Match
        // GET: Student/StudentParentMatch
        public IActionResult StudentParentMatch(long Id, int SystemId)
        {
            try
            {
                var detail = _context.GetStudentParentMatchDetails(SystemId, Id);

                var viewModel = new StudentParentMatchViewModel()
                {
                    PageId = "studentParentPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "StudentParent",
                    Title = "Student Parent Matching",
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    Id = detail.Id,
                    Integration = detail.IntegrationName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationDate = detail.IntegrationDate,
                    System = detail.SystemName,
                    SystemId = detail.SystemId,
                    SourceRecordId = detail.SourceRecordId,
                    CreatedOnDT = detail.RecordDate,

                    FirstName = detail.FirstName,
                        FirstName_BusinessName = detail.FirstName_BusinessName,
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Weight = detail.FirstName_MatchWeight,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = detail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Weight = detail.PreferredName_MatchWeight,
                    LastName = detail.LastName,
                        LastName_BusinessName = detail.LastName_BusinessName,
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Weight = detail.LastName_MatchWeight,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Weight = detail.Suffix_MatchWeight,
                    StudentId = detail.StudentId,
                        StudentId_BusinessName = detail.StudentId_BusinessName,
                        StudentId_BusinessDescription = detail.StudentId_BusinessDescription,
                        StudentId_Weight = detail.StudentId_MatchWeight,
                    StudentFirstName = detail.StudentFirstName,
                        StudentFirstName_BusinessName = detail.StudentFirstName_BusinessName,
                        StudentFirstName_BusinessDescription = detail.StudentFirstName_BusinessDescription,
                        StudentFirstName_Weight = detail.StudentFirstName_MatchWeight,
                    StudentLastName = detail.StudentLastName,
                        StudentLastName_BusinessName = detail.StudentLastName_BusinessName,
                        StudentLastName_BusinessDescription = detail.StudentLastName_BusinessDescription,
                        StudentLastName_Weight = detail.StudentLastName_MatchWeight,
                    Relationship = detail.Relationship,
                        Relationship_BusinessName = detail.Relationship_BusinessName,
                        Relationship_BusinessDescription = detail.Relationship_BusinessDescription,
                        Relationship_Weight = detail.Relationship_MatchWeight,
                    Phone1CountryCode = detail.Phone1CountryCode,
                        Phone1CountryCode_BusinessName = detail.Phone1CountryCode_BusinessName,
                        Phone1CountryCode_BusinessDescription = detail.Phone1CountryCode_BusinessDescription,
                        Phone1CountryCode_Weight = detail.Phone1CountryCode_MatchWeight,
                    Phone1Number = detail.Phone1Number,
                        Phone1Number_BusinessName = detail.Phone1Number_BusinessName,
                        Phone1Number_BusinessDescription = detail.Phone1Number_BusinessDescription,
                        Phone1Number_Weight = detail.Phone1Number_MatchWeight,
                    Phone1Extension = detail.Phone1Extension,
                        Phone1Extension_BusinessName = detail.Phone1Extension_BusinessName,
                        Phone1Extension_BusinessDescription = detail.Phone1Extension_BusinessDescription,
                        Phone1Extension_Weight = detail.Phone1Extension_MatchWeight,
                    Phone2CountryCode = detail.Phone2CountryCode,
                        Phone2CountryCode_BusinessName = detail.Phone2CountryCode_BusinessName,
                        Phone2CountryCode_BusinessDescription = detail.Phone2CountryCode_BusinessDescription,
                        Phone2CountryCode_Weight = detail.Phone2CountryCode_MatchWeight,
                    Phone2Number = detail.Phone2Number,
                        Phone2Number_BusinessName = detail.Phone2Number_BusinessName,
                        Phone2Number_BusinessDescription = detail.Phone2Number_BusinessDescription,
                        Phone2Number_Weight = detail.Phone2Number_MatchWeight,
                    Phone2Extension = detail.Phone2Extension,
                        Phone2Extension_BusinessName = detail.Phone2Extension_BusinessName,
                        Phone2Extension_BusinessDescription = detail.Phone2Extension_BusinessDescription,
                        Phone2Extension_Weight = detail.Phone2Extension_MatchWeight,
                    EmailAddress1 = detail.EmailAddress1,
                        EmailAddress1_BusinessName = detail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = detail.EmailAddress1_BusinessDescription,
                        EmailAddress1_Weight = detail.EmailAddress1_MatchWeight,
                    EmailAddress2 = detail.EmailAddress2,
                        EmailAddress2_BusinessName = detail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = detail.EmailAddress2_BusinessDescription,
                        EmailAddress2_Weight = detail.EmailAddress2_MatchWeight,
                    Address1 = detail.Address1,
                        Address1_BusinessName = detail.Address1_BusinessName,
                        Address1_BusinessDescription = detail.Address1_BusinessDescription,
                        Address1_Weight = detail.Address1_MatchWeight,
                    City = detail.City,
                        City_BusinessName = detail.City_BusinessName,
                        City_BusinessDescription = detail.City_BusinessDescription,
                        City_Weight = detail.City_MatchWeight,
                    State = detail.State,
                        State_BusinessName = detail.State_BusinessName,
                        State_BusinessDescription = detail.State_BusinessDescription,
                        State_Weight = detail.State_MatchWeight,
                    PostalCode = detail.PostalCode,
                        PostalCode_BusinessName = detail.PostalCode_BusinessName,
                        PostalCode_BusinessDescription = detail.PostalCode_BusinessDescription,
                        PostalCode_Weight = detail.PostalCode_MatchWeight,
                    Country = detail.Country,
                        Country_BusinessName = detail.Country_BusinessName,
                        Country_BusinessDescription = detail.Country_BusinessDescription,
                        Country_Weight = detail.Country_MatchWeight
                };

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        public IActionResult GetParentPossibleMatchList(long Id, int SystemId)
        {
            try
            {
                var viewModel = new StudentParentPossibleMatchViewModel()
                {
                    PossibleMatches = new List<StudentParentPossibleMatchViewModel.StudentParentMatchSummaryViewModel>()
                };
                foreach (var possibleMatch in _context.GetStudentParentPossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new StudentParentPossibleMatchViewModel.StudentParentMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,
                        Name = $"{possibleMatch.FirstName} {possibleMatch.PreferredName} {possibleMatch.LastName}",
                        Suffix = possibleMatch.Suffix
                    });
                }
                return PartialView("StudentParentPossibleMatchList", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentGetPossibleMatchList");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Parent Manual Match ***
        ******************************************************************/
        #region ManualMatch
        // GET: Student/StudentParentManualMatch
        public async Task<IActionResult> StudentParentManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentManualMatch");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentParentList));
        }
        #endregion

        /*  *** Compare ***
        ******************************************************************/
        #region Compare
        // GET: Student/StudentParentCompare
        // Mike: public async Task<IActionResult> StudentParentCompare(int SystemId, long Id, string MasterId)
        public IActionResult ParentIsNotAtAllLikeComparison(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetStudentParentComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new StudentParentCompareViewModel()
                {
                    Id = Id,
                    SystemId = detail.SystemId,
                    MasterId = MasterId,

                    System = detail.SystemName,
                    IntegrationName = detail.IntegrationName,
                    SystemName = detail.SystemName,
                    IntegrationId = detail.IntegrationId,
                    SourceRecordId = detail.SourceRecordId,
                    IntegrationDate = detail.IntegrationDate,
                    
                    //StudentParentTitle = detail.Title,
                    //    StudentParentTitle_BusinessName = detail.Title_BusinessName,
                    //    StudentParentTitle_BusinessDescription = detail.Title_BusinessDescription,
                    //    StudentParentTitle_Compare = detail.Title_Compare,
                    //    StudentParentTitle_IsDifferent = detail.Title != detail.Title_Compare,
                    FirstName = detail.FirstName,
                        FirstName_BusinessName = detail.FirstName_BusinessName,
                        FirstName_BusinessDescription = detail.FirstName_BusinessDescription,
                        FirstName_Compare = detail.FirstName_Compare,
                        FirstName_IsDifferent = detail.FirstName != detail.FirstName_Compare,
                    PreferredName = detail.PreferredName,
                        PreferredName_BusinessName = detail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = detail.PreferredName_BusinessDescription,
                        PreferredName_Compare = detail.PreferredName_Compare,
                        PreferredName_IsDifferent = detail.PreferredName != detail.PreferredName_Compare,
                    LastName = detail.LastName,
                        LastName_BusinessName = detail.LastName_BusinessName,
                        LastName_BusinessDescription = detail.LastName_BusinessDescription,
                        LastName_Compare = detail.LastName_Compare,
                        LastName_IsDifferent = detail.LastName != detail.LastName_Compare,
                    Suffix = detail.Suffix,
                        Suffix_BusinessName = detail.Suffix_BusinessName,
                        Suffix_BusinessDescription = detail.Suffix_BusinessDescription,
                        Suffix_Compare = detail.Suffix_Compare,
                        Suffix_IsDifferent = detail.Suffix != detail.Suffix_Compare,
                    StudentId = detail.StudentId,
                        StudentId_BusinessName = detail.StudentId_BusinessName,
                        StudentId_BusinessDescription = detail.StudentId_BusinessDescription,
                        StudentId_Compare = detail.StudentId_Compare,
                        StudentId_IsDifferent = detail.StudentId != detail.StudentId_Compare,
                    StudentFirstName = detail.StudentFirstName,
                        StudentFirstName_BusinessName = detail.StudentFirstName_BusinessName,
                        StudentFirstName_BusinessDescription = detail.StudentFirstName_BusinessDescription,
                        StudentFirstName_Compare = detail.StudentFirstName_Compare,
                        StudentFirstName_IsDifferent = detail.StudentFirstName != detail.StudentFirstName_Compare,
                    StudentLastName = detail.StudentLastName,
                        StudentLastName_BusinessName = detail.StudentLastName_BusinessName,
                        StudentLastName_BusinessDescription = detail.StudentLastName_BusinessDescription,
                        StudentLastName_Compare = detail.StudentLastName_Compare,
                        StudentLastName_IsDifferent = detail.StudentLastName != detail.StudentLastName_Compare,
                    Relationship = detail.Relationship,
                        Relationship_BusinessName = detail.Relationship_BusinessName,
                        Relationship_BusinessDescription = detail.Relationship_BusinessDescription,
                        Relationship_Compare = detail.Relationship_Compare,
                        Relationship_IsDifferent = detail.Relationship != detail.Relationship_Compare,
                    Phone1Number = detail.Phone1Number,
                        Phone1Number_BusinessName = detail.Phone1Number_BusinessName,
                        Phone1Number_BusinessDescription = detail.Phone1Number_BusinessDescription,
                        Phone1Number_Compare = detail.Phone1Number_Compare,
                        Phone1Number_IsDifferent = detail.Phone1Number != detail.Phone1Number_Compare,
                    Phone1Extension = detail.Phone1Extension,
                        Phone1Extension_BusinessName = detail.Phone1Extension_BusinessName,
                        Phone1Extension_BusinessDescription = detail.Phone1Extension_BusinessDescription,
                        Phone1Extension_Compare = detail.Phone1Extension_Compare,
                        Phone1Extension_IsDifferent = detail.Phone1Extension != detail.Phone1Extension_Compare,
                    Phone1CountryCode = detail.Phone1CountryCode,
                        Phone1CountryCode_BusinessName = detail.Phone1CountryCode_BusinessName,
                        Phone1CountryCode_BusinessDescription = detail.Phone1CountryCode_BusinessDescription,
                        Phone1CountryCode_Compare = detail.Phone1CountryCode_Compare,
                        Phone1CountryCode_IsDifferent = detail.Phone1CountryCode != detail.Phone1CountryCode_Compare,
                    Phone2Number = detail.Phone2Number,
                        Phone2Number_BusinessName = detail.Phone2Number_BusinessName,
                        Phone2Number_BusinessDescription = detail.Phone2Number_BusinessDescription,
                        Phone2Number_Compare = detail.Phone2Number_Compare,
                        Phone2Number_IsDifferent = detail.Phone2Number != detail.Phone2Number_Compare,
                    Phone2Extension = detail.Phone2Extension,
                        Phone2Extension_BusinessName = detail.Phone2Extension_BusinessName,
                        Phone2Extension_BusinessDescription = detail.Phone2Extension_BusinessDescription,
                        Phone2Extension_Compare = detail.Phone2Extension_Compare,
                        Phone2Extension_IsDifferent = detail.Phone2Extension != detail.Phone2Extension_Compare,
                    Phone2CountryCode = detail.Phone2CountryCode,
                        Phone2CountryCode_BusinessName = detail.Phone2CountryCode_BusinessName,
                        Phone2CountryCode_BusinessDescription = detail.Phone2CountryCode_BusinessDescription,
                        Phone2CountryCode_Compare = detail.Phone2CountryCode_Compare,
                        Phone2CountryCode_IsDifferent = detail.Phone2CountryCode != detail.Phone2CountryCode_Compare,
                    EmailAddress1 = detail.EmailAddress1,
                        EmailAddress1_BusinessName = detail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = detail.EmailAddress1_BusinessDescription,
                        EmailAddress1_Compare = detail.EmailAddress1_Compare,
                        EmailAddress1_IsDifferent = detail.EmailAddress1 != detail.EmailAddress1_Compare,
                    EmailAddress2 = detail.EmailAddress2,
                        EmailAddress2_BusinessName = detail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = detail.EmailAddress2_BusinessDescription,
                        EmailAddress2_Compare = detail.EmailAddress2_Compare,
                        EmailAddress2_IsDifferent = detail.EmailAddress2 != detail.EmailAddress2_Compare,
                    SystemRecords = detail.SystemRecords.ToList()
                };

                return PartialView("StudentParentComparison", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve StudentParentCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Parent Save ***
        ******************************************************************/
        #region Parent Save
        private void _parentSaveIntegrationRecord(StudentParentViewModel model)
        {
            _context.StudentParentChangeIntegrationRecord(model.SystemId, model.Id,                       
                        model.FirstName, model.PreferredName, model.LastName,
                        null, null,
                        model.Suffix, null, model.SuffixMasterRecordId,
                        model.StudentId, null, model.StudentFirstName, model.StudentLastName, model.StudentMasterRecordId,
                        model.Relationship, null, model.RelationshipMasterRecordId,
                        model.Phone1Number, model.Phone1MasterRecordId, model.Phone1Extension, model.Phone1CountryCode, model.Phone1CountryMasterRecordId,
                        model.Phone2Number, model.Phone2MasterRecordId, model.Phone2Extension, model.Phone2CountryCode, model.Phone2CountryMasterRecordId,
                        model.EmailAddress1, model.EmailAddress1MasterRecordId,
                        model.EmailAddress2, model.EmailAddress2MasterRecordId,
                        null, model.Address1, null,
                        model.City,
                        model.State, model.StateMasterId,
                        model.PostalCode, null,
                        model.Country, model.CountryMasterRecordId,
                        User.Identity.Name);
        }
        // POST: Student/StudentParentSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentParentSave(StudentParentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _parentSaveIntegrationRecord(model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentParentSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentParentList));
            }
            else
            {
                model.Title = "Student Parent";
                model.PageId = "studentParentPage";
                model.ActiveClass = "StudentParent";
                model.Message = "Your Student Parent Page";
                model.User = User.Identity.Name;
                model.NavigationGroups = GetNavigationGroups();

                return View(nameof(StudentParentEdit), model);
            }


        }
        #endregion

        /*  *** Student Revalidate ***
        ******************************************************************/
        #region Parent Revalidate
        // POST: Student/StudentParentRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult StudentParentRevalidate(StudentParentViewModel model)
        {
            if (model.IsValid())
            {
                try
                {

                    // Check if Dropdown have MasterId - If they dont, empty out the associated attributes per dropdown before revalidating
                    // TitleMasterRecordId
                    //if (string.IsNullOrEmpty(model.TitleMasterRecordId) && !string.IsNullOrEmpty(model.StudentParentTitle))
                    //{
                    //    model.StudentParentTitle = null;

                    //    model.IsChanged = true;
                    //}

                    // SuffixMasterRecordId
                    if (string.IsNullOrEmpty(model.SuffixMasterRecordId) && !string.IsNullOrEmpty(model.Suffix))
                    {
                        model.Suffix = null;

                        model.IsChanged = true;
                    }

                    // StudentMasterId
                    if (string.IsNullOrEmpty(model.StudentMasterRecordId) && (!string.IsNullOrEmpty(model.StudentId)))
                    {
                        model.StudentMasterRecordId = null;

                        model.IsChanged = true;
                    }

                    // RelationshipMasterRecordId
                    if (string.IsNullOrEmpty(model.RelationshipMasterRecordId) && !string.IsNullOrEmpty(model.Relationship))
                    {
                        model.RelationshipMasterRecordId = null;

                        model.IsChanged = true;
                    }

                    // Phone1CountryMasterRecordId
                    if (string.IsNullOrEmpty(model.Phone1CountryMasterRecordId) && (!string.IsNullOrEmpty(model.Phone1CountryCode)))
                    {
                        model.Phone1CountryCode = null;

                        model.IsChanged = true;
                    }

                    // Phone2CountryMasterRecordId
                    if (string.IsNullOrEmpty(model.Phone2CountryMasterRecordId) && (!string.IsNullOrEmpty(model.Phone2CountryCode)))
                    {
                        model.Phone2CountryCode = null;

                        model.IsChanged = true;
                    }

                    if (model.IsChanged)
                    {
                        _parentSaveIntegrationRecord(model);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to retrieve StudentParentRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(StudentParentList));
            }
            return View(model);
        }
        #endregion
        
        /*  *** Ignore/Remove ***
        ******************************************************************/
        #region Ignore/Remove
        public IActionResult StudentParentIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load StudentParentIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(StudentParentList));
        }
        #endregion


        #endregion

    }
}