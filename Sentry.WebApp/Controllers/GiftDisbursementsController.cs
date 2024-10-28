using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models.Notifications;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.ViewModels.GiftDisbursements;
using Sentry.WebApp.ViewModels.SupportingDocuments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sentry.Domain.AccountsPayable.Entities;
using Sentry.WebApp.Extensions;
using Microsoft.AspNetCore.Hosting;
using Sentry.WebApp.ViewModels.Invoices;
//using Microsoft.Extensions.Azure;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeFinance()]
    public class GiftDisbursementsController : IntegrationController
    {
        public readonly Config _config;
        private readonly INotificationService _notificationService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _invoicePdfPath;

        public GiftDisbursementsController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<GiftDisbursementsController> logger,
            IConfiguration configuration,
            IDomainService domainService,
            IOptions<Config> config,
            INotificationService notificationService,
            IWebHostEnvironment environment
            )
            : base(context, dwContext, logger, configuration, domainService)
        {
            _config = config.Value;
            _notificationService = notificationService;
            _environment = environment;
            _invoicePdfPath = $"{environment.WebRootPath}\\invoice-pdfs";

        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        private async Task<IEnumerable<GiftDisbursementsListItem>> SetupList(string type)
        {
            if (Helper.IsAPRole)
            {
                //Need to use the alternate role
                var currentFinancialRole = Constants.APApproverRoleIds[Helper.CurrentFinancialRole];
                var giftDisbursementList = await getGiftDisbursementByRoleId(currentFinancialRole);

                var listModel = giftDisbursementList
                    .GiftDisbursements
                    .Where(d => d.Type == type)
                    .Select(d => (GiftDisbursementsListItem)d)
                    .ToList();

                var employeeId = await Helper.EmployeeId();
                foreach (var item in listModel)
                {
                    if (item.Rejected)
                    {
                        item.RowStatusClass = "rejectedStatus";
                    }
                    else if (item.Resubmitted)
                    {
                        item.RowStatusClass = "resubmittedStatus";
                    }
                    else if (item.APReviewerEmployeeId == employeeId)
                    {
                        item.RowStatusClass = "approvedStatus";
                    }
                }

                return listModel;
            }

            return Enumerable.Empty<GiftDisbursementsListItem>();
        }

        private async Task<GiftDisbursementList> getGiftDisbursementByRoleId(int roleId)
        {
            return await _domainService.AccountsPayableOperations.GetGiftDisbursementsByRoleId(roleId);
        }

        private bool AllowEdit(string role)
        {
            return role == Constants.APReviewerRole
                || role == Constants.APManagerRole
                || role == Constants.ScholarshipRole;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            GiftDisbursementDashboard viewModel = new GiftDisbursementDashboard()
            {
                Title = "Gift Disbursements",
                PageId = "giftDisbursementsPage",
                ActiveClass = "GiftDisbursements",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole,
                AllowAlternateApprover = AllowAlternateApprovers(),
                AlternateApprovers = AlternateApprovers(),
                ApReviewerDisbursements = await getGiftDisbursementByRoleId(Constants.APReviewerRoleId),
                GeneralCounselDisbursements = await getGiftDisbursementByRoleId(Constants.GeneralCounselRoleId),
                AssociateVicePresidentDisbursements = await getGiftDisbursementByRoleId(Constants.AssistantVPRoleId),
                VicePresidentDisbursements = await getGiftDisbursementByRoleId(Constants.VPRoleId),
                CFODisbursements = await getGiftDisbursementByRoleId(Constants.CFORoleId),
                ScholarshipDisbursements = await getGiftDisbursementByRoleId(Constants.ScholarshipRoleId),
                ApManagerDisbursements = await getGiftDisbursementByRoleId(Constants.APManagerRoleId)
            };
            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(BaseViewModel model)
        {
            if (model.CurrentRole == Helper.FinancialRole)
            {
                HttpContext.Session.Remove(SESSION_ROLE_KEY);
            }
            else
            {
                HttpContext.Session.SetString(SESSION_ROLE_KEY, model.CurrentRole);
            }

            GiftDisbursementDashboard viewModel = new GiftDisbursementDashboard()
            {
                Title = "Gift Disbursements",
                PageId = "giftDisbursementsPage",
                ActiveClass = "GiftDisbursements",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole,
                AllowAlternateApprover = AllowAlternateApprovers(),
                AlternateApprovers = AlternateApprovers(),
                ApReviewerDisbursements = await getGiftDisbursementByRoleId(Constants.APReviewerRoleId),
                GeneralCounselDisbursements = await getGiftDisbursementByRoleId(Constants.GeneralCounselRoleId),
                AssociateVicePresidentDisbursements = await getGiftDisbursementByRoleId(Constants.AssistantVPRoleId),
                VicePresidentDisbursements = await getGiftDisbursementByRoleId(Constants.VPRoleId),
                CFODisbursements = await getGiftDisbursementByRoleId(Constants.CFORoleId),
                ScholarshipDisbursements = await getGiftDisbursementByRoleId(Constants.ScholarshipRoleId),
                ApManagerDisbursements = await getGiftDisbursementByRoleId(Constants.APReviewerRoleId)
            };
            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return View("Index", viewModel);
        }

        private bool AllowAlternateApprovers()
        {
            //return Helper.FinancialRole != Constants.ScholarshipRole
            //        && Helper.FinancialRole != Constants.APReviewerRole;

            return true;
        }

        private IEnumerable<SelectListItem> AlternateApprovers()
        {
            var role = Helper.FinancialRole; //Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            var approverRoles = new List<SelectListItem>();

            var alternateRoles = new Dictionary<string, SelectListItem>()
            {
                {
                    Constants.APReviewerRole, new SelectListItem(){
                    Text = Constants.APReviewerRole == Helper.FinancialRole ? $"{Constants.APReviewerRole} (My Role)" : Constants.APReviewerRole,
                    Value = Constants.APReviewerRole,
                    Selected = role == Constants.APReviewerRole
                }
                },
                {
                    Constants.APManagerRole, new SelectListItem(){
                    Text = Constants.APManagerRole == Helper.FinancialRole ? $"{Constants.APManagerRole} (My Role)" : Constants.APManagerRole,
                    Value = Constants.APManagerRole,
                    Selected = role == Constants.APManagerRole
                }
                },
                {
                    Constants.AssociateVPRole, new SelectListItem(){
                    Text = Constants.AssociateVPRole == Helper.FinancialRole ? $"{Constants.AssociateVPRole} (My Role)" : Constants.AssociateVPRole,
                    Value = Constants.AssociateVPRole,
                    Selected = role == Constants.AssociateVPRole
                }
                },
                {
                    Constants.VPRole, new SelectListItem(){
                    Text = Constants.VPRole == Helper.FinancialRole ? $"{Constants.VPRole} (My Role)" : Constants.VPRole,
                    Value = Constants.VPRole,
                    Selected = role == Constants.VPRole
                }
                },
                {
                    Constants.CFORole, new SelectListItem(){
                    Text = Constants.CFORole == Helper.FinancialRole ? $"{Constants.CFORole} (My Role)" : Constants.CFORole,
                    Value = Constants.CFORole,
                    Selected = role == Constants.CFORole
                }
                }

            };

            switch (role)
            {
                case Constants.APReviewerRole:
                    approverRoles.Add(alternateRoles[Constants.APManagerRole]);
                    approverRoles.Add(alternateRoles[Constants.APReviewerRole]);
                    break;
                case Constants.APManagerRole:
                    approverRoles.Add(alternateRoles[Constants.APManagerRole]);
                    approverRoles.Add(alternateRoles[Constants.APReviewerRole]);
                    break;
                case Constants.AssociateVPRole:
                case Constants.VPRole:
                case Constants.CFORole:
                    approverRoles.Add(alternateRoles[Constants.APManagerRole]);
                    approverRoles.Add(alternateRoles[Constants.AssociateVPRole]);
                    approverRoles.Add(alternateRoles[Constants.VPRole]);
                    approverRoles.Add(alternateRoles[Constants.CFORole]);
                    break;
                default:
                    break;
            }
            return approverRoles;
        }

        public async Task<IActionResult> UATransfers()
        {
            var disbursements = await SetupList(Constants.ETForm);

            var model = DisbursementStatusPageSetup();
            model.Disbursements = disbursements;
            model.AllowAlternateApprover = AllowAlternateApprovers();
            //model.AlternateApprovers = AlternateApprovers(alternateApproverRole);
            model.ListType = "UATransfers";
            ViewBag.EmployeeId = await Helper.EmployeeId();
            return View(model);
        }

        public async Task<IActionResult> Scholarships()
        {
            var disbursements = await SetupList(Constants.STForm);

            var model = DisbursementStatusPageSetup();
            model.Disbursements = disbursements;
            model.AllowAlternateApprover = AllowAlternateApprovers();
            //model.AlternateApprovers = AlternateApprovers(alternateApproverRole);
            model.ListType = "Scholarships";
            return View(model);

        }

        public async Task<IActionResult> Disbursements()
        {
            var disbursements = await SetupList(Constants.EMForm);

            var model = DisbursementStatusPageSetup();
            model.Disbursements = disbursements;

            model.AllowAlternateApprover = AllowAlternateApprovers();
            //model.AlternateApprovers = AlternateApprovers(alternateApproverRole);
            model.ListType = "Disbursements";
            ViewBag.EmployeeId = await Helper.EmployeeId();
            return View(model);
        }

        public async Task<IActionResult> ReadyForProcessing()
        {
            var processingDisbursements = await _domainService.AccountsPayableOperations.GetGiftDisbursementsAwaitingProcessing();
            var model = DisbursementStatusPageSetup();
            model.Disbursements = Enumerable.Empty<GiftDisbursementsListItem>();

            if (Helper.CurrentFinancialRole == Constants.APReviewerRole || Helper.CurrentFinancialRole == ADMIN_ROLE)
            {
                model.Disbursements = processingDisbursements.GiftDisbursements.Select(d => (GiftDisbursementsListItem)d);
            }

            return View(model);
        }

        private GiftDisbursementsListViewModel DisbursementStatusPageSetup()
        {
            var model = new GiftDisbursementsListViewModel()
            {
                Title = $"Gift Disbursements",
                PageId = "giftDisbursementsPage",
                ActiveClass = "Administration",
                Message = "Gift Disbursements Status Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                Organization = "UA",
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole
            };

            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return model;
        }

        private GiftDisbursementsViewModel SetupIntegration(GiftDisbursementsViewModel model)
        {
            model.IsChanged = false;
            model.Id = 0L;
            model.System = "GiftDisbursements";
            model.SystemId = 0;
            model.Integration = String.Empty;
            model.RecordStatus = String.Empty;
            model.SourceRecordId = String.Empty;
            model.ChangeAgent = String.Empty;
            model.CreatedOnDT = DateTime.Now;
            model.Title = "Gift Disbursements";
            model.PageId = "giftDisbursementsPage";
            model.ActiveClass = "Administration";
            model.Message = "Gift Disbursements Status Page";
            model.NavigationGroups = GetNavigationGroups();
            model.User = User.Identity.Name;
            model.Organization = "UA";

            return model;
        }

        private GiftDisbursementsViewModel SetupItemProjects(GiftDisbursementsViewModel model, Sentry.Domain.AccountsPayable.Entities.GiftDisbursement giftDisbursement)
        {
            //SETUP PROJECTS
            model.GiftDisbursementProjects = new List<ViewModels.GiftDisbursements.GiftDisbursementProject>();
            var items = giftDisbursement.GiftDisbursementItems;

            var projects = items.Select(i => i.ProjectId).Distinct();
            //model.GiftDisbursementItems.Select(i => i.ProjectId).Distinct();
            foreach (var project in projects)
            {
                var projectItems = items.Where(i => i.ProjectId == project);
                var reviewProject = new ViewModels.GiftDisbursements.GiftDisbursementProject()
                {
                    ProjectId = projectItems.First().ProjectId,
                    ProjectName = projectItems.First().ProjectName,
                    ProjectBalance = projectItems.First().ProjectBalance,
                    ProjectItems = projectItems.Select(i => (ViewModels.GiftDisbursements.ProjectItem)i).ToList(),
                    BlackbaudProjectUrl = $"{_config.Blackbaud.Project.BaseUrl}/{projectItems.First().ProjectFeId}?envid={_config.Blackbaud.Project.EnvironmentId}"
                };
                model.GiftDisbursementProjects.Add(reviewProject);
            }

            model.ProjectsTotalCount = projects.Count();
            model.TransactionsTotalCount = items.Count();

            return model;
        }

        private async Task<Sentry.Domain.AccountsPayable.Entities.GiftDisbursement> GetGiftDisbursement(long id)
        {
            var giftDisbursement = await _domainService.AccountsPayableOperations
                .GetGiftDisbursement(id);

            return giftDisbursement;
        }

        private async Task<Sentry.Domain.AccountsPayable.Entities.GiftDisbursement> GetGiftDisbursement(string formNumber)
        {
            var giftDisbursement = await _domainService.AccountsPayableOperations.GetGiftDisbursement(formNumber);
            return giftDisbursement;
        }

        //private GiftDisbursementsViewModel SetupViewModel(GiftDisbursementsViewModel model, string role)
        //{
        //    var giftDisbursement = GetGiftDisbursement(model.GiftDisbursementId);

        //    model = SetupIntegration(model);

        //    model.IsReadyForProcessing = IsReadyForProcessing(giftDisbursement); 
        //    model.AllowProcessing = Helper.FinancialRole == Constants.APReviewerRole;
        //    model.OverallTotal = giftDisbursement.GiftDisbursementItems.Sum(i => i.Amount);

        //    model.AllowEdit = AllowEdit(role);
        //    model.Approvers = giftDisbursement.GiftDisbursementApprovers
        //        .Select(a => (ViewModels.GiftDisbursements.GiftDisbursementApprover)a)
        //        .GroupBy(a => new { a.Name, a.Type }).Select(g => g.First())
        //        .OrderByDescending(a => a.ApprovedOn);
        //    model.ApprovalHistory = giftDisbursement.GiftDisbursementApprovalHistory
        //        .Select(a => (ViewModels.GiftDisbursements.GiftDisbursementApprover)a)
        //        .GroupBy(a => new { a.Name, a.Type }).Select(g => g.First())
        //        .OrderByDescending(a => a.ApprovedOn);
        //    model.ApprovingOnBehalfOfRole = role == Helper.FinancialRole ? String.Empty : role; ;
        //    model.AllowApproval = AllowedToApprove(giftDisbursement, role);
        //    ViewBag.CurrentRole = role;
        //    return model;
        //}

        private bool ApprovedByRole(Sentry.Domain.AccountsPayable.Entities.GiftDisbursement giftDisbursement)
        {
            return giftDisbursement.GiftDisbursementApprovers
                .Where(a => a.Type == (Helper.AlternateFinancialRole ?? Helper.FinancialRole))
                .Where(a => a.Approved.HasValue).Any();
        }

        private async Task<bool> ApprovedByUser(GiftDisbursement giftDisbursement)
        {
            var approverIds = giftDisbursement.GiftDisbursementApprovers
                .Where(a => a.Approved == true)
                .Where(a => a.Type != Constants.DesigneeRole) //All approval if FS user who approved on the campus side
                .Where(a => a.Type != Constants.SignatureAuthorityRole) //Allow approval is FS user who approved on the campus side
                .Select(a => a.EmployeeId);
            var employeeId = await Helper.EmployeeId();
            return approverIds.Contains(employeeId);
        }

        private bool AllowedToApprove(GiftDisbursement giftDisbursement, string role)
        {
            var approversRoles = giftDisbursement.GiftDisbursementApprovers;
            // checks to see if it still needs Provost or VP Health Scieneces approval still
            var provostandVPApprovers = approversRoles.Where(a => a.Type == "Provost" || a.Type == "VP Health Sciences")
                                                      .Where(a => !a.Approved.HasValue).Any();
            if (provostandVPApprovers)
            {
                return false;
            }

            foreach (var approver in approversRoles)
            {               
                if (role == approver.Type 
                    && !approver.Approved.HasValue)
                {
                    return true;
                }
            }
            return false;
        }

        private bool IsReadyForProcessing(GiftDisbursement form)
        {
            var ready = !form.GiftDisbursementApprovers.Any(a => a.Approved != true);
            return ready;
        }

        private async Task<GiftDisbursementsViewModel> SetupViewModel(long giftDisbursementId, string role)
        {
            var giftDisbursement = await GetGiftDisbursement(giftDisbursementId);

            var model = (GiftDisbursementsViewModel)giftDisbursement;

            model = SetupIntegration(model);
            model = SetupItemProjects(model, giftDisbursement);
            model.IsReadyForProcessing = IsReadyForProcessing(giftDisbursement); 
            model.AllowProcessing = Helper.FinancialRole == Constants.APReviewerRole;

            model.OverallTotal = giftDisbursement.GiftDisbursementItems.Sum(i => i.Amount);

            model.AllowEdit = AllowEdit(role);
            model.PreviouslyApprovedByRole = ApprovedByRole(giftDisbursement);

            model.PreviouslyApprovedByUser = await ApprovedByUser(giftDisbursement);

            model.PostDate = giftDisbursement.PostDate > DateTime.MinValue ? giftDisbursement.PostDate.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy");

            var giftProjects = model.GiftDisbursementProjects;
            var invoiceDescription = model.InvoiceDescription;
            var type = model.Type;
            var kfsAccountNumber = model.GiftDisbursementProjects[0].ProjectItems[0].UaAccount;
            var projectCount = model.GiftDisbursementProjects.Count();
            model.InvoiceDescription = GetInvoiceDescription(invoiceDescription, type, kfsAccountNumber, projectCount);
            //model.Approvers = model.Approvers; //.OrderByDescending(a => a.ApprovedOn);
            //model.ApprovalHistory = model.ApprovalHistory; //.OrderByDescending(a => a.ApprovedOn);
            model.ApprovingOnBehalfOfRole = role == Helper.FinancialRole ? String.Empty : role;
            model.AllowApproval = AllowedToApprove(giftDisbursement, role);
            model.ValidFileTypes = _config.ValidFileTypes;
            ViewBag.CurrentRole = role;
            model.ReviewerNotes = giftDisbursement.ReviewerNotes;
            return model;
        }

        private string GetInvoiceDescription(string invoiceDescription, string type, string kfsAccountNumber, int projectCount)
        {
            if (String.IsNullOrEmpty(invoiceDescription))
            {
                if (type == Constants.EMForm)
                {
                    return "REIMBURSEMENT";
                }
                else
                {
                    if (projectCount > 1)
                    {
                        return "MULTIPLE KFS";
                    }
                    else
                    {
                        return kfsAccountNumber;
                    }
                }

            }
            return invoiceDescription;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long giftDisbursementId)
        {
            var role = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            try
            {
                if (AllowEdit(role) || role == ADMIN_ROLE)
                {
                    var model = await SetupViewModel(giftDisbursementId, role);                    

                    return View(model);
                }

                return RedirectToAction("Review", new { giftDisbursementId = giftDisbursementId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting gift disbursement {giftDisbursementId}", giftDisbursementId);
                return RedirectToAction("SystemError", "Error");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(GiftDisbursementsViewModel model) //, string alternateApproverRole = "")
        {
            var role = Helper.CurrentFinancialRole;
            try
            {
                //Commenting out as a hotfix for valid account numbers that do not exist in the system
                //ValidateDebitAccountNumber(model);

                for (int index = model.GiftDisbursementProjects.Count - 1; index >= 0; index--)
                {
                    for(int itemIndex = model.GiftDisbursementProjects[index].ProjectItems.Count - 1; itemIndex >= 0; itemIndex--)
                    {
                        var item = model.GiftDisbursementProjects[index].ProjectItems[itemIndex];

                        if (item.Deleted)
                        {
                            model.GiftDisbursementProjects[index].ProjectItems.RemoveAt(itemIndex);
                        }
                        else if (model.Type == "ST")
                        {
                            //item.ProjectId = model.DisbursementFrom.ProjectId;
                            item.UaAccount = model.DisbursementTo.UaAccount;
                        }
                    }

                    
                }

                if (!ModelState.IsValid)
                {
                    model = await SetupViewModel(model.GiftDisbursementId, role);
                    return View(model);
                }

                await SaveChanges(model);
                var editModel = await SetupViewModel(model.GiftDisbursementId, role);
                return View(editModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving gift disbursement {model.GiftDisbursementId}", model.GiftDisbursementId);
                return RedirectToAction("SystemError", "Error");
            }
        }


        public async Task SaveChanges(GiftDisbursementsViewModel model)
        {
            try
            {
                var updateGiftDisbursement = (Sentry.Domain.AccountsPayable.Entities.UpdateGiftDisbursement)model;

                await _domainService.AccountsPayableOperations.UpdateGiftDisbursement(updateGiftDisbursement);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving gift disbursement {model.GiftDisbursementId}", model.GiftDisbursementId);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Review(long giftDisbursementId)
        {
            var role = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            if (AllowEdit(role))
            {
                return RedirectToAction("Edit", new { giftDisbursementId = giftDisbursementId });
            }

            var reviewModel = await SetupViewModel(giftDisbursementId, role);

            return View(reviewModel);
        }

        private async Task<SupportingDocumentsListViewModel> GetSupportingDocumentsListViewModel(string formNumber)
        {
            try
            {
                var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(formNumber);
                var model = new SupportingDocumentsListViewModel();
                model.SupportingDocuments = supportingDocuments.Select(d => (SupportingDocument)d);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting supporting documents for {formNumber}", formNumber);
                var model = new SupportingDocumentsListViewModel();
                model.SupportingDocuments = Enumerable.Empty<SupportingDocument>();
                model.Error = "Error retrieving supporting documents";
                return model;
            }
        }

        public async Task<IActionResult> ViewSupportingDocument(int id)
        {
            var document = await _domainService.PaperSaveOperations.GetDocumentById(id);

            return File(document.Contents, document.MimeType, $"{document.FileName}");
        }

        private async Task ApproveReject(GiftDisbursementsViewModel model, bool approved)
        {
            if (Helper.FinancialRole != ADMIN_ROLE)
            {
                int? alternateId = null;
                if (!String.IsNullOrEmpty(Helper.AlternateFinancialRole))
                {
                    alternateId = Constants.APApproverRoleIds[Helper.AlternateFinancialRole];
                }

                var approval = new Sentry.Domain.AccountsPayable.Entities.UAFApproval()
                {
                    ApproverRoleId = Constants.APApproverRoleIds[Helper.FinancialRole],
                    Comments = model.Comments ?? String.Empty,
                    Approved = approved,
                    ApprovingAsRoleId = alternateId
                };

                try
                {
                    await _domainService.AccountsPayableOperations.ApproveRejectGiftDisbursement(model.GiftDisbursementId, HttpContext.User.Identity.Name, approval);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error approving disbursement id [{model.GiftDisbursementId}] form number [{model.FormNumber}]");
                    throw;
                }
            }
        }

        private async Task SendAdditionalApproverNotification(long id)
        {
            var giftDisbursement = await _domainService.AccountsPayableOperations.GetGiftDisbursement(id);

            //Find out if has been approved by AP Reviewer
            var approvers = giftDisbursement.GiftDisbursementApprovers
                .Where(a => a.ManuallyAdded && !a.Approved.HasValue);

            if (approvers.Any())
            {
                _logger.LogInformation("Sending additional approber notification to additional approvers");
                await SendNotification(approvers, giftDisbursement);
            }
        }

        private async Task SendEscalatedNotifications(long id)
        {
            var giftDisbursement = await _domainService.AccountsPayableOperations.GetGiftDisbursement(id);

            //Find out if has been approved by AP Reviewer
            var approvers = giftDisbursement.GiftDisbursementApprovers;
            var escalatedApprovers = approvers.Where(a => a.Type != Constants.APReviewerRole
                                                          && a.Type != Constants.DesigneeRole
                                                          && a.Type != Constants.SignatureAuthorityRole
                                                          && !a.Approved.HasValue);
            if (escalatedApprovers.Any())
            {
                var apManagerApproved = approvers.Where(a => a.Type == Constants.APManagerRole && a.Approved == true).Any();
                if (apManagerApproved)
                {
                    //Send to all that apply
                    _logger.LogInformation("Sending escalated notification to escalated approvers");
                    await SendNotification(escalatedApprovers, giftDisbursement);
                    _logger.LogInformation($"Sent escalated notifications for form [{giftDisbursement.FormNumber}] to [{string.Join(",", escalatedApprovers.Select(a => a.Email))}]");
                }
            }

        }

        private async Task SendNotification(IEnumerable<Sentry.Domain.AccountsPayable.Entities.GiftDisbursementApprover> approvers,
            GiftDisbursement giftDisbursement)
        {
            var approverSendTos = approvers.Select(a => new Data.Models.Notifications.SendTo()
            {
                Email = a.Email,
                Name = a.Type
            });

            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();

            var details = new EscalatedApprovalDetails()
            {
                form_id = giftDisbursement.FormNumber,
                amount = giftDisbursement.GiftDisbursementItems.Sum(i => i.Amount).ToString("C"),
                //https://localhost:44383/GiftDisbursements/review?giftDisbursementId=1170
                approve_link = $"https://{HttpContext.Request.Host}/GiftDisbursements/review?giftDisbursementId={giftDisbursement.Id}"
            };
            try
            {
                await _notificationService.SendNotificationAsync(approverSendTos, ccEmailList, _config.SendGrid.DisbursementEscalatedApprovalTemplateId, details);
                _logger.LogInformation($"Sent notification for form [{giftDisbursement.FormNumber}] to [{string.Join(",", approverSendTos.Select(a => a.Email))}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending escalated approver notifications for disbursement id [{giftDisbursement.Id}]");
                throw;
            }
        }

        private async Task SendRejectionNotification(long id, string comments, string ccEmails)
        {
            var giftDisbursement = await _domainService.AccountsPayableOperations.GetGiftDisbursement(id);

            var sendTos = new List<Data.Models.Notifications.SendTo>()
            {
                new Data.Models.Notifications.SendTo()
                {
                    Email = giftDisbursement.PreparedByEmail,
                    Name = $"{giftDisbursement.PreparedByFirstName} {giftDisbursement.PreparedByFirstName}"
                }
            };
             var ccEmailList = SetupCCEmailList(ccEmails);            

            var details = new APReviewRejectionDetails()
            {
                //submitter_name = Constants.APReviewRole,
                comments = comments,
                form_id = giftDisbursement.FormNumber,
                uafdn_link = $"{_config.UAFDNDisbursementURL}/{id}"
            };

            try
            {
                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.APReviewRejectionTemplateId, details);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending escalated approver notifications for disbursement id [{id}]");

                throw;
            }
        }
        private IEnumerable<Data.Models.Notifications.SendTo> SetupCCEmailList(string ccEmails)
        {
            List<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();

            if (!String.IsNullOrEmpty(ccEmails))
            {
                if (ccEmails.Contains(','))
                {
                    var formattedCCEmailLists = ccEmails.Split(',').ToList();

                        foreach (var email in formattedCCEmailLists)
                        {
                            ccEmailList.Add(new Data.Models.Notifications.SendTo()
                            {
                                Name = String.Empty,
                                Email = email.Trim()
                            });
                        }
                        return ccEmailList;                    
                }
                else
                {
                    ccEmailList.Add(new Data.Models.Notifications.SendTo()
                    {
                        Name = String.Empty,
                        Email = ccEmails
                    });

                    return ccEmailList;
                }                
            }
            return ccEmailList;
        }

        public async Task<IActionResult> Reject(GiftDisbursementsViewModel model, string returnView)
        {
            try
            {
                ModelState.Clear();
                _logger.LogInformation($"Rejecting form [{model.FormNumber}]. Assigned Role [{Helper.FinancialRole}] Alternate selected role [{Helper.AlternateFinancialRole}]");

                await ApproveReject(model, false);

                if (Helper.FinancialRole == Constants.APReviewerRole || Helper.FinancialRole == Constants.ScholarshipRole)
                {
                    try
                    {
                        await SendRejectionNotification(model.GiftDisbursementId, model.Comments, model.CCEmails);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending rejection notification to submitter. Form number [{model.FormNumber}] disbursement id [{model.GiftDisbursementId}]"
                            , model.FormNumber, model.GiftDisbursementId);
                    }
                }
                _logger.LogInformation($"Rejected form [{model.FormNumber}]. Redirecting to [{returnView}]");
                return RedirectToAction(returnView);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting disbursement [{model.FormNumber}] return view [{returnView}]", model.FormNumber, returnView);
                return RedirectToAction("SystemError", "Error");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Approve(GiftDisbursementsViewModel model, bool save, string returnView)
        {           
            try
            {
                var role = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
                _logger.LogInformation($"Approving form [{model.FormNumber}]. Assigned Role [{Helper.FinancialRole}] Alternate selected role [{Helper.AlternateFinancialRole}]");

                if (save)
                {
                    //Commenting out to get wround the issue of using debit accounts that do not exist in the system
                    //ValidateDebitAccountNumber(model);
                    if (!ModelState.IsValid)
                    {
                        model = await SetupViewModel(model.GiftDisbursementId, role);
                        return View("Edit", model);
                    }
                    //TODO look at this why is this here?
                    model.IsPending = false;

                    await SaveChanges(model);
                }
                else
                {
                    var giftDisbursement = await GetGiftDisbursement(model.Id);

                    giftDisbursement.Pending = false;
                }

                await ApproveReject(model, true);

                if (role == Constants.APManagerRole)
                {
                    //Send escalated notification
                    try
                    {
                        await SendEscalatedNotifications(model.GiftDisbursementId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending escalated notifications for disbursement form number [{model.FormNumber}] disbursement id [{model.GiftDisbursementId}]", model.FormNumber, model.GiftDisbursementId);
                    }
                }

                if (!String.IsNullOrEmpty(Helper.AlternateFinancialRole))
                {
                    //Send escalated notification to additional approvers
                    try
                    {
                        await SendAdditionalApproverNotification(model.GiftDisbursementId);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error sending escalated notifications for disbursement form number [{model.FormNumber}] disbursement id [{model.GiftDisbursementId}]", model.FormNumber, model.GiftDisbursementId);
                    }
                }
                _logger.LogInformation($"Approved form [{model.FormNumber}]. Redirecting to [{returnView}]");
                return RedirectToAction(returnView);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving disbursement [{model.FormNumber}] return view [{returnView}]", model.FormNumber, returnView);
                return RedirectToAction("SystemError", "Error");
            }

        }

        private async Task UploadPDFView(long id)
        {
            var document = await CreatePDF(id);

            _logger.LogInformation($"Starting upload invoice pdf for file name [{document.FileName}]");

            try
            {
                await _domainService.PaperSaveOperations.UploadDocument(document);
                _logger.LogInformation($"Uploaded file [{document.FileName}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading invoice copy for form number [{document.FormNumber}]");
            }
        }

        private async Task CreateFinalDocuments(long invoiceId, string formNumber)
        {
            try
            {
                _logger.LogInformation($"Starting create invoice final documents for form number [{formNumber}]");
                var finalDocument = new Sentry.Domain.PaperSave.Entities.FinalDocument()
                {
                    SystemId = invoiceId.ToString()
                };
                await _domainService.PaperSaveOperations.CreateFinalDocuments(finalDocument, formNumber);
                _logger.LogInformation($"Created final documents for form number [{formNumber}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating final invoice documents for form number [{formNumber}]", formNumber);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Process(long giftDisbursementId)
        {
            var reviewModel = await SetupViewModel(giftDisbursementId, Helper.FinancialRole);

            return View("Review", reviewModel);

        }

        [HttpPost]
        public IActionResult Process(GiftDisbursementsViewModel model)
        {
            throw new NotImplementedException();
            //ModelState.Clear();
            //_logger.LogInformation($"Starting create invoice for form number [{model.FormNumber}]");

            //try
            //{
            //    CreateInvoice(model.GiftDisbursementId, Convert.ToDateTime(model.PostDate));
            //    _logger.LogInformation($"Created invoice for form number [{model.FormNumber}]");
            //}
            //catch (Exception ex)
            //{
            //    _logger.LogError(ex, "Error proccessing disbursement [{model.FormNumber}]", model.FormNumber);
            //    return RedirectToAction("SystemError", "Error");
            //}
            //return RedirectToAction("ReadyForProcessing");
        }

        [HttpPost]
        public async Task<IActionResult> ResetUAFApprovers(long giftDisbursementId)
        {
            try
            {
                _logger.LogInformation($"Resetting Disbursement [{giftDisbursementId}] UAF Approvers");

                await _domainService.AccountsPayableOperations.ResetGiftDisbursementUAFApprovers(giftDisbursementId);

                _logger.LogInformation($"Reset Disbursement [{giftDisbursementId}] UAF Approvers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting gift disbursement approvers for disbursement id [{giftDisbursementId}]", giftDisbursementId);
            }

            return RedirectToAction("ReadyForProcessing");
        }

        public async Task<IActionResult> UploadSupportingDocument(IFormFile supportingDocument, string formNumber)
        {
            ModelState.Clear();
            var upload = supportingDocument;
            if (supportingDocument == null ||
                String.IsNullOrWhiteSpace(formNumber))
            {
                string message = String.Empty;

                if (supportingDocument == null)
                    message = "Please upload a file to continue";
                else if (String.IsNullOrWhiteSpace(formNumber))
                    message = "A valid Form Number is required";

                return new JsonResult(
                    new
                    {
                        Status = "Fail",
                        Documents = new List<SupportingDocument>(),
                        Message = message
                    }
                );
            }
            var fileName = $"{formNumber}-{upload.FileName}";
            var contentType = upload.ContentType;
            //Send to papersave

            var document = new Sentry.Domain.PaperSave.Entities.NewDocument()
            {
                FileName = fileName,
                FormNumber = formNumber,
            };

            using (var memoryStream = new MemoryStream())
            {
                await supportingDocument.CopyToAsync(memoryStream);
                var contents = memoryStream.ToArray();
                document.Contents = Convert.ToBase64String(contents);
            }
            try
            {
                await _domainService.PaperSaveOperations.UploadDocument(document);
                var supportingDocuments = await GetSupportingDocumentsListViewModel(formNumber);

                return PartialView("_SupportingDocumentsList", supportingDocuments); 
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading document for form number [{formNumber}]");
                return BadRequest("Error uploading supporting document");
            }
        }
        /// <summary>
        /// Deletes supporting documentation.
        /// </summary>
        /// <param name="id">Supporting document ID.</param>
        /// <param name="formNumber">Form number to associate with supporting documentation.</param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteSupportingDocument(long id, string formNumber)
        {
            if (id <= 0)
            {
                return new JsonResult(
                    new
                    {
                        Status = "Fail",
                        Documents = new List<SupportingDocument>(),
                        Message = "A valid supporting document is required for deletion."
                    }
                );
            }

            try
            {
                await _domainService.PaperSaveOperations.DeleteDocumentById(id);
                var supportingDocuments = await GetSupportingDocumentsListViewModel(formNumber);

                return PartialView("_SupportingDocumentsList", supportingDocuments);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting document for form number [{formNumber}]");
                return BadRequest("Error deleting supporting document");
            }
        }

        private async Task<IEnumerable<SelectListItem>> Colleges(string divisionCode)
        {
            try
            {
                var divisions = await _domainService.CAMOperations.GetDivisions();
                return divisions
                                .Select(s => new SelectListItem()
                                {
                                    Value = s.Code,
                                    Text = $"{s.Code} - {s.Name}",
                                    Selected = s.Code.Equals(divisionCode)
                                })
                                .OrderBy(c => c.Text)
                                .ToList();
            }
            catch
            {
                throw;
            }

        }

        private async Task<IEnumerable<SelectListItem>> Departments(string divisionCode, string departmentCode)
        {
            try
            {
                var departments = await _domainService.CAMOperations.GetDepartments(divisionCode);

                return departments
                                .Select(s => new SelectListItem()
                                {
                                    Value = s.Code,
                                    Text = $"{s.Code} - {s.Name}",
                                    Selected = s.Code.Equals(departmentCode)
                                })
                                .OrderBy(c => c.Text)
                                .ToList();
            }
            catch
            {
                throw;
            }
        }

        private async Task<IEnumerable<SelectListItem>> Projects(string departmentCode, string projectId)
        {
            try
            {
                var projects = await _domainService.CAMOperations.GetProjectsByDepartment(departmentCode);

                return projects
                            .Select(s => new SelectListItem()
                            {
                                Value = s.ProjectId,
                                Text = $"{s.ProjectId} - {s.ProjectName}",
                                Selected = s.ProjectId.Equals(projectId)
                            })
                            .OrderBy(c => c.Text)
                            .ToList();
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectPurpose(string projectId, string departmentCode)
        {
            try
            {
                var purpose = await ProjectPurpose(projectId, departmentCode);
                return new JsonResult(purpose);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting projects department code [{departmentCode}] project id [{projectId}]", departmentCode, projectId);
                return BadRequest("Error getting projects");

            }

        }

        private async Task<String> ProjectPurpose(string projectId, string departmentCode)
        {
            try
            {
                var projects = await _domainService.CAMOperations.GetProjectsByDepartment(departmentCode);

                if (projects.Any())
                {
                    return projects
                                .Where(p => p.ProjectId.Equals(projectId))
                                .Select(p => p.ProjectPurpose)
                                .FirstOrDefault();
                }
                else
                {
                    return String.Empty;
                }
            }
            catch
            {
                throw;
            }
        }


        [HttpGet]
        public async Task<IActionResult> GetDepartments(string collegeCode, string departmentCode)
        {
            try
            {
                var departments = await Departments(collegeCode, departmentCode);
                return new JsonResult(departments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error gettting deparments college code [{collegeCode}] department code [{departmentCode}]", collegeCode, departmentCode);
                return BadRequest("Error getting departments");

            }

        }

        public async Task<IActionResult> GetProjects(string departmentCode, string projectId)
        {
            try
            {
                var projects = await Projects(departmentCode, projectId);
                return new JsonResult(projects);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting projects department code [{departmentCode}] project id [{projectId}]", departmentCode, projectId);
                return BadRequest("Error getting projects");

            }

        }

        private async Task<Sentry.Domain.AccountsPayable.Entities.ProjectAccountDetails> ProjectAccountDetails(string accountNumber, string projectId, string objectCode)
        {
            var projectAccountDetails = new Sentry.Domain.AccountsPayable.Entities.ProjectAccountDetails();

            if (!String.IsNullOrWhiteSpace(accountNumber)
                && !String.IsNullOrWhiteSpace(projectId)
                && !String.IsNullOrWhiteSpace(objectCode))
            {
                var projectAccount = new Sentry.Domain.AccountsPayable.Entities.GetProjectAccount()
                {
                    accountNumber = accountNumber,
                    projectId = projectId,
                    objectCode = objectCode
                };

                projectAccountDetails = await _domainService.AccountsPayableOperations
                    .GetProjectAccountDetails(projectAccount);
            }

            return projectAccountDetails;
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectAccountDetails(string accountNumber, string projectId, string objectCode)
        {
            try
            {
                var projectAccountDetails = await ProjectAccountDetails(accountNumber, projectId, objectCode);
                return new JsonResult(projectAccountDetails);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting project account details account number [{accountNumber}] project id [{projectId}] object code [{objectCode}]", accountNumber, projectId, objectCode);
                return BadRequest("Error getting projects");
            }
        }
        
        public async Task<Sentry.Domain.PaperSave.Entities.NewDocument> CreatePDF(long giftDisbursementId)
        {

            var model = await SetupViewModel(giftDisbursementId, Helper.FinancialRole);
            var fileName = $"{model.FormNumber}-invoice.pdf";

            _logger.LogInformation($"Starting create invoice PDF for form number [{model.FormNumber}]");

            var html = await this.RenderViewAsync("PDFView", model);
            iText.Html2pdf.ConverterProperties converterProperties = new iText.Html2pdf.ConverterProperties();
            string contents = String.Empty;
            using (FileStream pdfDest = System.IO.File.Open($"{_invoicePdfPath}\\{fileName}", FileMode.Create))
            {
                iText.Html2pdf.HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
                pdfDest.Close();
                pdfDest.Dispose();
            }

            var document = new Sentry.Domain.PaperSave.Entities.NewDocument()
            {
                FileName = fileName,
                FormNumber = model.FormNumber
            };

            using (FileStream file = System.IO.File.Open($"{_invoicePdfPath}\\{fileName}", FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    document.Contents = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            _logger.LogInformation($"Created invoice PDF for form number [{model.FormNumber}]");

            return document;
        }

        /// <summary>
        /// https://itextpdf.com/products/itext-7/convert-html-css-to-pdf-pdfhtml
        /// </summary>
        /// <param name="giftDisbursementId"></param>
        /// <returns></returns>
        public async Task<IActionResult> CreateItextSharp(long giftDisbursementId)
        {
            var model = await SetupViewModel(giftDisbursementId, Helper.FinancialRole);
            var html = await this.RenderViewAsync("PDFView", model);
            iText.Html2pdf.ConverterProperties converterProperties = new iText.Html2pdf.ConverterProperties();
            using (FileStream pdfDest = System.IO.File.Open($"{_invoicePdfPath}\\{model.FormNumber}-invoice.pdf", FileMode.Create))
            {
                iText.Html2pdf.HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
            }
            return View("PDFView", model);
        }

        [HttpGet]
        public async Task<IActionResult> GetSupportingDocuments(string formNumber)
        {
            try
            {
                var supportingDocuments = await GetSupportingDocumentsListViewModel(formNumber);
                return PartialView("_SupportingDocumentsList", supportingDocuments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error gettting supporting documents form number [{formNumber}]", formNumber);
                return BadRequest("Error getting departments");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetReadOnlySupportingDocuments(string formNumber)
        {
            try
            {
                var supportingDocuments = await GetSupportingDocumentsListViewModel(formNumber);
                return PartialView("_ReadOnlySupportingDocumentsList", supportingDocuments);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error gettting supporting documents form number [{formNumber}]", formNumber);
                return BadRequest("Error getting departments");
            }
        }


        public async Task<IActionResult> GetGiftDisbursementByFormNumber(string formNumber)
        {
            try
            {
                var disbursement = await GetGiftDisbursement(formNumber);
                return new JsonResult(disbursement);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting gift disbursement by form number [{formNumber}]", formNumber);
                return BadRequest("Error searching for gift disbursement");

            }

        }

        private async Task ValidateDebitAccountNumber(GiftDisbursementsViewModel model)
        {
            foreach (var project in model.GiftDisbursementProjects)
            {
                var debitAccountNumbers = await _domainService.AccountsPayableOperations.GetDebitAccounts(project.ProjectId);
                foreach (var item in project.ProjectItems)
                {
                    if (!debitAccountNumbers.Where(d => d.DebitAccount == item.DebitAccountNumber).Any())
                    {
                        ModelState.AddModelError("DebitAccountNumber", $"Debit account number {item.DebitAccountNumber} is invalid for project {project.ProjectId}.");
                    }
                }
            }
        }

        [HttpGet]
        public IActionResult AddItem(int projectIndex, int itemIndex, string disbursementType)
        {
            ViewData["projectIndex"] = projectIndex;
            ViewData["projectItemIndex"] = itemIndex;
            ViewData["type"] = disbursementType;

            var model = new ViewModels.GiftDisbursements.ProjectItem();

            return PartialView("_ProjectItem", model);
        }

        [HttpGet]
        public async Task<IActionResult> AddProject(int index, string disbursementType, string departmentCode, string projectId)
        {
            ViewData["index"] = index;
            ViewData["disbursementType"] = disbursementType;
            ViewData["departmentCode"] = departmentCode;

            var projectDetails = await _domainService.CAMOperations.GetProjectByProjectId(projectId);

            var model = new ViewModels.GiftDisbursements.GiftDisbursementProject() 
            {
                ProjectId = projectDetails.ProjectId,
                ProjectName = projectDetails.Description,
                ProjectBalance = projectDetails.CurrentBalance ?? decimal.Zero,
                ProjectItems = new List<ViewModels.GiftDisbursements.ProjectItem>(),
                BlackbaudProjectUrl = $"{_config.Blackbaud.Project.BaseUrl}/{projectDetails.FeProjectId}?envid={_config.Blackbaud.Project.EnvironmentId}"

            };

            var projectItem = new ViewModels.GiftDisbursements.ProjectItem()
            {
                ObjectCode = (disbursementType == "ET" || disbursementType == "ST" ? "7930" : string.Empty)
            };

            model.ProjectItems.Add(projectItem);

            return PartialView("_Project", model);
        }
    }


}

