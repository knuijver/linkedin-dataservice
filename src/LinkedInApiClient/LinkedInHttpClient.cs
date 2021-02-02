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

namespace LinkedInApiClient
{
    public class LinkedInHttpClient
    {
        HttpClient client;

        public LinkedInHttpClient() : this(new HttpClientHandler())
        {
        }

        public LinkedInHttpClient(HttpMessageHandler handler)
        {
            this.client = new HttpClient(handler);
            this.client.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl, UriKind.Absolute);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //this.client.DefaultRequestHeaders.Add("X-Restli-Protocol-Version", "2.0.0");
        }

        public static HttpRequestMessage CreateRequest(HttpMethod method, Uri uri, string token, HttpContent content = null)
        {
            var message = new HttpRequestMessage(method, uri)
            {
                Content = content,
                Version = new Version(2, 0),
                VersionPolicy = HttpVersionPolicy.RequestVersionOrLower
            };

            if (token != null)
                message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return message;
        }

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
                return Result.Fail(LinkedInHttpError.With(HttpStatusCode.BadRequest, ex.Message));
            }
        }


        public async Task<Result<LinkedInError, T>> ExecuteRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken)
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

        public T ConvertTo<T>(string response)
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

        public static HttpContent FormData(IEnumerable<KeyValuePair<string, string>> data)
            => new FormUrlEncodedContent(data);

        public Task<Result<LinkedInError, string>> GetAsync(string token, IBaseApiRequest request, CancellationToken cancellationToken)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest(message, cancellationToken);
        }

        public Task<Result<LinkedInError, TResponse>> GetAsync<TResponse>(string token, IBaseApiRequest request, CancellationToken cancellationToken)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest<TResponse>(message, cancellationToken);
        }

        public static HttpContent JsonContent(object content)
        {
            var json = content is string ? (content as string) : JsonSerializer.Serialize(content);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        public Task<Result<LinkedInError, RefreshAccessToken>> RefreshAccessToken(
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
                    new Dictionary<string, string>
                    {
                        ["grant_type"] = "refresh_token",
                        ["client_id"] = clientId,
                        ["client_secret"] = secret,
                        ["refresh_token"] = refreshToken
                    }));

            return ExecuteRequest<RefreshAccessToken>(message, cancellationToken);
        }

        public Task<Result<LinkedInError, AccessTokenResponse>> RequestAccessToken(
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
                    new Dictionary<string, string>
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
               HttpMethod.Post,
               uri,
               null,
               FormData(
                   new Dictionary<string, string>
                   {
                       ["response_type"] = "code",
                       ["client_id"] = clientId,
                       ["redirect_uri"] = redirectUri.AbsoluteUri,
                       ["state"] = state,
                       ["scope"] = string.Join(" ", scope)
                   }));

            return ExecuteRequest<JsonElement>(message, cancellationToken);
        }
    }
}
