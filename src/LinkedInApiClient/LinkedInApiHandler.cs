using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LinkedInApiClient.Types;
using System.Text.Json;

namespace LinkedInApiClient
{
    public class LinkedInApiHandler
    {
        HttpClient httpClient;

        public LinkedInApiHandler()
            : this(new HttpClientHandler())
        {
        }

        public LinkedInApiHandler(HttpMessageHandler handler)
        {
            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl);
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        protected void SetBasicAuthenticationHeader(string userName, string password)
        {
            var authToken = Encoding.ASCII.GetBytes($"{userName}:{password}");
            this.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                    Convert.ToBase64String(authToken));
        }

        protected void SetUserAgentHeader(string userAgent = "C# console program")
        {
            this.httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
        }

        public Task<Result<LinkedInError, string>> Query(AuthenticatedRequest request)
        {
            return QueryAsync(request.BearerToken, request.Request);
        }

        public Task<Result<LinkedInError, string>> QueryAsync(string token, ILinkedInRequest request)
        {
            return GetJsonAsync(httpClient, token, request.HttpRequestUrl());
        }

        public async Task<Result<LinkedInError, T>> Query<T>(string token, ILinkedInRequest request) where T : ILinkedInResponse<T>
        {
            var result = await GetJsonAsync(httpClient, token, request.HttpRequestUrl());
            return result.ConvertFromJson<T>();
        }

        private static async Task<Result<LinkedInError, string>> GetJsonAsync(HttpClient client, string token, Uri uri)
        {
            var message = new HttpRequestMessage(HttpMethod.Get, uri);
            message.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.SendAsync(
                message,
                HttpCompletionOption.ResponseContentRead
                ).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                return LinkedInError
                    .From(error)
                    //.With(response.StatusCode, $"Your request on {url} failed with status code {response.StatusCode}")
                    .ToResult();
            }
        }

        public static async Task<Result<LinkedInError, string>> RequestAccessToken(HttpClient client, Uri url, string clientId, string secret)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "client_credentials",
                ["client_id"] = clientId,
                ["client_secret"] = secret,
            });

            var response = await client.SendAsync(
                message,
                HttpCompletionOption.ResponseContentRead
                ).ConfigureAwait(false);

            try
            {
                if (response.IsSuccessStatusCode)
                {
                    return Result.Success(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                }
                else
                {
                    var error = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                    return LinkedInError
                        .From(error)
                        //.With(response.StatusCode, $"Your request on {url} failed with status code {response.StatusCode}")
                        .ToResult();
                }
            }
            catch (Exception exception)
            {
                return LinkedInError
                    .With(response.StatusCode, exception.Message)
                    .ToResult();

            }
        }

        public static async Task<Result<LinkedInError, string>> RefreshAccessToken(HttpClient client, Uri url, string clientId, string secret, string refreshToken)
        {
            var message = new HttpRequestMessage(HttpMethod.Post, url);
            message.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            message.Content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                ["grant_type"] = "refresh_token",
                ["client_id"] = clientId,
                ["client_secret"] = secret,
                ["refresh_token"] = refreshToken
            });

            var response = await client.SendAsync(
                message,
                HttpCompletionOption.ResponseContentRead
                ).ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                return Result.Success(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
            }
            else
            {
                var error = JsonSerializer.Deserialize<ErrorResponse>(await response.Content.ReadAsStringAsync().ConfigureAwait(false));
                return LinkedInError
                    .From(error)
                    //.With(response.StatusCode, $"Your request on {url} failed with status code {response.StatusCode}")
                    .ToResult();
            }
        }
    }
}
