using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class SentryUsersAuthorizationPolicy
    {
        public static string Name => "SentryUsers";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.SentryUsersGroupRequirement());
    }
}