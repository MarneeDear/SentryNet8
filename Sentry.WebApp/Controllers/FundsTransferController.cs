using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models.Notifications;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.ViewModels.FundsTransfer;
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
//using Microsoft.Extensions.Azure;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Sentry.Domain.AccountsReceivable.Entities;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeFinance()]
    public class FundsTransferController : IntegrationController
    {
        public readonly Config _config;
        private readonly INotificationService _notificationService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _pdfCopyPath;

        public FundsTransferController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<FundsTransferController> logger,
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
            _pdfCopyPath = $"{environment.WebRootPath}\\fundstransfer-pdfs";

        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        private bool AllowEdit()
        {
            return Helper.IsFTReviewerRole;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            if (!Helper.IsFTRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(0);

            var pageId = string.Empty;

            if (Helper.IsFTReviewerRole)
                pageId = "fundsTransferReviewerDashboardPage";
            else if (Helper.IsFTApproverRole)
                pageId = "fundsTransferApproverDashboardPage";
            else if (Helper.IsFTGeneralCounselApproverRole)
                pageId = "fundsTransferGeneralCounselDashboardPage";

            FundsTransferDashboard viewModel = new FundsTransferDashboard()
            {
                Title = "Funds Transfer",
                PageId = (pageId ?? "fundsTransferPage"),
                ActiveClass = "FundsTransfer",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
                FundsTransfers = fundsTransferRequests
                                    .Select(v => (FundsTransferListItem)v)
            };
            
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Unrouted()
        {
            if (!Helper.IsFTReviewerRole)
            {
                return View("../Error/Unauthorized");
            }
            var model = FundsTransferPageSetup(Constants.UnroutedBucket);
            model.PageId = "fundsTransferUnroutedPage";

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTReviewerRoleId);
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Edit";
            return View("List", model);
        }

        [HttpGet]
        public async Task<IActionResult> Restricted()
        {
            if (!Helper.IsFTApproverRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTApproverRoleId);
            fundsTransferRequests = fundsTransferRequests.Where(r => r.RoutingType == Constants.RestrictedBucketId);

            var model = FundsTransferPageSetup(Constants.RestrictedBucket);
            model.PageId = "fundsTransferRestrictedPage";
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Review";
            return View("List", model);
        }

        [HttpGet]
        public async Task<IActionResult> Unrestricted()
        {
            if (!Helper.IsFTApproverRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTApproverRoleId);
            fundsTransferRequests = fundsTransferRequests.Where(r => r.RoutingType == Constants.UnrestrictedBucketId);

            var model = FundsTransferPageSetup(Constants.UnrestrictedBucket);
            model.PageId = "fundsTransferUnrestrictedPage";
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Review";
            return View("List", model);
        }

        [HttpGet]
        public async Task<IActionResult> Gift()
        {
            if (!Helper.IsFTApproverRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTApproverRoleId);
            fundsTransferRequests = fundsTransferRequests.Where(r => r.RoutingType == Constants.GiftBucketId);

            var model = FundsTransferPageSetup(Constants.GiftBucket);
            model.PageId = "fundsTransferGiftPage";
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Review";
            return View("List", model);
        }

        [HttpGet]
        public async Task<IActionResult> Endowment()
        {
            if (!Helper.IsFTApproverRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTApproverRoleId);
            fundsTransferRequests = fundsTransferRequests.Where(r => r.RoutingType == Constants.EndowmentBucketId);

            var model = FundsTransferPageSetup(Constants.EndowmentBucket);
            model.PageId = "fundsTransferEndowmentPage";
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Review";
            return View("List", model);
        }

        //TODO General Counsel listing
        [HttpGet]
        public async Task<IActionResult> GeneralCounsel()
        {
            if (!Helper.IsFTGeneralCounselApproverRole)
            {
                return View("../Error/Unauthorized");
            }

            var fundsTransferRequests = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingApproval(Constants.FTGeneralCounselApproverRoleId);
            var model = FundsTransferPageSetup(Constants.GeneralCounselBucket);
            model.PageId = "fundsTransferGeneralCounselPage";
            model.FundsTransfers = fundsTransferRequests
                                            .Select(v => (FundsTransferListItem)v);
            ViewBag.GetFundsTransferAction = "Review";
            return View("List", model);
        }

        private bool IsReadyForProcessing(Sentry.Domain.AccountsPayable.Entities.FundsTransfer form)
        {
            //TODO use a sproc and API endpoint???
            var ready = !form.FundsTransferApprovers.Any(a => a.Approved != true);
            return ready;
        }

        private FundsTransferListViewModel FundsTransferPageSetup(string listType)
        {
            var model = new FundsTransferListViewModel()
            {
                Title = $"{listType} Funds Transfers",
                PageId = "fundsTransferPage",
                ActiveClass = "FundsTransfer",
                Message = "Funds Transfer Status Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                Organization = "UA",
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole
            };

            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long fundsTransferId)
        {
            try
            {
                var model = await SetupViewModel(fundsTransferId);

                if (AllowEdit() && !model.IsReadyForProcessing)
                {
                    return View(model);
                }

                return RedirectToAction("Review", new { fundsTransferId = fundsTransferId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting funds transfer {fundsTransferId}", fundsTransferId);
                return RedirectToAction("SystemError", "Error");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FundsTransferViewModel model)
        {
            var role = Helper.CurrentFinancialRole;
            try
            {

                for (int index = model.Items.Count - 1; index >= 0; index--)
                {
                    if (model.Items[index].Deleted)
                    {
                        model.Items.RemoveAt(index);
                    }
                }

                var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(model.FormNumber);
                if (supportingDocuments.Count() < 1)
                {
                    ModelState.AddModelError("SupportingDocuments", "Supporting Documents are required");
                }

                if (!ModelState.IsValid)
                {
                    model = await SetupViewModel(model.FundsTransferId);
                    return View(model);
                }

                await SaveChanges(model);
                var editModel = await SetupViewModel(model.FundsTransferId);
                return View(editModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving funds transfer {model.FundsTransferId}", model.Id);
                return RedirectToAction("SystemError", "Error");
            }
        }

        public async Task SaveChanges(FundsTransferViewModel model)
        {
            try
            {
                var updateFundsTransfer = (Sentry.Domain.AccountsPayable.Entities.UpdateFundsTransfer)model;

                await _domainService.AccountsPayableOperations.UpdateFundsTransfer(updateFundsTransfer);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving funds transfer {model.FundsTransferId}", model.Id);
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Review(long fundsTransferId)
        {
            var reviewModel = await SetupViewModel(fundsTransferId);

            return View(reviewModel);
        }

        private async Task<FundsTransferViewModel> SetupViewModel(long fundsTransferId)
        {
            var fundsTransfer = await _domainService.AccountsPayableOperations.GetFundsTransfer(fundsTransferId);

            try
            {
                var model = (FundsTransferViewModel)fundsTransfer;


                model = SetupIntegration(model);
                model.IsReadyForProcessing = IsReadyForProcessing(fundsTransfer);
                model.AllowProcessing = Helper.IsFTReviewerRole;

                model.OverallTotal = fundsTransfer.FundsTransferItems.Sum(i => i.Amount);

                model.AllowEdit = AllowEdit();

                model.AllowFileUpload = AllowFileUpload();


                var routingTypes = await _domainService.AccountsPayableOperations.GetTransferRoutingTypes();
                //TODO: Once the approvers are set up, return to this. Routing can ONLY happen IF ProjectType has a value and the Reviewer
                //      Approved the form
                model.TransferRoutingTypes = routingTypes.Select(t => new SelectListItem()
                {
                    Value = t.Id.ToString(),
                    Text = t.RoutingTypeDescription,
                    Selected = t.Id == model.TransferRoutingType
                });

                model.AllowApproval = await AllowedToApprove(fundsTransfer);

                model.PreviouslyApprovedByUser = await ApprovedByUser(fundsTransfer);
                //model.PreviouslyApprovedByRole =  ApprovedByRole(fundsTransfer); 

                model.PostDate = fundsTransfer.PostDate.HasValue ? fundsTransfer.PostDate.Value.ToString("MM/dd/yyyy") : DateTime.Now.ToString("MM/dd/yyyy");
                model.ValidFileTypes = _config.ValidFileTypes;

                //if (model.IsReadyForProcessing && model.AllowProcessing)
                //{
                    var batches = await _domainService.AccountsPayableOperations.GetJournalEntryBatchPreview(fundsTransferId);
                    model.FundsTransferJournalEntries = batches.Select(e => (ViewModels.FundsTransfer.FundsTransferJournalEntry)e);

                //}

                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error loading funds transfer id [{fundsTransferId}]");
                throw;
            }
        }

        private FundsTransferViewModel SetupIntegration(FundsTransferViewModel model)
        {
            model.IsChanged = false;
            model.Id = 0L;
            model.System = "FundsTransfer";
            model.SystemId = 0;
            model.Integration = String.Empty;
            model.RecordStatus = String.Empty;
            model.SourceRecordId = String.Empty;
            model.ChangeAgent = String.Empty;
            model.CreatedOnDT = DateTime.Now;
            model.Title = "Funds Transfer";
            model.PageId = "fundsTransferPage";
            model.ActiveClass = "FundsTransfer";
            model.Message = "Funds Transfer Status Page";
            model.NavigationGroups = GetNavigationGroups();
            model.User = User.Identity.Name;

            return model;
        }

        [HttpGet]
        public IActionResult AddItem(int itemIndex)
        {
            ViewData["projectItemIndex"] = itemIndex;

            var model = new ViewModels.FundsTransfer.FundsTransferItem();

            return PartialView("_Item", model);
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

        [HttpGet]
        public async Task<IActionResult> ViewSupportingDocument(int id)
        {
            var document = await _domainService.PaperSaveOperations.GetDocumentById(id);

            return File(document.Contents, document.MimeType, $"{document.FileName}");
        }

        //private bool ApprovedByRole(Sentry.Domain.AccountsPayable.Entities.FundsTransfer fundsTransfer)
        //{

        //    //TODO adjust to work for funds transfer approver roles

        //    return fundsTransfer.FundsTransferApprovers
        //        .Where(a => Helper.UserFTRoles.Contains(a.Description)) // == (Helper.AlternateFinancialRole ?? Helper.FinancialRole))
        //        .Where(a => a.Approved.HasValue).Any();
        //}

        private async Task<bool> ApprovedByUser(FundsTransfer fundsTransfer)
        {
            var employeeId = await Helper.EmployeeId();
            var approverIds = fundsTransfer.FundsTransferApprovers
                .Where(a => a.Approved == true)
                .Where(a => a.Description != Constants.DesigneeRole) //All approval if FS user who approved on the campus side
                .Where(a => a.Description != Constants.SignatureAuthorityRole) //Allow approval is FS user who approved on the campus side
                .Where(a => a.Description != Constants.FTUnrestrictedCampusRole) //Allow approval if FS user who approved on the campus side as an unrestricted approver
                .Select(a => a.ApproverEmployeeId);

            return approverIds.Contains(employeeId);
        }

        private async Task<bool> AllowedToApprove(FundsTransfer fundsTransfer)
        {           

            if (fundsTransfer.CampusApprovalStatus != FundsTransferViewModel.Approved)
            {
                return false;
            }

            var approvedByUser = await ApprovedByUser(fundsTransfer);
            if (approvedByUser)
            {
                return false;
            }

            var approvers = fundsTransfer.FundsTransferApprovers;

            var isFtReviewerApproved = approvers.Where(a => a.Description == Constants.FTReviewerRole && a.Approved == true).Any();
            var isFtApproverApproved = approvers.Where(a => a.Description == Constants.FTApproverRole && a.Approved == true).Any();
            var isGeneralCounselApproved = approvers.Where(a => a.Description == Constants.GeneralCounselRole && a.Approved == true).Any();

            if (Helper.IsFTReviewerRole && Helper.IsFTApproverRole)
            {
                return !isFtReviewerApproved || (isFtReviewerApproved && !isFtApproverApproved); 
            }

            if (Helper.IsFTReviewerRole)
            {
                return !isFtReviewerApproved;
            }

            if (Helper.IsFTApproverRole)
            {
                return isFtReviewerApproved && !isFtApproverApproved ;
            }

            if (Helper.IsFTGeneralCounselApproverRole)
            {
                return isFtReviewerApproved && isFtApproverApproved && !isGeneralCounselApproved;
            }

            return false;

        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="fundsTransferId"></param>
       /// <param name="comments"></param>
       /// <param name="approved"></param>
       /// <param name="review">Is in teh FT Reviewer stage</param>
       /// <returns></returns>
        private async Task ApproveReject(long fundsTransferId, string comments, bool approved, bool review)
        {
            int ftRoleId = 0;

            if (review)
            {
                ftRoleId = Constants.FTReviewerRoleId;
            }
            else
            {
                if (Helper.IsFTApproverRole)
                {
                    ftRoleId = Constants.FTApproverRoleId;
                }
                else if (Helper.IsFTGeneralCounselApproverRole)
                {
                    ftRoleId = Constants.FTGeneralCounselApproverRoleId;
                }
            }

            var approval = new Sentry.Domain.AccountsPayable.Entities.UAFApproval()
            {
                ApproverRoleId = ftRoleId,
                Comments = comments,
                Approved = approved
            };

            var employeeId = await Helper.EmployeeId();

            try
            {
                await _domainService.AccountsPayableOperations.ApproveRejectFundsTransfer(fundsTransferId, employeeId, approval);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving/rejecting funds transfer");
                throw;
            }
        }

        private async Task SendNotification(FundsTransfer fundsTransfer)
        {

            IEnumerable<Data.Models.Notifications.SendTo> sendTos = new List<Data.Models.Notifications.SendTo>()
            {
                new Data.Models.Notifications.SendTo()
                {
                    Email = fundsTransfer.PreparedByEmail,
                    Name = $"{fundsTransfer.PreparedByFirstName} {fundsTransfer.PreparedByLastName}"
                }
            };

            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();

            var details = new FundsTransferApprovalDetails()
            {
                form_id = fundsTransfer.FormNumber,
            };
            try
            {
                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.FundsTransferApproved, details);
                _logger.LogInformation($"Sent approval notification for form [{fundsTransfer.FormNumber}] to [{string.Join(",", sendTos.Select(a => a.Email))}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending funds transfer approver notifications for funds transfer id [{fundsTransfer.Id}]");
                throw;
            }
        }

        private async Task SendRejectionNotification(long id, string comments, string ccEmails)
        {
            var fundsTransfer = await _domainService.AccountsPayableOperations.GetFundsTransfer(id);

            var sendTos = new List<Data.Models.Notifications.SendTo>()
                {
                    new Data.Models.Notifications.SendTo()
                    {
                        Email = fundsTransfer.PreparedByEmail,
                        Name = $"{fundsTransfer.PreparedByFirstName} {fundsTransfer.PreparedByLastName}"
                    }
                };

            var ccEmailList = SetupCCEmailList(ccEmails);

            var details = new FundsTransferRejectionDetails()
            {
                comments = comments,
                form_id = fundsTransfer.FormNumber,
                uafdn_link = $"{_config.UAFDNNewVendorRequestURL}/{id}"
            };

            try
            {

                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.FundsTransferRejected, details);
                _logger.LogInformation($"Sent rejection notification for form [{fundsTransfer.FormNumber}] to [{string.Join(",", sendTos.Select(a => a.Email))}]");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending funds transfer approver notifications for funds transfer id [{id}]");

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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reject(FundsTransferViewModel model, bool review, string returnView)
        {
            try
            {
                ModelState.Clear();
                _logger.LogInformation($"Rejecting funds transfer [{model.FormNumber}]. Assigned Role [{Helper.FinancialRole}]");

                await ApproveReject(model.FundsTransferId, model.Comments, false, review);

                try
                {
                    await SendRejectionNotification(model.FundsTransferId, model.Comments, model.CCEmails);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending rejection notification to submitter. Form number [{model.FormNumber}] funds transfer id [{model.FundsTransferId}]"
                        , model.FormNumber, model.FundsTransferId);
                }

                _logger.LogInformation($"Rejected funds transfer [{model.FormNumber}]. Redirecting to [{returnView}]");
                return RedirectToAction(returnView);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error rejecting funds transfer [{model.FormNumber}] return view [{returnView}]", model.FormNumber, returnView);
                return RedirectToAction("SystemError", "Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Approve(FundsTransferViewModel model, bool save, bool review, string returnView)
        {

            //TODO approve for FT reviewer if in unrouted
            // approve as FT Approver in an approval bucket
            // approve as FT General Counsel if in general counsel bucket
            if (!Helper.IsFTRole)
            {
                return View("../Error/Unauthorized");
            }

            try
            {
                var role = Helper.UserFTRoles;
                _logger.LogInformation($"Approving form [{model.FormNumber}]. Assigned Role [{Helper.FTRole}]");

                if (save)
                {
                    var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(model.FormNumber);
                    if (supportingDocuments.Count() < 1)
                    {
                        ModelState.AddModelError("SupportingDocuments", "Supporting Documents are required");
                    }

                    if (!ModelState.IsValid)
                    {
                        model = await SetupViewModel(model.FundsTransferId);
                        return View("Edit", model);
                    }

                    await SaveChanges(model);
                }

                var fundsRequest = await _domainService.AccountsPayableOperations.GetFundsTransfer(model.FundsTransferId);

                //TODO
                //if (review)
                //{
                //    await ApproveReject(model.FundsTransferId, model.Comments, true, Constants.FTReviewerRoleId);
                //}
                //else
                //{
                //    //Constants.FTApproverRoleIds[Helper.FTRole],
                //    if (Helper.IsFTApproverRole)
                //    {
                //        await ApproveReject(model.FundsTransferId, model.Comments, true, Constants.FTApproverRoleId);
                //    }
                //    else if (Helper.IsFTGeneralCounselApproverRole)
                //    {
                //        await ApproveReject(model.FundsTransferId, model.Comments, true, Constants.FTGeneralCounselApproverRoleId);
                //    }
                //}

                await ApproveReject(model.FundsTransferId, model.Comments, true, review);

                try
                {
                    await SendNotification((FundsTransfer)model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending notifications for funds transfer form number [{model.FormNumber}] funds transfer id [{model.FundsTransferId}]");
                }

                _logger.LogInformation($"Approved form [{model.FormNumber}]. Redirecting to [{returnView}]");
                return RedirectToAction(returnView);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error approving funds transfer [{model.FormNumber}] return view [{returnView}]", model.FormNumber, returnView);
                return RedirectToAction("SystemError", "Error");
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
                SupportingDocumentType = "UAF Funds Transfer"
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
        [ValidateAntiForgeryToken]
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

        [HttpGet]
        public async Task<IActionResult> ReadyForProcessing()
        {
            if (!Helper.IsFTReviewerRole)
            {
                return View("../Error/Unauthorized");
            }

            var processingFundsTransfer = await _domainService.AccountsPayableOperations.GetFundsTransfersAwaitingProcessing();
            var model = FundsTransferStatusPageSetup();
            model.FundsTransfers = processingFundsTransfer.Select(d => (FundsTransferListItem)d);

            return View(model);
        }

        private FundsTransferListViewModel FundsTransferStatusPageSetup()
        {
            var model = new FundsTransferListViewModel()
            {
                Title = $"Funds Transfer",
                PageId = "fundsTransferReadyForProcessingPage",
                ActiveClass = "FundsTransfer",
                Message = "Funds Transfer Status Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                Organization = "UA",
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole
            };

            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return model;
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

        [HttpGet]
        public async Task<IActionResult> GetFundsTransferByFormNumber(string formNumber)
        {
            try
            {
                var fundsTransfer = await _domainService.AccountsPayableOperations.GetFundsTransfer(formNumber);
                return new JsonResult(fundsTransfer);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting funds transfer by form number [{formNumber}]", formNumber);
                return BadRequest("Error searching for funds transfer");

            }

        }

        [HttpGet]
        public async Task<IActionResult> Process(long fundsTransferId)
        {
            var reviewModel = await SetupViewModel(fundsTransferId);

            return View("Review", reviewModel);

        }

        private bool AllowFileUpload()
        {
            return !Helper.IsFTGeneralCounselApproverRole;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Process(FundsTransferViewModel model)
        {
            ModelState.Clear();

            try
            {
                _logger.LogInformation($"Starting create journal entry batch {(model.TransferRoutingType == 4 ? "gift" : "")} for form number [{model.FormNumber}] to {(model.TransferRoutingType == 4 ? "Papersave" : "FeNxt")}");

                //this has has to happen for all forms when processed
                var fundsTransfer = await _domainService.AccountsPayableOperations.GetFundsTransfer(model.FundsTransferId);

                var pdfModel = await SetupPDFViewModel(model.FundsTransferId);

                //Delete old form if failed previously

                var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(model.FormNumber);

                var printedForms = supportingDocuments.Where(d => d.FileName == $"{model.FormNumber}-FundsTransferPrintedForm.pdf");

                foreach (var file in printedForms)
                {
                    await _domainService.PaperSaveOperations.DeleteDocumentById(file.Id);
                }

                //Create PDF copy of the form
                _logger.LogInformation($"Creating PDF copy of funds transfer for form [{model.FundsTransferId}]");
                var pdf = await CreatePDF(pdfModel);

                //Upload PDF copy of the form to temp supporting documents
                _logger.LogInformation($"Uploading PDF copy for form [{model.FundsTransferId}]");
                await _domainService.PaperSaveOperations.UploadDocument(pdf);
                _logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{model.FundsTransferId}]");

                if (fundsTransfer.RoutingType == 4)
                {

                    //Is Gift, send to workflow
                    var projects = new List<string>
                    {
                        fundsTransfer.FromProjectId
                    };
                    projects.AddRange(fundsTransfer.FundsTransferItems.Select(t => t.ProjectId));

                    var uafTrx = new UAFTransaction()
                    {
                        ConstituentName = String.Empty,
                        TransactionType = "Other",
                        GiftTotal = fundsTransfer.FundsTransferItems.Select(t => t.Amount).Sum(),
                        ReceiptTotal = fundsTransfer.FundsTransferItems.Select(t => t.Amount).Sum(),
                        Projects = projects,
                        PostDate = fundsTransfer.PostDate.HasValue ? fundsTransfer.PostDate.Value : DateTime.MinValue,
                        GiftTransmittalId = Guid.Empty,
                        FundsTransferId = model.FundsTransferId
                    };
                    //await _domainService.AccountsReceivableOperations.CreateUAFTransaction(model.FormNumber, uafTrx);
                    await _domainService.AccountsReceivableOperations.CreateUAFTransactionFundsTransfer(model.FormNumber, uafTrx);

                }
                else
                {
                    //Send to FeNxt Journal Entry Batch via UAF Services                
                    var recordId = await _domainService.AccountsPayableOperations.CreateJournalEntryBatch(model.FundsTransferId);

                    //Create final documents
                    _logger.LogInformation($"Creating final documents for form [{model.FundsTransferId}]");
                    await CreateFinalDocuments(recordId, model.FormNumber, model.PostDate);
                }
                _logger.LogInformation($"Created journal batch entry {(model.TransferRoutingType == 4 ? "gift" : "")} for form number [{model.FormNumber}]");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error proccessing funds transfer [{model.FormNumber}]", model.FormNumber);
                //return RedirectToAction("SystemError", "Error");
                return RedirectToAction("ReadyForProcessing");
            }
            return RedirectToAction("ReadyForProcessing");
        }

        private async Task<Sentry.Domain.PaperSave.Entities.NewDocument> CreatePDF(ViewModels.FundsTransfer.PDFViewModelSimplified model)
        {

            var fileName = $"{model.FormNumber}-FundsTransferPrintedForm.pdf";

            _logger.LogInformation($"Starting create funds transfer PDF for form number [{model.FormNumber}]");

            var html = await this.RenderViewAsync("PDFViewSimplified", model);
            iText.Html2pdf.ConverterProperties converterProperties = new iText.Html2pdf.ConverterProperties();
            string contents = String.Empty;
            using (FileStream pdfDest = System.IO.File.Open($"{_pdfCopyPath}\\{fileName}", FileMode.Create))
            {
                iText.Html2pdf.HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
                pdfDest.Close();
                pdfDest.Dispose();
            }

            var document = new Sentry.Domain.PaperSave.Entities.NewDocument()
            {
                FileName = fileName,
                FormNumber = model.FormNumber,
                SupportingDocumentType = "UAF Funds Transfer"
            };

            using (FileStream file = System.IO.File.Open($"{_pdfCopyPath}\\{fileName}", FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    document.Contents = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            _logger.LogInformation($"Created funds transfer PDF for form number [{model.FormNumber}]");

            return document;
        }

        private async Task CreateFinalDocuments(long recordId, string formNumber, string postDate)
        {
            try
            {
                _logger.LogInformation($"Starting create funds transfer final documents for form number [{formNumber}]");
                var finalDocument = new Sentry.Domain.PaperSave.Entities.FinalDocument()
                {
                    SystemId = recordId.ToString(),
                    DocumentAttributes = new Dictionary<string, string>()
                    {
                        { "Form Number", formNumber },
                        { "Post Date", postDate }
                    }
                };
                await _domainService.PaperSaveOperations.CreateFinalDocuments(finalDocument, formNumber);
                _logger.LogInformation($"Created final documents for form number [{formNumber}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating final funds transfer documents for form number [{formNumber}]", formNumber);
            }
        }

        //public async Task<IActionResult> TestPDF()
        //{
        //    var fundsTransferId = 7;
        //    var model = await SetupPDFViewModel(fundsTransferId);
        //    return View("PDFViewSimplified", model);
        //}

        private async Task<PDFViewModelSimplified> SetupPDFViewModel(long id)
        {
            var form = await _domainService.AccountsPayableOperations.GetFundsTransfer(id);
            var model = (PDFViewModelSimplified)form;
            try
            {
                var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(form.FormNumber);
                var docsModel = new SupportingDocumentsListViewModel();
                model.SupportingDocuments = supportingDocuments.Select(d => (SupportingDocument)d)
                    .Select(d => d.FileName);
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting supporting documents for {formNumber}", form.FormNumber);
                model.SupportingDocuments = Enumerable.Empty<string>();
                return model;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectDescriptionByProjectId(string projectId)
        {
            var project = await _domainService.CAMOperations.GetProjectByProjectId(projectId);

            return new JsonResult(project.Description);
        }

        [HttpGet]
        public async Task<IActionResult> GetProjectPurposeByProjectId(string projectId)
        {
            var project = await _domainService.CAMOperations.GetProjectByProjectId(projectId);

            return new JsonResult(project.Purpose);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetUAFApprovers(long fundsTransferId)
        {
            try
            {
                _logger.LogInformation($"Resetting Funds Transfer [{fundsTransferId}] UAF Approvers");

                await _domainService.AccountsPayableOperations.ResetFundsTransferUAFApprovers(fundsTransferId);

                _logger.LogInformation($"Reset Funds Transfer [{fundsTransferId}] UAF Approvers");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting funds transfer approvers for funds transfer id [{fundsTransferId}]", fundsTransferId);
            }

            return RedirectToAction("ReadyForProcessing");
        }

    }


}

