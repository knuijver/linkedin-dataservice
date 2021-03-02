using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Messages;
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
        public static async Task<Result<LinkedInError, string>> ExecuteRequest(HttpMessageInvoker client, LinkedInRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);

                

                var responseContent = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success(responseContent);
                }
                else
                {
                    var error = string.IsNullOrEmpty(responseContent)
                        ? default
                        : JsonSerializer.Deserialize<ErrorResponse>(responseContent);

                    return LinkedInHttpError
                        .From(error)
                        .ToResult();
                }
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
              ex is InvalidOperationException ||
              ex is HttpRequestException ||
              ex is JsonException)
            {
                return Result.Fail(LinkedInCaughtException.Create($"A {request.Method.Method} request on [{request.RequestUri?.AbsoluteUri ?? "<uri is null>"}] failed.", ex));
            }
        }

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

        public static Task<Result<LinkedInError, JsonElement>> ToJsonElementAsync(this Task<Result<LinkedInError, string>> result)
        {
            return result.ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    if(task.Result.Try(out string json))
                    {
                        return Result.Success(JsonDocument.Parse(json).RootElement);
                    }
                    else
                    {
                        return Result.Fail(result.Result.Error);
                    }
                }
            });
        }

        /// <summary>
        /// Send an HTTP Get request for the given query object, optionally authenticate using a Bearer token.
        /// </summary>
        /// <param name="token">Bearer token</param>
        /// <param name="request">A LinkedIn Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<LinkedInError, string>> GetAsync(HttpMessageInvoker client, LinkedInRequest request, CancellationToken cancellationToken)
        {
            request.Method = HttpMethod.Get;
            request.Prepare();

            return clietn.ExecuteRequest(request, cancellationToken);
        }

    }
}
