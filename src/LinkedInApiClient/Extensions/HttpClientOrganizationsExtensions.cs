using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Organizations;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientOrganizationsExtensions
    {
        public static Task<LinkedInResponse> FindOrganizationByEmailDomainRequestAsync(
            this HttpMessageInvoker client,
            FindOrganizationByEmailDomainRequest request,
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

        public static Task<LinkedInResponse> FindOrganizationByVanityNameAsync(
            this HttpMessageInvoker client,
            FindOrganizationByVanityNameRequest request,
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

        public static Task<Result<LinkedInError, Paged<OrganizationShare>>> OrganizationSharesAsync(
            this HttpMessageInvoker client,
            OrganizationSharesRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(accessToken)) throw new ArgumentNullException(nameof(accessToken));
            return client.GetAsync<Paged<OrganizationShare>>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<LinkedInResponse> RetrieveAnAdministeredOrganizationAsync(
            this HttpMessageInvoker client,
            RetrieveAnAdministeredOrganizationRequest request,
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

        public static Task<LinkedInResponse> RetrieveLifetimeFollowerStatisticsAsync(
            this HttpMessageInvoker client,
            RetrieveLifetimeFollowerStatisticsRequest request,
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

        public static Task<LinkedInResponse> RetrieveLifetimeOrganizationPageStatisticsAsync(
            this HttpMessageInvoker client,
            RetrieveLifetimeOrganizationPageStatisticsRequest request,
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

        public static Task<LinkedInResponse> RetrieveOrganizationFollowerCountAsync(
            this HttpMessageInvoker client,
            RetrieveOrganizationFollowerCountRequest request,
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
