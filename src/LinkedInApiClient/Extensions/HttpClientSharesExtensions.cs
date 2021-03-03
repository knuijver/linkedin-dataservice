using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Shares;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientSharesExtensions
    {
        public static Task<LinkedInResponse> LookUpShareByIdAsync(
            this HttpMessageInvoker client,
            LookUpShareByIdRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
