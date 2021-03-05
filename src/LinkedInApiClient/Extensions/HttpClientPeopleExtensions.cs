using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.People;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientPeopleExtensions
    {
        public static Task<LinkedInResponse> GetEmailAsync(
            this HttpMessageInvoker client,
            GetEmailRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));

            request.Method = HttpMethod.Get;
            request
                .WithAccessToken(accessToken)
                .Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> GetMyProfileAsync(
            this HttpMessageInvoker client,
            GetMyProfileRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
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
