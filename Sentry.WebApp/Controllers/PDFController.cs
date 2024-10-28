using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sentry.WebApp.Services;
using System.Threading.Tasks;

namespace Sentry.WebApp.Controllers
{
    public class PDFController : Controller
    {
        private readonly ILogger<PDFController> _logger;
        private readonly Config _config;
        private readonly IWebHostEnvironment _env;
        private readonly IDomainService _domainService;
        private readonly IPdfService _pdfService;

        public PDFController(ILogger<PDFController> logger,
            IOptions<Config> config,
            IWebHostEnvironment environment,
            IDomainService domainService,
            IPdfService pdfService)
        {
            _logger = logger;
            _config = config.Value;
            _env = environment;
            _domainService = domainService;
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(Guid giftTransmittalId)
        {
            var md = _domainService.MasterDataWebService;
            var giftTransmittal = await _domainService.LynxWebService.LoadGiftTransmittal(giftTransmittalId);
            var pdf = _pdfService.CreateGiftTransmittalPDF(giftTransmittal, "uaf");

            //throw new NotImplementedException();
            return Ok("CREATED");
        }

    }
}
