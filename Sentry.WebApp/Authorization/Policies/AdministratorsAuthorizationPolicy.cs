using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class AdministratorsAuthorizationPolicy
    {
        public static string Name => "Administrators";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.AdministratorsGroupRequirement());
    }
}