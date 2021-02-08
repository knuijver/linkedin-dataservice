using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace LinkedInApiClient
{
    public class LinkedInHttpClient : IDisposable
    {
        private readonly HttpClient client;

        public LinkedInHttpClient() : this(new HttpClientHandler())
        {
        }

        public LinkedInHttpClient(HttpMessageHandler handler)
        {
            this.client = new HttpClient(handler);
            this.client.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl, UriKind.Absolute);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Create a HttpRequestMessage with HTTP2 set as default and optionally add a Bearer token
        /// </summary>
        /// <param name="method"><see cref="HttpMethod"/></param>
        /// <param name="uri"><see cref="Uri"/></param>
        /// <param name="token">optional Bearer token</param>
        /// <param name="content"><see cref="HttpContent"/></param>
        /// <returns></returns>
        public static HttpRequestMessage CreateRequest(HttpMethod method, Uri uri, string? token, HttpContent? content = null)
        {
            var message = new HttpRequestMessage(method, uri)
            {
                Content = content,
                Version = new Version(2, 0),
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            if (token != null)
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //message.Headers.Add("X-Restli-Protocol-Version", "2.0.0");

            return message;
        }

        /// <summary>
        /// Send the HttRequestMessage and read the Response as a String if request was successful,
        /// Otherwise the result will contain an HttpLinkedInError or incase of less unexpected exceptions it will be a LinkedInCaughtException
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<LinkedInError, string>> ExecuteRequest(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);

                Console.WriteLine($"{request.Method} {request.RequestUri}");

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

        /// <summary>
        /// Send the HttRequestMessage and read the Response as an object of type T when the request is successful,
        /// otherwise the result will contain an HttpLinkedInError or incase of less unexpected exceptions it will be a LinkedInCaughtException
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<LinkedInError, T?>> ExecuteRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await ExecuteRequest(request, cancellationToken);
            if (response.IsSuccess)
            {
                T result = ConvertTo<T>(response.Data);
                return Result.Success(result);
            }
            else
            {
                return Result.Fail(response.Error);
            }
        }

        private T? ConvertTo<T>(string response)
        {
            T result = default;
            if (result is JsonElement)
            {
                var document = JsonDocument.Parse(response);
                result = (T)(object)document.RootElement;
            }
            else if (result is JsonDocument)
            {
                var document = JsonDocument.Parse(response);
                result = (T)(object)document.RootElement;
            }
            else if (typeof(T).IsClass)
            {
                result = JsonSerializer.Deserialize<T>(response);
            }
            else
            {
                result = (T)Convert.ChangeType(response, typeof(T));
            }

            return result;
        }

        /// <summary>
        /// Create an FormUrlEncodedContent from a KeyValuePair collection with a non-nullable Key.
        /// example Dictionary&lt;string,string?&gt;
        /// <see cref="https://github.com/dotnet/runtime/issues/38494"/>
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static HttpContent FormData(IEnumerable<KeyValuePair<string, string?>> data)
            => new FormUrlEncodedContent((IEnumerable<KeyValuePair<string?, string?>>)data);

        /// <summary>
        /// Send an HTTP Get request for the given query object, optionally authenticate using a Bearer token.
        /// </summary>
        /// <param name="token">Bearer token</param>
        /// <param name="request">A LinkedIn Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<LinkedInError, string>> GetAsync(string token, IBaseApiRequest request, CancellationToken cancellationToken)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest(message, cancellationToken);
        }

        /// <summary>
        /// Send an HTTP Get request for the given query object, optionally authenticate using a Bearer token.
        /// </summary>
        /// <param name="token">Bearer token</param>
        /// <param name="request">A LinkedIn Query object</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<Result<LinkedInError, TResponse?>> GetAsync<TResponse>(string token, IBaseApiRequest request, CancellationToken cancellationToken)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest<TResponse>(message, cancellationToken);
        }

        internal static HttpContent JsonContent(object content)
        {
            var json = content is string str ? str : JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public Task<Result<LinkedInError, RefreshAccessToken?>> RefreshAccessToken(
            Uri uri,
            string clientId,
            string secret,
            string refreshToken,
            CancellationToken cancellationToken)
        {
            var message = CreateRequest(
                HttpMethod.Post,
                uri,
                null,
                FormData(
                    new Dictionary<string, string?>
                    {
                        ["grant_type"] = "refresh_token",
                        ["client_id"] = clientId,
                        ["client_secret"] = secret,
                        ["refresh_token"] = refreshToken
                    }));

            return ExecuteRequest<RefreshAccessToken>(message, cancellationToken);
        }

        public Task<Result<LinkedInError, AccessTokenResponse?>> RequestAccessToken(
            Uri uri,
            string clientId,
            string secret,
            CancellationToken cancellationToken)
        {
            var message = CreateRequest(
                HttpMethod.Post,
                uri,
                null,
                FormData(
                    new Dictionary<string, string?>
                    {
                        ["grant_type"] = "client_credentials",
                        ["client_id"] = clientId,
                        ["client_secret"] = secret,
                    }));

            return ExecuteRequest<AccessTokenResponse>(message, cancellationToken);
        }

        public Task<Result<LinkedInError, JsonElement>> RequestAnAuthorizationCode(
            Uri uri,
            string clientId,
            Uri redirectUri,
            ICollection<string> scope,
            string state,
            CancellationToken cancellationToken)
        {
            var message = CreateRequest(
               HttpMethod.Get,
               uri,
               null,
               FormData(
                   new Dictionary<string, string?>
                   {
                       ["response_type"] = "code",
                       ["client_id"] = clientId,
                       ["redirect_uri"] = redirectUri.AbsoluteUri,
                       ["state"] = state,
                       ["scope"] = string.Join(" ", scope)
                   }));

            return ExecuteRequest<JsonElement>(message, cancellationToken);
        }

        protected void Dispose(bool disposing)
        {
            client?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~LinkedInHttpClient()
        {
            Dispose(false);
        }
    }
}
