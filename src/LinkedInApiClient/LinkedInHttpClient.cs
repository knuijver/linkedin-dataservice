using LinkedInApiClient.Authentication;
using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
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
            this.client.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl);
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
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

        public async Task<Result<LinkedInError, string>> ExecuteRequest(HttpRequestMessage request)
        {
            try
            {
                var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead)
                    .ConfigureAwait(false);

                var responseContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success(responseContent);
                }
                else
                {
                    var error = string.IsNullOrEmpty(responseContent)
                        ? default
                        : JsonSerializer.Deserialize<ErrorResponse>(responseContent);
                    return LinkedInError
                        .From(error)
                        .ToResult();
                }
            }
            catch (Exception ex) when (ex is ArgumentNullException ||
              ex is InvalidOperationException ||
              ex is HttpRequestException ||
              ex is JsonException)
            {
                return Result.Fail(LinkedInError.With(HttpStatusCode.BadRequest, ex.Message));
            }
        }

        public async Task<Result<LinkedInError, T>> ExecuteRequest<T>(HttpRequestMessage request)
        {
            var response = await ExecuteRequest(request);
            if (response.IsSuccess)
            {
                var result = string.IsNullOrEmpty(response.Data)
                    ? default
                    : JsonSerializer.Deserialize<T>(response.Data);
                return Result.Success(result);
            }
            else
            {
                return Result.Fail(response.Error);
            }
        }

        public static HttpContent FormData(IEnumerable<KeyValuePair<string, string>> data)
            => new FormUrlEncodedContent(data);

        public Task<Result<LinkedInError, string>> GetAsync(string token, IBaseApiRequest request)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest(message);
        }

        public Task<Result<LinkedInError, TResponse>> GetAsync<TResponse>(string token, IBaseApiRequest request)
        {
            var message = CreateRequest(HttpMethod.Get, request.HttpRequestUrl(), token);
            return ExecuteRequest<TResponse>(message);
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
            string refreshToken)
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

            return ExecuteRequest<RefreshAccessToken>(message);
        }

        public Task<Result<LinkedInError, AccessTokenResponse>> RequestAccessToken(
            Uri uri,
            string clientId,
            string secret)
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

            return ExecuteRequest<AccessTokenResponse>(message);
        }
    }
}
