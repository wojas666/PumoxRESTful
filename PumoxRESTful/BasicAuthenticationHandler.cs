﻿using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace PumoxRESTful
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock) : base(options, logger, encoder, clock)
        { }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            Response.Headers.Add("WWW-Authenticate", "Basic");

            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization Header Missing"));
            }

            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var authorizationHeaderRegex = new Regex("Basic (.*)");

            if (!authorizationHeaderRegex.IsMatch(authorizationHeader))
            {
                return Task.FromResult(AuthenticateResult.Fail("Authorization code not formatted properly"));
            }

            var authorizationBase64 =
                Encoding.UTF8.GetString(
                    Convert.FromBase64String(authorizationHeaderRegex.Replace(authorizationHeader, "$1")));
            var authorizationSplit = authorizationBase64.Split(Convert.ToChar(":"), 2);
            var authorizationUserName = authorizationSplit[0];
            var authorizationPassword = authorizationSplit.Length > 1
                ? authorizationSplit[1]
                : throw new Exception("Unable to get password.");

            if (authorizationUserName != "pumox" || authorizationPassword != "pumox")
            {
                return Task.FromResult(AuthenticateResult.Fail("The username or password is not corrected!"));
            }

            var authenticatedUser = new AuthenticatedUser("BasicAuthentication", true, "pumox");

            var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(authenticatedUser));

            return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name)));
        }
    }
}
