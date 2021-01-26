using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Authentication;

namespace LinkedInApiClient
{
    public interface ILinkedInHttpClient
    {
        Task<Result<LinkedInError, string>> ExecuteRequest(HttpRequestMessage request, CancellationToken cancellationToken);
        Task<Result<LinkedInError, T>> ExecuteRequest<T>(HttpRequestMessage request, CancellationToken cancellationToken);
        Task<Result<LinkedInError, string>> GetAsync(string token, IBaseApiRequest request, CancellationToken cancellationToken);
        Task<Result<LinkedInError, TResponse>> GetAsync<TResponse>(string token, IBaseApiRequest request, CancellationToken cancellationToken);
        Task<Result<LinkedInError, RefreshAccessToken>> RefreshAccessToken(Uri uri, string clientId, string secret, string refreshToken, CancellationToken cancellationToken);
        Task<Result<LinkedInError, AccessTokenResponse>> RequestAccessToken(Uri uri, string clientId, string secret, CancellationToken cancellationToken);
    }
}
