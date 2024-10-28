using Sentry.WebApp.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization.Handlers
{
	public class HumanResourcesHandler : AuthorizationHandler<HumanResourcesGroupRequirement>
	{
        private IConfiguration _config;

        public HumanResourcesHandler(IConfiguration config) : base()
        {
            _config = config;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       HumanResourcesGroupRequirement requiredGroup)
		{
            if (context.User.Claims.Where(c => c.Value == _config["AzureAd:Data_Steward_HR_GroupId"]).FirstOrDefault() != null ||
                context.User.Claims.Where(c => c.Value == _config["AzureAd:Sentry_Admins_GroupId"]).FirstOrDefault() != null)
            {
                    context.Succeed(requiredGroup);
			}
            return Task.CompletedTask;
		}
	}
}
