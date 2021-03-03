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
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> GetMyProfileAsync(
            this HttpMessageInvoker client,
            GetMyProfileRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
