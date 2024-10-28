using Sentry.WebApp.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization.Handlers
{
	public class AdministratorsHandler : AuthorizationHandler<AdministratorsGroupRequirement>
	{
        private IConfiguration _config;

        public AdministratorsHandler(IConfiguration config)
        {
            _config = config;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       AdministratorsGroupRequirement requiredGroup)
		{
            if (context.User.Claims.Where(c => c.Value == _config["AzureAd:Sentry_Admins_GroupId"]).FirstOrDefault() != null)
            {
                context.Succeed(requiredGroup);
            }
            return Task.CompletedTask;
        }

    }
}
