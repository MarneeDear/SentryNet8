using Sentry.WebApp.Data;
using Sentry.WebApp.Data.Models.PowerBi;
using Sentry.WebApp.Services;
using Sentry.WebApp.Services.PowerBi;
using Sentry.WebApp.ViewModels;
using Sentry.WebApp.ViewModels.Reports;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{
    public class ReportsController : IntegrationController
    {
        private readonly Config _config;
        private readonly PbiEmbedService _pbiEmbedService;
        private readonly IOptions<AzureAd> _azureAd;
        private readonly IOptions<PowerBI> _powerBI;


        public ReportsController(AppDbContext context,
            DwDbContext dwContext,
            ILogger<FinancialController> logger,
            IConfiguration configuration,
            IDomainService domainService,
            PbiEmbedService pbiEmbedService, IOptions<AzureAd> azureAd, IOptions<PowerBI> powerBI,
            IOptions<Config> config) : base(context, dwContext, logger, configuration, domainService) 
        {
            _config = config.Value;
            _pbiEmbedService = pbiEmbedService;
            _azureAd = azureAd;
            _powerBI = powerBI;

        }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        public async Task<IActionResult> Index(int reportType)
        {
            var userDetails = await _domainService.CAMOperations.GetUserDetails(HttpContext.User.Identity.Name);
            var reportConfig = _config.EnhancedReporting.FirstOrDefault(r => r.ReportGuideId == reportType); 
            var model = new ReportViewModel()
            {
                ReportName = reportConfig.ReportName, 
                //ReportGuide = reportConfig.ReportGuide, 
                ReportDisplayName = reportConfig.ReportDisplayName,
                ReportType = reportConfig.ReportGuideId, 
                Width = $"{reportConfig.Width}px",
                Height = $"{reportConfig.Height}px",
                TableauId = userDetails.SecureId, 
                Environment = _configuration["Environment"],
                User = User.Identity.Name,
                ActiveClass = "FinancialDashboard",
                Title = "Reports",
                PageId = "FinancialDashboardPage",
                NavigationGroups = GetNavigationGroups(),
            };
                            
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> ViewReportGuide(int reportType)
        {
            var docId = await _domainService.PaperSaveOperations.GetDocumentIdByReportType(reportType);
            var document = await _domainService.PaperSaveOperations.GetDocumentById(docId);
            return File(document.Contents, document.MimeType);
        }

        [HttpGet]
        public async Task<IActionResult> PowerBi(Guid reportId)
        {
            if (reportId == Guid.Empty)
            {
                return View("Error");
            }

            var reportConfig = _config.PowerBiReports.FirstOrDefault(r => r.ReportId == reportId);
            var secureId = await Helper.TableauId();

            var model = new PowerBiReportViewModel()
            {                
                User = User.Identity.Name,
                ActiveClass = "FinancialDashboard",
                Title = "Reports",
                PageId = "FinancialDashboardPage",
                NavigationGroups = GetNavigationGroups(),
                ReportId = reportId,
                ReportDisplayName = reportConfig.ReportDisplayName,
                FilterTable = reportConfig.Filter != null ? reportConfig.Filter.Table : String.Empty,
                FilterColumn = reportConfig.Filter != null ? reportConfig.Filter.Column : String.Empty,
                FilterValue = secureId
            };
            return View("PowerBi/Index", model);
        }

        /// <summary>
        /// Returns Embed token, Embed URL, and Embed token expiry to the client
        /// </summary>
        /// <returns>JSON containing parameters for embedding</returns>
        [HttpGet]
        public string GetEmbedInfo(Guid reportId)
        {
            try
            {
                // Validate whether all the required configurations are provided in appsettings.json
                string configValidationResult = ConfigValidatorService.ValidateConfig(_azureAd, _powerBI); //TODO why are these null??
                if (configValidationResult != null)
                {
                    HttpContext.Response.StatusCode = 400;
                    return configValidationResult;
                }
                EmbedParams embedParams = _pbiEmbedService.GetEmbedParams(new Guid(_powerBI.Value.WorkspaceId), reportId); //new Guid(_powerBI.Value.ReportId));

                return JsonSerializer.Serialize<EmbedParams>(embedParams);
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = 500;
                return ex.Message + "\n\n" + ex.StackTrace;
            }
        }
    }
}
