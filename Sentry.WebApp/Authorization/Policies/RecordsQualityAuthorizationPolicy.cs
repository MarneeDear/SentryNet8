using Microsoft.AspNetCore.Authorization;

namespace Sentry.WebApp.Authorization.Policies
{
    public class RecordsQualityAuthorizationPolicy
    {
        public static string Name => "RecordsQuality";

        public static void Build(AuthorizationPolicyBuilder builder) =>
            builder.Requirements.Add(new Requirements.RecordsQualityGroupRequirement());
    }
}
