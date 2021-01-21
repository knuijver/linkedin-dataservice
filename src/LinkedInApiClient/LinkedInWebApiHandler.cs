using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LinkedInApiClient
{
    public class LinkedInWebApiHandler
    {
        HttpClient httpClient;

        public LinkedInWebApiHandler()
            : this(new HttpClientHandler())
        {
        }

        public LinkedInWebApiHandler(HttpMessageHandler handler)
        {
            this.httpClient = new HttpClient(handler);
            this.httpClient.BaseAddress = new Uri(LinkedInConstants.DefaultBaseUrl);
            this.httpClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static readonly IEnumerable<KeyValuePair<string, string>> EmptyParameters = Enumerable.Empty<KeyValuePair<string, string>>();


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
            return Query(request.BearerToken, request.Request);
        }

        public Task<Result<LinkedInError, string>> Query(string token, ILinkedInRequest request)
        {
            return GetJsonAsync(httpClient, token, request.Url, request.QueryParameters);
        }

        public async Task<Result<LinkedInError, T>> Query<T>(string token, T request) where T : ILinkedInRequest, ILinkedInResponse<T>
        {
            var result = await GetJsonAsync(httpClient, token, request.Url, request.QueryParameters);
            return result.ConvertFromJson<T>();
        }

        public static string AppendQueryToUrl(string url, IEnumerable<KeyValuePair<string, string>> query)
        {
            if (!query.Any())
            {
                return url;
            }
            else
            {
                return url
                    + (url.Contains("?") ? "&" : "?")
                    + string.Join("&", query.Select(x => Uri.EscapeDataString(x.Key) + "=" + Uri.EscapeDataString(x.Value)));
            }
        }

        public static string Combine(string baseUri, string path)
            => Combine(new Uri(baseUri), path).ToString();

        public static Uri Combine(Uri baseUri, string path)
        {
            var builder = new UriBuilder(baseUri);

            builder.Path = (builder.Path.EndsWith("/"))
                ? string.Concat(builder.Path, path)
                : string.Concat(builder.Path, "/", path);

            return builder.Uri;
        }

        public static async Task<Result<LinkedInError, string>> GetJsonAsync(HttpClient client, string token, string url, IEnumerable<KeyValuePair<string, string>> query)
        {
            var uri = new Uri(AppendQueryToUrl(url, query));
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
                    .ToStringResult();
            }
        }

        public static async Task<Result<LinkedInError, string>> RequestAccessToken(HttpClient client, string url, string clientId, string secret)
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
                        .ToStringResult();
                }
            }
            catch (Exception exception)
            {
                return LinkedInError
                    .With(response.StatusCode, exception.Message)
                    .ToStringResult();

            }
        }

        public static async Task<Result<LinkedInError, string>> RefreshAccessToken(HttpClient client, string url, string clientId, string secret, string refreshToken)
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
                    .ToStringResult();
            }
        }
    }
}
