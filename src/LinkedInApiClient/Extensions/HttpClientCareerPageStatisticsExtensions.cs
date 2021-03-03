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
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
