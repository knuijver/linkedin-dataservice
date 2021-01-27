using LinkedInApiClient.UseCases;
using LinkedInApiClient.UseCases.EmailAddress;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading;
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
                TokenId = identity.Name,
                AccessToken = tokens.AccessToken,
                ExpiresIn = tokens.ExpiresIn,
                RefreshToken = tokens.RefreshToken,
                TokenType = tokens.TokenType
            };

            var saveToken = JsonSerializer.Serialize(args);

            var handler = new LinkedInHttpClient();

            var email = await handler.GetAsync(tokens.AccessToken, new GetEmail(null), CancellationToken.None);
            var profile = await handler.GetAsync(tokens.AccessToken, new GetProfile(null), CancellationToken.None);

            var context = new OAuthCreatingTicketContext(
                new ClaimsPrincipal(identity),
                properties,
                Context,
                Scheme,
                Options,
                Backchannel,
                tokens,
                profile.Data
                );

            context.RunClaimActions();
            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);

            /*
            using (var payload = JsonDocument.Parse(profile.Data.GetRawText()))
            {   //   var payload = JObject.Parse(profile.Data);
                var context = new OAuthCreatingTicketContext(
                    new ClaimsPrincipal(identity),
                    properties,
                    Context,
                    Scheme,
                    Options,
                    Backchannel,
                    tokens,
                    payload.RootElement
                    );

                context.RunClaimActions();
                await Events.CreatingTicket(context);
                return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
            }
            */
        }
    }
}
