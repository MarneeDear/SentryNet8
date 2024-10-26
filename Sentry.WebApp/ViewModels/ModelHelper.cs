using Sentry.WebApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sentry.Domain.AccountsPayable.Entities;
using Sentry.Domain.Security;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Sentry.WebApp.ViewModels
{
    public class ModelHelper
    {
        private readonly HttpContext _context;
        private readonly IConfiguration _config;
        private const string DEFAULT_PLACEHOLDER = "-- Choose {0} --";
        public const string SESSION_ROLE_KEY = "__FinancialRole__";
        public const string ADMIN_SESSION_ROLE_KEY = "__AdminRole__";
        public const string ADMIN_ROLE = "Admin";
        public const string DEFAULT_PAGE_KEY = "__DefaultPage__";


        public ModelHelper(HttpContext context)
        {
            _context = context;
            _config = _context.RequestServices.GetRequiredService<IConfiguration>();
        }

        private IDomainService _domainService;

        public IDomainService DomainService => _domainService ?? (_domainService = _context.RequestServices.GetRequiredService<IDomainService>());
        public string Version => _config["Version"];
        public string Environment => _config["Environment"];
        //public string TableauId => _context.Session.GetString(_config["Tableau:TableauId"]);

        public string AdminImpersonationRole
        {
            get
            {
                return _context.Session.GetString(ADMIN_SESSION_ROLE_KEY);
            }
        }

        public string CurrentFinancialRole
        {
            get
            {
                return !String.IsNullOrEmpty(AlternateFinancialRole) ? AlternateFinancialRole : FinancialRole;
            }
        }

        public string AlternateFinancialRole
        {
            get
            {
                return _context.Session.GetString(SESSION_ROLE_KEY);
            }
        }

        public bool IsAdmin
        {
            get
            {
                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                return groups.Contains(_config["AzureAd:Sentry_Admins_GroupId"]);
            }
        }

        public bool IsRecordsRole
        {
            get
            {
                return String.IsNullOrEmpty(FinancialRole);
            }
        }

        public bool IsFinancialRole
        {
            get
            {
                return !String.IsNullOrEmpty(FinancialRole) || IsFTRole;
            }
        }

        public bool IsAPRole
        {
            get
            {
                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                
                var apRoles = APRoles;

                if (apRoles.Values.Contains(AdminImpersonationRole))
                {
                    return true;
                }

                foreach (var group in groups)
                {
                    if (apRoles.Keys.Contains(group))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsFTRole
        {
            get
            {
                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);

                var ftRoles = FTGroupsMapping;

                if (ftRoles.Values.Contains(AdminImpersonationRole))
                {
                    return true;
                }

                foreach (var group in groups)
                {
                    if (ftRoles.Keys.Contains(group))
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsFTReviewerRole
        {
            get
            {
                if (AdminImpersonationRole == Constants.FTReviewerRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:FT_Reviewers_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsFTApproverRole
        {
            get
            {
                if (AdminImpersonationRole == Constants.FTApproverRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:FT_Approvers_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsFTGeneralCounselApproverRole
        {
            get
            {
                if (AdminImpersonationRole == Constants.FTGeneralCounselApproverRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:FT_GeneralCounselApprover_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsAPReviewerRole
        {
            get
            {
                if (AdminImpersonationRole == Constants.APReviewerRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:AP_Reviewers_GroupId"]))
                {
                    return true;
                }                

                return false;
            }
        }

        public bool IsAPManagerRole
        {
            get
            {
                if (AdminImpersonationRole == Constants.APManagerRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:AP_Managers_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsARRole
        {
            get
            {
                return IsARReviewer || IsARSecondaryReviewer || IsARGUReviewer;                
            }
        }

        public bool IsGTRole
        {
            get
            {
                return IsARReviewer || IsARSecondaryReviewer;
            }
        }

        public bool IsGURole
        {
            get
            {
                return IsARGUReviewer || IsARSecondaryReviewer;
            }
        }

        public bool IsARReviewer
        {
            get
            {
                if (AdminImpersonationRole == Constants.ARStaffRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:AR_Staff_GroupId"]))
                {
                    return true;
                }

                return  false;
            }
        }

        public bool IsARSecondaryReviewer
        {
            get
            {
                if (AdminImpersonationRole == Constants.ARSupervisorReviewerRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:AR_SupervisorReviewers_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public bool IsARGUReviewer
        {
            get
            {
                if (AdminImpersonationRole == Constants.ARGUStaffRole)
                {
                    return true;
                }

                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);
                if (groups.Contains(_config["AzureAd:AR_GUStaff_GroupId"]))
                {
                    return true;
                }

                return false;
            }
        }

        public Dictionary<string, string> APRoles
        {
            get
            {
                var groupMapping = new Dictionary<string, string>()
                {
                    { _config["AzureAd:AP_Reviewers_GroupId"], Constants.APReviewerRole},
                    { _config["AzureAd:AP_General_Counsel_GroupId"], Constants.GeneralCounselRole},
                    { _config["AzureAd:AP_Assistant_VP_GroupId"], Constants.AssociateVPRole},
                    { _config["AzureAd:AP_VP_GroupId"], Constants.VPRole},
                    { _config["AzureAd:AP_CFO_GroupId"], Constants.CFORole},
                    { _config["AzureAd:AP_Managers_GroupId"], Constants.APManagerRole},
                    { _config["AzureAd:AP_Scholarship_Managers_GroupId"], Constants.ScholarshipRole},
                };
                return groupMapping;
            }
        }

        public Dictionary<string, string> FTGroupsMapping
        {
            get
            {
                var groupMapping = new Dictionary<string, string>()
                {
                    { _config["AzureAd:FT_Reviewers_GroupId"], Constants.FTReviewerRole},
                    { _config["AzureAd:FT_Approvers_GroupId"], Constants.FTApproverRole},
                    { _config["AzureAd:FT_GeneralCounselApprover_GroupId"], Constants.FTGeneralCounselApproverRole},
                };
                return groupMapping;
            }
        }

        public IDictionary<string, string> APARGroupMapping
        {
            get
            {
                var groupMapping = new Dictionary<string, string>()
                {
                    { _config["AzureAd:Sentry_Admins_GroupId"], ADMIN_ROLE},
                    //{ _config["AzureAd:Data_Steward_Financial_Services_GroupId"], Constants.FinancialServicesStewardRole },
                    { _config["AzureAd:AP_Reviewers_GroupId"], Constants.APReviewerRole},
                    { _config["AzureAd:AP_General_Counsel_GroupId"], Constants.GeneralCounselRole},
                    { _config["AzureAd:AP_Assistant_VP_GroupId"], Constants.AssociateVPRole},
                    { _config["AzureAd:AP_VP_GroupId"], Constants.VPRole},
                    { _config["AzureAd:AP_CFO_GroupId"], Constants.CFORole},
                    { _config["AzureAd:AP_Managers_GroupId"], Constants.APManagerRole},
                    { _config["AzureAd:AP_Scholarship_Managers_GroupId"], Constants.ScholarshipRole},
                    //{ _config["AzureAd:AR_Gift_Processing_GroupId"], Constants.GiftProcessingRole},
                    { _config["AzureAd:AR_Staff_GroupId"], Constants.ARStaffRole},
                    { _config["AzureAd:AR_SupervisorReviewers_GroupId"], Constants.ARSupervisorReviewerRole},
                    { _config["AzureAd:AR_GUStaff_GroupId"], Constants.ARGUStaffRole }
                };
                return groupMapping;
            }
        }

        public IEnumerable<string> UserFTRoles
        {
            get
            {                                
                var groupClaims = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);

                if (groupClaims.Contains(_config["AzureAd:Sentry_Admins_GroupId"]))
                {
                    return new List<string>() { AdminImpersonationRole ?? ADMIN_ROLE };
                }

                IList<string> ftroles = new List<string>();

                foreach (var groupId in groupClaims)
                {
                    var mapping = FTGroupsMapping;
                    if (mapping.ContainsKey(groupId))
                    {
                        ftroles.Add(mapping[groupId]);
                    }
                }

                return ftroles;
            }
        }

        public string FinancialRole
        {
            //Can only be in one financial role
            get
            {                              
                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);                

                if (groups.Contains(_config["AzureAd:Sentry_Admins_GroupId"]))
                {
                    return AdminImpersonationRole ?? ADMIN_ROLE;
                }

                foreach (var groupId in groups)
                {
                    var mapping = APARGroupMapping;
                    if (mapping.ContainsKey(groupId))
                    {
                        return mapping[groupId];
                    }
                }

                return String.Empty;
            }

        }

        public string FTRole
        {
            //This returns the first one it finds. Maybe we need a 4th role???
            get
            {
                var groups = _context.User.Claims.Where(c => c.Type == "groups").Select(g => g.Value);

                if (groups.Contains(_config["AzureAd:Sentry_Admins_GroupId"]))
                {
                    return AdminImpersonationRole ?? ADMIN_ROLE;
                }

                foreach (var groupId in groups)
                {
                    var mapping = FTGroupsMapping;
                    if (mapping.ContainsKey(groupId))
                    {
                        return mapping[groupId];
                    }
                }

                return String.Empty;
            }

        }

        public string DefaultPage
        {
            get
            {
                if (String.IsNullOrEmpty(_context.Session.GetString(DEFAULT_PAGE_KEY)))
                {
                    var result = DomainService.UsersOperations.GetProfile(_context.User.Identity.Name).Result;
                    _context.Session.SetString(DEFAULT_PAGE_KEY, result.DefaultPage);
                }
                return _context.Session.GetString(DEFAULT_PAGE_KEY);
            }
        }

        public async Task<string> EmployeeId()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.EmployeeID) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.EmployeeID);

        }

        public async Task<string> NetId()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.NetId) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.NetId);

        }

        public async Task<string> FullName()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.FullName) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.FullName);

        }

        public async Task<string> Email()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.Email) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.Email);

        }

        public async Task<string> JobTitle()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.JobTitle) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.JobTitle);

        }

        public async Task<string> DepartmentCode()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.DepartmentCode) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.DepartmentCode);

        }

        public async Task<string> DepartmentName()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.DepartmentName) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.DepartmentName);

        }

        public async Task<string> TableauId()
        {

            if (!_context.Session.Keys.Contains(WebConstants.SessionVariables.TableauId) && _context.User.Identity.IsAuthenticated)
            {
                await LoadUAFDNUser();
            }

            return _context.Session.GetString(WebConstants.SessionVariables.TableauId);

        }

        private async Task LoadUAFDNUser()
        {
            var identity = _context.User.Identity.Name;
            if (!string.IsNullOrWhiteSpace(identity))
            {
                var netId = identity.Split('@')[0];

                var username = string.Empty;

                if (identity.ToString().Contains("uafoundation.org"))
                {
                    username = identity.ToString();
                }
                else
                {
                    username = netId;
                }

                var user = await DomainService.SecurityOperations.GetUserDetails(username); // SecurityOperations.GetUserDetails(username);

                if (user != null)
                {
                    _context.Session.SetString(WebConstants.SessionVariables.NetId, netId);
                    _context.Session.SetString(WebConstants.SessionVariables.EmployeeID, user.EmployeeId);
                    _context.Session.SetString(WebConstants.SessionVariables.TableauId, string.IsNullOrWhiteSpace(user.SecureId) ? string.Empty : user.SecureId);
                    _context.Session.SetString(WebConstants.SessionVariables.UserName, username);
                    _context.Session.SetString(WebConstants.SessionVariables.Phone, user.Phone);
                    _context.Session.SetString(WebConstants.SessionVariables.FullName, $"{user.FirstName} {user.LastName}");
                    _context.Session.SetString(WebConstants.SessionVariables.Email, user.Email);
                    _context.Session.SetString(WebConstants.SessionVariables.JobTitle, user.JobTitle);
                    _context.Session.SetString(WebConstants.SessionVariables.DepartmentCode, user.DepartmentCode);
                    _context.Session.SetString(WebConstants.SessionVariables.DepartmentName, user.DepartmentName);
                }

                //TODO do you have to return user here?
            }
        }

    }
}