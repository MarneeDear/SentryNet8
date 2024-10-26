using Sentry.WebApp.Data;
using Sentry.WebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
//using System.Linq;
//using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace Sentry.WebApp.Controllers
{
    public class SentryController : Controller
    {
        protected const int DEFAULT_PAGE_SIZE = 25;

        protected readonly AppDbContext _context;
        protected readonly DwDbContext _dwContext;
        protected readonly ILogger _logger;
        protected readonly IConfiguration _configuration;

        public SentryController(AppDbContext context, DwDbContext dwContext, ILogger<SentryController> logger, IConfiguration configuration) : base()
        {
            _context = context;
            _dwContext = dwContext;
            _logger = logger;
            _configuration = configuration;
        }

        public List<NavigationGroup> GetNavigationGroups()
        {
            return _configuration.GetSection("NavigationGroup").Get<List<NavigationGroup>>();
            //List <NavigationGroup> navigationGroups = _configuration.GetSection("NavigationGroup").Get<List<NavigationGroup>>();

            //foreach (NavigationGroup group in navigationGroups)
            //{
            //    foreach (var item in group.NavigationItem)
            //    {
            //        item.Accessible = Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Sentry_Admins_GroupId"]).FirstOrDefault() != null ? true : false;
            //        switch (group.Name)
            //        {
            //            case "Organization":
            //                switch (item.Name)
            //                {
            //                    case "OfficeLocation":
            //                    case "OrganizationalUnit":
            //                        if (Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Data_Steward_HR_GroupId"]).FirstOrDefault() != null)
            //                        {
            //                            item.Accessible = true;
            //                        }
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Employee":
            //                switch (item.Name)
            //                {
            //                    case "Employee":
            //                        if (Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Data_Steward_HR_GroupId"]).FirstOrDefault() != null)
            //                        {
            //                            item.Accessible = true;
            //                        }
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Finance":
            //                switch (item.Name)
            //                {
            //                    case "Designation":
            //                        if (Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Data_Steward_Financial_Services_GroupId"]).FirstOrDefault() != null)
            //                        {
            //                            item.Accessible = true;
            //                        }
            //                        break;
            //                    case "PostToGl":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Student":
            //                switch (item.Name)
            //                {
            //                    case "BioDem":
            //                    case "Enrollment":
            //                    case "Degree":
            //                    case "AcademicInvolvement":
            //                    case "Scholarship":
            //                    case "Contact":
            //                        if (Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Data_Steward_Records_Quality_GroupId"]).FirstOrDefault() != null)
            //                        {
            //                            item.Accessible = true;
            //                        }
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Constituent":
            //                switch (item.Name)
            //                {
            //                    case "Individual":
            //                        if (Request.HttpContext.User.Claims.Where(c => c.Value == _configuration["AzureAd:Data_Steward_Records_Quality_GroupId"]).FirstOrDefault() != null)
            //                        {
            //                            item.Accessible = true;
            //                        }
            //                        break;
            //                    case "Group":
            //                    case "Organization":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Parent":
            //                switch (item.Name)
            //                {
            //                    case "Employee":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "UACares":
            //                switch (item.Name)
            //                {
            //                    case "UACares":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "CampusCall":
            //                switch (item.Name)
            //                {
            //                    case "CampusCall":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Administration":
            //                switch (item.Name)
            //                {
            //                    case "Monitor":
            //                    case "QueueProcessor":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            case "Utilities":
            //                switch (item.Name)
            //                {
            //                    case "Utilities":
            //                        break;
            //                    default:
            //                        break;
            //                }
            //                break;
            //            default:
            //                break;
            //        }
            //    }
            //}
            //return navigationGroups;
        }

        protected void LogException (Exception exception)
        {
            // TODO: Log exception
        }
    }
}
