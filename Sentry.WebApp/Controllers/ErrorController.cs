using Microsoft.AspNetCore.Mvc;
using Sentry.WebApp.Data;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;

namespace Sentry.WebApp.Controllers
{
    public class ErrorController : SentryController
    {
        public ErrorController(AppDbContext context, DwDbContext dwContext, ILogger<ErrorController> logger, IConfiguration configuration) : base(context, dwContext, logger, configuration) { }

        public IActionResult SystemError()
        {               
            return View();
        }
    }
}
