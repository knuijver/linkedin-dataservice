using System;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.Store
{
    public interface IAccessTokenRegistry
    {
        Task<Result<LinkedInError, string>> AccessTokenAsync(string tokenId, CancellationToken cancellationToken);

        Task<Result<LinkedInError, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken, CancellationToken cancellationToken);

        Task<Result<LinkedInError, ValueTuple>> RefreshTokenAsync(string tokenId, CancellationToken cancellationToken);

        Task<Result<LinkedInError, IStoredToken[]>> ListAsync(CancellationToken cancellationToken);
    }
}
