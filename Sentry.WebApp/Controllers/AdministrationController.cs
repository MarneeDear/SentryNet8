using Sentry.WebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
//using System.ServiceProcess;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using Sentry.WebApp.ViewModels.Administration;
using Sentry.WebApp.Services;
using Sentry.Domain.AccountsPayable.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
//using Humanizer;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeSentryUsers]
    public class AdministrationController : IntegrationController
    {
        public readonly Config _config;

        public AdministrationController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<AdministrationController> logger, 
            IConfiguration configuration,
            IOptions<Config> config,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) 
        {
            _config = config.Value;
        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        private IEnumerable<SelectListItem> Roles(string currentRole)
        {
            return new List<SelectListItem>() 
            {
                new SelectListItem()
                {
                    Text = ADMIN_ROLE,
                    Value = ADMIN_ROLE,
                    Selected = currentRole == ADMIN_ROLE
                },
                new SelectListItem()
                {
                    Text = Constants.APReviewerRole,
                    Value = Constants.APReviewerRole,
                    Selected = currentRole == Constants.APReviewerRole
                },
                new SelectListItem()
                {
                    Text = Constants.ScholarshipRole,
                    Value = Constants.ScholarshipRole,
                    Selected = currentRole == Constants.ScholarshipRole
                },
                new SelectListItem()
                {
                    Text = Constants.APManagerRole,
                    Value = Constants.APManagerRole,
                    Selected = currentRole == Constants.APManagerRole
                },
                new SelectListItem()
                {
                    Text = Constants.AssociateVPRole,
                    Value = Constants.AssociateVPRole,
                    Selected = currentRole == Constants.AssociateVPRole
                },
                new SelectListItem()
                {
                    Text = Constants.VPRole,
                    Value = Constants.VPRole,
                    Selected = currentRole == Constants.VPRole
                },
                new SelectListItem()
                {
                    Text = Constants.CFORole,
                    Value = Constants.CFORole,
                    Selected = currentRole == Constants.CFORole
                },
                new SelectListItem()
                {
                    Text = Constants.GeneralCounselRole,
                    Value = Constants.GeneralCounselRole,
                    Selected = currentRole == Constants.GeneralCounselRole
                },
                new SelectListItem()
                {
                    Text = Constants.ARStaffRole,
                    Value = Constants.ARStaffRole,
                    Selected = currentRole == Constants.ARStaffRole
                },
                new SelectListItem()
                {
                    Text = Constants.ARSupervisorReviewerRole,
                    Value = Constants.ARSupervisorReviewerRole,
                    Selected = currentRole == Constants.ARSupervisorReviewerRole
                },
                new SelectListItem()
                {
                    Text = Constants.ARGUStaffRole,
                    Value = Constants.ARGUStaffRole,
                    Selected = currentRole == Constants.ARGUStaffRole
                },
                new SelectListItem()
                {
                    Text = Constants.FTReviewerRole,
                    Value = Constants.FTReviewerRole,
                    Selected = currentRole == Constants.FTReviewerRole
                },
                new SelectListItem()
                {
                    Text = Constants.FTApproverRole,
                    Value = Constants.FTApproverRole,
                    Selected = currentRole == Constants.FTApproverRole
                },
                new SelectListItem()
                {
                    Text = Constants.FTGeneralCounselApproverRole,
                    Value = Constants.FTGeneralCounselApproverRole,
                    Selected = currentRole == Constants.FTGeneralCounselApproverRole
                }
                //new SelectListItem()
                //{
                //    Text = Constants.FinancialServicesStewardRole,
                //    Value = Constants.FinancialServicesStewardRole,
                //    Selected = currentRole == Constants.FinancialServicesStewardRole
                //}
            };
        }

        [HttpGet]
        public IActionResult RolesAdmin()
        {
            var model = new BaseViewModel()
            {
                Title = "Roles Admin",
                PageId = "rolesAdminPage",
                ActiveClass = "Administration",
                Message = "Roles Admin",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                CurrentRole = Helper.AdminImpersonationRole ?? Helper.FinancialRole,
                AllowAlternateApprover = true,
                AlternateApprovers = Roles(Helper.AdminImpersonationRole ?? Helper.FinancialRole)
            };

            return View(model);
        }


        [HttpPost]
        public IActionResult RolesAdmin(BaseViewModel model)
        {
            HttpContext.Session.Remove(SESSION_ROLE_KEY);
            if (model.CurrentRole == ADMIN_ROLE)
            {
                HttpContext.Session.Remove(ADMIN_SESSION_ROLE_KEY);
            }
            else
            {
                HttpContext.Session.SetString(ADMIN_SESSION_ROLE_KEY, model.CurrentRole);
            }

            var newModel = new BaseViewModel()
            {
                Title = "Roles Admin",
                PageId = "rolesAdminPage",
                ActiveClass = "Administration",
                Message = "Roles Admin",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                CurrentRole = model.CurrentRole,
                AllowAlternateApprover = true,
                AlternateApprovers = Roles(model.CurrentRole)
            };

            return View(newModel);
        }

        public IActionResult QueueProcessor()
        {
            try
            {
                QueueProcessors queueProcessorsConfiguration = _configuration.GetSection("QueueProcessors").Get<QueueProcessors>();

                var model = new QueueProcessorViewModel()
                {
                    Title = "Queue Processors",
                    PageId = "queueProcessorPage",
                    ActiveClass = "Administration",
                    Message = "Queue Processor Status Page",
                    NavigationGroups = GetNavigationGroups(),
                    User = User.Identity.Name,
                    ServerName = queueProcessorsConfiguration.Server,
                    ApplicationQueueProcessors = LoadApplicationQueueProcessors(queueProcessorsConfiguration)
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting queue processor statuses");

                throw;
            }
        }

        private List<ApplicationQueueProcessor> LoadApplicationQueueProcessors(QueueProcessors QueueProcessorsConfiguration)
        {
            var applicationQueueProcessorList = new List<ApplicationQueueProcessor>();
            //TODO revisit functionality
            //foreach (var application in QueueProcessorsConfiguration.Applications)
            //{
            //    var applicationProcessor = new ApplicationQueueProcessor()
            //    {
            //        Name = application.Name,
            //        InboundQueueProcessor = null,
            //        OutboundQueueProcessor = null
            //    };

            //    if (application.InboundQueueProcessor != null)
            //    {
            //        var inboundServiceController = new ServiceController(application.InboundQueueProcessor, QueueProcessorsConfiguration.Server);

            //        applicationProcessor.InboundQueueProcessor = new Sentry.WebApp.ViewModels.QueueProcessor()
            //        {
            //            Name = inboundServiceController.ServiceName,
            //            DisplayName = inboundServiceController.DisplayName,
            //            Status = (QueueProcessorStatus)inboundServiceController.Status
            //        };
            //    }

            //    if (application.OutboundQueueProcessor != null)
            //    {
            //        var outboundServiceController = new ServiceController(application.OutboundQueueProcessor, QueueProcessorsConfiguration.Server);

            //        applicationProcessor.OutboundQueueProcessor = new QueueProcessor()
            //        {
            //            Name = outboundServiceController.ServiceName,
            //            DisplayName = outboundServiceController.DisplayName,
            //            Status = (QueueProcessorStatus)outboundServiceController.Status
            //        };
            //    }

            //    applicationQueueProcessorList.Add(applicationProcessor);
            //}
            return applicationQueueProcessorList;
        }

        public async Task<IActionResult> QueueEntryCounts()
        {
            try
            {
                var model = new QueueEntryCountsViewModel()
                {
                    Title = "Queue Entry Counts",
                    PageId = "queueEntryCountsPage",
                    ActiveClass = "Administration",
                    Message = "Queue Entry Counts Page",
                    NavigationGroups = GetNavigationGroups(),
                    User = User.Identity.Name,
                    QueueEntryCountRows = await _context.GetQueueEntryCounts()
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting QueueEntryCounts");

                throw;
            }
        }

        [Route("[controller]/IntegrationHealth")]
        public IActionResult IntegrationHealth()
        {
            try
            {
                var model = new IntegrationHealthViewModel()
                {
                    Title = "Integration Health",
                    PageId = "integrationHealthPage",
                    ActiveClass = "Administration",
                    Message = "Integration Health Page",
                    User = User.Identity.Name,
                    //Server = _configuration["server"],
                    NavigationGroups = GetNavigationGroups(),
                    //Integrations = LoadIntegrationsList(DefaultDisplayName: "Integration"),
                    //Systems = LoadIntegrationSystemsList(DefaultDisplayName: "System"),
                };

                var entities = _config.HealthCategories;

                var entitiesModel = new List<Entity>();

                foreach (var entity in entities)
                {
                    var categorySystemIntegrations = _context.GetCategorySystemIntegrations(entity);

                    entitiesModel.Add(new Entity(categorySystemIntegrations)
                    {
                        Name = entity
                        //Categories = created with constructor
                    });
                }

                model.Entities = entitiesModel;

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception getting Integration Health");
                throw;
            }
        }

        /// <summary>
        /// Designed to be called with AJAX
        /// Gets the integration health from the database and return via ajax
        /// </summary>
        /// <param name="systemId"></param>
        /// <param name="integrationId"></param>
        /// <returns>SystemIntegrationHealth</returns>
        public async Task<IActionResult> LoadIntegrationHealth(int systemId, int integrationId)
        {
            try
            {
                var integrationHealth = _context.GetIntegrationHealth(systemId, integrationId);

                var integrationHealthModel = new SystemIntegrationHealth()
                {
                    SourceSystemId = integrationHealth.SystemId,
                    UnprocessedRecords = integrationHealth.UnprocessedRecords,
                    UnmasteredRecords = integrationHealth.UnmasteredRecords,
                    UnpromotedRecords = integrationHealth.UnpromotedRecords,
                    HistoryInsertTriggerEnabled = integrationHealth.HistoryTriggerEnabled,
                    StageInsertTriggerEnabled = integrationHealth.StgTriggerEnabled,
                    GoodInsertTriggerEnabled = integrationHealth.GoodTriggerEnabled,
                    BadInsertTriggerEnabled = integrationHealth.BadTriggerEnabled
                };

                return await Task.FromResult(new JsonResult(integrationHealthModel));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [Route("[controller]/IntegrationStatusesDataByIntegration/{integration}")]
        public async Task<IActionResult> IntegrationStatusesDataByIntegration([FromRoute] string integration)
        {
            try
            {
                var model = new IntegrationsStatusesViewModel()
                {
                    IntegrationStatuses = await LoadIntegrationsStatuses(Integration: integration),
                    IterateOver = "System"
                };

                return PartialView("IntegrationStatuses_partial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in action, IntegrationStatusesTableDataByIntegration");
                return BadRequest("Failed to load data");
            }
        }

        [Route("[controller]/IntegrationStatusesDataBySystem/{system}")]
        public async Task<IActionResult> IntegrationStatusesTableDataBySystem([FromRoute] string system)
        {
            try
            {
                var model = new IntegrationsStatusesViewModel()
                {
                    IntegrationStatuses = await LoadIntegrationsStatuses(System: system),
                    IterateOver = "Integration"
                };

                return PartialView("IntegrationStatuses_partial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in action, IntegrationStatusesTableDataBySystem");
                return BadRequest("Failed to load data");
            }
        }

        //private async Task<IActionResult> LoadIntegrationStatusesBySystemVM(string system = null)
        //{
        //    try
        //    {
        //        var model = new IntegrationsStatusesViewModel()
        //        {
        //            Title = "Integration Statuses",
        //            PageId = "integrationStatusesPage",
        //            ActiveClass = "Administration",
        //            Message = "Integration Statuses Administration Page",
        //            User = User.Identity.Name,
        //            Server = _configuration["server"],
        //            RemediationCounts = GetRemediationCounts(),
        //            NavigationGroups = GetNavigationGroups(),
        //            Integrations = LoadIntegrationsList(DefaultDisplayName: "System")
        //        };

        //        if (system != null) model.IntegrationStatuses = await LoadIntegrationsStatuses(system);
        //        model.SystemCount = model.IntegrationStatuses != null ? model.IntegrationStatuses.Length : 0;

        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception getting Integration Statuses");
        //        throw;
        //    }
        //}
        //private async Task<IActionResult> LoadIntegrationStatusesByIntegrationVM(string integration = null)
        //{
        //    try
        //    {
        //        var model = new IntegrationsStatusesViewModel()
        //        {
        //            Title = "Integration Statuses",
        //            PageId = "integrationStatusesPage",
        //            ActiveClass = "Administration",
        //            Message = "Integration Statuses Administration Page",
        //            User = User.Identity.Name,
        //            Server = _configuration["server"],
        //            RemediationCounts = GetRemediationCounts(),
        //            NavigationGroups = GetNavigationGroups(),
        //            Integrations = LoadIntegrationsList(DefaultDisplayName: "Integration")
        //        };

        //        if (integration != null) model.IntegrationStatuses = await LoadIntegrationsStatuses(integration);
        //        model.IntegrationCount = model.IntegrationStatuses != null ? model.IntegrationStatuses.Length : 0;

        //        return View(model);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception getting Integration Statuses");
        //        throw;
        //    }
        //}

        private async Task<IntegrationStatusData[]> LoadIntegrationsStatuses(string Integration = null, string System = null)
        {
            IntegrationStatus[] integrationStatuses = Integration != null ? await _context.LoadIntegrationStatusesByIntegration(Integration) :
                                                      System != null ? await _context.LoadIntegrationStatusesBySystem(System) : integrationStatuses = null;
            IntegrationStatusData[] integrationStatuses_VM = new IntegrationStatusData[integrationStatuses.Length];
            int i = 0;

            foreach (IntegrationStatus integrationStatus in integrationStatuses)
            {
                integrationStatuses_VM[i++] = new IntegrationStatusData()
                {
                    SystemName = integrationStatus.SystemName,
                    IntegrationName = integrationStatus.IntegrationName,
                    StagingTableTrigger = new IntegrationTrigger
                    {
                        Table = $"{integrationStatus.SystemName}.{integrationStatus.StagingTable}",
                        CurrentState = integrationStatus.StagingTableEnabled.Equals(1) ? TriggerState.Enabled : TriggerState.Disabled,
                        IsChecked = integrationStatus.StagingTableEnabled.Equals(1) ? "checked" : null
                    },
                    GoodTableTrigger = new IntegrationTrigger
                    {
                        Table = $"{integrationStatus.SystemName}.{integrationStatus.GoodTable}",
                        CurrentState = integrationStatus.GoodTableEnabled.Equals(1) ? TriggerState.Enabled : TriggerState.Disabled,
                        IsChecked = integrationStatus.GoodTableEnabled.Equals(1) ? "checked" : null
                    },
                    BadTableTrigger = new IntegrationTrigger
                    {
                        Table = $"{integrationStatus.SystemName}.{integrationStatus.BadTable}",
                        CurrentState = integrationStatus.BadTableEnabled.Equals(1) ? TriggerState.Enabled : TriggerState.Disabled,
                        IsChecked = integrationStatus.BadTableEnabled.Equals(1) ? "checked" : null
                    }
                };
            }
            return integrationStatuses_VM;
        }

        [HttpPost]
        public JsonResult SetMultipleTriggers([FromBody] IntegrationTrigger[] integrationTriggers)
        {
            foreach (IntegrationTrigger trigger in integrationTriggers)
            {
                try
                {
                    string[] split = trigger.Table.Split(".");
                    trigger.Schema = split[0];
                    trigger.Table = split[1];
                    _context.SetTriggerState(trigger.Schema, trigger.Table, trigger.ActionIsEnable);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning($"Unable to set trigger: {ex.Message}");
                }
            }
            return Json(true);
        }

        [HttpPost]
        public JsonResult SetSingleTrigger(string SchemaAndTable, bool ActionIsEnable)
        {
            string[] split = SchemaAndTable.Split(".");
            string Schema = split[0];
            string Table = split[1];
            return _context.SetTriggerState(Schema, Table, ActionIsEnable) == 0 ? Json(true) : Json(false);
        }

        public IActionResult Monitor()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Integrations Monitor",
                PageId = "integrationMonitorPage",
                ActiveClass = "Administration",
                Message = "Queue Entry Counts Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name
            };

            ViewBag.IntegrationsMonitorUri = _configuration["IntegrationsMonitor"];

            return View(viewModel);
        }

        public async Task<IActionResult> TopDrivers(TimeFrame timeFrame = TimeFrame.ThisMonth)
        {
            try
            {
                var model = new TopDriversViewModel()
                {
                    Title = "Errors - Top Drivers",
                    PageId = "topDriversPage",
                    ActiveClass = "Administration",
                    Message = "Top Drivers Administration Page",
                    User = User.Identity.Name,
                    NavigationGroups = GetNavigationGroups(),
                    TopDrivers = await LoadTopDrivers(timeFrame)
                    };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in action, TopDrivers");
                return BadRequest("Failed to load page");
            }
        }

        [Route("[controller]/TopDriversTableData/{timeFrame}")]
        public async Task<IActionResult> TopDriversTableData([FromRoute] TimeFrame timeFrame = TimeFrame.ThisMonth)
        {
            try
            {
                var model = new TopDriversViewModel()
                {
                    TopDrivers = await LoadTopDrivers(timeFrame)
                };

                return PartialView("TopDrivers_partial", model);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception in action, TopDriversDataTable");
                return BadRequest("Failed to load data");
            }
        }

        private async Task<IEnumerable<LogEntry>> LoadTopDrivers(TimeFrame timeFrame)
        {
            DateTime? StartDate = null;
            DateTime? EndDate = null;
            DateTime today = DateTime.Now;


            switch (timeFrame)
            {
                case TimeFrame.Last7Days:
                    StartDate = today.AddDays(-7);
                    break;
                case TimeFrame.ThisMonth:
                    StartDate = new DateTime(today.Year, today.Month, 1);
                    break;
                case TimeFrame.Last30Days:
                    StartDate = today.AddDays(-30);
                    break;
                case TimeFrame.YearToDate:
                    StartDate = new DateTime(today.Year, 1, 1);
                    break;
                default:
                    break;
            }
            return await _context.LoadTopDrivers(StartDate, EndDate);
        }

    }
}