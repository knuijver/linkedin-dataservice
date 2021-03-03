using LinkedInApiClient.Types;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Store;
using System.Net.Http;

#nullable enable

namespace LinkedInApiClient
{
    public static class LinkedInExtensions
    {
        public static DelayedError<LinkedInError> ToResult(this LinkedInError error) => Result.Fail(error);

        public static Uri HttpRequestUrl(this LinkedInRequest request)
        {
            return new Uri(request.QueryParameters.ToUrlQueryString(request.Address), UriKind.Relative);
        }

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

        public static T? ToObject<T>(this JsonElement element, JsonSerializerOptions? options = null)
        {
            var json = element.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }

        public static T? ToObject<T>(this JsonDocument document, JsonSerializerOptions? options = null)
        {
            var json = document.RootElement.GetRawText();
            return JsonSerializer.Deserialize<T>(json, options);
        }
    }
}
