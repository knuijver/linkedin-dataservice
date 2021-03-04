using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Messages;
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

        public static Task<LinkedInResponse> FindUGCPostsByAuthorsAsync(
            this HttpMessageInvoker client,
            FindUGCPostsByAuthorsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            return InternalGet(client, request, accessToken, cancellationToken);
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
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request
                .WithAccessToken(accessToken)
                .Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
