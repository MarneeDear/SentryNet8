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
    [AuthorizeHumanResources]
    public class EmployeeController : IntegrationController
    {
        public const string READONLY = "Not Provided";
        public EmployeeController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<EmployeeController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        #region Employee

        /*  *** List View ***
        ******************************************************************/
        #region List
        // GET: Employee/EmployeeList
        public IActionResult EmployeeList()
        {
            var model = new EmployeeListViewModel()
            {
                Title = "Employee",
                PageId = "employeePage",
                ActiveClass = "Employee",
                Message = "Your Employee Page",
                Integration = "Employee",
                IntegrationId = 3,
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<EmployeeRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetEmployeeList(AjaxDataTableRequest request)
        {
            try
            {
                var employees = _context.EmployeeRemediationList.AsQueryable();

                int recordsTotal = employees.Count();

                var employeeList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? employees
                                                : employees.Where(s => s.FirstName.Contains(request.searchValue) ||
                                                                       s.MiddleName.Contains(request.searchValue) ||
                                                                       s.LastName.Contains(request.searchValue) ||
                                                                       s.UAPersonId.Contains(request.searchValue) ||
                                                                       s.ErrorCategories.Contains(request.searchValue) ||
                                                                       s.SystemName.Contains(request.searchValue))
                                         )
                                         .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                         .ToListAsync();

                int recordsFiltered = employeeList.Count();

                var employeeRemediationList = new List<EmployeeRemediationListItemViewModel>();

                foreach (var item in employeeList.Skip(request.start).Take(request.length))
                {
                    employeeRemediationList.Add(new EmployeeRemediationListItemViewModel()
                    {
                        Id = item.Id.ToString(),
                        SystemId = item.SystemId,
                        SystemName = item.SystemName,
                        IntegrationId = item.IntegrationId,
                        IntegrationDate = item.IntegrationDate,
                        ErrorCategories = item.ErrorCategories,
                        ErrorCount = item.ErrorCount,

                        Name = string.Format("{0} {1} {2}", item.FirstName, item.MiddleName, item.LastName),
                        UAPersonId = item.UAPersonId,

                        RecordStatus = item.RecordStatus,
                        CreatedDate = item.CreatedDate,
                    });
                }

                var data = employeeRemediationList.ToList();

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
                _logger.LogError(ex, "Unable to retrieve EmployeeList details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Edit View ***
        ******************************************************************/
        #region Edit
        // GET: Employee/EmployeeEdit
        public IActionResult EmployeeEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetEmployeeHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var employeeDetail = history.First();
                var employeeSource = history.Last();

                var viewModel = new EmployeeViewModel()
                {
                    Id = employeeDetail.Id,
                    Title = "Employee",
                    PageId = "employeePage",
                    ActiveClass = "Employee",
                    PageWrapperClass = "toggled",
                    System = employeeDetail.SystemName,
                    SystemId = employeeDetail.SystemId,
                    Integration = employeeDetail.IntegrationName,
                    IntegrationId = employeeDetail.IntegrationId,
                    SourceRecordId = employeeDetail.SourceRecordId,
                    RecordStatus = employeeDetail.RecordStatus,
                    CreatedOnDT = employeeDetail.IntegrationDate,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    IsChanged = false,
                    HistoryData = new List<EmployeeHistoryViewModel>(),

                    #region Bio/Dem Details
                    // Bio/Dem Details
                    FirstName = employeeDetail.FirstName,
                        FirstName_BusinessName = "First Name",
                        FirstName_BusinessDescription = "The employee's first name",
                        FirstName_Source = employeeDetail.FirstName_Source,
                        FirstName_OriginalValue = employeeSource.FirstName,
                        FirstName_Category = employeeDetail.FirstName_Category,
                        FirstName_Status = employeeDetail.FirstName_Status,
                        FirstName_AttributeId = employeeDetail.FirstName_AttributeId,
                        FirstName_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.FirstName_Source }),
                    PreferredName = employeeDetail.PreferredName,
                        PreferredName_BusinessName = "Preferred Name",
                        PreferredName_BusinessDescription = employeeDetail.PreferredName_BusinessDescription,
                        PreferredName_Source = employeeDetail.PreferredName_Source,
                        PreferredName_OriginalValue = employeeSource.PreferredName,
                        PreferredName_Category = employeeDetail.PreferredName_Category,
                        PreferredName_Status = employeeDetail.PreferredName_Status,
                        PreferredName_AttributeId = employeeDetail.PreferredName_AttributeId,
                        PreferredName_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.PreferredName_Source }),
                    MiddleName = employeeDetail.MiddleName,
                        MiddleName_BusinessName = "Middle Name",
                        MiddleName_BusinessDescription = "The employee's middle name",
                        MiddleName_Source = employeeDetail.MiddleName_Source,
                        MiddleName_OriginalValue = employeeSource.MiddleName,
                        MiddleName_Category = employeeDetail.MiddleName_Category,
                        MiddleName_Status = employeeDetail.MiddleName_Status,
                        MiddleName_AttributeId = employeeDetail.MiddleName_AttributeId,
                        MiddleName_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.MiddleName_Source }),
                    LastName = employeeDetail.LastName,
                        LastName_BusinessName = "Last Name",
                        LastName_BusinessDescription = "The employee's last name",
                        LastName_Source = employeeDetail.LastName_Source,
                        LastName_OriginalValue = employeeSource.LastName,
                        LastName_Category = employeeDetail.LastName_Category,
                        LastName_Status = employeeDetail.LastName_Status,
                        LastName_AttributeId = employeeDetail.LastName_AttributeId,
                        LastName_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.LastName_Source }),
                    MaidenName = employeeDetail.MaidenName,
                        MaidenName_BusinessName = "Maiden Name",
                        MaidenName_BusinessDescription = "The employee's maiden name",
                        MaidenName_Source = employeeDetail.MaidenName_Source,
                        MaidenName_OriginalValue = employeeSource.MaidenName,
                        MaidenName_Category = employeeDetail.MaidenName_Category,
                        MaidenName_Status = employeeDetail.MaidenName_Status,
                        MaidenName_AttributeId = employeeDetail.MaidenName_AttributeId,
                        MaidenName_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.MaidenName_Source }),
                    Suffix = employeeDetail.Suffix,
                        Suffix_BusinessName = employeeDetail.Suffix_BusinessName,
                        Suffix_BusinessDescription = employeeDetail.Suffix_BusinessDescription,
                        Suffix_Source = employeeDetail.Suffix_Source,
                        Suffix_OriginalValue = employeeSource.Suffix,
                        Suffix_Category = employeeDetail.Suffix_Category,
                        Suffix_Status = employeeDetail.Suffix_Status,
                        Suffix_AttributeId = employeeDetail.Suffix_AttributeId,
                    SuffixSourceSystemRecordId = employeeDetail.SuffixSourceSystemRecordId,
                        SuffixSourceSystemRecordId_BusinessName = employeeDetail.SuffixSourceSystemRecordId_BusinessName,
                        SuffixSourceSystemRecordId_BusinessDescription = employeeDetail.SuffixSourceSystemRecordId_BusinessDescription,
                        SuffixSourceSystemRecordId_AttributeId = employeeDetail.SuffixSourceSystemRecordId_AttributeId,
                        SuffixSourceSystemRecordId_OriginalValue = employeeSource.SuffixSourceSystemRecordId,
                        SuffixSourceSystemRecordId_Status = employeeDetail.SuffixSourceSystemRecordId_Status,
                        SuffixSourceSystemRecordId_Source = employeeDetail.SuffixSourceSystemRecordId_Source,
                    SuffixMasterId = employeeDetail.SuffixMasterId,
                        SuffixMasterId_BusinessName = employeeDetail.SuffixMasterId_BusinessName,
                        SuffixMasterId_BusinessDescription = employeeDetail.SuffixMasterId_BusinessDescription,
                        SuffixMasterId_AttributeId = employeeDetail.SuffixMasterId_AttributeId,
                        SuffixMasterId_OriginalValue = employeeSource.SuffixMasterId,
                        SuffixMasterId_Status = employeeDetail.SuffixMasterId_Status,
                        SuffixMasterId_Source = employeeDetail.SuffixMasterId_Source,
                        SuffixMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.SuffixMasterId_Source, employeeDetail.SuffixSourceSystemRecordId_Source }),
                    BirthDate = employeeDetail.BirthDate,
                        BirthDate_BusinessName = employeeDetail.BirthDate_BusinessName,
                        BirthDate_BusinessDescription = employeeDetail.BirthDate_BusinessDescription,
                        BirthDate_Source = employeeDetail.BirthDate_Source,
                        BirthDate_OriginalValue = employeeDetail.BirthDate,
                        BirthDate_Category = employeeDetail.BirthDate_Category,
                        BirthDate_Status = employeeDetail.BirthDate_Status,
                        BirthDate_AttributeId = employeeDetail.BirthDate_AttributeId,
                        BirthDate_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.BirthDate_Source }),
                    DeceasedDate = employeeDetail.DeceasedDate,
                        DeceasedDate_BusinessName = employeeDetail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = employeeDetail.DeceasedDate_BusinessDescription,
                        DeceasedDate_Source = employeeDetail.DeceasedDate_Source,
                        DeceasedDate_OriginalValue = employeeDetail.DeceasedDate,
                        DeceasedDate_Category = employeeDetail.DeceasedDate_Category,
                        DeceasedDate_Status = employeeDetail.DeceasedDate_Status,
                        DeceasedDate_AttributeId = employeeDetail.DeceasedDate_AttributeId,
                        DeceasedDate_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.DeceasedDate_Source }),
                    MaritalStatus = employeeDetail.MaritalStatus,
                        MaritalStatus_BusinessName = employeeDetail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = employeeDetail.MaritalStatus_BusinessDescription,
                        MaritalStatus_Source = employeeDetail.MaritalStatus_Source,
                        MaritalStatus_OriginalValue = employeeSource.MaritalStatus,
                        MaritalStatus_Category = employeeDetail.MaritalStatus_Category,
                        MaritalStatus_Status = employeeDetail.MaritalStatus_Status,
                        MaritalStatus_AttributeId = employeeDetail.MaritalStatus_AttributeId,
                    MaritalStatusSourceSystemRecordId = employeeDetail.MaritalStatusSourceSystemRecordId,
                        MaritalStatusSourceSystemRecordId_BusinessName = employeeDetail.MaritalStatusSourceSystemRecordId_BusinessName,
                        MaritalStatusSourceSystemRecordId_BusinessDescription = employeeDetail.MaritalStatusSourceSystemRecordId_BusinessDescription,
                        MaritalStatusSourceSystemRecordId_AttributeId = employeeDetail.MaritalStatusSourceSystemRecordId_AttributeId,
                        MaritalStatusSourceSystemRecordId_OriginalValue = employeeSource.MaritalStatusSourceSystemRecordId,
                        MaritalStatusSourceSystemRecordId_Status = employeeDetail.MaritalStatusSourceSystemRecordId_Status,
                        MaritalStatusSourceSystemRecordId_Source = employeeDetail.MaritalStatusSourceSystemRecordId_Source,
                    MaritalStatusMasterId = employeeDetail.MaritalStatusMasterId,
                        MaritalStatusMasterId_BusinessName = employeeDetail.MaritalStatusMasterId_BusinessName,
                        MaritalStatusMasterId_BusinessDescription = employeeDetail.MaritalStatusMasterId_BusinessDescription,
                        MaritalStatusMasterId_AttributeId = employeeDetail.MaritalStatusMasterId_AttributeId,
                        MaritalStatusMasterId_OriginalValue = employeeSource.MaritalStatusMasterId,
                        MaritalStatusMasterId_Status = employeeDetail.MaritalStatusMasterId_Status,
                        MaritalStatusMasterId_Source = employeeDetail.MaritalStatusMasterId_Source,
                        MaritalStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.MaritalStatusMasterId_Source, employeeDetail.MaritalStatusSourceSystemRecordId_Source, employeeDetail.MaritalStatus_Source }),
                    EmailAddress1 = employeeDetail.EmailAddress1,
                        EmailAddress1_BusinessName = employeeDetail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = employeeDetail.EmailAddress1_BusinessDescription,
                        EmailAddress1_AttributeId = employeeDetail.EmailAddress1_AttributeId,
                        EmailAddress1_OriginalValue = employeeSource.EmailAddress1,
                        EmailAddress1_Status = employeeDetail.EmailAddress1_Status,
                        EmailAddress1_Source = employeeDetail.EmailAddress1_Source,
                    EmailAddress1MasterRecordId = employeeDetail.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_BusinessName = employeeDetail.EmailAddress1MasterRecordId_BusinessName,
                        EmailAddress1MasterRecordId_BusinessDescription = employeeDetail.EmailAddress1MasterRecordId_BusinessDescription,
                        EmailAddress1MasterRecordId_AttributeId = employeeDetail.EmailAddress1MasterRecordId_AttributeId,
                        EmailAddress1MasterRecordId_OriginalValue = employeeSource.EmailAddress1MasterRecordId,
                        EmailAddress1MasterRecordId_Status = employeeDetail.EmailAddress1MasterRecordId_Status,
                        EmailAddress1MasterRecordId_Source = employeeDetail.EmailAddress1MasterRecordId_Source,
                        EmailAddress1MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.EmailAddress1MasterRecordId_Source, employeeDetail.EmailAddress1_Source }),
                    EmailAddress2 = employeeDetail.EmailAddress2,
                        EmailAddress2_BusinessName = employeeDetail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = employeeDetail.EmailAddress2_BusinessDescription,
                        EmailAddress2_AttributeId = employeeDetail.EmailAddress2_AttributeId,
                        EmailAddress2_OriginalValue = employeeSource.EmailAddress2,
                        EmailAddress2_Status = employeeDetail.EmailAddress2_Status,
                        EmailAddress2_Source = employeeDetail.EmailAddress2_Source,
                    EmailAddress2MasterRecordId = employeeDetail.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_BusinessName = employeeDetail.EmailAddress2MasterRecordId_BusinessName,
                        EmailAddress2MasterRecordId_BusinessDescription = employeeDetail.EmailAddress2MasterRecordId_BusinessDescription,
                        EmailAddress2MasterRecordId_AttributeId = employeeDetail.EmailAddress2MasterRecordId_AttributeId,
                        EmailAddress2MasterRecordId_OriginalValue = employeeSource.EmailAddress2MasterRecordId,
                        EmailAddress2MasterRecordId_Status = employeeDetail.EmailAddress2MasterRecordId_Status,
                        EmailAddress2MasterRecordId_Source = employeeDetail.EmailAddress2MasterRecordId_Source,
                        EmailAddress2MasterRecordId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.EmailAddress2MasterRecordId_Source, employeeDetail.EmailAddress2_Source }),
                    NetId = employeeDetail.NetId,
                        NetId_BusinessName = employeeDetail.NetId_BusinessName,
                        NetId_BusinessDescription = employeeDetail.NetId_BusinessDescription,
                        NetId_AttributeId = employeeDetail.NetId_AttributeId,
                        NetId_OriginalValue = employeeSource.NetId,
                        NetId_Status = employeeDetail.NetId_Status,
                        NetId_Source = employeeDetail.NetId_Source,
                        NetId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.NetId_Source, employeeDetail.NetId_Source }),

                    #endregion

                    #region Employee Details
                    // Employee Details
                    UAPersonId = employeeDetail.UAPersonId,
                        UAPersonId_OriginalValue = employeeSource.UAPersonId,
                        UAPersonId_BusinessName = employeeDetail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = employeeDetail.UAPersonId_BusinessDescription,
                        UAPersonId_AttributeId = employeeDetail.UAPersonId_AttributeId,
                        UAPersonId_Category = employeeDetail.UAPersonId_Category,
                        UAPersonId_Status = employeeDetail.UAPersonId_Status,
                        UAPersonId_Source = employeeDetail.UAPersonId_Source,
                        UAPersonId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.UAPersonId_Source }),
                    EmployeeConstituentId = employeeDetail.EmployeeConstituentId,
                        EmployeeConstituentId_OriginalValue = employeeSource.EmployeeConstituentId,
                        EmployeeConstituentId_BusinessName = employeeDetail.EmployeeConstituentId_BusinessName,
                        EmployeeConstituentId_BusinessDescription = employeeDetail.EmployeeConstituentId_BusinessDescription,
                        EmployeeConstituentId_AttributeId = employeeDetail.EmployeeConstituentId_AttributeId,
                        EmployeeConstituentId_Category = employeeDetail.EmployeeConstituentId_Category,
                        EmployeeConstituentId_Status = employeeDetail.EmployeeConstituentId_Status,
                        EmployeeConstituentId_Source = employeeDetail.EmployeeConstituentId_Source,
                        EmployeeConstituentId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.EmployeeConstituentId_Source }),
                    HireDate = employeeDetail.HireDate,
                        HireDate_BusinessName = employeeDetail.HireDate_BusinessName,
                        HireDate_BusinessDescription = employeeDetail.HireDate_BusinessDescription,
                        HireDate_Source = employeeDetail.HireDate_Source,
                        HireDate_OriginalValue = employeeSource.HireDate,
                        HireDate_Category = employeeDetail.HireDate_Category,
                        HireDate_Status = employeeDetail.HireDate_Status,
                        HireDate_AttributeId = employeeDetail.HireDate_AttributeId,
                        HireDate_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.HireDate_Source }),
                    TerminationDate = employeeDetail.TerminationDate,
                        TerminationDate_BusinessName = employeeDetail.TerminationDate_BusinessName,
                        TerminationDate_BusinessDescription = employeeDetail.TerminationDate_BusinessDescription,
                        TerminationDate_Source = employeeDetail.TerminationDate_Source,
                        TerminationDate_OriginalValue = employeeSource.TerminationDate,
                        TerminationDate_Category = employeeDetail.TerminationDate_Category,
                        TerminationDate_Status = employeeDetail.TerminationDate_Status,
                        TerminationDate_AttributeId = employeeDetail.TerminationDate_AttributeId,
                        TerminationDate_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.TerminationDate_Source }),
                    #endregion

                    #region Employment Details
                    // Employment Details
                    OrganizationName = employeeDetail.OrganizationName,
                        OrganizationName_BusinessName = employeeDetail.OrganizationName_BusinessName,
                        OrganizationName_BusinessDescription = employeeDetail.OrganizationName_BusinessDescription,
                        OrganizationName_Source = employeeDetail.OrganizationName_Source,
                        OrganizationName_OriginalValue = employeeSource.OrganizationName,
                        OrganizationName_Category = employeeDetail.OrganizationName_Category,
                        OrganizationName_Status = employeeDetail.OrganizationName_Status,
                        OrganizationName_AttributeId = employeeDetail.OrganizationName_AttributeId,
                    OrganizationSourceSystemRecordId = employeeDetail.OrganizationSourceSystemRecordId,
                        OrganizationSourceSystemRecordId_BusinessName = employeeDetail.OrganizationSourceSystemRecordId_BusinessName,
                        OrganizationSourceSystemRecordId_BusinessDescription = employeeDetail.OrganizationSourceSystemRecordId_BusinessDescription,
                        OrganizationSourceSystemRecordId_Source = employeeDetail.OrganizationSourceSystemRecordId_Source,
                        OrganizationSourceSystemRecordId_OriginalValue = employeeSource.OrganizationSourceSystemRecordId,
                        OrganizationSourceSystemRecordId_Category = employeeDetail.OrganizationSourceSystemRecordId_Category,
                        OrganizationSourceSystemRecordId_Status = employeeDetail.OrganizationSourceSystemRecordId_Status,
                        OrganizationSourceSystemRecordId_AttributeId = employeeDetail.OrganizationSourceSystemRecordId_AttributeId,
                    OrganizationMasterId = employeeDetail.OrganizationMasterId,
                        OrganizationMasterId_BusinessName = employeeDetail.OrganizationMasterId_BusinessName,
                        OrganizationMasterId_BusinessDescription = employeeDetail.OrganizationMasterId_BusinessDescription,
                        OrganizationMasterId_Source = employeeDetail.OrganizationMasterId_Source,
                        OrganizationMasterId_OriginalValue = employeeSource.OrganizationMasterId,
                        OrganizationMasterId_Category = employeeDetail.OrganizationMasterId_Category,
                        OrganizationMasterId_Status = employeeDetail.OrganizationMasterId_Status,
                        OrganizationMasterId_AttributeId = employeeDetail.OrganizationMasterId_AttributeId,
                        OrganizationMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.OrganizationMasterId_Source, employeeDetail.OrganizationSourceSystemRecordId_Source, employeeDetail.OrganizationName_Source }),
                    EmployeeType = employeeDetail.EmployeeType,
                        EmployeeType_BusinessName = employeeDetail.EmployeeType_BusinessName,
                        EmployeeType_BusinessDescription = employeeDetail.EmployeeType_BusinessDescription,
                        EmployeeType_Source = employeeDetail.EmployeeType_Source,
                        EmployeeType_OriginalValue = employeeSource.EmployeeType,
                        EmployeeType_Category = employeeDetail.EmployeeType_Category,
                        EmployeeType_Status = employeeDetail.EmployeeType_Status,
                        EmployeeType_AttributeId = employeeDetail.EmployeeType_AttributeId,
                    EmployeeTypeName = employeeDetail.EmployeeType,
                        EmployeeTypeName_BusinessName = employeeDetail.EmployeeType_BusinessName,
                        EmployeeTypeName_BusinessDescription = employeeDetail.EmployeeType_BusinessDescription,
                        EmployeeTypeName_Source = employeeDetail.EmployeeType_Source,
                        EmployeeTypeName_OriginalValue = employeeSource.EmployeeType,
                        EmployeeTypeName_Category = employeeDetail.EmployeeType_Category,
                        EmployeeTypeName_Status = employeeDetail.EmployeeType_Status,
                        EmployeeTypeName_AttributeId = employeeDetail.EmployeeType_AttributeId,
                    EmployeeTypeSourceSystemRecordId = employeeDetail.EmployeeTypeSourceSystemRecordId,
                        EmployeeTypeSourceSystemRecordId_BusinessName = employeeDetail.EmployeeTypeSourceSystemRecordId_BusinessName,
                        EmployeeTypeSourceSystemRecordId_BusinessDescription = employeeDetail.EmployeeTypeSourceSystemRecordId_BusinessDescription,
                        EmployeeTypeSourceSystemRecordId_Source = employeeDetail.EmployeeTypeSourceSystemRecordId_Source,
                        EmployeeTypeSourceSystemRecordId_OriginalValue = employeeSource.EmployeeTypeSourceSystemRecordId,
                        EmployeeTypeSourceSystemRecordId_Category = employeeDetail.EmployeeTypeSourceSystemRecordId_Category,
                        EmployeeTypeSourceSystemRecordId_Status = employeeDetail.EmployeeTypeSourceSystemRecordId_Status,
                        EmployeeTypeSourceSystemRecordId_AttributeId = employeeDetail.EmployeeTypeSourceSystemRecordId_AttributeId,
                    EmployeeTypeMasterId = employeeDetail.EmployeeTypeMasterId,
                        EmployeeTypeMasterId_BusinessName = employeeDetail.EmployeeTypeMasterId_BusinessName,
                        EmployeeTypeMasterId_BusinessDescription = employeeDetail.EmployeeTypeMasterId_BusinessDescription,
                        EmployeeTypeMasterId_Source = employeeDetail.EmployeeTypeMasterId_Source,
                        EmployeeTypeMasterId_OriginalValue = employeeSource.EmployeeTypeMasterId,
                        EmployeeTypeMasterId_Category = employeeDetail.EmployeeTypeMasterId_Category,
                        EmployeeTypeMasterId_Status = employeeDetail.EmployeeTypeMasterId_Status,
                        EmployeeTypeMasterId_AttributeId = employeeDetail.EmployeeTypeMasterId_AttributeId,
                        EmployeeTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { employeeDetail.EmployeeTypeMasterId_Source, employeeDetail.EmployeeTypeSourceSystemRecordId_Source, employeeDetail.EmployeeType_Source }),
                    #endregion

                    //TitleList = GetTitleList(),
                    SuffixList = GetSuffixList(),
                    MaritalStatusList = GetMaritalStatusList(),
                    OrganizationList = GetOrganizationList(),
                    EmployeeTypeList = GetEmployeeTypeList().ToList(),
                };

                #region History
                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new EmployeeHistoryViewModel()
                    {
                        FirstName = item.FirstName,
                            FirstName_Status = item.FirstName_Status,
                        PreferredName = item.PreferredName,
                            PreferredName_Status = item.PreferredName_Status,
                        MiddleName = item.MiddleName,
                            MiddleName_Status = item.MiddleName_Status,
                        LastName = item.LastName,
                            LastName_Status = item.LastName_Status,
                        MaidenName = item.MaidenName,
                            MaidenName_Status = item.MaidenName_Status,
                        UAPersonId = item.UAPersonId,
                            UAPersonId_Status = item.UAPersonId_Status,
                        //EmployeeTitle = item.Title,
                        //    EmployeeTitle_Status = item.Title_Status,
                        //TitleSourceSystemRecordId = item.TitleSourceSystemRecordId,
                        //    TitleSourceSystemRecordId_Status = item.TitleSourceSystemRecordId_Status,
                        //TitleMasterId = item.TitleMasterId,
                        //    TitleMasterId_Status = item.TitleMasterId_Status,
                        Suffix = item.Suffix,
                            Suffix_Status = item.Suffix_Status,
                        SuffixSourceSystemRecordId = item.SuffixSourceSystemRecordId,
                            SuffixSourceSystemRecordId_Status = item.SuffixSourceSystemRecordId_Status,
                        SuffixMasterId = item.SuffixMasterId,
                            SuffixMasterId_Status = item.SuffixMasterId_Status,
                        BirthDate = item.BirthDate,
                            BirthDate_Status = item.BirthDate_Status,
                        DeceasedDate = item.DeceasedDate,
                            DeceasedDate_Status = item.DeceasedDate_Status,
                        MaritalStatus = item.MaritalStatus,
                            MaritalStatus_Status = item.MaritalStatus_Status,
                        MaritalStatusSourceSystemRecordId = item.MaritalStatusSourceSystemRecordId,
                            MaritalStatusSourceSystemRecordId_Status = item.MaritalStatusSourceSystemRecordId_Status,
                        MaritalStatusMasterId = item.MaritalStatusSourceSystemRecordId,
                            MaritalStatusMasterId_Status = item.MaritalStatusSourceSystemRecordId_Status,
                        Organization = item.OrganizationName,
                            Organization_Status = item.OrganizationName_Status,
                        OrganizationName = item.OrganizationName,
                            OrganizationName_Status = item.OrganizationName_Status,
                        OrganizationSourceSystemRecordId = item.OrganizationSourceSystemRecordId,
                            OrganizationSourceSystemRecordId_Status = item.OrganizationSourceSystemRecordId_Status,
                        OrganizationMasterId = item.OrganizationMasterId,
                            OrganizationMasterId_Status = item.OrganizationMasterId_Status,
                        HireDate = item.HireDate,
                            HireDate_Status = item.HireDate_Status,
                        TerminationDate = item.TerminationDate,
                            TerminationDate_Status = item.TerminationDate_Status,
                        EmployeeType = item.EmployeeType,
                            EmployeeType_Status = item.EmployeeType_Status,
                        EmployeeTypeName = item.EmployeeType,
                            EmployeeTypeName_Status = item.EmployeeType_Status,
                        EmployeeTypeSourceSystemRecordId = item.EmployeeTypeSourceSystemRecordId,
                            EmployeeTypeSourceSystemRecordId_Status = item.EmployeeTypeSourceSystemRecordId_Status,
                        EmployeeTypeMasterId = item.EmployeeTypeMasterId,
                            EmployeeTypeMasterId_Status = item.EmployeeTypeMasterId_Status,
                        EmailAddress1 = item.EmailAddress1,
                            EmailAddress1_Status = item.EmailAddress1_Status,
                        EmailAddress1MasterRecordId = item.EmailAddress1MasterRecordId,
                            EmailAddress1MasterRecordId_Status = item.EmailAddress1MasterRecordId_Status,
                        EmailAddress2 = item.EmailAddress2,
                            EmailAddress2_Status = item.EmailAddress2_Status,
                        EmailAddress2MasterRecordId = item.EmailAddress2MasterRecordId,
                            EmailAddress2MasterRecordId_Status = item.EmailAddress2MasterRecordId_Status,
                        NetId = item.NetId,
                            NetId_Status = item.NetId_Status,

                        HistoryDate = item.RecordDate
                    });
                }
                #endregion

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting EmployeeEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Match ***
        ******************************************************************/
        #region Match
        // GET: Employee/EmployeeMatch
        public IActionResult EmployeeMatch(long Id, int SystemId)
        {
            try
            {
                var employeeDetail = _context.GetEmployeeMatchDetails(SystemId, Id);

                var viewModel = new EmployeeMatchViewModel()
                {
                    Id = Id,
                    PageId = "employeePage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Employee",
                    Title = "Employee Matching",
                    Integration = employeeDetail.IntegrationName,
                    IntegrationId = employeeDetail.IntegrationId,
                    IntegrationDate = employeeDetail.IntegrationDate,
                    CreatedOnDT = employeeDetail.IntegrationDate,
                    System = employeeDetail.SystemName,
                    SystemId = employeeDetail.SystemId,
                    SourceRecordId = employeeDetail.SourceRecordId,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    #region Bio/Dem
                    FirstName = employeeDetail.FirstName,
                        FirstName_Weight = employeeDetail.FirstName_MatchWeight,
                        FirstName_BusinessName = employeeDetail.FirstName_BusinessName,
                        FirstName_BusinessDescription = employeeDetail.FirstName_BusinessDescription,
                    PreferredName = employeeDetail.PreferredName,
                        PreferredName_Weight = employeeDetail.PreferredName_MatchWeight,
                        PreferredName_BusinessName = employeeDetail.PreferredName_BusinessName,
                        PreferredName_BusinessDescription = employeeDetail.PreferredName_BusinessDescription,
                    MiddleName = employeeDetail.MiddleName,
                        MiddleName_Weight = employeeDetail.MiddleName_MatchWeight,
                        MiddleName_BusinessName = employeeDetail.MiddleName_BusinessName,
                        MiddleName_BusinessDescription = employeeDetail.MiddleName_BusinessDescription,
                    LastName = employeeDetail.LastName,
                        LastName_Weight = employeeDetail.LastName_MatchWeight,
                        LastName_BusinessName = employeeDetail.LastName_BusinessName,
                        LastName_BusinessDescription = employeeDetail.LastName_BusinessDescription,
                    MaidenName = employeeDetail.MaidenName,
                        MaidenName_Weight = employeeDetail.MaidenName_MatchWeight,
                        MaidenName_BusinessName = employeeDetail.MaidenName_BusinessName,
                        MaidenName_BusinessDescription = employeeDetail.MaidenName_BusinessDescription,
                    Suffix = employeeDetail.Suffix,
                        Suffix_Weight = employeeDetail.Suffix_MatchWeight,
                        Suffix_BusinessName = employeeDetail.Suffix_BusinessName,
                        Suffix_BusinessDescription = employeeDetail.Suffix_BusinessDescription,
                    DateOfBirth = employeeDetail.BirthDate,
                        DateOfBirth_Weight = employeeDetail.BirthDate_MatchWeight,
                        DateOfBirth_BusinessName = employeeDetail.BirthDate_BusinessName,
                        DateOfBirth_BusinessDescription = employeeDetail.BirthDate_BusinessDescription,
                    DeceasedDate = employeeDetail.DeceasedDate,
                        DeceasedDate_Weight = employeeDetail.DeceasedDate_MatchWeight,
                        DeceasedDate_BusinessName = employeeDetail.DeceasedDate_BusinessName,
                        DeceasedDate_BusinessDescription = employeeDetail.DeceasedDate_BusinessDescription,
                    MaritalStatus = employeeDetail.MaritalStatus,
                        MaritalStatus_Weight = employeeDetail.MaritalStatus_MatchWeight,
                        MaritalStatus_BusinessName = employeeDetail.MaritalStatus_BusinessName,
                        MaritalStatus_BusinessDescription = employeeDetail.MaritalStatus_BusinessDescription,
                    EmailAddress1 = employeeDetail.EmailAddress1,
                        EmailAddress1_Weight = employeeDetail.EmailAddress1_MatchWeight,
                        EmailAddress1_BusinessName = employeeDetail.EmailAddress1_BusinessName,
                        EmailAddress1_BusinessDescription = employeeDetail.EmailAddress1_BusinessDescription,
                    EmailAddress2 = employeeDetail.EmailAddress2,
                        EmailAddress2_Weight = employeeDetail.EmailAddress2_MatchWeight,
                        EmailAddress2_BusinessName = employeeDetail.EmailAddress2_BusinessName,
                        EmailAddress2_BusinessDescription = employeeDetail.EmailAddress2_BusinessDescription,
                    NetId = employeeDetail.NetId,
                    NetId_Weight = employeeDetail.NetId_MatchWeight,
                    NetId_BusinessName = employeeDetail.NetId_BusinessName,
                    NetId_BusinessDescription = employeeDetail.NetId_BusinessDescription,

                    #endregion

                    #region Employee Info
                    UAPersonId = employeeDetail.UAPersonId,
                        UAPersonId_Weight = employeeDetail.UAPersonId_MatchWeight,
                        UAPersonId_BusinessName = employeeDetail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = employeeDetail.UAPersonId_BusinessDescription,
                    HireDate = employeeDetail.HireDate,
                        HireDate_BusinessName = employeeDetail.HireDate_BusinessName,
                        HireDate_BusinessDescription = employeeDetail.HireDate_BusinessDescription,
                    TerminationDate = employeeDetail.TerminationDate,
                        TerminationDate_BusinessName = employeeDetail.TerminationDate_BusinessName,
                        TerminationDate_BusinessDescription = employeeDetail.TerminationDate_BusinessDescription,
                    #endregion

                    #region Employment Info
                    OrganizationName = employeeDetail.OrganizationName,
                        OrganizationName_Weight = employeeDetail.OrganizationName_MatchWeight,
                        OrganizationName_BusinessName = employeeDetail.OrganizationName_BusinessName,
                        OrganizationName_BusinessDescription = employeeDetail.OrganizationName_BusinessDescription,
                    EmployeeType = employeeDetail.EmployeeType,
                        EmployeeType_Weight = employeeDetail.EmployeeType_MatchWeight,
                        EmployeeType_BusinessName = employeeDetail.EmployeeType_BusinessName,
                        EmployeeType_BusinessDescription = employeeDetail.EmployeeType_BusinessDescription,
                    #endregion

                };

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting EmployeeMatch details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        public IActionResult GetPossibleMatchList(long Id, int SystemId)
        {
            try
            {
                var viewModel = new EmployeePossibleMatchViewModel()
                {
                    PossibleMatches = new List<EmployeePossibleMatchViewModel.EmployeeMatchSummaryViewModel>()
                };
                foreach (var possibleMatch in _context.GetEmployeePossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new EmployeePossibleMatchViewModel.EmployeeMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,

                        Name = $"{possibleMatch.FirstName} {possibleMatch.MiddleName} {possibleMatch.LastName}",
                        FirstName = possibleMatch.FirstName,
                        MiddleName = possibleMatch.MiddleName,
                        LastName = possibleMatch.LastName,
                        UAPersonId = possibleMatch.UAPersonId,
                        BirthDate = possibleMatch.BirthDate
                    });
                }
                return PartialView("EmployeePossibleMatchList", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve Employee GetPossibleMatchList");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Compare ***
        ******************************************************************/
        #region Compare
        // GET: Employee/EmployeeCompare
        public IActionResult EmployeeCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetEmployeeComparisonDetail(SystemId, Id, MasterId);

                var viewModel = new EmployeeCompareViewModel()
                {
                    Id = Id,
                    SystemId = SystemId,
                    MasterId = MasterId,

                    System = detail.SystemName,
                    IntegrationId = detail.IntegrationId,
                    IntegrationName = detail.IntegrationName,
                    SystemName = detail.SystemName,
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
                    NetId = detail.NetId,
                        NetId_BusinessName = detail.NetId_BusinessName,
                        NetId_BusinessDescription = detail.EmailAddress2_BusinessDescription,
                        NetId_Compare = detail.NetId_Compare,
                        NetId_IsDifferent = detail.NetId != detail.NetId_Compare,
                    UAPersonId = detail.UAPersonId,
                        UAPersonId_BusinessName = detail.UAPersonId_BusinessName,
                        UAPersonId_BusinessDescription = detail.UAPersonId_BusinessDescription,
                        UAPersonId_Compare = detail.UAPersonId_Compare,
                        UAPersonId_IsDifferent = detail.UAPersonId != detail.UAPersonId_Compare,
                    //EmployeeTitle = detail.Title,
                    //    EmployeeTitle_BusinessName = detail.Title_BusinessName,
                    //    EmployeeTitle_BusinessDescription = detail.Title_BusinessDescription,
                    //    EmployeeTitle_Compare = detail.Title_Compare,
                    //    EmployeeTitle_IsDifferent = detail.Title != detail.Title_Compare,
                    HireDate = detail.HireDate,
                        HireDate_BusinessName = detail.HireDate_BusinessName,
                        HireDate_BusinessDescription = detail.HireDate_BusinessDescription,
                        HireDate_Compare = detail.HireDate_Compare,
                        HireDate_IsDifferent = detail.HireDate != detail.HireDate_Compare,
                    TerminationDate = detail.TerminationDate,
                        TerminationDate_BusinessName = detail.TerminationDate_BusinessName,
                        TerminationDate_BusinessDescription = detail.TerminationDate_BusinessDescription,
                        TerminationDate_Compare = detail.TerminationDate_Compare,
                        TerminationDate_IsDifferent = detail.TerminationDate != detail.TerminationDate_Compare,
                    OrganizationName = detail.OrganizationName,
                        OrganizationName_BusinessName = detail.OrganizationName_BusinessName,
                        OrganizationName_BusinessDescription = detail.OrganizationName_BusinessDescription,
                        OrganizationName_Compare = detail.OrganizationName_Compare,
                        OrganizationName_IsDifferent = detail.OrganizationName != detail.OrganizationName_Compare,
                    EmployeeType = detail.EmployeeType,
                        EmployeeType_BusinessName = detail.EmployeeType_BusinessName,
                        EmployeeType_BusinessDescription = detail.EmployeeType_BusinessDescription,
                        EmployeeType_Compare = detail.EmployeeType_Compare,
                        EmployeeType_IsDifferent = detail.EmployeeType != detail.EmployeeType_Compare,
                    SystemRecords = detail.SystemRecords
                };

                return PartialView("EmployeeCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting EmployeeCompare details");
                return RedirectToAction("SystemError", "Error");
            }
        }
        #endregion

        /*  *** Manual Match ***
        ******************************************************************/
        #region ManualMatch
        // GET: Employee/EmployeeManualMatch
        public async Task<IActionResult> EmployeeManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting EmployeeManualMatch details");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(EmployeeList));
        }
        #endregion

        /*  *** Save ***
        ******************************************************************/
        #region Save
        // POST: Employee/EmployeeSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmployeeSave(EmployeeViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeEmployeeIntegrationRecord(model.SystemId, model.Id, model.EmployeeConstituentId, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.UAPersonId,
                        model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId, 
                        model.BirthDate, model.DeceasedDate, model.MaritalStatus, model.MaritalStatusSourceSystemRecordId, model.MaritalStatusMasterId, model.OrganizationName, model.OrganizationSourceSystemRecordId, model.OrganizationMasterId,
                        model.HireDate, model.TerminationDate, model.EmployeeType, model.EmployeeTypeSourceSystemRecordId, model.EmployeeTypeMasterId,
                        model.EmailAddress1, model.EmailAddress1MasterRecordId, model.EmailAddress2, model.EmailAddress2MasterRecordId, model.NetId, User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in EmployeeSave");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(EmployeeList));
            }
            return View(model);
        }
        #endregion

        /*  *** Revalidate ***
        ******************************************************************/
        #region Revalidate
        // POST: Employee/EmployeeRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmployeeRevalidate(EmployeeViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    if (model.IsChanged)
                    {
                        _context.ChangeEmployeeIntegrationRecord(model.SystemId, model.Id, model.EmployeeConstituentId, model.FirstName, model.PreferredName, model.MiddleName, model.LastName, model.MaidenName, model.UAPersonId,
                            model.Suffix, model.SuffixSourceSystemRecordId, model.SuffixMasterId, 
                            model.BirthDate, model.DeceasedDate, model.MaritalStatus, model.MaritalStatusSourceSystemRecordId, model.MaritalStatusMasterId, model.OrganizationName, model.OrganizationSourceSystemRecordId, model.OrganizationMasterId,
                            model.HireDate, model.TerminationDate, model.EmployeeType, model.EmployeeTypeSourceSystemRecordId, model.EmployeeTypeMasterId,
                            model.EmailAddress1, model.EmailAddress1MasterRecordId, model.EmailAddress2, model.EmailAddress2MasterRecordId, model.NetId, User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in EmployeeRevalidate");
                    return RedirectToAction("SystemError", "Error");
                }

                return RedirectToAction(nameof(EmployeeList));
            }
            return View(model);
        }
        #endregion

        /*  *** Ignore ***
        ******************************************************************/
        #region Ignore
        public IActionResult EmployeeIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load EmployeeIgnore method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(EmployeeList));
        }
        #endregion

        #endregion

    }
}
