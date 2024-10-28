using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace Sentry.WebApp.Security
{
    public class AuthenticationKeyHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IConfiguration _config;
        public IServiceProvider ServiceProvider { get; set; }

        public AuthenticationKeyHandler(IOptionsMonitor<ApiKeyAuthenticationOptions> options, IConfiguration config, ILoggerFactory logger, UrlEncoder encoder, IServiceProvider serviceProvider)
            : base(options, logger, encoder)
        {
            ServiceProvider = serviceProvider;
            _config = config;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (string.IsNullOrEmpty(Request.Headers["X-API-KEY"]))
            {
                return Task.FromResult(AuthenticateResult.Fail("X-API-KEY header is required"));
            }

            string apiKey = Request.Headers["X-API-Key"][0].Trim();

            if (_config["XAPIKey"] != apiKey)
            {
                //throw new Exception("BAD");
                return Task.FromResult(AuthenticateResult.Fail($"No match for X-API-KEY. Provided {apiKey}. Expected {_config["XAPIKey"]}"));
            }

            var claims = new[] { new Claim("ApiKey", apiKey) };
            var identity = new ClaimsIdentity(claims, nameof(AuthenticationKeyHandler));
            var ticket = new AuthenticationTicket(new ClaimsPrincipal(identity), this.Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }

    public class ApiKeyAuthenticationOptions : AuthenticationSchemeOptions
    {
    }

    public static class AuthenticationSchemeNamesConstants
    {
        public const string AuthenticationKeyDefaultScheme = "AuthenticationKeyScheme";
    }
}
