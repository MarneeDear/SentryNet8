using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sentry.WebApp.Data;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Sentry.WebApp.Controllers
{
    public class AccountController : IntegrationController
    {
        public AccountController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<EmployeeController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        [Route("logout")]
        public void Logout()
        {
            DeleteCookies();
            string uri = string.Format(_configuration["AzureAd:LogoutPath"], _configuration["AzureAd:TenantId"], HttpContext.Request.Host.Value);
            Response.Redirect(uri);
        }

        public IActionResult AccessDenied()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "accountPage",
                PageWrapperClass = "toggled",
                ActiveClass = "active",
                Title = "SENTRY",
                User = User.Identity.Name,
                NavigationGroups = GetNavigationGroups()
            };

            return View(viewModel);
        }

        private void DeleteCookies()
        {
            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }
        }

    }
}