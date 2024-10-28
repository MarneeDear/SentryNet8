using Sentry.WebApp.Authorization.Attributes;
using Sentry.WebApp.Data;
using Sentry.WebApp.Services;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Sentry.WebApp.Controllers
{
    [AuthorizeSentryUsers]
    public class HomeController : IntegrationController
    {
        public HomeController(AppDbContext context, 
            DwDbContext dwContext, 
            ILogger<HomeController> logger, 
            IConfiguration configuration,
            IDomainService domainService) : base(context, dwContext, logger, configuration, domainService) { }

        private ModelHelper Helper
        {
            get
            {
                return new ModelHelper(HttpContext);
            }
        }

        public IActionResult Index()
        {
            //_logger.LogTrace("[Get] Index()");

            //var defaultPage = Helper.DefaultPage;
            //if (!String.IsNullOrWhiteSpace(defaultPage)) //TODO get from UAF SERVICES or the helper/session
            //{
            //    var path = defaultPage.Split("/");
            //    var action = path[1];
            //    var controller = path[0];
            //    return RedirectToAction(action, controller);
            //}

            //Use during troubleshooting membership issues
            //var groups = HttpContext.User.Claims.Where(c => c.Type == "groups"); //.Select(g => g.Value);
            //foreach (var group in groups)
            //{
            //    _logger.LogInformation($"GROUP [{group.Value}]");
            //}

            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "dashboardPage",
                PageWrapperClass = "toggled",
                ActiveClass = "active",
                Title = "SENTRY",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
            };
			
            return View(viewModel);
        }
        
		public IActionResult Employee2()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "employeePage",
                PageWrapperClass = "toggled",
                ActiveClass = "Employee",
                Title = "Employee",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

		public IActionResult Student()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "studentPage",
                PageWrapperClass = "toggled",
                ActiveClass = "Student",
                Title = "Student",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult Parent()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "parentPage",
                PageWrapperClass = "toggled",
                ActiveClass = "Parent",
                Title = "Parent",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult Scholarship()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "scholarshipPage",
                PageWrapperClass = "toggled",
                ActiveClass = "Scholarship",
                Title = "Scholarship",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult UACares()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "uaCaresPage",
                PageWrapperClass = "toggled",
                ActiveClass = "UACares",
                Title = "UA Cares",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult PostToGL()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "postToGLPage",
                PageWrapperClass = "toggled",
                ActiveClass = "PostToGL",
                Title = "Post To GL",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult TOPCampusCall()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "TOPCampusCallPage",
                PageWrapperClass = "toggled",
                ActiveClass = "TOPCampusCall",
                Title = "TOP / Campus Call",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult UI_Utilities()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "uiUtilitiesPage",
                PageWrapperClass = "toggled",
                ActiveClass = "UIUtilities",
                Title = "UI Utilities",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult WAIARIA()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "waiariaPage",
                PageWrapperClass = "toggled",
                ActiveClass = "WaiAria",
                Title = "Accessibility Statement",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }

        public IActionResult Privacy()
        {
            BaseViewModel viewModel = new BaseViewModel()
            {
                PageId = "privacyPage",
                PageWrapperClass = "toggled",
                ActiveClass = "PrivacyPolicy",
                Title = "Privacy Policy",
				User = User.Identity.Name,
				NavigationGroups = GetNavigationGroups(),
			};
            return View(viewModel);
        }
    }
}
