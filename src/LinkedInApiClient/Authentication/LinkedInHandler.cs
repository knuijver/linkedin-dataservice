using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace LinkedInApiClient.Authentication
{
    public class LinkedInHandler : OAuthHandler<LinkedInOptions>
    {
        public LinkedInHandler(IOptionsMonitor<LinkedInOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override string BuildChallengeUrl(AuthenticationProperties properties, string redirectUri)
        {
            var queryStrings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                ["response_type"] = "code",
                ["client_id"] = Options.ClientId,
                ["redirect_uri"] = redirectUri,
                ["scope"] = FormatScope()
            };

            var state = Options.StateDataFormat.Protect(properties);
            queryStrings.Add("state", state);

            var authorizationEndpoint = QueryHelpers.AddQueryString(Options.AuthorizationEndpoint, queryStrings);
            return authorizationEndpoint;
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var args = new
            {
                OrganizationId = identity.Name,
                AccessToken = tokens.AccessToken,
                ExpiresIn = tokens.ExpiresIn,
                RefreshToken = tokens.RefreshToken
            };

            var handler = new LinkedInWebApiHandler();

            var profile = handler.Request(new AuthenticatedRequest(tokens.AccessToken, new GetProfile()));
            var email = handler.Request(new AuthenticatedRequest(tokens.AccessToken, new GetEmail()));

            //using (var payload = JsonDocument.Parse(profile.Result.Result()))
            //{
            var payload = JObject.Parse(profile.Result.Result());
            var context = new OAuthCreatingTicketContext(
                new ClaimsPrincipal(identity),
                properties,
                Context,
                Scheme,
                Options,
                Backchannel,
                tokens,
                payload
                );

            context.RunClaimActions();
            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
            //}
        }
    }
}
