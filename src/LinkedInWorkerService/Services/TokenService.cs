using IdentityModel.Client;
using LinkedInWorkerService.Common;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
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
            var policy = Policy
               .HandleResult<DiscoveryDocumentResponse>(r => !r.IsError)
               .WaitAndRetry(3, c => TimeSpan.FromSeconds(2 * c));

            this.discoveryDocument = policy.Execute(() => client.GetDiscoveryDocumentAsync(settings.Value.DiscoveryUrl).Result);
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

            var policy = Policy
               .HandleResult<TokenResponse>(r => !r.IsError)
               .WaitAndRetryAsync(3, c => TimeSpan.FromSeconds(2 * c));


            var tokenResponse = await policy.ExecuteAsync((ct) => client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = currentSettings.ClientName,
                ClientSecret = currentSettings.ClientPassword,
                Scope = scope
            }, ct), cancellationToken);

            if (tokenResponse.IsError)
            {
                logger.LogError($"Unable to get token. Error is: {tokenResponse.Error}");
                throw new Exception("Unable to get token.", tokenResponse.Exception);
            }

            return tokenResponse;
        }
    }
}
