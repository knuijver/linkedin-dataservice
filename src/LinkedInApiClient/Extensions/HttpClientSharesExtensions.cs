using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Shares;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientSharesExtensions
    {
        public static Task<LinkedInResponse> LookUpShareByIdAsync(
            this HttpMessageInvoker client,
            LookUpShareByIdRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            return InternalGet(client, request, accessToken, cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<UGCPost>>> FindUGCPostsByAuthorsAsync(
            this HttpMessageInvoker client,
            FindUGCPostsByAuthorsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return client.GetAsync<Paged<UGCPost>>(request.WithAccessToken(accessToken), cancellationToken);
            //return InternalGet(client, request, accessToken, cancellationToken);
        }

        public static Task<LinkedInResponse> FindUGCPostsByContainerEntitiesAsync(
            this HttpMessageInvoker client,
            FindUGCPostsByContainerEntitiesRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            return InternalGet(client, request, accessToken, cancellationToken);
        }

        public static Task<LinkedInResponse> BatchGetASummaryOfSocialActionsAsync(
            this HttpMessageInvoker client,
            BatchGetASummaryOfSocialActionsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            return InternalGet(client, request, accessToken, cancellationToken);
        }

        private static Task<LinkedInResponse> InternalGet(HttpMessageInvoker client, LinkedInRequest request, string accessToken, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

            request.Method = HttpMethod.Get;
            request
                .WithAccessToken(accessToken)
                .Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
