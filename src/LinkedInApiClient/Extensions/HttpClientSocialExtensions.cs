using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Social;
using LinkedInApiClient.UseCases.Social.Models;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientSocialExtensions
    {
        public static Task<Result<LinkedInError, SummaryOfSocialAction>> RetrieveASummaryOfSocialActionsAsync(
            this HttpMessageInvoker client,
            RetrieveASummaryOfSocialActionsRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<SummaryOfSocialAction>(request, cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<LikesOnShares>>> RetrieveLikesOnSharesAsync(
            this HttpMessageInvoker client,
            RetrieveLikesOnSharesRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<Paged<LikesOnShares>>(request, cancellationToken);
        }
    }
}
