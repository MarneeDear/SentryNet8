using Sentry.WebApp.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization.Handlers
{
    public class SentryUsersHandler : AuthorizationHandler<SentryUsersGroupRequirement>
    {
        private IConfiguration _config;

        public SentryUsersHandler(IConfiguration config) : base()
        {
            _config = config;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       SentryUsersGroupRequirement requiredGroup)
        {
            if (context.User.Claims.Where(c => c.Value == _config["AzureAd:UDP_GroupId"]).FirstOrDefault() != null ||
                context.User.Claims.Where(c => c.Value == _config["AzureAd:Sentry_Admins_GroupId"]).FirstOrDefault() != null)
            {
                context.Succeed(requiredGroup);
            }
            return Task.CompletedTask;
        }
    }
}
