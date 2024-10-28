using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class DataManagementAuthorizationPolicy
    {
        public static string Name => "DataManagement";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.DataManagementGroupRequirement());
    }
}