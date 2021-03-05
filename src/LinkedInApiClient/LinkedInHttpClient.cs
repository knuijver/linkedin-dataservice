using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
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
            this.client.UseDefaultLinkedInBaseUrl();
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
        public async Task<Result<LinkedInError, T?>> ExecuteRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                var response = await client
                    .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
                    .ConfigureAwait(false);

                var responseContent = await response.Content
                    .ReadAsStringAsync(cancellationToken)
                    .ConfigureAwait(false);

                if (response.IsSuccessStatusCode)
                {
                    T? result = ConvertTo<T>(responseContent);
                    return Result.Success(result);
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
                return Result.Fail(LinkedInError.FromException(ex, $"A {request.Method.Method} request on [{request.RequestUri?.AbsoluteUri ?? "<uri is null>"}] failed."));
            }
        }

        private T? ConvertTo<T>(string response)
        {
            T? result = default;
            if (result is JsonElement)
            {
                var document = JsonDocument.Parse(response);
                return (T)(object)document.RootElement;
            }
            else if (result is JsonDocument)
            {
                var document = JsonDocument.Parse(response);
                return (T)(object)document.RootElement;
            }
            else if (typeof(T).IsClass)
            {
                return JsonSerializer.Deserialize<T>(response);
            }
            else
            {
                return (T)Convert.ChangeType(response, typeof(T));
            }
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
                ContentHelpers.FormData(
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
                ContentHelpers.FormData(
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
               ContentHelpers.FormData(
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
    /*
    class RetryFailedRequest : DelegatingHandler
    {
        private string tokenEndpointUrl = LinkedInConstants.DefaultTokenEndpoint;

        public RetryFailedRequest()
        {
        }

        public RetryFailedRequest(HttpMessageHandler innerHandler)
            : base(innerHandler)
        {
        }

        private HttpClient GetHttpClient()
        {
            return new HttpClient()
            {
                BaseAddress = new Uri(tokenEndpointUrl)
            };
        }

        private async Task<string> RefreshToken(HttpClient client)
        {
            string clientId = string.Empty;
            string secret = string.Empty;
            string refreshToken = string.Empty;

            var message = new HttpRequestMessage(HttpMethod.Post, LinkedInConstants.DefaultTokenEndpoint)
            {
                Content = ContentHelpers.FormData(new Dictionary<string, string?>
                {
                    ["grant_type"] = "refresh_token",
                    ["client_id"] = clientId,
                    ["client_secret"] = secret,
                    ["refresh_token"] = refreshToken
                }),
                Version = new Version(2, 0),
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            var response = await client.SendAsync(message);

            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var response = await base.SendAsync(request, cancellationToken);
            if (response.IsSuccessStatusCode)
            {
                return response;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                var tokenResponse = await RefreshToken(GetHttpClient());
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenResponse);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.ServiceUnavailable)
                {
                    Content = new StringContent("Try again later.")
                };
            }

            return default;
        }
    }
    */
}
