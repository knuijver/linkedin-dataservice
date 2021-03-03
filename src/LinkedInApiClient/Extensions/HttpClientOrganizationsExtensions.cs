using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Organizations;
using LinkedInApiClient.UseCases.Organizations.Models;
using LinkedInApiClient.UseCases.Shares;
using LinkedInApiClient.UseCases.Social;
using LinkedInApiClient.UseCases.Social.Models;
using LinkedInApiClient.UseCases.Standardized;
using LinkedInApiClient.UseCases.Standardized.Models;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientOrganizationsExtensions
    {
        public static Task<LinkedInResponse> FindOrganizationByEmailDomainRequestAsync(
            this HttpMessageInvoker client,
            FindOrganizationByEmailDomainRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            
            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> FindOrganizationByVanityNameAsync(
            this HttpMessageInvoker client,
            FindOrganizationByVanityNameRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<OrganizationShare>>> OrganizationSharesAsync(
            this HttpMessageInvoker client,
            OrganizationSharesRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<Paged<OrganizationShare>>(request, cancellationToken);
        }

        public static Task<LinkedInResponse> RetrieveAnAdministeredOrganizationAsync(
            this HttpMessageInvoker client,
            RetrieveAnAdministeredOrganizationRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> RetrieveLifetimeFollowerStatisticsAsync(
            this HttpMessageInvoker client,
            RetrieveLifetimeFollowerStatisticsRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> RetrieveLifetimeOrganizationPageStatisticsAsync(
            this HttpMessageInvoker client,
            RetrieveLifetimeOrganizationPageStatisticsRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }

        public static Task<LinkedInResponse> RetrieveOrganizationFollowerCountAsync(
            this HttpMessageInvoker client,
            RetrieveOrganizationFollowerCountRequest request,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));

            request.Method = HttpMethod.Get;
            request.Prepare();

            return client.ExecuteRequest(request, cancellationToken);
        }
    }
}
