using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class FinanceAuthorizationPolicy
    {
        public static string Name => "Finance";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.FinancesGroupRequirement());
    }

    //public class 
}
