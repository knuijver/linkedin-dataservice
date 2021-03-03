using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

#nullable enable

namespace LinkedInApiClient
{
    public static class HttpClientExtensions
    {
        public static HttpClient UseDefaultLinkedInBaseUrl(this HttpClient client)
        {
            client.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl, UriKind.Absolute);
            return client;
        }
        /// <summary>
        /// Send the HttRequestMessage and read the Response as a String if request was successful,
        /// Otherwise the result will contain an HttpLinkedInError or incase of less unexpected exceptions it will be a LinkedInCaughtException
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<LinkedInResponse> ExecuteRequest(this HttpMessageInvoker client, LinkedInRequest request, CancellationToken cancellationToken = default)
        {
            var httpResponse = await client.SendAsync(request, cancellationToken)
                .ConfigureAwait(false);

            var response = await LinkedInResponse.FromHttpResponseAsync(httpResponse, cancellationToken);
            return response;
        }

        /// <summary>
        /// Convert a string result to Json
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Result<LinkedInError, JsonElement> ToJsonElement(this Result<LinkedInError, string> result)
        {
            if (result.Try(out string json))
            {
                return Result.Success(JsonDocument.Parse(json).RootElement);
            }
            else
            {
                return Result.Fail(result.Error);
            }
        }

        /// <summary>
        /// Convert a string result to Json
        /// </summary>
        /// <param name="result"></param>
        /// <returns></returns>
        public static Task<Result<LinkedInError, JsonElement>> ToJsonElementAsync(this Task<Result<LinkedInError, string>> result)
        {
            return result.ContinueWith(task => ToJsonElement(task.Result));
        }

        /// <summary>
        /// Send an HTTP Get request for the given query object, optionally authenticate using a Bearer token.
        /// </summary>
        /// <param name="token">Bearer token</param>
        /// <param name="request">A LinkedIn Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task<LinkedInResponse> GetAsync(this HttpMessageInvoker client, LinkedInRequest request, CancellationToken cancellationToken = default)
        {
            request.Method = HttpMethod.Get;
            request.Prepare();

            return ExecuteRequest(client, request, cancellationToken);
        }

        /// <summary>
        /// Send an HTTP Get request for the given query object, optionally authenticate using a Bearer token.
        /// </summary>
        /// <param name="token">Bearer token</param>
        /// <param name="request">A LinkedIn Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task<Result<LinkedInError, T>> GetAsync<T>(this HttpMessageInvoker client, LinkedInRequest request, CancellationToken cancellationToken = default)
        {
            request.Method = HttpMethod.Get;
            request.Prepare();

            try
            {
                var response = await client.ExecuteRequest(request, cancellationToken);
                if (response.IsError)
                {
                    return Result.Fail(LinkedInError.FromResponseError(response));
                }
                else
                {
                    return Result.Success(response.ToObject<T>());
                }
            }
            catch (Exception ex)
            {
                return Result.Fail(LinkedInError.FromException(ex));
            }
        }

    }
}
