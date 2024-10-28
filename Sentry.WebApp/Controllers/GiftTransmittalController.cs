using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels.GiftTransmittal;
using System.IO;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Sentry.WebApp.ViewModels.SupportingDocuments;
using Sentry.WebApp.Data.Models.Notifications;
using Microsoft.Extensions.Options;
using Sentry.WebApp.ViewModels.GiftDisbursements;
using Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal;
using Sentry.Domain.AccountsReceivable;
using Sentry.Domain.AccountsReceivable.Entities;
using Sentry.Domain.PaperSave.Entities;
using Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal;
using Sentry.WebApp.Data.Models.GiftTransmittal;
using Sentry.Domain.CentralizedAccessManagement.Entities;
using System.Globalization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;
using Sentry.WebApp.Extensions;
using Sentry.WebApp.WebConstants;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;

namespace Sentry.WebApp.Controllers
{
    public class GiftTransmittalController : IntegrationController
    {
        //private readonly IDomainService _domainService;
        public readonly Config _config;
        private readonly INotificationService _notificationService;
        private readonly AccountsReceivableOperations _arOperations;
        private readonly IPdfService _pdfService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _bursarPdfPath;

        public GiftTransmittalController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<GiftTransmittalController> logger,
            IConfiguration configuration,
            IDomainService domainService,
            IOptions<Config> config,
            INotificationService notificationService,
            IPdfService pdfService,
            IWebHostEnvironment environment)
            : base(context, dwContext, logger, configuration, domainService)
        {
            _config = config.Value;
            _notificationService = notificationService;
            _arOperations = domainService.AccountsReceivableOperations;
            _pdfService = pdfService;
            _environment = environment;
            _bursarPdfPath = $"{environment.WebRootPath}\\bursar-pdfs";
        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        //public async Task<IActionResult> BursarPDFView()
        //{
        //    var giftTransmittalId = new Guid("bdcb745f-1430-458c-a703-260454dd413b");
        //    var giftTransmittalItemId = new Guid("5abb8579-2426-4761-8f0c-80cd9d6af926");

        //    var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
        //    //when we map it to the view model using the details of the gift transmittal item that matches the giftTransmittalItemId
        //    var giftTransmittalItem = giftTransmittal.GiftTransmittalItems.Where(i => i.Id == giftTransmittalItemId).First();
        //    var distributions = giftTransmittalItem.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Select(d => (Distribution)d).ToList();

        //    var organization = giftTransmittal.FormNumber.StartsWith("GT") ? "UAF" : "UA";
        //    foreach (var distribution in distributions)
        //    {
        //        var designation = await _domainService.MasterDataWebService.GetDesignation(organization, distribution.FundAccount);
        //        distribution.Designation = designation.Name;
        //    }

        //    var model = new BursarPDF()
        //    {
        //        FormNumber = giftTransmittal.FormNumber,
        //        CheckNumber = giftTransmittalItem.CheckNumber,
        //        Distributions = distributions,
        //        PreparedBy = new PreparedBy()
        //        {
        //            ContactName = giftTransmittal.ContactName
        //        }
        //    };

        //    return View(model);
        //}

        public IActionResult GTForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGTRole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments)
                    .Where(t => t.FormNumber.StartsWith("GT"))
                    .Where(t => t.Status != "Initialized")
                    .Where(t => t.WaitingOnResponseFromBursar != true)
                    .Where(t => t.WaitingOnResponseFromPreparer != true)
                    .Where(t => t.CurrentApprovalStage == 1);
            }

            var listType = "GTForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;

            return View("List", model);
        }

        #region Status Lists

        public IActionResult WaitingOnBursarForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGTRole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments)
                    .Where(t => t.WaitingOnResponseFromBursar == true);
            }

            var listType = "WaitingOnBursarForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;
            model.ListType = listType;

            return View("StatusList", model);
        }

        public IActionResult WaitingOnPreparerForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGTRole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments)
                .Where(t => t.WaitingOnResponseFromPreparer == true);
            }

            var listType = "WaitingOnPreparerForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;
            model.ListType = listType;

            return View("StatusList", model);
        }
        #endregion

        public IActionResult GUForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGURole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments)
                    .Where(t => t.FormNumber.StartsWith("GU"))
                    .Where(t => t.Status != "Initialized")
                    .Where(t => t.WaitingOnResponseFromBursar != true)
                    .Where(t => t.WaitingOnResponseFromPreparer != true)
                    .Where(t => t.CurrentApprovalStage == 1);
            }
            var listType = "GUForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;

            return View("List", model);
        }
        public IActionResult InitializedForms()
        {
            var giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(true)
                .Where(t => t.Status == "Initialized");

            var listType = "InitializedForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g);
            model.DisplayRequiringPhysicalDocuments = true;

            return View("InitializedList", model);
        }

        public IActionResult SecondaryApproverForms(bool displayRequiringPhysicalDocuments = false)
        {
            var listType = "SecondaryApproverForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;
            if (Helper.IsARSecondaryReviewer)
            {
                var giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments);
                model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g)
                    .Where(g => g.CurrentApprovalStage == 2);
            }
            else
            {
                model.GiftTransmittals = Enumerable.Empty<GiftTransmittalListItem>();
            }

            return View("List", model);
        }

        public IActionResult FinalizeGTForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGTRole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments);

            }
            var listType = "FinalizeGTForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g)
                .Where(g => g.CurrentApprovalStage == 3 && g.FormNumber.StartsWith("GT") || g.HasProcessingError == true);
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;

            return View("List", model);
        }

        public IActionResult FinalizeGUForms(bool displayRequiringPhysicalDocuments = false)
        {
            var giftTransmittals = Enumerable.Empty<Sentry.Domain.Lynx.DataAccess.Entities.GiftTransmittal.GiftTransmittal>();
            if (Helper.IsGURole)
            {
                giftTransmittals = _domainService.LynxDataOperations.GetGiftTransmittals(!displayRequiringPhysicalDocuments);
            }

            var listType = "FinalizeGUForms";
            var model = GiftTransmittalStatusPageSetup(listType);
            model.GiftTransmittals = giftTransmittals.Select(g => (GiftTransmittalListItem)g)
                .Where(g => g.CurrentApprovalStage == 3 && g.FormNumber.StartsWith("GU"));
            model.DisplayRequiringPhysicalDocuments = displayRequiringPhysicalDocuments;

            return View("List", model);
        }

        public IActionResult Index()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "Gift Transmittals",
                PageId = "giftTransmittalsPage",
                ActiveClass = "GiftTransmittals",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups(),
            };

            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Create(Guid giftTransmittalId, string organization)
        {

            var model = new GTInitializeViewModel();

            var eFormName = String.Empty;

            if (giftTransmittalId != Guid.Empty)
            {
                var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
                model = (GTInitializeViewModel)giftTransmittal;

            }
            else
            {
                if (organization.Equals("uaf", StringComparison.InvariantCultureIgnoreCase))
                {
                    eFormName = Constants.GTForm;
                }
                else
                {
                    eFormName = Constants.GUForm;
                }

                model.GiftTransmittalId = Guid.Empty;
                model.GiftTransmittalItemId = Guid.Empty;
                model.GiftTransmittalDistributionId = Guid.Empty;
                model.FormNumber = await _domainService.FormOperations.GetFormNumber(eFormName);
                model.ProjectDistribution = new ProjectDistribution();
                model.InitializedConstituent = new InitializedConstituent();
                model.Organization = organization;
            }

            model = SetupIntegration(model);
            model.InitializedConstituent.ConstituentFor = String.IsNullOrEmpty(model.InitializedConstituent.OrganizationName) ? "Individual" : "Organization";
            model.SupportingDocuments = new SupportingDocumentsListViewModel();
            model.ValidFileTypes = _config.ValidFileTypes;
            return View("Create", model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(GTInitializeViewModel model)
        {
            model = SetupIntegration(model);
            model.SupportingDocuments = new SupportingDocumentsListViewModel();

            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            //Validate project
            if (!String.IsNullOrWhiteSpace(model.ProjectDistribution.ProjectId))
            {
                var projectExists = await ProjectExists(model.Organization, model.ProjectDistribution.ProjectId);
                if (!projectExists)
                {
                    ModelState.AddModelError("Project", "Designation is invalid. Please enter a valid designation");
                    return View("Create", model);
                }
            }

            //Calculate the UDF fees
            var giftTransmittal = (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal)model;

            try
            {
                giftTransmittal.Id = await _domainService.LynxWebService.SaveGiftTransmittal(giftTransmittal);
                //needs to have the Id of the giftdistrubutionId instead of being a null guid

                if (model.GiftTransmittalId == Guid.Empty)
                {
                    model.GiftTransmittalId = await _domainService.LynxWebService.SaveGiftTransmittal(giftTransmittal);
                }

                return RedirectToAction("Review", new { giftTransmittalId = model.GiftTransmittalId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving initialized gift trasnmital");
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> Review(Guid giftTransmittalId)
        {
            try
            {
                //Get the giftr tranmittal data
                var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);

                var model = (GTInitializeViewModel)giftTransmittal;

                model = SetupIntegration(model);

                model.SupportingDocuments = await GetSupportingDocumentsListViewModel(model.FormNumber);
                model.SupportingDocuments.PreventDelete = true;

                return View("Review", model);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Gift Transmittal");
                throw;
            }

        }

        [HttpPost]
        public async Task<IActionResult> Submit(GTInitializeViewModel model)
        {

            model = SetupIntegration(model);
            try
            {
                _logger.LogInformation("Sending notification to submitters");
                await SendNotification(model.Email, model.PreparerName, model.FormNumber, model.GiftTransmittalId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending notifications for gift transmittal form number [{model.GiftTransmittalId}]");
            }
            return RedirectToAction("InitializedForms");
        }


        /// <summary>
        /// Get a gif transmittal for making changes
        /// </summary>
        /// <param name="giftTransmittalId"></param>
        /// <param name="giftTransmittalItemId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Edit(Guid giftTransmittalId, Guid giftTransmittalItemId)
        {
            //var giftTransmittal = _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            ////when we map it to the view model using the details of the gift transmittal item that matches the giftTransmittalItemId
            //var giftTransmittalItem = giftTransmittal.GiftTransmittalItems.Where(i => i.Id == giftTransmittalItemId).First();
            //var model = (GiftTransmittalViewModel)giftTransmittal;
            //model.ItemId = giftTransmittalItemId;
            //model.Constituent = (Constituent)giftTransmittalItem;
            //model.GiftTransmittalRecognitionCredits = giftTransmittalItem.GiftTransmittalItemRecognitionCredits
            //                                                                .Where(c => !c.IsDeleted)
            //                                                                .Select(c => (Constituent)c).ToList();
            //model.IncludeRecognitionCredit = giftTransmittalItem.GiftTransmittalItemRecognitionCredits.Any();
            //model.Distributions = giftTransmittalItem.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Select(d => (Distribution)d).ToList();
            //model.RequireSecondaryApprover = model.Approvals.Where(a => a.ApprovalStageCode == 2).Any();
            ////model.ObjectCode = giftTransmittalItem.ObjectCode;
            ////model.ItemDescription = giftTransmittalItem.ItemDescription;
            //model.TitlesList = GetTitleList();
            //model.SuffixesList = GetSuffixList();
            //model.CountriesList = GetCountryList();
            //model.StatesList = GetStateList();
            ////model.Colleges = Colleges();
            //model.Title = "Gift Transmittal";
            //model.PageId = "giftTransmittalsPage";
            //model.ActiveClass = "GiftTransmittals";
            ////model.Message = "Gift Transmittal Admin";
            //model.NavigationGroups = GetNavigationGroups();
            //model.User = User.Identity.Name;
            //model.Organization = giftTransmittal.FormNumber.StartsWith("GT") ? "UAF" : "UA";

            //model.IsChanged = false;
            //model.Id = 0L;
            //model.System = "GiftTransmittal";
            //model.SystemId = 0;
            //model.Integration = String.Empty;
            //model.RecordStatus = String.Empty;
            //model.SourceRecordId = String.Empty;
            //model.ChangeAgent = String.Empty;
            //model.CreatedOnDT = DateTime.Now;

            //model.SupportingDocuments = new ViewModels.SupportingDocuments.SupportingDocumentsListViewModel();

            var model = await SetupViewModel(giftTransmittalId, giftTransmittalItemId);

            return View("Edit", model);
        }


        private async Task SaveGiftTransmital(GiftTransmittalViewModel model)
        {
            try
            {
                var giftTransmittal = (Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal)model;

                model.GiftTransmittalId = await _domainService.LynxWebService.SaveGiftTransmittal(giftTransmittal);
                var gt = await _domainService.LynxWebService.LoadGiftTransmittal(model.GiftTransmittalId);
                var modifiedGiftTransmittal = (GiftTransmittalViewModel)gt;

                var distrubutions = modifiedGiftTransmittal.Distributions;
                distrubutions = model.Distributions;
                var distrubutionsTotalRequiresSecondaryApprover = distrubutions.Sum(d => d.Amount) >= 10000;

                if (model.RequireSecondaryApprover
                    && !modifiedGiftTransmittal.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.FormNumber.StartsWith("GT")
                    )
                {
                    await _domainService.LynxWebService.AddSecondaryApprover(model.GiftTransmittalId);
                    //This is prevented on forms greater than or equal to 10,0000
                    model.Approvals.Add(new Approval() { ApprovalStageCode = 2 });
                }
                else if (!model.RequireSecondaryApprover
                    && modifiedGiftTransmittal.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && !(distrubutionsTotalRequiresSecondaryApprover))
                {
                    var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == 2).First();

                    await _domainService.LynxWebService.RemoveSecondaryApprover((Guid)approvalRecord.Id);

                    model.Approvals = model.Approvals.Where(a => a.ApprovalStageCode != 2).ToList();
                }

                await RegeneratePrintedForm(giftTransmittal, model.Organization);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error saving Gift Transmittal");
                throw;
            }
        }

        /// <summary>
        /// Update the gift transmittal printed form
        /// </summary>
        /// <param name="giftTransmittal"></param>
        /// <param name="organization"></param>
        /// <returns></returns>
        private async Task RegeneratePrintedForm(Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal, string organization)
        {
            var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(giftTransmittal.FormNumber);

            var printedForms = supportingDocuments.Where(d => d.FileName == $"{giftTransmittal.FormNumber}-PrintedForm.pdf" 
                                                                            || d.FileName == $"{giftTransmittal.FormNumber}-BursarForm.pdf");

            foreach (var file in printedForms)
            {
                await _domainService.PaperSaveOperations.DeleteDocumentById(file.Id);
            }

            var pdf = _pdfService.CreateGiftTransmittalPDF(giftTransmittal, organization);
            await uploadGiftTransmittal(pdf);

            if(giftTransmittal.BatchTypeCode == (byte)GTBatchTypeCodes.Check
                && giftTransmittal.GiftTransmittalItems != null
                && giftTransmittal.GiftTransmittalItems.Any()
                && giftTransmittal.CheckPayableToUniversity)
                await uploadPDFView(giftTransmittal.Id, giftTransmittal.GiftTransmittalItems.First().Id);
        }

        private async Task CreateWorkflow(GiftTransmittalViewModel model, Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal giftTransmittal)
        {
            //if (model.Approved == true)
            //{
            var item = giftTransmittal.GiftTransmittalItems.First();
            var batchType = giftTransmittal.BatchType; //Enum.GetName(typeof(BatchTypeCodes), model.Transaction.BatchTypeCode);
            var id = model.GiftTransmittalId;
            var outPostDate = DateTime.MinValue;
            var postDate = DateTime.TryParse(model.PostDate, out outPostDate) ? outPostDate : DateTime.Today; //What happens if this fails?
            //Create workflow documents
            switch (model.FormNumber.Substring(0, 2))
            {
                case "GT":
                    var constituent = String.IsNullOrWhiteSpace(model.Constituent.OrganizationName) ? $"{model.Constituent.FirstName} {model.Constituent.LastName}" : model.Constituent.OrganizationName;

                    var uafTransaction = new UAFTransaction()
                    {
                        ConstituentName = constituent,
                        GiftTotal = item.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Sum(d => d.Amount),
                        ReceiptTotal = item.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Sum(d => d.ReceiptAmount),
                        Projects = item.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Select(d => d.FundAccount),
                        TransactionType = batchType,
                        PostDate = postDate == DateTime.MinValue ? DateTime.Today : postDate,
                        GiftTransmittalId = id
                    };
                    await _arOperations.QueueUAFTransaction(model.FormNumber, uafTransaction);
                    break;
                case "GU":
                    var uaTransaction = new UATransaction()
                    {
                        TransactionType = batchType,
                        PostDate = postDate == DateTime.MinValue ? DateTime.Today : postDate,
                        GiftTransmittalId = id
                    };
                    await _arOperations.QueueUATransaction(model.FormNumber, uaTransaction);
                    break;
                default:
                    break;
            }
            //}
        }

        public async Task<Sentry.Domain.PaperSave.Entities.NewDocument> CreateBursarPDF(Guid giftTransmittalId, Guid giftTransmittalItemId)
        {

            var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            //when we map it to the view model using the details of the gift transmittal item that matches the giftTransmittalItemId
            var giftTransmittalItem = giftTransmittal.GiftTransmittalItems.Where(i => i.Id == giftTransmittalItemId).First();
            var distributions = giftTransmittalItem.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Select(d => (Distribution)d).ToList();

            var organization = giftTransmittal.FormNumber.StartsWith("GT") ? "UAF" : "UA";
            foreach (var distribution in distributions)
            {
                var designation = await _domainService.MasterDataWebService.GetDesignation(organization, distribution.FundAccount);
                distribution.Designation = designation.Name;
            }

            var preparedBy = await _domainService.CAMOperations.FindEmployeesByEmployeeId(giftTransmittal.PreparedByName);

            var makerOfCheck = string.Empty;

            if (!String.IsNullOrWhiteSpace(giftTransmittalItem.ConstituentOrganizationName))
            {
                makerOfCheck = giftTransmittalItem.ConstituentOrganizationName;
            }
            else
            {
                makerOfCheck = $"{giftTransmittalItem.ConstituentFirstName} {giftTransmittalItem.ConstituentMiddleName} {giftTransmittalItem.ConstituentLastName}";
            }

            var model = new BursarPDF()
            {
                MakerOfCheck = makerOfCheck,
                FormNumber = giftTransmittal.FormNumber,
                CheckNumber = giftTransmittal.CheckNumber,
                Distributions = distributions,
                PreparedBy = new PreparedBy()
                {
                    ContactName = $"{preparedBy.FirstName} {preparedBy.LastName}",
                    ContactEmail = preparedBy.Email,
                    ContactJobTitle = preparedBy.JobTitle,
                    ContactDepartmentCode = preparedBy.DepartmentCode,
                    ContactDepartmentName = preparedBy.DepartmentName,
                    ContactNetId = preparedBy.NetId
                }
            };

            var fileName = $"{giftTransmittal.FormNumber}-BursarForm.pdf";

            _logger.LogInformation($"Starting create bursar PDF for form number [{giftTransmittal.FormNumber}]");

            var html = await this.RenderViewAsync("BursarPDFView", model);
            iText.Html2pdf.ConverterProperties converterProperties = new iText.Html2pdf.ConverterProperties();
            string contents = String.Empty;
            using (FileStream pdfDest = System.IO.File.Open($"{_bursarPdfPath}\\{fileName}", FileMode.Create))
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

            using (FileStream file = System.IO.File.Open($"{_bursarPdfPath}\\{fileName}", FileMode.Open))
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


        private bool AllowSecondaryApproval(GiftTransmittalViewModel model)
        {
            return Helper.IsARSecondaryReviewer && model.ApproverStage == 2;            
        }

        private async Task Approve(Guid? id,
                            Guid giftTransmittalId, 
                            int approverStage, 
                            string employeeId, 
                            string firstName,
                            string lastName,
                            string userName,
                            bool approved, 
                            string comments)
        {


            var approval = new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalApproval()
            {
                Id = id,
                GiftTransmittalId = giftTransmittalId,
                ApprovalStageCode = (byte)approverStage,
                ApproverEmployeeId = employeeId,
                ApproverFirstName = firstName,
                ApproverLastName = lastName,
                ApproverUserName = userName,
                Approved = approved,
                Comments = comments,
                ApprovedOn = DateTime.Now
            };
            
            await _domainService.LynxWebService.ApproveGiftTransmittal(approval);
        }

        [HttpPost]
        public async Task<IActionResult> ReviewerApprove(GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            for (int i = model.Distributions.Count() - 1; i >= 0; i--)
            {
                if (model.Distributions[i].IsDeleted && model.Distributions[i].Id == Guid.Empty)
                {
                    model.Distributions.RemoveAt(i);
                }
            }

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is equal to or greater than $10,000");
            }

            if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View("Edit", model);
            }

            await SaveGiftTransmital(model);

            var user = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);

            var approvalStage = 1;
            var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == approvalStage).First();

            await Approve(approvalRecord.Id, model.GiftTransmittalId, approvalStage, user.EmployeeId, user.FirstName, user.LastName, user.UserName, true, model.ApprovalComments);

            var redirectedView = model.FormNumber.StartsWith("GT") ? nameof(GTForms) : nameof(GUForms);

            return RedirectToAction(redirectedView);
        }

        public async Task<IActionResult> SecondaryApprove (GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            for (int i = model.Distributions.Count() - 1; i >= 0; i--)
            {
                if (model.Distributions[i].IsDeleted && model.Distributions[i].Id == Guid.Empty)
                {
                    model.Distributions.RemoveAt(i);
                }
            }

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is equal to or greater than $10,000");
            }

                if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View("Edit", model);
            }

            await SaveGiftTransmital(model);

            var user = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);

            var approvalStage = 2;
            var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == approvalStage).First();

            await Approve(approvalRecord.Id, model.GiftTransmittalId, approvalStage, user.EmployeeId, user.FirstName, user.LastName, user.UserName, true, model.ApprovalComments);

            if (model.FormNumber.StartsWith("GT"))
            {
                await SendUAFApprovalNotification(model.GiftTransmittalId);
            }

            return RedirectToAction(nameof(SecondaryApproverForms));
        }

        [HttpPost]
        public async Task<IActionResult> ReviewerReject(GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            for (int i = model.Distributions.Count() - 1; i >= 0; i--)
            {
                if (model.Distributions[i].IsDeleted && model.Distributions[i].Id == Guid.Empty)
                {
                    model.Distributions.RemoveAt(i);
                }
            }

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is equal to or greater than $10,000");
            }

            if (String.IsNullOrWhiteSpace(model.ApprovalComments))
            {
                ModelState.AddModelError("ApprovalComments", "Rejection Comments are required");
            }

            if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View("Edit", model);
            }

            await SaveGiftTransmital(model);

            var user = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);

            var approvalStage = 1;
            var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == approvalStage).First();

            await Approve(approvalRecord.Id, model.GiftTransmittalId, approvalStage, user.EmployeeId, user.FirstName, user.LastName, user.UserName, false, model.ApprovalComments);

            await SendCampusRejectionNotification(model.GiftTransmittalId, model.ApprovalComments, model.CCEmails);

            var redirectedView = model.FormNumber.StartsWith("GT") ? nameof(GTForms) : nameof(GUForms);

            return RedirectToAction(redirectedView);
        }

        public async Task<IActionResult> SecondaryReject(GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            for (int i = model.Distributions.Count() - 1; i >= 0; i--)
            {
                if (model.Distributions[i].IsDeleted && model.Distributions[i].Id == Guid.Empty)
                {
                    model.Distributions.RemoveAt(i);
                }
            }

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is equal to or greater than $10,000");
            }

            if (String.IsNullOrWhiteSpace(model.ApprovalComments))
            {
                ModelState.AddModelError("ApprovalComments", "Rejection Comments are required");
            }

            if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View("Edit", model);
            }

            await SaveGiftTransmital(model);

            var user = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);

            var approvalStage = 2;
            var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == approvalStage).First();
            await Approve(approvalRecord.Id, model.GiftTransmittalId, approvalStage, user.EmployeeId, user.FirstName, user.LastName, user.UserName, false, model.ApprovalComments);

            if (model.FormNumber.StartsWith("GT"))
            {
                await SendUAFApprovalNotification(model.GiftTransmittalId);
            }

            return RedirectToAction(nameof(SecondaryApproverForms));
        }

        public async Task<IActionResult> Finalize(GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is equal to or greater than $10,000");
            }

            var dPostDate = DateTime.MinValue;
            DateTime.TryParse(model.PostDate, out dPostDate);

            if (String.IsNullOrWhiteSpace(model.PostDate)
                || dPostDate <= DateTime.MinValue)
            {
                if (String.IsNullOrWhiteSpace(model.PostDate))
                    ModelState.AddModelError("PostDate", "The Post Date is required");
                else
                    ModelState.AddModelError("PostDate", "The Post Date is not valid");
            }

            if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View("Edit", model);
            }

            await SaveGiftTransmital(model);

            var user = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);

            var approvalStage = 3;
            var approvalRecord = model.Approvals.Where(a => a.ApprovalStageCode == approvalStage).First();
            await Approve(approvalRecord.Id, model.GiftTransmittalId, approvalStage, user.EmployeeId, user.FirstName, user.LastName, user.UserName, true, model.ApprovalComments);

            var giftTransmital = await _domainService.LynxWebService.LoadGiftTransmittal(model.GiftTransmittalId);
            await CreateWorkflow(model, giftTransmital);

            if (model.FormNumber.StartsWith("GT"))
            {
                return RedirectToAction(nameof(FinalizeGTForms));
            }
            else
            {
                return RedirectToAction(nameof(FinalizeGUForms));
            }
        }

        [HttpPost]
        public async Task<IActionResult> ResetUAFApprovers(GiftTransmittalViewModel model)
        {
            foreach (var approval in model.Approvals)
            {
                if (approval.ApprovalStageCode != 3)
                {
                    var approvalRecord = new Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittalApproval()
                    {
                        Id = approval.Id,
                        GiftTransmittalId = model.GiftTransmittalId,
                        ApprovalStageCode = approval.ApprovalStageCode
                    };
                    await _domainService.LynxWebService.ResetGiftTransmittalApprover(approvalRecord);
                }                
            }           

            var redirectedView = model.FormNumber.StartsWith("GT") ? nameof(GTForms) : nameof(GUForms);

            return RedirectToAction(redirectedView);
        }

        /// <summary>
        /// Save the gift transmittal changes
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(GiftTransmittalViewModel model)
        {
            var md = _domainService.MasterDataWebService;

            for (int i = model.Distributions.Count() - 1; i >= 0; i--)
            {
                if (model.Distributions[i].IsDeleted && model.Distributions[i].Id == Guid.Empty)
                {
                    model.Distributions.RemoveAt(i);
                }
            }

            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidateEmail(model.Constituent.Email))
            {
                ModelState.AddModelError("Constituent.Email", $"The Constituent Email {model.Constituent.Email} is not valid");
            }
            if (!String.IsNullOrWhiteSpace(model.Constituent.Email) && !md.ValidatePhone(model.Constituent.PhoneNumber))
            {
                ModelState.AddModelError("Constituent.PhoneNumber", $"The Constituent Phone Number {model.Constituent.PhoneNumber} is not valid");

            }
            if (!Object.ReferenceEquals(model.Constituent.Address, null) && !md.ValidateMailingAddress(((Sentry.Domain.MasterDataWebService.Entities.MailingAddress)model.Constituent.Address)))
            {
                ModelState.AddModelError("Constituent.Address", $"The Constituent Address {model.Constituent.Address} is not valid");
            }

            if (!model.RequireSecondaryApprover
                    && model.Approvals.Where(a => a.ApprovalStageCode == 2).Any()
                    && model.Distributions.Sum(d => d.Amount) >= 10000)
            {
                ModelState.AddModelError("RequireSecondaryApprover", "Cannot remove a Secondary Approver if the Gift Total is greater than or equal to $10,000");
            }

            if (!ModelState.IsValid)
            {
                model.SupportingDocuments = new SupportingDocumentsListViewModel();
                model.NavigationGroups = GetNavigationGroups();
                return View(model);
            }

            await SaveGiftTransmital(model);
            var returnViewModel = await SetupViewModel(model.GiftTransmittalId, model.ItemId);

            return View("Edit", returnViewModel);
        }
        private async Task SendNotification(string preparerEmail, string preparerName,
            string formNumber, Guid giftTransmittalId)
        {
            IEnumerable<Data.Models.Notifications.SendTo> preparerSendTos = new List<Data.Models.Notifications.SendTo>()
            {
                new Data.Models.Notifications.SendTo()
                {
                    Email = preparerEmail,
                    Name = preparerName
                }
            };

            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();

            //determines if cc emails need to go out
            //var sendCCEmail = _config.SendGrid.SendCCEmailList.SendARApproval;

            //if (sendCCEmail)
            //{
            //    ccEmailList = ApprovalCCEmailList();
            //}

            var details = new GiftTransmittalInitializedDetails()
            {
                form_id = formNumber,
                //https://localhost:44383/GiftDisbursements/review?giftTransmittalId=1170
                uafdn_link = $"{_config.UAFDNTransmittalURL}?giftTransmittalId={giftTransmittalId}&organization=uaf" //TODO need to use the appropriate organization
                //Link needs to take them to the create page in UAFDN 
            };

            try
            {
                await _notificationService.SendNotificationAsync(preparerSendTos, ccEmailList, _config.SendGrid.TransmittalInitializedTemplateId, details);
                _logger.LogInformation($"Sent notification for initialized transmittal form [{formNumber}] to [{string.Join(",", preparerSendTos.Select(a => a.Email))}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending initialized transmittal form [{formNumber}]");
                throw;
            }
        }

        private async Task<IEnumerable<SelectListItem>> Colleges()
        {
            var colleges = await _domainService.MasterDataWebService.GetColleges();
            return colleges
                        .Select(s => new SelectListItem()
                        {
                            Value = s.MasterRecordCode,
                            Text = s.Name
                        })
                        .OrderBy(c => c.Text)
                        .ToList();

        }

        private async Task<IEnumerable<SelectListItem>> Departments(string collegeCode)
        {
            var departments = await _domainService.MasterDataWebService.GetDepartments(collegeCode);
            return departments
                            .Select(s => new SelectListItem()
                            {
                                Value = s.Code,
                                Text = s.Name
                            })
                            .OrderBy(c => c.Text)
                            .ToList();

        }

        private async Task<IEnumerable<SelectListItem>> Projects(string departmentCode, string organization)
        {
            var projects = await _domainService.MasterDataWebService.GetDesignations(organization, departmentCode);
            return projects
                        .Select(s => new SelectListItem()
                        {
                            Value = s.DesignationId,
                            Text = $"{s.DesignationId} - {s.Name}"
                        })
                        .OrderBy(c => c.Text)
                        .ToList();
        }
        [HttpPost]
        public IActionResult Cancel(GiftTransmittalViewModel model)
        {
            //TODO use lynx method to delete 
            model.IsDeleted = true;
            return View("Index");
        }

        private async Task SendCampusRejectionNotification(Guid giftTransmittalId, string comments, string ccEmails)
        {
            var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            var employee = await _domainService.CAMOperations.FindEmployeesByEmployeeId(giftTransmittal.PreparedByName);

            if (employee != null)
            {
                var sendTos = new List<Data.Models.Notifications.SendTo>()
                {
                    new Data.Models.Notifications.SendTo()
                    {
                        Email = employee.Email,
                        Name = $"{employee.FirstName} {employee.LastName}"
                    }
                };
               
                var ccEmailList = SetupCCEmailList(ccEmails);               

                var organization = giftTransmittal.FormNumber.StartsWith(Constants.GTForm) ? "uaf" : "ua";

                var details = new GiftTransmittalRejectionDetails()
                {
                    comments = comments,
                    form_id = giftTransmittal.FormNumber,
                    uafdn_link = $"{_config.UAFDNTransmittalURL}?giftTransmittalId={giftTransmittalId}&organization={organization}"
                };

                try
                {
                    await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.TransmittalRejectionTemplateId, details);
                }
                catch
                {
                    throw;
                }
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


        //private async Task SendUAFRejectionNotification(Guid giftTransmittalId)
        //{
        //    var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
        //    var sendTos = new List<Data.Models.Notifications.SendTo>()
        //    {
        //        new Data.Models.Notifications.SendTo()
        //        {
        //            Email = _config.SendGrid.ARStaffGroupEmail,
        //            Name = _config.SendGrid.ARStaffGroupEmailName
        //        }
        //    };


        //    var ccEmailList = new List<Data.Models.Notifications.SendTo>();

        //    //list to determine if cc emails need to go out 
        //    var sendCCEmailList = _config.SendGrid.SendCCEmailList;
        //    var sendCCEmail = _config.SendGrid.SendCCEmailList.Where(c => c.SendARRejection).Any();

        //    if (sendCCEmail)
        //    {
        //        ccEmailList = RejectionCCEmailList().ToList();
        //    }

        //    var organization = giftTransmittal.FormNumber.StartsWith(Constants.GTForm) ? "uaf" : "ua";

        //    var details = new GiftTransmittalRejectionDetails()
        //    {
        //        form_id = giftTransmittal.FormNumber,
        //        uafdn_link = $"{_config.UAFDNTransmittalURL}?giftTransmittalId={giftTransmittalId}&organization={organization}"
        //    };

        //    try
        //    {
        //        await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.GTSecondaryRejected, details);
        //    }
        //    catch
        //    {
        //        throw;
        //    }
        //}

        private async Task SendUAFApprovalNotification(Guid giftTransmittalId)
        {
            var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            var sendTos = new List<Data.Models.Notifications.SendTo>()
            {
                new Data.Models.Notifications.SendTo()
                {
                    Email = _config.SendGrid.ARStaffGroupEmail,
                    Name = _config.SendGrid.ARStaffGroupEmailName
                }
            };

            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();

            var organization = giftTransmittal.FormNumber.StartsWith(Constants.GTForm) ? "uaf" : "ua";

            var details = new GiftTransmittalRejectionDetails()
            {
                form_id = giftTransmittal.FormNumber,
                uafdn_link = $"{_config.UAFDNTransmittalURL}?giftTransmittalId={giftTransmittalId}&organization={organization}"
            };

            try
            {
                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.GTSeconaryApproved, details);
            }
            catch
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetColleges()
        {
            var colleges = await Colleges();
            return new JsonResult(colleges);
        }

        [HttpGet]
        public async Task<IActionResult> GetDepartments(string collegeCode)
        {
            var departments = await Departments(collegeCode);
            return new JsonResult(departments);

        }

        [HttpGet]
        public async Task<IActionResult> GetProjects(string departmentCode, string organization)
        {
            var projects = await Projects(departmentCode, organization);
            return new JsonResult(projects);
        }

        [HttpPost]
        public async Task<IActionResult> SearchProject()
        {
            var id = "";
            var organization = "";
            var index = 0;

            MemoryStream stream = new MemoryStream();
            await Request.Body.CopyToAsync(stream);
            stream.Position = 0;
            using (StreamReader reader = new StreamReader(stream))
            {
                //Read data and put into FundData object.
                string requestBody = reader.ReadToEnd();
                if (requestBody.Length > 0)
                {
                    var fund = JsonConvert.DeserializeObject<FundData>(requestBody);
                    if (fund != null)
                    {
                        if (fund.id.IndexOf(" ") != -1)
                        {
                            id = fund.id.Substring(0, fund.id.IndexOf(" "));
                        }
                        else
                        {
                            id = fund.id;
                        }
                        index = fund.index;
                    }
                    organization = fund.organization;
                }
            }

            var projectExists = await ProjectExists(organization, id);
            if (!projectExists)
            {
                ModelState.AddModelError("FundNotFound", "The designation was not found in the database.");
                return new JsonResult("Not Found");
            }

            //Get fund description.
            var designation = await _domainService.MasterDataWebService.GetDesignation(organization, id);

            var projDesc = designation.Name;

            return new JsonResult(new { id = id, projDesc = projDesc });
        }

        private async Task<bool> ProjectExists(string organization, string id)
        {
            var result = await _domainService.MasterDataWebService.GetDesignation(organization, id);
            return !String.IsNullOrWhiteSpace(result.DesignationId);
        }

        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="giftTransmittalId"></param>
        ///// <param name="itemId"></param>
        ///// <param name="distributionId"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteDistribution(GiftTransmittalViewModel model, int itemIndex)
        //{
        //    ModelState.Clear();

        //    model = SetupViewModel(model.GiftTransmittalId, model.ItemId);

        //    var giftTrasnmittalId = model.GiftTransmittalId;
        //    var gifTransmittalItemId = model.ItemId;
        //    var distributionId = model.Distributions[itemIndex].Id;

        //    try
        //    {
        //        if (distributionId != Guid.Empty)
        //        {
        //            //Delete Distribution
        //            var giftTransmittal = _domainService.LynxWebService.LoadGiftTransmittal(giftTrasnmittalId);
        //            var giftTransmittalItem = giftTransmittal.GiftTransmittalItems
        //                                                        .Where(i => i.Id == gifTransmittalItemId)
        //                                                        .First();
        //            var giftTransmittalItemDistribution = giftTransmittalItem.GiftTransmittalItemDistributions
        //                                                                        .Where(d => d.Id == distributionId)
        //                                                                        .First();
        //            giftTransmittalItemDistribution.IsDeleted = true;

        //            var retGuid = _domainService.LynxWebService.DeleteDistribution(giftTransmittalItemDistribution);
        //        }
        //        else
        //        {
        //            var giftTransmittalItemDistribution = new GiftTransmittalItemDistribution();
        //            giftTransmittalItemDistribution.IsDeleted = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Error deleting Gift Transmittal Distribution {distributionId}", distributionId);
        //        return BadRequest("Error deleting Gift Transmittal Distribution");
        //    }

        //    model.Distributions.RemoveAt(itemIndex);
        //    model.ExpandDistributions = true;

        //    return View("Edit", model);
        //}
        private async Task<GiftTransmittalViewModel> SetupViewModel(Guid giftTransmittalId, Guid giftTransmittalItemId)
        {
            var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            //when we map it to the view model using the details of the gift transmittal item that matches the giftTransmittalItemId
            var giftTransmittalItem = giftTransmittal.GiftTransmittalItems.Where(i => i.Id == giftTransmittalItemId).First();
            var model = (GiftTransmittalViewModel)giftTransmittal;
            model.ItemId = giftTransmittalItemId;
            model.Constituent = (Constituent)giftTransmittalItem;
            model.GiftTransmittalRecognitionCredits = giftTransmittalItem.GiftTransmittalItemRecognitionCredits
                                                                            .Where(c => !c.IsDeleted)
                                                                            .Select(c => (Constituent)c).ToList();
            model.IncludeRecognitionCredit = giftTransmittalItem.GiftTransmittalItemRecognitionCredits.Any();
            model.Distributions = giftTransmittalItem.GiftTransmittalItemDistributions.Where(d => !d.IsDeleted).Select(d => (Distribution)d).ToList();
            model.RequireSecondaryApprover = model.Approvals.Where(a => a.ApprovalStageCode == 2).Any();
            //model.ObjectCode = giftTransmittalItem.ObjectCode;
            //model.ItemDescription = giftTransmittalItem.ItemDescription;
            model.TitlesList = GetTitleList();
            model.SuffixesList = GetSuffixList();
            model.CountriesList = GetCountryList();
            model.StatesList = GetStateList();
            //model.Colleges = Colleges();
            model.Title = "Gift Transmittal";
            model.PageId = "giftTransmittalsPage";
            model.ActiveClass = "GiftTransmittals";
            //model.Message = "Gift Transmittal Admin";
            model.NavigationGroups = GetNavigationGroups();
            model.User = User.Identity.Name;
            model.Organization = giftTransmittal.FormNumber.StartsWith("GT") ? "UAF" : "UA";

            model.IsChanged = false;
            model.Id = 0L;
            model.System = "GiftTransmittal";
            model.SystemId = 0;
            model.Integration = String.Empty;
            model.RecordStatus = String.Empty;
            model.SourceRecordId = String.Empty;
            model.ChangeAgent = String.Empty;
            model.CreatedOnDT = DateTime.Now;

            model.SupportingDocuments = new ViewModels.SupportingDocuments.SupportingDocumentsListViewModel();
            model.ValidFileTypes = _config.ValidFileTypes;
            model.CCEmails = giftTransmittal.CCEmails;

            return model;
        }

        private GiftTransmittalListViewModel GiftTransmittalStatusPageSetup(string listType)
        {
            var title = string.Empty;
            var defaultTitle = "Gift Transmittals";

            switch (listType)
            {
                case "WaitingOnBursarForms":
                    title = defaultTitle + " - Waiting On Bursar";
                    break;
                case "WaitingOnPreparerForms":
                    title = defaultTitle + " - Waiting On Preparer";
                    break;
                case "SecondaryApproverForms":
                    title = defaultTitle + " - Secondary Approval";
                    break;
                case "FinalizeForms":
                    title = defaultTitle + " - Finalize";
                    break;
                default:
                    title = defaultTitle;
                    break;
            }

            var model = new GiftTransmittalListViewModel()
            {
                Title = title,
                PageId = "giftTransmittalsPage",
                ActiveClass = "GiftTransmittals",
                Message = "Gift Transmittal Status Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
            };
            model.ListType = listType;
            return model;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddDistribution(GiftTransmittalViewModel model)
        {
            ModelState.Clear();
            model = await SetupViewModel(model.GiftTransmittalId, model.ItemId);

            model.Distributions.Add(new Distribution());
            model.ExpandDistributions = true;
            return View("Edit", model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewSupportingDocument(int id)
        {
            var document = await _domainService.PaperSaveOperations.GetDocumentById(id);

            return File(document.Contents, document.MimeType, $"{document.FormNumber}-{document.FileName}");
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
                model.SupportingDocuments = supportingDocuments.Select(d => (SupportingDocument)d).ToList();
                return model;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting supporting documents for {formNumber}", formNumber);
                var model = new SupportingDocumentsListViewModel();
                model.SupportingDocuments = new List<SupportingDocument>();
                model.Error = "Error retrieving supporting documents";
                return model;
            }

        }

        [HttpPost]
        public async Task<IActionResult> UploadSupportingDocument(IFormFile supportingDocument, string formNumber)
        {
            ModelState.Clear();
            var upload = supportingDocument;
            if (supportingDocument == null ||
                String.IsNullOrWhiteSpace(formNumber))
            {
                string message = "";

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

            private GTInitializeViewModel SetupIntegration(GTInitializeViewModel model)
        {
            model.Title = "Gift Transmittal";
            model.PageId = "giftTransmittalsPage";
            model.ActiveClass = "GiftTransmittals";
            model.NavigationGroups = GetNavigationGroups();
            model.User = User.Identity.Name;
            model.IsChanged = false;
            model.Id = 0L;
            model.System = "GiftTransmittal";
            model.SystemId = 0;
            model.Integration = String.Empty;
            model.RecordStatus = String.Empty;
            model.SourceRecordId = String.Empty;
            model.ChangeAgent = String.Empty;
            model.CreatedOnDT = DateTime.Now;
            return model;
        }

        [HttpGet]
        public async Task<IActionResult> GetEmployeeByName(string term)
        {
            try
            {
                var preparer = await _domainService.CAMOperations.FindEmployeesByName(term);
                var employees = preparer.Select(c => new
                {
                    id = c.EmployeeId,
                    department = $"{c.DepartmentCode}-{c.DepartmentName}",
                    jobTitle = c.JobTitle,
                    email = c.Email,
                    phone = c.Phone,
                    text = $"{c.FirstName} {c.LastName}"
                });

                return new JsonResult(employees);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting preparer name [{preparer}]", term);
                return BadRequest("Error getting vendor details");
            }
        }
        [HttpGet]
        public async Task<IActionResult> GetEmployeesByEmployeeId(string preparedByEmployeeId)
        {
            var employee = await _domainService.CAMOperations.FindEmployeesByEmployeeId(preparedByEmployeeId);
            return new JsonResult(employee);
        }

        private async Task uploadGiftTransmittal(PDF pdf)
        {
            var newDocument = new NewDocument()
            {
                FileName = $"{pdf.FormNumber}-PrintedForm.pdf",
                FormNumber = pdf.FormNumber
            };

            using (FileStream file = System.IO.File.Open(pdf.FileName, FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    newDocument.Contents = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            await _domainService.PaperSaveOperations.UploadDocument(newDocument);
        }

        private async Task uploadPDFView(Guid giftTransmittalId, Guid giftTransmittalItemId)
        {
            var document = await CreateBursarPDF(giftTransmittalId, giftTransmittalItemId);

            _logger.LogInformation($"Starting upload bursar pdf for file name [{document.FileName}]");

            try
            {
                await _domainService.PaperSaveOperations.UploadDocument(document);
                _logger.LogInformation($"Uploaded file [{document.FileName}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error uploading bursar copy for form number [{document.FormNumber}]");
            }
        }

        public async Task<IActionResult> GetGiftTransmittalByFormNumber(string formNumber)
        {
            try
            {
                var transmittal = await GetGiftTransmittal(formNumber);
                return new JsonResult(transmittal);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting gift transmittal by form number [{formNumber}]", formNumber);
                return BadRequest("Error searching for gift transmittal");

            }

        }

        private async Task<Sentry.Domain.Lynx.Entities.WebService.GiftTransmittal.GiftTransmittal> GetGiftTransmittal(string formNumber)
        {
            return await _domainService.LynxWebService.GetGiftTransmittal(formNumber);
        }

        /// <summary>
        /// Adds a disbursement line item.
        /// </summary>
        /// <param name="index">Item identifier.</param>
        /// <param name="department">Department associated with disbursement item.</param>
        /// <param name="disbursementType">Disbursement type.</param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult AddItem(int index, string organization, string formNumber)
        {
            ViewData["index"] = index;
            ViewData["organization"] = organization;
            ViewData["placeholder"] = (organization == "uaf" ? "99-99-9999" : "Designation Name");
            ViewData["formNumber"] = formNumber;

            var model = new Distribution() { ReceiptAmount = decimal.Zero, UdfFeeAmount = decimal.Zero };

            return PartialView("_Item", model);
        }
        //private IEnumerable<Data.Models.Notifications.SendTo> RejectionCCEmailList()
        //{
        //    var ccList = _config.SendGrid.ARRejectCCEmailList;
        //    return ccList
        //                .Select(c => new Data.Models.Notifications.SendTo()
        //                {
        //                    Name = c.Name,
        //                    Email = c.Email
        //                })
        //                .ToList();

        //}
        //private IEnumerable<Data.Models.Notifications.SendTo> ApprovalCCEmailList()
        //{
        //    var ccList = _config.SendGrid.ARApproveCCEmailList;
        //    return ccList
        //                .Select(c => new Data.Models.Notifications.SendTo()
        //                {
        //                    Name = c.Name,
        //                    Email = c.Email
        //                })
        //                .ToList();

        //}
    }
}
