using IdentityModel.Client;
using LinkedInWorkerService.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInWorkerService.Services
{
    internal class TokenService : ITokenService
    {
        public const string HttpClientName = nameof(TokenService);

        private readonly ILogger<TokenService> logger;
        private readonly IOptions<TokenStoreSettings> settings;
        private readonly DiscoveryDocumentResponse discoveryDocument;

        public TokenService(ILogger<TokenService> logger, IOptions<TokenStoreSettings> settings)
        {
            this.logger = logger;
            this.settings = settings;

            using var client = new HttpClient();
            this.discoveryDocument = client.GetDiscoveryDocumentAsync(settings.Value.DiscoveryUrl).Result;
            if (discoveryDocument.IsError)
            {
                logger.LogError($"Unable to get discovery document. Error is:{discoveryDocument.Error}");
                throw new Exception("Unable to get discovery document", discoveryDocument.Exception);
            }
        }

        public async Task<TokenResponse> GetTokenAsync(string scope, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient();
            var currentSettings = settings.Value;
            var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = currentSettings.ClientName,
                ClientSecret = currentSettings.ClientPassword,
                Scope = scope
            }, cancellationToken);

            if (tokenResponse.IsError)
            {
                logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token.", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
