using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Social;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientSocialExtensions
    {
        public static Task<Result<LinkedInError, SummaryOfSocialAction>> RetrieveASummaryOfSocialActionsAsync(
            this HttpMessageInvoker client,
            RetrieveASummaryOfSocialActionsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return client.GetAsync<SummaryOfSocialAction>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<LikesOnShares>>> RetrieveLikesOnSharesAsync(
            this HttpMessageInvoker client,
            RetrieveLikesOnSharesRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return client.GetAsync<Paged<LikesOnShares>>(request.WithAccessToken(accessToken), cancellationToken);
        }
    }
}
