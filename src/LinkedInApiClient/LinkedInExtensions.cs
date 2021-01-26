using LinkedInApiClient.Types;
using System;
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

        public static async Task<Result<LinkedInError, string>> Handle(this ILinkedInRequest request, IAccessTokenRegistry tokenRegistry, ILinkedInHttpClient client, CancellationToken cancellationToken)
        {
            var toke = await tokenRegistry.AccessTokenAsync(request.TokenId, cancellationToken);
            if (toke.IsSuccess)
            {
                var result = await client.GetAsync(request.TokenId, request, cancellationToken).ConfigureAwait(false);
                return result;
            }
            else
            {
                return Result.Fail(new LinkedInError(toke.Error));
            }
        }

        public static async Task<Result<LinkedInError, T>> Handle<T>(this ILinkedInRequest<T> request, IAccessTokenRegistry tokenRegistry, ILinkedInHttpClient client, CancellationToken cancellationToken)
        {
            var toke = await tokenRegistry.AccessTokenAsync(request.TokenId, cancellationToken);
            if (toke.IsSuccess)
            {
                var result = await client.GetAsync<T>(request.TokenId, request, cancellationToken).ConfigureAwait(false);
                return result;
            }
            else
            {
                return Result.Fail(new LinkedInError(toke.Error));
            }
        }
    }
}
