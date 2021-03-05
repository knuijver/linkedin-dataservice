using LinkedInApiClient.Types;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Store;
using System.Net.Http;
using System.Diagnostics;

#nullable enable

namespace LinkedInApiClient
{
    public static class LinkedInExtensions
    {
        [DebuggerStepThrough]
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);

        [DebuggerStepThrough]
        public static Uri HttpRequestUrl(this LinkedInRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Address), UriKind.Relative);
        }

        [DebuggerStepThrough]
        public static TRequest WithAccessToken<TRequest>(this TRequest request, string accessToken) where TRequest : LinkedInRequest
        {
            request.AccessToken = accessToken;
            return request;
        }

        public static async Task<Result<LinkedInError, T>> Send<T>(
            this LinkedInRequest request,
            IAccessTokenRegistry tokenRegistry,
            string tokenId,
            HttpMessageInvoker client,
            CancellationToken cancellationToken)
        {
            var token = await tokenRegistry.AccessTokenAsync(tokenId, cancellationToken);
            if (token.IsSuccess)
            {
                request.AccessToken = token.Data;
                return await client.GetAsync<T>(request, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return Result.Fail(token.Error);
            }
        }

        [DebuggerStepThrough]
        public static T? ToObject<T>(this JsonElement element, JsonSerializerOptions? options = null)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }

        [DebuggerStepThrough]
        public static T? ToObject<T>(this JsonDocument document, JsonSerializerOptions? options = null)
        {
            var json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }

        [DebuggerStepThrough]
        internal static int KiloBytes(this int value)
            => value * 1024;
    }

    [DebuggerStepThrough]
    internal static class InternalStringExtensions
    {
        public static bool IsMissing(this string value) => string.IsNullOrWhiteSpace(value);

        public static bool IsPresent(this string value) => !value.IsMissing();
    }
}
