using LinkedInApiClient.Types;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public static class LinkedInExtensions
    {
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);

        public static Uri HttpRequestUrl(this ILinkedInRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Url), UriKind.Relative);
        }

        public static Uri HttpRequestUrl(this IBaseApiRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Url), UriKind.Relative);
        }

        public static void Validate<T>(this ILinkedInRequest<T> request)
        {
            if (string.IsNullOrEmpty(request.TokenId))
            {
                throw new ArgumentException($"'{nameof(request.TokenId)}' cannot be null or empty", nameof(request.TokenId));
            }

            Validate((IBaseApiRequest)request);
        }
        public static void Validate(this IBaseApiRequest request)
        {
            if (request.QueryParameters == null)
            {
                throw new ArgumentNullException(nameof(request.QueryParameters));
            }

            if (request.Url == null || request.Url.Length == 0)
            {
                throw new ArgumentException(nameof(request.Url));
            }
        }

        public static async Task<Result<LinkedInError, JsonElement>> Handle(this ILinkedInRequest request, IAccessTokenRegistry tokenRegistry, LinkedInHttpClient client, CancellationToken cancellationToken)
        {
            var token = await tokenRegistry.AccessTokenAsync(request.TokenId, cancellationToken);
            if (token.IsSuccess)
            {
                var result = await client.GetAsync(token.Data, request, cancellationToken).ConfigureAwait(false);
                return result;
            }
            else
            {
                return Result.Fail(new LinkedInError(token.Error));
            }
        }
        public static async Task<Result<LinkedInError, T>> Handle<T>(this ILinkedInRequest<T> request, IAccessTokenRegistry tokenRegistry, LinkedInHttpClient client, CancellationToken cancellationToken)
        {
            var token = await tokenRegistry.AccessTokenAsync(request.TokenId, cancellationToken);
            if (token.IsSuccess)
            {
                var result = await client.GetAsync<T>(token.Data, request, cancellationToken).ConfigureAwait(false);
                return result;
            }
            else
            {
                return Result.Fail(new LinkedInError(token.Error));
            }
        }
    }
}
