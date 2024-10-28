using Sentry.WebApp.Authorization.Policies;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sentry.WebApp.Authorization.Attributes
{
    public class AuthorizeHumanResources : AuthorizeAttribute
    {

        public AuthorizeHumanResources() : base(HumanResourcesAuthorizationPolicy.Name)
        {
        }
    }
}
