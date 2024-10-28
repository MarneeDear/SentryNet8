using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Sentry.WebApp.ViewModels.Reports;
using Microsoft.Extensions.Options;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeFinance]
    public class FinancialController : IntegrationController
    {
        private readonly Config _config;

        public const string READONLY = "Not Provided";
        public FinancialController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<FinancialController> logger, 
            IConfiguration configuration,
            IDomainService domainService,
            IOptions<Config> config) : base(context, dwContext, logger, configuration, domainService) 
        {
            _config = config.Value;
        }

        #region Designation

        /*******************************************************************************************************************************
			* Designation
		*******************************************************************************************************************************/

        // GET: Finance Dashboard sentry/financial/
        public IActionResult Index()
        {
            IList<ReportItem> powerBiItems = new List<ReportItem>();

            foreach (var report in _config.PowerBiReports)
            {
                powerBiItems.Add(
                    new ReportItem()
                    {
                        Report = report.ReportId.ToString(),
                        DisplayName = report.ReportDisplayName,
                        ReportGuideId = report.ReportGuideId
                    }
                );
            }

            var reportItems = _configuration.GetSection("EnhancedReporting").GetChildren().ToList().Select(item => new ReportItem
            {
                Report = item.GetValue<string>("Report"),
                DisplayName = item.GetValue<string>("ReportDisplayName"),
                ReportGuideId = item.GetValue<int>("ReportType")
            });

            var viewModel = new ReportListViewModel()
            {
                Title = "Financial",
                PageId = "financialDashboardPage",
                ActiveClass = "FinancialDashboard",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
                ReportItems = reportItems,
                PowerBiItems = powerBiItems
            };
            
            return View(viewModel);
        }

        // GET: Designation/DesignationList
        public IActionResult DesignationList()
        {
            var model = new DesignationListViewModel()
            {
                Title = "Designation",
                PageId = "designationPage",
                ActiveClass = "Designation",
                Message = "Your Designation Page",
                Integration = "Designation",
                IntegrationId = 4,
                User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
                RemediationList = new List<DesignationRemediationListItemViewModel>()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> GetDesignationList(AjaxDataTableRequest request)
        {
            try
            {
                var designations = _context.DesignationRemediationList.AsQueryable();

                int recordsTotal = designations.Count();

                var designationList = await (string.IsNullOrEmpty(request.searchValue)
                                                ? designations
                                                : designations.Where(s => s.DesignationName.Contains(request.searchValue) || s.DesignationId.Contains(request.searchValue) || s.ErrorCategories.Contains(request.searchValue) || s.SystemName.Contains(request.searchValue))
                                            )
                                            .OrderBy($"{request.sortColumn ?? "IntegrationDate"} {request.sortColumnDirection ?? "DESC"}")
                                            .ToListAsync();

                int recordsFiltered = designationList.Count();

                var designationRemediationList = new List<DesignationRemediationListItemViewModel>();

                foreach (var item in designationList.Skip(request.start).Take(request.length))
                {
                    designationRemediationList.Add(new DesignationRemediationListItemViewModel()
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
                        DesignationName = item.DesignationName,
                        DesignationId = item.DesignationId
                    });
                }

                var data = designationRemediationList.ToList();

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
                _logger.LogError(ex, "Unable to retrieve DesignationList details");
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: Designation/DesignationEdit
        public IActionResult DesignationEdit(long Id, int SystemId)
        {
            try
            {
                var history = _context.GetDesignationHistory(SystemId, Id).OrderByDescending(m => m.RecordDate);
                var designationDetail = history.First();
                var designationSource = history.Last();

                var viewModel = new DesignationViewModel()
                {
                    Title = "Designation",
                    PageId = "designationPage",
                    ActiveClass = "Designation",
                    Message = "Your Designation Page",

                    IsChanged = false,
                    Id = designationDetail.Id,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),
                    System = designationDetail.SystemName,
                    SystemId = designationDetail.SystemId,
                    HistoryData = new List<DesignationHistoryViewModel>(),
                    Integration = designationDetail.IntegrationName,
                    CreatedOnDT = designationDetail.RecordDate,
                    RecordStatus = designationDetail.RecordStatus,
                    IntegrationId = designationDetail.IntegrationId,
                    SourceRecordId = designationDetail.SourceRecordId,
                    IntegrationDate = designationDetail.IntegrationDate,

                    DesignationName = designationDetail.DesignationName,
                        DesignationName_BusinessName = designationDetail.DesignationName_BusinessName,
                        DesignationName_BusinessDescription = designationDetail.DesignationName_BusinessDescription,
                        DesignationName_AttributeId = designationDetail.DesignationName_AttributeId,
                        DesignationName_Status = designationDetail.DesignationName_Status,
                        DesignationName_OriginalValue = designationSource.DesignationName,
                        DesignationName_Source = designationDetail.DesignationName_Source,
                        DesignationName_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationName_Source }),
                    DesignationId = designationDetail.DesignationId,
                        DesignationId_BusinessName = designationDetail.DesignationId_BusinessName,
                        DesignationId_BusinessDescription = designationDetail.DesignationId_BusinessDescription,
                        DesignationId_AttributeId = designationDetail.DesignationId_AttributeId,
                        DesignationId_Status = designationDetail.DesignationId_Status,
                        DesignationId_OriginalValue = designationSource.DesignationId,
                        DesignationId_Source = designationDetail.DesignationId_Source,
                        DesignationId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationId_Source }),
                    Description = designationDetail.Description,
                        Description_BusinessName = designationDetail.Description_BusinessName,
                        Description_BusinessDescription = designationDetail.Description_BusinessDescription,
                        Description_AttributeId = designationDetail.Description_AttributeId,
                        Description_Status = designationDetail.Description_Status,
                        Description_OriginalValue = designationSource.Description,
                        Description_Source = designationDetail.Description_Source,
                        Description_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.Description_Source }),
                    DesignationType = designationDetail.DesignationTypeMasterId,
                        DesignationType_BusinessName = designationDetail.DesignationTypeMasterId_BusinessName,
                        DesignationType_BusinessDescription = designationDetail.DesignationTypeMasterId_BusinessDescription,
                        DesignationType_AttributeId = designationDetail.DesignationTypeMasterId_AttributeId,
                        DesignationType_Status = designationDetail.DesignationTypeMasterId_Status,
                        DesignationType_OriginalValue = designationSource.DesignationTypeName,
                        DesignationType_Source = designationDetail.DesignationTypeMasterId_Source,
                        DesignationType_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationTypeMasterId_Source }),
                    DesignationTypeName = designationDetail.DesignationTypeName,
                        DesignationTypeName_BusinessName = designationDetail.DesignationTypeName_BusinessName,
                        DesignationTypeName_BusinessDescription = designationDetail.DesignationTypeName_BusinessDescription,
                        DesignationTypeName_AttributeId = designationDetail.DesignationTypeName_AttributeId,
                        DesignationTypeName_Status = designationDetail.DesignationTypeName_Status,
                        DesignationTypeName_OriginalValue = designationSource.DesignationTypeName,
                        DesignationTypeName_Source = designationDetail.DesignationTypeName_Source,
                    DesignationTypeSourceSystemRecordId = designationDetail.DesignationTypeSourceSystemRecordId,
                        DesignationTypeSourceSystemRecordId_BusinessName = designationDetail.DesignationTypeSourceSystemRecordId_BusinessName,
                        DesignationTypeSourceSystemRecordId_BusinessDescription = designationDetail.DesignationTypeSourceSystemRecordId_BusinessDescription,
                        DesignationTypeSourceSystemRecordId_AttributeId = designationDetail.DesignationTypeSourceSystemRecordId_AttributeId,
                        DesignationTypeSourceSystemRecordId_Status = designationDetail.DesignationTypeSourceSystemRecordId_Status,
                        DesignationTypeSourceSystemRecordId_OriginalValue = designationSource.DesignationTypeSourceSystemRecordId,
                        DesignationTypeSourceSystemRecordId_Source = designationDetail.DesignationTypeSourceSystemRecordId_Source,
                    DesignationTypeMasterId = designationDetail.DesignationTypeMasterId,
                        DesignationTypeMasterId_BusinessName = designationDetail.DesignationTypeMasterId_BusinessName,
                        DesignationTypeMasterId_BusinessDescription = designationDetail.DesignationTypeMasterId_BusinessDescription,
                        DesignationTypeMasterId_AttributeId = designationDetail.DesignationTypeMasterId_AttributeId,
                        DesignationTypeMasterId_Status = designationDetail.DesignationTypeMasterId_Status,
                        DesignationTypeMasterId_OriginalValue = designationSource.DesignationTypeMasterId,
                        DesignationTypeMasterId_Source = designationDetail.DesignationTypeMasterId_Source,
                        DesignationTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationTypeMasterId_Source, designationDetail.DesignationTypeSourceSystemRecordId_Source, designationDetail.DesignationTypeName_Source }),
                    StartDate = designationDetail.StartDate,
                        StartDate_BusinessName = designationDetail.StartDate_BusinessName,
                        StartDate_BusinessDescription = designationDetail.StartDate_BusinessDescription,
                        StartDate_AttributeId = designationDetail.StartDate_AttributeId,
                        StartDate_Status = designationDetail.StartDate_Status,
                        StartDate_OriginalValue = designationSource.StartDate,
                        StartDate_Source = designationDetail.StartDate_Source,
                        StartDate_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.StartDate_Source }),
                    EndDate = designationDetail.EndDate,
                        EndDate_BusinessName = designationDetail.EndDate_BusinessName,
                        EndDate_BusinessDescription = designationDetail.EndDate_BusinessDescription,
                        EndDate_AttributeId = designationDetail.EndDate_AttributeId,
                        EndDate_Status = designationDetail.EndDate_Status,
                        EndDate_OriginalValue = designationSource.EndDate,
                        EndDate_Source = designationDetail.EndDate_Source,
                        EndDate_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.EndDate_Source }),
                    OrganizationalUnit = designationDetail.OrganizationalUnit,
                        OrganizationalUnit_BusinessName = designationDetail.OrganizationalUnit_BusinessName,
                        OrganizationalUnit_BusinessDescription = designationDetail.OrganizationalUnit_BusinessDescription,
                        OrganizationalUnit_AttributeId = designationDetail.OrganizationalUnit_AttributeId,
                        OrganizationalUnit_Status = designationDetail.OrganizationalUnit_Status,
                        OrganizationalUnit_OriginalValue = designationSource.OrganizationalUnit,
                        OrganizationalUnit_Source = designationDetail.OrganizationalUnit_Source,
                        OrganizationalUnit_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.OrganizationalUnit_Source }),
                    OrganizationalUnitCode = designationDetail.OrganizationalUnitCode,
                    OrganizationalUnitMasterId = designationDetail.OrganizationalUnitMasterId,
                        OrganizationalUnitMasterId_BusinessName = designationDetail.OrganizationalUnitMasterId_BusinessName,
                        OrganizationalUnitMasterId_BusinessDescription = designationDetail.OrganizationalUnitMasterId_BusinessDescription,
                        OrganizationalUnitMasterId_AttributeId = designationDetail.OrganizationalUnitMasterId_AttributeId,
                        OrganizationalUnitMasterId_Status = designationDetail.OrganizationalUnitMasterId_Status,
                        OrganizationalUnitMasterId_OriginalValue = designationSource.OrganizationalUnitMasterId,
                        OrganizationalUnitMasterId_Source = designationDetail.OrganizationalUnitMasterId_Source,
                        OrganizationalUnitMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.OrganizationalUnitMasterId_Source, designationDetail.OrganizationalUnit_Source }),
                    KFSAccountCode = designationDetail.KFSAccountCode,
                        KFSAccountCode_BusinessName = designationDetail.KFSAccountCode_BusinessName,
                        KFSAccountCode_BusinessDescription = designationDetail.KFSAccountCode_BusinessDescription,
                        KFSAccountCode_AttributeId = designationDetail.KFSAccountCode_AttributeId,
                        KFSAccountCode_Status = designationDetail.KFSAccountCode_Status,
                        KFSAccountCode_OriginalValue = designationSource.KFSAccountCode,
                        KFSAccountCode_Source = designationDetail.KFSAccountCode_Source,
                        KFSAccountCode_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.KFSAccountCode_Source }),
                    VSECategoryName = designationDetail.VSECategoryName,
                        VSECategoryName_BusinessName = designationDetail.VSECategoryName_BusinessName,
                        VSECategoryName_BusinessDescription = designationDetail.VSECategoryName_BusinessDescription,
                        VSECategoryName_AttributeId = designationDetail.VSECategoryName_AttributeId,
                        VSECategoryName_Status = designationDetail.VSECategoryName_Status,
                        VSECategoryName_OriginalValue = designationSource.VSECategoryName,
                        VSECategoryName_Source = designationDetail.VSECategoryName_Source,
                    VSECategorySourceSystemRecordId = designationDetail.VSECategorySourceSystemRecordId,
                        VSECategorySourceSystemRecordId_BusinessName = designationDetail.VSECategorySourceSystemRecordId_BusinessName,
                        VSECategorySourceSystemRecordId_BusinessDescription = designationDetail.VSECategorySourceSystemRecordId_BusinessDescription,
                        VSECategorySourceSystemRecordId_AttributeId = designationDetail.VSECategorySourceSystemRecordId_AttributeId,
                        VSECategorySourceSystemRecordId_Status = designationDetail.VSECategorySourceSystemRecordId_Status,
                        VSECategorySourceSystemRecordId_OriginalValue = designationSource.VSECategorySourceSystemRecordId,
                        VSECategorySourceSystemRecordId_Source = designationDetail.VSECategorySourceSystemRecordId_Source,
                    VSECategoryMasterId = designationDetail.VSECategoryMasterId,
                        VSECategoryMasterId_BusinessName = designationDetail.VSECategoryMasterId_BusinessName,
                        VSECategoryMasterId_BusinessDescription = designationDetail.VSECategoryMasterId_BusinessDescription,
                        VSECategoryMasterId_AttributeId = designationDetail.VSECategoryMasterId_AttributeId,
                        VSECategoryMasterId_Status = designationDetail.VSECategoryMasterId_Status,
                        VSECategoryMasterId_OriginalValue = designationSource.VSECategoryMasterId,
                        VSECategoryMasterId_Source = designationDetail.VSECategoryMasterId_Source,
                        VSECategoryMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.VSECategoryMasterId_Source, designationDetail.VSECategorySourceSystemRecordId_Source, designationDetail.VSECategoryName_Source }),
                    GLOrganizationName = designationDetail.GLOrganizationName,
                        GLOrganizationName_BusinessName = designationDetail.GLOrganizationName_BusinessName,
                        GLOrganizationName_BusinessDescription = designationDetail.GLOrganizationName_BusinessDescription,
                        GLOrganizationName_AttributeId = designationDetail.GLOrganizationName_AttributeId,
                        GLOrganizationName_Status = designationDetail.GLOrganizationName_Status,
                        GLOrganizationName_OriginalValue = designationSource.GLOrganizationName,
                        GLOrganizationName_Source = designationDetail.GLOrganizationName_Source,
                    GLOrganizationCode = designationDetail.GLOrganizationCode,
                        GLOrganizationCode_BusinessName = designationDetail.GLOrganizationCode_BusinessName,
                        GLOrganizationCode_BusinessDescription = designationDetail.GLOrganizationCode_BusinessDescription,
                        GLOrganizationCode_AttributeId = designationDetail.GLOrganizationCode_AttributeId,
                        GLOrganizationCode_Status = designationDetail.GLOrganizationCode_Status,
                        GLOrganizationCode_OriginalValue = designationSource.GLOrganizationCode,
                        GLOrganizationCode_Source = designationDetail.GLOrganizationCode_Source,
                    GLOrganizationMasterId = designationDetail.GLOrganizationMasterId,
                        GLOrganizationMasterId_BusinessName = designationDetail.GLOrganizationMasterId_BusinessName,
                        GLOrganizationMasterId_BusinessDescription = designationDetail.GLOrganizationMasterId_BusinessDescription,
                        GLOrganizationMasterId_AttributeId = designationDetail.GLOrganizationMasterId_AttributeId,
                        GLOrganizationMasterId_Status = designationDetail.GLOrganizationMasterId_Status,
                        GLOrganizationMasterId_OriginalValue = designationSource.GLOrganizationMasterId,
                        GLOrganizationMasterId_Source = designationDetail.GLOrganizationMasterId_Source,
                        GLOrganizationMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.GLOrganizationMasterId_Source, designationDetail.GLOrganizationCode_Source, designationDetail.GLOrganizationName_Source }),
                    DesignationUseTypeName = designationDetail.DesignationUseTypeName,
                        DesignationUseTypeName_BusinessName = designationDetail.DesignationUseTypeName_BusinessName,
                        DesignationUseTypeName_BusinessDescription = designationDetail.DesignationUseTypeName_BusinessDescription,
                        DesignationUseTypeName_AttributeId = designationDetail.DesignationUseTypeName_AttributeId,
                        DesignationUseTypeName_Status = designationDetail.DesignationUseTypeName_Status,
                        DesignationUseTypeName_OriginalValue = designationSource.DesignationUseTypeName,
                        DesignationUseTypeName_Source = designationDetail.DesignationUseTypeName_Source,
                    DesignationUseTypeMasterId = designationDetail.DesignationUseTypeMasterId,
                        DesignationUseTypeMasterId_BusinessName = designationDetail.DesignationUseTypeMasterId_BusinessName,
                        DesignationUseTypeMasterId_BusinessDescription = designationDetail.DesignationUseTypeMasterId_BusinessDescription,
                        DesignationUseTypeMasterId_AttributeId = designationDetail.DesignationUseTypeMasterId_AttributeId,
                        DesignationUseTypeMasterId_Status = designationDetail.DesignationUseTypeMasterId_Status,
                        DesignationUseTypeMasterId_OriginalValue = designationSource.DesignationUseTypeMasterId,
                        DesignationUseTypeMasterId_Source = designationDetail.DesignationUseTypeMasterId_Source,
                        DesignationUseTypeMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationUseTypeMasterId_Source, designationDetail.DesignationUseTypeName_Source }),
                    DesignationStatus = designationDetail.DesignationStatus,
                        DesignationStatus_BusinessName = designationDetail.DesignationStatus_BusinessName,
                        DesignationStatus_BusinessDescription = designationDetail.DesignationStatus_BusinessDescription,
                        DesignationStatus_AttributeId = designationDetail.DesignationStatus_AttributeId,
                        DesignationStatus_Status = designationDetail.DesignationStatus_Status,
                        DesignationStatus_OriginalValue = designationSource.DesignationStatus,
                        DesignationStatus_Source = designationDetail.DesignationStatus_Source,
                    DesignationStatusMasterId = designationDetail.DesignationStatusMasterId,
                        DesignationStatusMasterId_BusinessName = designationDetail.DesignationStatusMasterId_BusinessName,
                        DesignationStatusMasterId_BusinessDescription = designationDetail.DesignationStatusMasterId_BusinessDescription,
                        DesignationStatusMasterId_AttributeId = designationDetail.DesignationStatusMasterId_AttributeId,
                        DesignationStatusMasterId_Status = designationDetail.DesignationStatusMasterId_Status,
                        DesignationStatusMasterId_OriginalValue = designationSource.DesignationStatusMasterId,
                        DesignationStatusMasterId_Source = designationDetail.DesignationStatusMasterId_Source,
                        DesignationStatusMasterId_IsReadOnly = AttributeIsReadOnly(new string[] { designationDetail.DesignationStatusMasterId_Source, designationDetail.DesignationStatus_Source }),

                    GetDepartmentList = GetDepartmentList().ToList(),
                    DesignationTypeList = GetDesignationTypeList().ToList(),
                    VSECategoryList = GetVSECategoryList().ToList(),
                    GLOrganizationList = GetGLOrganizationList().ToList(),
                    DesignationUseTypeList = GetDesignationUseTypeList().ToList(),
                    OrganizationalUnitList = GetDepartmentList().ToList(),
                    DesignationStatusList = GetDesignationStatusList().ToList()
                };

                for (int i = 0; i <= history.Count() - 2; i++)
                {
                    var item = history.ElementAt(i);
                    var previousitem = history.ElementAt(i + 1);

                    viewModel.HistoryData.Add(new DesignationHistoryViewModel()
                    {
                        DesignationId = item.DesignationId,
                            DesignationId_Status = item.DesignationId_Status,

                        DesignationName = item.DesignationName,
                            DesignationName_Status = item.DesignationName_Status,

                        Description = item.Description,
                            Description_Status = item.Description_Status,

                        DesignationTypeName = item.DesignationTypeName,
                            DesignationTypeName_Status = item.DesignationTypeName_Status,
                        DesignationTypeSourceSystemRecordId = item.DesignationTypeSourceSystemRecordId,
                            DesignationTypeSourceSystemRecordId_Status = item.DesignationTypeSourceSystemRecordId_Status,
                        DesignationTypeMasterId = item.DesignationTypeMasterId,
                            DesignationTypeMasterId_Status = item.DesignationTypeMasterId_Status,

                        StartDate = item.StartDate,
                            StartDate_Status = item.StartDate_Status,

                        EndDate = item.EndDate,
                            EndDate_Status = item.EndDate_Status,

                        OrganizationalUnit = item.OrganizationalUnit,
                            OrganizationalUnit_Status = item.OrganizationalUnit_Status,
                        OrganizationalUnitMasterId = item.OrganizationalUnitMasterId,
                            OrganizationalUnitMasterId_Status = item.OrganizationalUnitMasterId_Status,

                        KFSAccountCode = item.KFSAccountCode,
                            KFSAccountCode_Status = item.KFSAccountCode_Status,

                        VSECategoryName = item.VSECategoryName,
                            VSECategoryName_Status = item.VSECategoryName_Status,
                        VSECategorySourceSystemRecordId = item.VSECategorySourceSystemRecordId,
                            VSECategorySourceSystemRecordId_Status = item.VSECategorySourceSystemRecordId_Status,
                        VSECategoryMasterId = item.VSECategoryMasterId,
                            VSECategoryMasterId_Status = item.VSECategoryMasterId_Status,

                        GLOrganizationName = item.GLOrganizationName,
                            GLOrganizationName_Status = item.GLOrganizationName_Status,
                        GLOrganizationCode = item.GLOrganizationCode,
                            GLOrganizationCode_Status = item.GLOrganizationCode_Status,
                        GLOrganizationMasterId = item.GLOrganizationMasterId,
                            GLOrganizationMasterId_Status = item.GLOrganizationMasterId_Status,

                        DesignationUseTypeName = item.DesignationUseTypeName,
                            DesignationUseTypeName_Status = item.DesignationUseTypeName_Status,
                        DesignationUseTypeMasterId = item.DesignationUseTypeMasterId,
                            DesignationUseTypeMasterId_Status = item.DesignationUseTypeMasterId_Status,

                        DesignationStatus = item.DesignationStatus,
                            DesignationStatus_Status = item.DesignationStatus_Status,
                        DesignationStatusMasterId = item.DesignationStatusMasterId,
                            DesignationStatusMasterId_Status = item.DesignationStatusMasterId_Status,

                        HistoryDate = item.RecordDate
                    });
                }

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve DesignationEdit details");
                return RedirectToAction("SystemError", "Error");
            }
        }


        // GET: Designation/DesignationMatch
        public IActionResult DesignationMatch(long Id, int SystemId)
        {
            try
            {
                var designationDetail = _context.GetDesignationMatchDetails(SystemId, Id);

                var viewModel = new DesignationMatchViewModel()
                {
                    Id = Id,
                    PageId = "designationPage",
                    PageWrapperClass = "toggled",
                    ActiveClass = "Designation",
                    Title = "Designation Matching",
                    Integration = designationDetail.IntegrationName,
                    IntegrationId = designationDetail.IntegrationId,
                    IntegrationDate = designationDetail.IntegrationDate,
                    System = designationDetail.SystemName,
                    SystemId = designationDetail.SystemId,
                    SourceRecordId = designationDetail.SourceRecordId,
                    CreatedOnDT = designationDetail.IntegrationDate,
                    User = User.Identity.Name,
					NavigationGroups = GetNavigationGroups(),

                    DesignationId = designationDetail.DesignationId,
                        DesignationId_BusinessName = designationDetail.DesignationId_BusinessName,
                        DesignationId_BusinessDescription = designationDetail.DesignationId_BusinessDescription,
                        DesignationId_Weight = designationDetail.DesignationId_MatchWeight,
                    DesignationName = designationDetail.DesignationName,
                        DesignationName_BusinessName = designationDetail.DesignationName_BusinessName,
                        DesignationName_BusinessDescription = designationDetail.DesignationName_BusinessDescription,
                        DesignationName_Weight = designationDetail.DesignationName_MatchWeight,
                    Description = designationDetail.Description,
                        Description_BusinessName = designationDetail.Description_BusinessName,
                        Description_BusinessDescription = designationDetail.Description_BusinessDescription,
                        Description_Weight = designationDetail.Description_MatchWeight,
                    DesignationTypeName = designationDetail.DesignationTypeName,
                        DesignationTypeName_BusinessName = designationDetail.DesignationTypeName_BusinessName,
                        DesignationTypeName_BusinessDescription = designationDetail.DesignationTypeName_BusinessDescription,
                        DesignationTypeName_Weight = designationDetail.DesignationTypeName_MatchWeight,
                    StartDate = designationDetail.StartDate,
                        StartDate_BusinessName = designationDetail.StartDate_BusinessName,
                        StartDate_BusinessDescription = designationDetail.StartDate_BusinessDescription,
                        StartDate_Weight = designationDetail.StartDate_MatchWeight,
                    EndDate = designationDetail.EndDate,
                        EndDate_BusinessName = designationDetail.EndDate_BusinessName,
                        EndDate_BusinessDescription = designationDetail.EndDate_BusinessDescription,
                        EndDate_Weight = designationDetail.EndDate_MatchWeight,
                    KFSAccountCode = designationDetail.KFSAccountCode,
                        KFSAccountCode_BusinessName = designationDetail.KFSAccountCode_BusinessName,
                        KFSAccountCode_BusinessDescription = designationDetail.KFSAccountCode_BusinessDescription,
                        KFSAccountCode_Weight = designationDetail.KFSAccountCode_MatchWeight,
                    //KFSAccountMasterId = designationDetail.KFSAccountMasterId,
                    //    KFSAccountMasterId_BusinessName = designationDetail.KFSAccountMasterId_BusinessName,
                    //    KFSAccountMasterId_BusinessDescription = designationDetail.KFSAccountMasterId_BusinessDescription,
                    //    KFSAccountMasterId_Weight = designationDetail.KFSAccountMasterId_MatchWeight,
                    VSECategoryName = designationDetail.VSECategoryName,
                        VSECategoryName_BusinessName = designationDetail.VSECategoryName_BusinessName,
                        VSECategoryName_BusinessDescription = designationDetail.VSECategoryName_BusinessDescription,
                        VSECategoryName_Weight = designationDetail.VSECategoryName_MatchWeight,
                    VSECategoryMasterId = designationDetail.VSECategoryMasterId,
                        VSECategoryMasterId_BusinessName = designationDetail.VSECategoryMasterId_BusinessName,
                        VSECategoryMasterId_BusinessDescription = designationDetail.VSECategoryMasterId_BusinessDescription,
                        VSECategoryMasterId_Weight = designationDetail.VSECategoryMasterId_MatchWeight,
                    GLOrganizationName = designationDetail.GLOrganizationName,
                        GLOrganizationName_BusinessName = designationDetail.GLOrganizationName_BusinessName,
                        GLOrganizationName_BusinessDescription = designationDetail.GLOrganizationName_BusinessDescription,
                        GLOrganizationName_Weight = designationDetail.GLOrganizationName_MatchWeight,
                    GLOrganizationCode = designationDetail.GLOrganizationCode,
                        GLOrganizationCode_BusinessName = designationDetail.GLOrganizationCode_BusinessName,
                        GLOrganizationCode_BusinessDescription = designationDetail.GLOrganizationCode_BusinessDescription,
                        GLOrganizationCode_Weight = designationDetail.GLOrganizationCode_MatchWeight,
                    GLOrganizationMasterId = designationDetail.GLOrganizationMasterId,
                        GLOrganizationMasterId_BusinessName = designationDetail.GLOrganizationMasterId_BusinessName,
                        GLOrganizationMasterId_BusinessDescription = designationDetail.GLOrganizationMasterId_BusinessDescription,
                        GLOrganizationMasterId_Weight = designationDetail.GLOrganizationMasterId_MatchWeight,
                    DesignationUseTypeName = designationDetail.DesignationUseTypeName,
                        DesignationUseTypeName_BusinessName = designationDetail.DesignationUseTypeName_BusinessName,
                        DesignationUseTypeName_BusinessDescription = designationDetail.DesignationUseTypeName_BusinessDescription,
                        DesignationUseTypeName_Weight = designationDetail.DesignationUseTypeName_MatchWeight,
                    DesignationUseTypeMasterId = designationDetail.DesignationUseTypeMasterId,
                        DesignationUseTypeMasterId_BusinessName = designationDetail.DesignationUseTypeMasterId_BusinessName,
                        DesignationUseTypeMasterId_BusinessDescription = designationDetail.DesignationUseTypeMasterId_BusinessDescription,
                        DesignationUseTypeMasterId_Weight = designationDetail.DesignationUseTypeMasterId_MatchWeight,
                    OrganizationalUnit = designationDetail.OrganizationalUnit,
                        OrganizationalUnit_BusinessName = designationDetail.OrganizationalUnit_BusinessName,
                        OrganizationalUnit_BusinessDescription = designationDetail.OrganizationalUnit_BusinessDescription,
                        OrganizationalUnit_Weight = designationDetail.OrganizationalUnit_MatchWeight,
                    OrganizationalUnitMasterId = designationDetail.OrganizationalUnitMasterId,
                        OrganizationalUnitMasterId_BusinessName = designationDetail.OrganizationalUnitMasterId_BusinessName,
                        OrganizationalUnitMasterId_BusinessDescription = designationDetail.OrganizationalUnitMasterId_BusinessDescription,
                        OrganizationalUnitMasterId_Weight = designationDetail.OrganizationalUnitMasterId_MatchWeight
                };

                return View(viewModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load DesignationMatch method");
                return RedirectToAction("SystemError", "Error");
            }
        }

        public IActionResult GetPossibleMatchList(long Id, int SystemId)
        {
            try
            {
                var viewModel = new DesignationPossibleMatchViewModel()
                {
                    PossibleMatches = new List<DesignationPossibleMatchViewModel.DesignationMatchSummaryViewModel>()
                };
                foreach (var possibleMatch in _context.GetDesignationPossibleMatches(SystemId, Id))
                {
                    viewModel.PossibleMatches.Add(new DesignationPossibleMatchViewModel.DesignationMatchSummaryViewModel()
                    {
                        MatchConfidence = possibleMatch.MatchConfidence,
                        MasterId = possibleMatch.MasterId,
                        
                        DesignationId = possibleMatch.DesignationId,
                        DesignationName = possibleMatch.DesignationName,
                        KFSAccount = possibleMatch.KFSAccount,
                        VSECategory = possibleMatch.VSECategoryName
                    });
                }
                return PartialView("DesignationPossibleMatchList", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to retrieve Designation GetPossibleMatchList");
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: Designation/DesignationCompare
        public IActionResult DesignationCompare(long Id, int SystemId, string MasterId)
        {
            try
            {
                var detail = _context.GetDesignationMatchDetails(SystemId, Id);
                var comparison = _context.GetDesignationComparisonDetail(SystemId, Id, MasterId);
                var viewModel = new DesignationCompareViewModel()
                {
                    Id = Id,
                    IntegrationId = comparison.IntegrationId,
                    SystemId = comparison.SystemId,
                    MasterId = MasterId,
                    IntegrationDate = comparison.IntegrationDate,
                    System = comparison.System,

                    SourceRecordId = comparison.SourceRecordId,

                    DesignationId = comparison.DesignationId,
                        DesignationId_BusinessName = "Designation ID",
                        DesignationId_Compare = comparison.DesignationId_Compare,
                        DesignationId_IsDifferent = detail.DesignationId != comparison.DesignationId_Compare,
                    DesignationName = comparison.DesignationName,
                        DesignationName_BusinessName = "Designation Name",
                        DesignationName_Compare = comparison.DesignationName_Compare,
                        DesignationName_IsDifferent = detail.DesignationName != comparison.DesignationName_Compare,
                    Description = comparison.Description,
                        Description_BusinessDescription = "Description",
                        Description_Compare = comparison.Description_Compare,
                        Description_IsDifferent = detail.Description != comparison.Description_Compare,
                    DesignationTypeName = comparison.DesignationTypeName,
                        DesignationTypeName_BusinessName = "Designation Type",
                        DesignationTypeName_Compare = comparison.DesignationTypeName_Compare,
                        DesignationTypeName_IsDifferent = detail.DesignationTypeName != comparison.DesignationTypeName_Compare,
                    StartDate = comparison.StartDate,
                        StartDate_BusinessName = "Start Date",
                        StartDate_Compare = comparison.StartDate_Compare,
                        StartDate_IsDifferent = detail.StartDate != (comparison.StartDate_Compare.HasValue ? comparison.StartDate_Compare.Value.ToString("MM/dd/yyyy") : null),
                    EndDate = comparison.EndDate,
                        EndDate_BusinessName = "End Date",
                        EndDate_Compare = comparison.EndDate_Compare,
                        EndDate_IsDifferent = detail.EndDate != (comparison.EndDate_Compare.HasValue ? comparison.EndDate_Compare.Value.ToString("MM/dd/yyyy") : null),
                    KFSAccountCode = comparison.KFSAccountCode,
                        KFSAccountCode_BusinessName = "KFS Account Code",
                        KFSAccountCode_Compare = comparison.KFSAccountCode_Compare,
                        KFSAccountCode_IsDifferent = detail.KFSAccountCode != comparison.KFSAccountCode_Compare,
                    VSECategoryName = comparison.VSECategoryName,
                        VSECategoryName_BusinessName = "VSE Category Name",
                        VSECategoryName_Compare = comparison.VSECategoryName_Compare,
                        VSECategoryName_IsDifferent = detail.VSECategoryName != comparison.VSECategoryName_Compare,
                    GLOrganizationName = comparison.GLOrganizationName,
                        GLOrganizationName_BusinessName = "GL Organization Name",
                        GLOrganizationName_Compare = comparison.GLOrganizationName_Compare,
                        GLOrganizationName_IsDifferent = detail.GLOrganizationName != comparison.GLOrganizationName_Compare,
                    DesignationUseTypeName = comparison.DesignationUseTypeName,
                        DesignationUseTypeName_BusinessName = "Designation Use Type Name",
                        DesignationUseTypeName_Compare = comparison.DesignationUseTypeName_Compare,
                        DesignationUseTypeName_IsDifferent = detail.DesignationUseTypeName != comparison.DesignationUseTypeName_Compare,
                };

                return PartialView("DesignationCompare", viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load DesignationCompare method");
                return RedirectToAction("SystemError", "Error");
            }
        }

        // GET: Designation/DesignationManualMatch
        public async Task<IActionResult> DesignationManualMatch(long Id, int IntegrationId, int SystemId, string MasterId, string ChangeAgent)
        {
            try
            {
                int returnValue = await _context.ManuallyMatchIntegrationRecord(SystemId, IntegrationId, Id, MasterId, ChangeAgent);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load DesignationsManualMatch method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(DesignationList));
        }

        // POST: Designation/DesignationSave
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DesignationSave(DesignationViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    _context.ChangeDesignationIntegrationRecord(model.SystemId, model.Id, model.DesignationId, model.DesignationName, model.Description, model.StartDate, model.EndDate,
                        model.OrganizationalUnit, model.OrganizationalUnitCode, model.OrganizationalUnitMasterId, model.DesignationTypeName, model.DesignationTypeSourceSystemRecordId, model.DesignationTypeMasterId,
                        model.KFSAccountCode, model.VSECategoryName, model.VSECategorySourceSystemRecordId, model.VSECategoryMasterId,
                        model.GLOrganizationName, model.GLOrganizationCode, model.GLOrganizationMasterId, model.DesignationUseTypeName, model.DesignationUseTypeMasterId, model.DesignationStatus, 
                        model.DesignationStatusMasterId, User.Identity.Name);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to load DesignationSave method");
                    return RedirectToAction("SystemError", "Error");
                }
                return RedirectToAction(nameof(DesignationList));
            }
            return View(model);
        }

        // POST: Designation/DesignationRevalidate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DesignationRevalidate(DesignationViewModel model)
        {
            if (model.IsValid())
            {
                try
                {
                    if (model.IsChanged)
                    {
                        _context.ChangeDesignationIntegrationRecord(model.SystemId, model.Id, model.DesignationId, model.DesignationName, model.Description, model.StartDate, model.EndDate,
                            model.OrganizationalUnit, model.OrganizationalUnitCode, model.OrganizationalUnitMasterId, model.DesignationTypeName, model.DesignationTypeSourceSystemRecordId, model.DesignationTypeMasterId,
                            model.KFSAccountCode, model.VSECategoryName, model.VSECategorySourceSystemRecordId, model.VSECategoryMasterId,
                            model.GLOrganizationName, model.GLOrganizationCode, model.GLOrganizationMasterId, model.DesignationUseTypeName, model.DesignationUseTypeMasterId, model.DesignationStatus,
                            model.DesignationStatusMasterId, User.Identity.Name);
                    }
                    _context.RevalidateRecord(model.SystemId, model.IntegrationId, model.Id);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Unable to load DesignationRevalidate method");
                    return RedirectToAction("SystemError", "Error");
                }
                return RedirectToAction(nameof(DesignationList));
            }
            return View(model);
        }

        public IActionResult DesignationIgnore(long Id, int IntegrationId, int SystemId)
        {
            try
            {
                this.RemoveIntegrationRecord(SystemId, IntegrationId, Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unable to load DesignationsManualMatch method");
                return RedirectToAction("SystemError", "Error");
            }

            return RedirectToAction(nameof(DesignationList));
        }

        #endregion

    }
}