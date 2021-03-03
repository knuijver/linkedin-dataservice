using LinkedInApiClient;
using LinkedInApiClient.Store;
using LinkedInApiClient.Types;
using LinkedInWorkerService.Models;
using Microsoft.Extensions.Logging;
using Polly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInWorkerService.Common
{
    internal class AccessTokenStore : IAccessTokenRegistry
    {
        private readonly ILogger<AccessTokenStore> logger;

        private readonly IHttpClientFactory clientFactory;


        public AccessTokenStore(ILogger<AccessTokenStore> logger, IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
            this.logger = logger;
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(Program.BaseAddress)
            };
        }

        private async Task<string> RefreshTokens(HttpClient client)
        {
            string clientId = string.Empty;
            string secret = string.Empty;
            string refreshToken = string.Empty;

            var message = new HttpRequestMessage(HttpMethod.Post, LinkedInConstants.DefaultTokenEndpoint)
            {
                Content = ContentHelpers.FormData(new Dictionary<string, string?>
                {
                    ["grant_type"] = "refresh_token",
                    ["client_id"] = clientId,
                    ["client_secret"] = secret,
                    ["refresh_token"] = refreshToken
                }),
                Version = new Version(2, 0),
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            var response = await client.SendAsync(message);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        public async Task<Result<LinkedInError, string>> AccessTokenAsync(string tokenId, CancellationToken cancellationToken)
        {
            /*
            var policy = Policy
                .HandleResult<HttpResponseMessage>(r => r.StatusCode == HttpStatusCode.Unauthorized)
                .RetryAsync(1, async (response, count, context) =>
                {
                    logger.LogInformation("Re-Authenticating");
                    var token = await RefreshTokens(GetHttpClient());

                    context["AccessToken"] = token;
                });

            var response = await policy.ExecuteAsync(
                (context, cancellationToken) => FetchTokenFromStoreApi(tokenId, context, cancellationToken),
                new Context(),
                cancellationToken
                );
            */

            var client = clientFactory.CreateClient(nameof(AccessTokenStore));

            var request = new HttpRequestMessage(HttpMethod.Get, $"api/fans/{tokenId}");
            var response = await client.SendAsync(request, cancellationToken);

            var content = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var entry = JsonSerializer.Deserialize<StoreTokenEntry>(content);
                return Result.Success(entry.AccessToken);
            }
            else
            {
                return Result.Fail(LinkedInError.FromTokenResponse(
                    $"Failed to fetch an access token for {tokenId}, store responded with status {response.StatusCode} and {response.ReasonPhrase}"
                    ));
            }
        }

        //private async Task<HttpResponseMessage> FetchTokenFromStoreApi(HttpClient client, string tokenId, CancellationToken cancellationToken)
        //{
        //    var request = new HttpRequestMessage(HttpMethod.Get, $"api/fans/{tokenId}");
        //    var response = await client.SendAsync(request, cancellationToken);
        //    var content = await response.Content.ReadAsStringAsync();
        //}

        public Task<Result<LinkedInError, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken, CancellationToken cancellationToken)
        {
            return Task.FromResult<Result<LinkedInError, string>>(Result.Fail(LinkedInError.FromTokenResponse("")));
        }

        public async Task<Result<LinkedInError, ValueTuple>> RefreshTokenAsync(string tokenId, CancellationToken cancellationToken)
        {
            //var FetchTokenFromStoreApi(tokenId, new Context())
            await Task.CompletedTask;

            return Result.Fail(LinkedInError.FromTokenResponse(""));
        }

        public async Task<Result<LinkedInError, IStoredToken[]>> ListAsync(CancellationToken cancellationToken)
        {
            var client = clientFactory.CreateClient(nameof(AccessTokenStore));

            var request = new HttpRequestMessage(HttpMethod.Get, "api/tokens");
            var response = await client.SendAsync(request, cancellationToken).ConfigureAwait(false);

            var content = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
            if (response.IsSuccessStatusCode)
            {
                IStoredToken[] entry = JsonSerializer.Deserialize<StoreTokenEntry[]>(content);
                return Result.Success(entry);
            }
            else
            {
                return Result.Fail(LinkedInError.FromTokenResponse(
                    $"Failed to fetch an access tokens, store responded with status {response.StatusCode} and {response.ReasonPhrase}"));
            }
        }
    }
}
