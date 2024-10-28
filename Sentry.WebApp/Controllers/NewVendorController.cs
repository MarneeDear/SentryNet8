using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models.Notifications;
using Sentry.WebApp.Extensions;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.ViewModels.GiftDisbursements;
using Sentry.WebApp.ViewModels.NewVendor;
using Sentry.WebApp.ViewModels.SupportingDocuments;
using Sentry.WebApp.WebConstants;
using iText.StyledXmlParser.Jsoup.Safety;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
//using Org.BouncyCastle.Asn1.X509;
using Sentry.Domain.AccountsPayable.Entities;
using Sentry.Domain.Forms.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{
    public class NewVendorController : IntegrationController
    {
        public readonly Config _config;
        private readonly INotificationService _notificationService;
        private readonly IWebHostEnvironment _environment;
        private readonly string _vendorPdfPath;

        public NewVendorController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<IntegrationController> logger,
            IConfiguration configuration,
            IOptions<Config> config,
            IDomainService domainService,
            INotificationService notificationService,
            IWebHostEnvironment environment

            )
            : base(context, dwContext, logger, configuration, domainService)
        {
            _config = config.Value;
            _environment = environment;
            _notificationService = notificationService;
            _vendorPdfPath = $"{environment.WebRootPath}\\vendor-pdfs";
        }
        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }
        [HttpGet]
        public async Task<IActionResult> List()
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            var newVendorRequests = await _domainService.AccountsPayableOperations.GetNewVendorRequestsAwaitingApproval();
            var model = NewVendorRequestPageSetup();
            model.NewVendorRequests = newVendorRequests.Select(v => (NewVendorRequestListItem)v);

            return View(model);
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            BaseViewModel viewModel = new BaseViewModel()
            {
                Title = "New Vendor",
                PageId = "newVendorsPage",
                ActiveClass = "NewVendor",
                PageWrapperClass = "toggled",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };
            return View(viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(long id)
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            var model = await SetupViewModel(id);

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(NewVendorRequestViewModel model)
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            if (model.PaymentType == "EFT")
            {
                var supportingDocuments = await _domainService.PaperSaveOperations.AdvancedSearchDocumentsByFormNumber(model.FormNumber);

                if (!supportingDocuments.Any(d => d.SupportingDocumentType.Contains("EFT")))
                    ModelState.AddModelError("PaymentType", "All Vendors paying with EFT require a Voided Check or a Bank Letter");
            }
            if (!ModelState.IsValid)
            {
                model = await SetupViewModel(model.NewVendorRequestId);
                return View(model);
            }
            await SaveChanges(model);
            return RedirectToAction("Edit", new { id = model.NewVendorRequestId });
        }

        [HttpPost]
        public async Task<IActionResult> Approve(NewVendorRequestViewModel model)
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            try
            {
                var role = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
                _logger.LogInformation($"Approving form [{model.FormNumber}]. Assigned Role [{Helper.FinancialRole}]");

               
                if (!ModelState.IsValid)
                {
                    model = await SetupViewModel(model.NewVendorRequestId);
                    return View("Edit", model);
                }

                await SaveChanges(model);
                
                var newVendorRequest = await GetNewVendorRequest(model.NewVendorRequestId);
                
                await ApproveReject(model, true);

                _logger.LogInformation($"Creating vendor for form [{model.FormNumber}]");

                //create vendor and get new id
                var vendorId = await _domainService.AccountsPayableOperations.CreateVendor(model.NewVendorRequestId);

                //create copy of the vendor form               
                //upload to papersave
                var pdfModel = await SetupPDFViewModel(model.NewVendorRequestId);
                var pdf = await CreatePDF(pdfModel);
                await _domainService.PaperSaveOperations.UploadDocument(pdf);

                //create final documents
                //associate the attachments with the vendor
                //Metadata attributes               
                var attributes = new Dictionary<string, string>()
                {
                    { "W9 Year", model.W9Year },
                    { "ICA Year", model.ICAYear }
                };

                await CreateFinalDocuments(vendorId, model.FormNumber, attributes);
                
                try
                {
                    await SendNotification((NewVendorRequest)model);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending notifications for vendor request form number [{model.FormNumber}] vendor request id [{model.NewVendorRequestId}]");
                }


                _logger.LogInformation($"Approved form [{model.FormNumber}].");
                return RedirectToAction("List");
            }
            catch (Exception ex)
            {
                await _domainService.AccountsPayableOperations.ResetNewVendorRequest(model.NewVendorRequestId);
                _logger.LogError(ex, $"Error approving new vendor request [{model.FormNumber}]");
                return RedirectToAction("List");
            }

        }

        [HttpPost]
        public async Task<IActionResult> Reject(NewVendorRequestViewModel model)
        {
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            try
            {
                ModelState.Clear();
                _logger.LogInformation($"Rejecting form [{model.FormNumber}]. Assigned Role [{Helper.FinancialRole}]");

                await SaveChanges(model);

                var newVendorRequest = await GetNewVendorRequest(model.NewVendorRequestId);

                await ApproveReject(model, false);
                try
                {
                    await SendRejectionNotification(model.NewVendorRequestId, model.ApproveRejectComments);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error sending rejection notification to submitter. Form number [{model.FormNumber}] new vendor request id [{model.NewVendorRequestId}]"
                        , model.FormNumber, model.NewVendorRequestId);
                }

                _logger.LogInformation($"Rejected form [{model.FormNumber}].");
                return RedirectToAction("List", "NewVendor");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error rejecting new vendor request [{model.FormNumber}]");
                return RedirectToAction("SystemError", "Error");
            }

        }

        private async Task<NewVendorRequestViewModel> SetupViewModel(long id)
        {
            var newVendor = await _domainService.AccountsPayableOperations.GetNewVendorRequestById(id);
            var employeeId = await Helper.EmployeeId();
            var model = (NewVendorRequestViewModel)newVendor;
            model.PreparedByEmployeeId = employeeId;
            model = SetupIntegration(model);
            model.ValidFileTypes = _config.ValidFileTypes;
            model.StatesList = GetStateList();
            model.PayeeTypes = PayeeTypes();
            model.AttatchmentTypes = AttachmentTypes();
            model.SupportingDocuments = new ViewModels.SupportingDocuments.SupportingDocumentsListViewModel();
            if (model.VendorType == "Individual"
                                 && String.IsNullOrEmpty(model.BusinessContactFirstName)
                                 && String.IsNullOrEmpty(model.BusinessContactLastName))
            {
                int indexOfSpace = model.VendorName.IndexOf(' ');
                if (indexOfSpace == -1)
                {
                    model.BusinessContactFirstName = model.VendorName;
                    model.BusinessContactLastName = String.Empty;
                }
                else
                {
                    string businessContactFirstName = model.VendorName.Substring(0, indexOfSpace);
                    string businessContactLastName = model.VendorName.Substring(indexOfSpace + 1);

                    model.BusinessContactFirstName = businessContactFirstName;
                    model.BusinessContactLastName = businessContactLastName;
                }                
            }

            return model;
        }
        private async Task<PDFViewModelSimplified> SetupPDFViewModel(long id)
        {
            var form = await _domainService.AccountsPayableOperations.GetNewVendorRequestById(id);
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
        //This is to test easier just change the id for whatever form you want to see
        public async Task<IActionResult> TestPDF()
        {
            var newVendorRequestId = 31024;
            var model = await SetupPDFViewModel(newVendorRequestId);
            return View("PDFViewSimplified", model);
        }


        private NewVendorRequestViewModel SetupIntegration(NewVendorRequestViewModel model)
        {
            model.IsChanged = false;
            model.Id = 0;
            model.System = "NewVendorRequests";
            model.SystemId = 0;
            model.Integration = String.Empty;
            model.RecordStatus = String.Empty;
            model.SourceRecordId = String.Empty;
            model.ChangeAgent = String.Empty;
            model.CreatedOnDT = DateTime.Now;
            model.Title = "New Vendor Request";
            model.PageId = "newVendorRequestsPage";
            model.ActiveClass = "Administration";
            model.Message = "New Vendor Requests Status Page";
            model.NavigationGroups = GetNavigationGroups();
            model.User = User.Identity.Name;
            model.Organization = "UA";

            return model;
        }


        private IEnumerable<SelectListItem> PayeeTypes()
        {
            var payeeTypes = _config.PayeeTypes;
            return payeeTypes
                        .Select(c => new SelectListItem()
                        {
                            Value = c,
                            Text = c
                        })
                        .ToList();

        }

        private IEnumerable<SelectListItem> AttachmentTypes()
        {
            var attachmentTypes = _config.SupportingDocuments.VendorAttachmentTypes;
            return attachmentTypes
                        .Select(c => new SelectListItem()
                        {
                            Value = c.TempSupportingDocumentType,
                            Text = c.Description
                        })
                        .ToList();

        }
        private NewVendorRequestListViewModel NewVendorRequestPageSetup()
        {
            var model = new NewVendorRequestListViewModel()
            {
                Title = $"New Vendor Requests",
                PageId = "newVendorRequestsPage",
                ActiveClass = "Administration",
                Message = "New Vendor Request Status Page",
                NavigationGroups = GetNavigationGroups(),
                User = User.Identity.Name,
                Organization = "UA",
                CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole
            };

            ViewBag.CurrentRole = Helper.AlternateFinancialRole ?? Helper.FinancialRole;
            return model;
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
        public async Task<IActionResult> UploadSupportingDocument(IFormFile supportingDocument, string formNumber, string supportingDocumentType)
        {
            ModelState.Clear();
            var upload = supportingDocument;
            if (supportingDocument == null ||
                String.IsNullOrWhiteSpace(formNumber) ||
                String.IsNullOrWhiteSpace(supportingDocumentType))
            {
                string message = String.Empty;

                if (supportingDocument == null)
                    message = "Please upload a file to continue";
                else if (String.IsNullOrWhiteSpace(formNumber))
                    message = "A valid Form Number is required";
                else if (String.IsNullOrWhiteSpace(supportingDocumentType))
                    message = "An Attachment Type is required";

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
                SupportingDocumentType = supportingDocumentType
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
            if (!Helper.IsAPRole)
            {
                return View("../Error/Unauthorized");
            }

            var document = await _domainService.PaperSaveOperations.GetDocumentById(id);

            return File(document.Contents, document.MimeType, $"{document.FileName}");
        }

        private async Task ApproveReject(NewVendorRequestViewModel model, bool approved)
        {
            var employeeId = await Helper.EmployeeId();
            var approval = new NewVendorRequestApproval()
            {
                EmployeeId = employeeId,
                Comments = model.ApproveRejectComments ?? String.Empty,
                Approved = approved,
            };

            try
            {
                await _domainService.AccountsPayableOperations.ApproveRejectNewVendorRequest(model.NewVendorRequestId, approval);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error approving new vendor request id [{model.NewVendorRequestId}] form number [{model.FormNumber}]");
                throw;
            }

        }
        public async Task SaveChanges(NewVendorRequestViewModel model)
        {

            try
            {
                var updateNewVendorRequest = (UpdateNewVendorRequest)model;

                await _domainService.AccountsPayableOperations.UpdateNewVendorRequest(updateNewVendorRequest);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving new vendor Request {model.NewVendorRequestId}");
                throw;
            }
        }
        private async Task<NewVendorRequest> GetNewVendorRequest(long id)
        {
            var newVendorRequest = await _domainService.AccountsPayableOperations
                .GetNewVendorRequestById(id);

            return newVendorRequest;
        }

        private async Task SendNotification(NewVendorRequest newVendorRequest)
        {

            IEnumerable<Data.Models.Notifications.SendTo> sendTos = new List<Data.Models.Notifications.SendTo>()
            {
                new Data.Models.Notifications.SendTo()
                {
                    Email = newVendorRequest.PreparedByEmail,
                    Name = $"{newVendorRequest.PreparedByFirstName} {newVendorRequest.PreparedByLastName}"
                }
            };

            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();            

            var details = new NewVendorRequestApprovalDetails()
            {
                form_id = newVendorRequest.FormNumber,
            };
            try
            {
                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.VendorApproved, details);
                _logger.LogInformation($"Sent approval notification for form [{newVendorRequest.FormNumber}] to [{string.Join(",", sendTos.Select(a => a.Email))}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending vendor approver notifications for vendor id [{newVendorRequest.Id}]");
                throw;
            }
        }

        private async Task SendRejectionNotification(long id, string comments)
        {
            var newVendorRequest = await _domainService.AccountsPayableOperations.GetNewVendorRequestById(id);

            var sendTos = new List<Data.Models.Notifications.SendTo>()
                {
                    new Data.Models.Notifications.SendTo()
                    {
                        Email = newVendorRequest.PreparedByEmail,
                        Name = $"{newVendorRequest.PreparedByFirstName} {newVendorRequest.PreparedByLastName}"
                    }
                };

            //var ccEmailList = SetupCCEmailList(ccEmails);
            IEnumerable<Data.Models.Notifications.SendTo> ccEmailList = new List<Data.Models.Notifications.SendTo>();


            var details = new NewVendorRequestRejectionDetails()
            {
                comments = comments,
                form_id = newVendorRequest.FormNumber,
                uafdn_link = $"{_config.UAFDNNewVendorRequestURL}/{id}"
            };

            try
            {

                await _notificationService.SendNotificationAsync(sendTos, ccEmailList, _config.SendGrid.VendorRejected, details);
                _logger.LogInformation($"Sent rejection notification for form [{newVendorRequest.FormNumber}] to [{string.Join(",", sendTos.Select(a => a.Email))}]");

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error sending vendor approver notifications for vendor id [{id}]");

                throw;
            }
        }


        //[HttpPost("vendorrequestcopy")]
        //[AllowAnonymous]
        //public async Task<IActionResult> CreateVendorRequestCopy([FromBody] NewVendorFormCreate form)
        //{
        //    if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
        //    {
        //        return Unauthorized();
        //    }

        //    _logger.LogInformation($"Starting processing form [{form}]");
        //    var formDetails = JsonConvert.SerializeObject(form);

        //    //Create the PDF
        //    var model = await SetupPDFViewModel(form.NewVendorRequestId);
        //    //Create PDF copy of the form
        //    _logger.LogInformation($"Creating PDF copy of vendor for form [{formDetails}]");
        //    var pdf = await CreatePDF(model);

        //    //Create the temp document
        //    _logger.LogInformation($"Uploading PDF copy for form [{formDetails}]");
        //    await _domainService.PaperSaveOperations.UploadDocument(pdf);

        //    _logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{formDetails}]");
        //    _logger.LogInformation($"Creating final documents for form [{formDetails}]");
        //    await CreateVendorCopy(model.FormNumber, form.VendorId);


        //    return Ok("Form vendor copy created");
        //}

        //public async Task<IActionResult> CreateVendorAndDocuments(long newVendorRequestId, long vendorId)
        //{
        //    if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
        //    {
        //        return Unauthorized();
        //    }


        //    _logger.LogInformation($"Starting processing form [{newVendorRequestId}]");
        //    var formDetails = JsonConvert.SerializeObject(newVendorRequestId);

        //    try
        //    {
        //        _logger.LogInformation($"Starting create invoice and documents [{formDetails}]");

        //        var model = await SetupPDFViewModel(newVendorRequestId);


        //        //Create PDF copy of the form
        //        _logger.LogInformation($"Creating PDF copy of vendor for form [{formDetails}]");
        //        var pdf = await CreatePDF(model);

        //        //Upload PDF copy of the form
        //        _logger.LogInformation($"Uploading PDF copy for form [{formDetails}]");
        //        await _domainService.PaperSaveOperations.UploadDocument(pdf);
        //        _logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{formDetails}]");

        //        //Create final documents
        //        _logger.LogInformation($"Creating final documents for form [{formDetails}]");
        //        await CreateFinalDocuments(vendorId, model.FormNumber);

        //        return Ok("vendor and documents created");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error processing form [{formDetails}]");
        //        return BadRequest($"Error processing form [{formDetails}]");
        //    }
        //}

        private async Task<Sentry.Domain.PaperSave.Entities.NewDocument> CreatePDF(PDFViewModelSimplified model)
        {

            var fileName = $"{model.FormNumber}-vendor.pdf";

            _logger.LogInformation($"Starting create vendor PDF for form number [{model.FormNumber}]");

            var html = await this.RenderViewAsync("PDFViewSimplified", model);
            iText.Html2pdf.ConverterProperties converterProperties = new iText.Html2pdf.ConverterProperties();
            string contents = String.Empty;
            using (FileStream pdfDest = System.IO.File.Open($"{_vendorPdfPath}\\{fileName}", FileMode.Create))
            {
                iText.Html2pdf.HtmlConverter.ConvertToPdf(html, pdfDest, converterProperties);
                pdfDest.Close();
                pdfDest.Dispose();
            }

            var document = new Sentry.Domain.PaperSave.Entities.NewDocument()
            {
                FileName = fileName,
                FormNumber = model.FormNumber,
                SupportingDocumentType = "New Vendor Request"
            };

            using (FileStream file = System.IO.File.Open($"{_vendorPdfPath}\\{fileName}", FileMode.Open))
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    document.Contents = Convert.ToBase64String(memoryStream.ToArray());
                }
            }

            _logger.LogInformation($"Created vendor PDF for form number [{model.FormNumber}]");

            return document;
        }

        //private async Task CreateVendorCopy(string formNumber, long vendorId)
        //{
        //    try
        //    {
        //        _logger.LogInformation($"Starting create vendor final documents for form number [{formNumber}]");
        //        var finalDocument = new Sentry.Domain.PaperSave.Entities.FinalDocument()
        //        {
        //            SystemId = vendorId.ToString()
        //        };
        //        await _domainService.PaperSaveOperations.CreateFinalDocument(finalDocument, formNumber, $"{formNumber}-vendor.pdf");
        //        _logger.LogInformation($"Created vendor copy form number [{formNumber}]");
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, $"Error creating vendor copy for form number [{formNumber}]");
        //    }
        //}
        private async Task CreateFinalDocuments(long vendorId, string formNumber, IDictionary<string, string> attributes)
        {
            try
            {
                _logger.LogInformation($"Starting create vendor final documents for form number [{formNumber}]");
                var finalDocument = new Sentry.Domain.PaperSave.Entities.FinalDocument()
                {
                    SystemId = vendorId.ToString(),
                    DocumentAttributes = attributes
                };
                await _domainService.PaperSaveOperations.CreateFinalDocuments(finalDocument, formNumber);
                _logger.LogInformation($"Created final documents for form number [{formNumber}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating final vendor documents for form number [{formNumber}]", formNumber);
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

    }
}
