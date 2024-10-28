using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class HumanResourcesAuthorizationPolicy
    {
        public static string Name => "HumanResources";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.HumanResourcesGroupRequirement());
    }
}
