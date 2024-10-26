using Microsoft.AspNetCore.Authentication;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization
{
    public class FilterGroupClaimsTransformation : IClaimsTransformation
    {
        private string[] _groupObjectIds;

        public FilterGroupClaimsTransformation(params string[] groupObjectIds)
        {
            _groupObjectIds = groupObjectIds;
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var id = ((ClaimsIdentity)principal.Identity);

            var ci = new ClaimsIdentity(id.Claims, id.AuthenticationType, id.NameClaimType, id.RoleClaimType);

            var unused = ci.FindAll(GroupsToRemove).ToList();

            unused.ForEach(c => ci.TryRemoveClaim(c));

            var cp = new ClaimsPrincipal(ci);

            return Task.FromResult(cp);

        }

        private bool GroupsToRemove(Claim claim)
        {
            return claim.Type == "groups" &&
                   !_groupObjectIds.Contains(claim.Value);
        }
    }
}
