using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sentry.WebApp.Data;
using Sentry.WebApp.Extensions;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels.GiftDisbursements;
using Sentry.WebApp.ViewModels.Invoices;
using Sentry.WebApp.ViewModels.SupportingDocuments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Sentry.Domain.Forms.Entities;

namespace Sentry.WebApp.Controllers
{
    [ApiController]
    [Route("invoices")]
    [Authorize]
    public class InvoicesController : Controller
    {
        private readonly Config _config;
        private readonly IWebHostEnvironment _environment;
        private readonly string _invoicePdfPath;
        private readonly IDomainService _domainService;
        private readonly ILogger _logger;
        //private readonly IConfiguration _configuration;

        public InvoicesController(
            ILogger<InvoicesController> logger,
            //IConfiguration configuration,
            IDomainService domainService,
            IOptions<Config> config,
            INotificationService notificationService,
            IWebHostEnvironment environment)
        {
            _config = config.Value;
            _environment = environment;
            _invoicePdfPath = $"{environment.WebRootPath}\\invoice-pdfs";
            _domainService = domainService;
            _logger = logger;

        }

        [HttpGet("")]
        [AllowAnonymous]
        public IActionResult Index()
        {
            if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
            {
                return Unauthorized();
            }
            return Ok("YOU HAVE BEEN AUTHORIZED TO ACCESS INVOICES");
        }

        private async Task<PDFViewModelSimplified> SetupViewModel(long id)
        {
            var form = await _domainService.AccountsPayableOperations.GetGiftDisbursement(id);
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

        private async Task<Sentry.Domain.PaperSave.Entities.NewDocument> CreatePDF(PDFViewModelSimplified model)
        {

            var fileName = $"{model.FormNumber}-invoice.pdf";

            _logger.LogInformation($"Starting create invoice PDF for form number [{model.FormNumber}]");

            var html = await this.RenderViewAsync("PDFViewSimplified", model);
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

        private async Task CreateInvoiceCopy(string formNumber, long invoiceId)
        {
            try
            {
                _logger.LogInformation($"Starting create invoice final documents for form number [{formNumber}]");
                var finalDocument = new Sentry.Domain.PaperSave.Entities.FinalDocument()
                {
                    SystemId = invoiceId.ToString()
                };
                await _domainService.PaperSaveOperations.CreateFinalDocument(finalDocument, formNumber, $"{formNumber}-invoice.pdf");
                _logger.LogInformation($"Created invoice copy form number [{formNumber}]");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice copy for form number [{formNumber}]", formNumber);
            }
        }

        [HttpPost("{invoiceId}/documents/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> AssociateFinalDocuments(long invoiceId, long id)
        {
            if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
            {
                return Unauthorized();
            }
            try
            {
                var model = await SetupViewModel(id);
                //Create PDF copy of the form
                //We dont need to create the PDF because we already had one
                //_logger.LogInformation($"Creating PDF copy of invoice for form [{id}]");
                //var pdf = await CreatePDF(model);
                //_logger.LogInformation($"Uploading PDF copy for form [{id}]");
                //_domainService.PaperSaveOperations.UploadDocument(pdf);
                //_logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{id}]");

                //Create final documents
                _logger.LogInformation($"Creating final documents for form [{id}]");
                await CreateFinalDocuments(invoiceId, model.FormNumber);
                return Ok("documents created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInvoiceAndDocuments([FromBody] InvoiceCreate form)
        {
            if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
            {
                return Unauthorized();
            }

            _logger.LogInformation($"Starting processing form [{form.DisbursementId}]");
            var formDetails = JsonConvert.SerializeObject(form);

            try
            {
                _logger.LogInformation($"Starting create invoice and documents [{formDetails}]");

                var model = await SetupViewModel(form.DisbursementId);

                var createInvoice = new Sentry.Domain.AccountsPayable.Entities.Invoice.CreateInvoice()
                {
                    DisbursementId = form.DisbursementId,
                    PostDate = form.PostDate
                };

                //Create PDF copy of the form
                _logger.LogInformation($"Creating PDF copy of invoice for form [{formDetails}]");
                var pdf = await CreatePDF(model);

                //CREATE INVOICE
                _logger.LogInformation($"Creating invoice for form [{formDetails}]");
                var invoiceId = await _domainService.AccountsPayableOperations.CreateInvoice(createInvoice);

                //Upload PDF copy of the form
                _logger.LogInformation($"Uploading PDF copy for form [{formDetails}]");
                await _domainService.PaperSaveOperations.UploadDocument(pdf);
                _logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{formDetails}]");

                //Create final documents
                _logger.LogInformation($"Creating final documents for form [{formDetails}]");
                await CreateFinalDocuments(invoiceId, model.FormNumber);

                return Ok("Invoice and documents created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error processing form [{formDetails}]");
                return BadRequest($"Error processing form [{formDetails}]");
            }
        }

        [HttpPost("invoicecopy")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateInvoiceCopy(InvoiceFormCreate form)
        {
            if (HttpContext.Request.Headers["X-API-KEY"] != _config.XAPIKey)
            {
                return Unauthorized();
            }

            _logger.LogInformation($"Starting processing form [{form.DisbursementId}]");
            var formDetails = JsonConvert.SerializeObject(form);

            //Create the PDF
            var model = await SetupViewModel(form.DisbursementId);
            //Create PDF copy of the form
            _logger.LogInformation($"Creating PDF copy of invoice for form [{formDetails}]");
            var pdf = await CreatePDF(model);
            
            //Create the temp document
            _logger.LogInformation($"Uploading PDF copy for form [{formDetails}]");
            await _domainService.PaperSaveOperations.UploadDocument(pdf);
            
            _logger.LogInformation($"Uploaded file [{pdf.FileName}] for form [{formDetails}]");
            _logger.LogInformation($"Creating final documents for form [{formDetails}]");
            await CreateInvoiceCopy(model.FormNumber, form.InvoiceId);


            return Ok("Form invoice copy created");
        }
        
        private async Task CreateInvoice(long id, DateTime postDate)
        {
            try
            {
                var createInvoice = new Sentry.Domain.AccountsPayable.Entities.Invoice.CreateInvoice()
                {
                    DisbursementId = id,
                    PostDate = postDate
                };

                await _domainService.AccountsPayableOperations.QueueFormForProcessing(createInvoice);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating invoice for disbursement [{id}]", id);
                throw;
            }
        }

        //Used by the Sentry front end. Needs to be secured
        [HttpPost("BulkProcess")]
        public async Task<IActionResult> BulkProcess(BulkProcess bulkProcess)
        {
            IList<long> errored = new List<long>();

            _logger.LogInformation($"Starting bulk processing");

            foreach (var id in bulkProcess.DisbursementIds)
            {
                try
                {
                    await CreateInvoice(Convert.ToInt64(id), bulkProcess.PostDate);
                    _logger.LogInformation($"Queued invoice for ID [{id}]");
                }
                catch (Exception ex)
                {
                    errored.Add(Convert.ToInt64(id));
                    _logger.LogError(ex, "Error queueing disbursement id [{id}]", id);
                }
            }

            if (errored.Any())
            {
                return BadRequest(errored);
            }

            return Ok();
        }
    }
}
