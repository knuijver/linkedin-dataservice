using System;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public interface IAccessTokenRegistry
    {
        Task<Result<TokenFailure, string>> AccessTokenAsync(string tokenId, CancellationToken cancellationToken);
        Task<Result<TokenFailure, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken, CancellationToken cancellationToken);

        Task<Result<TokenFailure, ValueTuple>> RefreshTokenAsync(string tokenId, CancellationToken cancellationToken);
    }
}
