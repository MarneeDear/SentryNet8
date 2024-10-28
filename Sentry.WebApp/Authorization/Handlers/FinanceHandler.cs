using Sentry.WebApp.Authorization.Requirements;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization.Handlers
{

    public class FinanceHandler : AuthorizationHandler<FinancesGroupRequirement>
    {
        private IConfiguration _config;

        public FinanceHandler(IConfiguration config) : base()
        {
            _config = config;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       FinancesGroupRequirement requiredGroup)
        {
            if (context.User.Claims.Where(c => c.Value == _config["AzureAd:Data_Steward_Financial_Services_GroupId"]).FirstOrDefault() != null ||
                context.User.Claims.Where(c => c.Value == _config["AzureAd:Sentry_Admins_GroupId"]).FirstOrDefault() != null)
            {
                context.Succeed(requiredGroup);
            }
            return Task.CompletedTask;
        }
    }

}
