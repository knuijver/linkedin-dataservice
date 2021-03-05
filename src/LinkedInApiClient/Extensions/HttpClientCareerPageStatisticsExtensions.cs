using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.CareerPageStatistics;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientCareerPageStatisticsExtensions
    {
        public static Task<LinkedInResponse> RetrieveOrganizationBrandPageStatisticsAsync(
            this HttpMessageInvoker client,
            RetrieveOrganizationBrandPageStatisticsRequest request,
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
