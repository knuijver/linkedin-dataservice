using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.UseCases.Models;
using LinkedInApiClient.UseCases.Standardized;
using LinkedInApiClient.UseCases.Standardized.Models;

namespace LinkedInApiClient.Extensions
{
    public static class HttpClientStandardizedExtensions
    {
        public static Task<Result<LinkedInError, Paged<Country>>> GetAllCountriesAsync(
            this HttpMessageInvoker client,
            GetAllCountriesRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<Paged<Country>>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<CountryGroup>>> GetAllCountryGroupsAsync(
            this HttpMessageInvoker client,
            GetAllCountryGroupsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<Paged<CountryGroup>>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<Result<LinkedInError, Paged<JobsFunction>>> GetAllFunctionsAsync(
            this HttpMessageInvoker client,
            GetAllFunctionsRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrWhiteSpace(request.AccessToken)) throw new ArgumentNullException(nameof(request.AccessToken));
            return client.GetAsync<Paged<JobsFunction>>(request.WithAccessToken(accessToken), cancellationToken);
        }

        public static Task<LinkedInResponse> GetAllSenioritiesAsync(
            this HttpMessageInvoker client,
            GetAllSenioritiesRequest request,
            string accessToken,
            CancellationToken cancellationToken = default)
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
