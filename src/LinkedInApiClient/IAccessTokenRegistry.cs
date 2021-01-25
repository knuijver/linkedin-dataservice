using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public interface IAccessTokenRegistry
    {
        Task<Result<string, string>> AccessTokenAsync(string tokenId);
        Task<Result<string, string>> UpdateAccessTokenAsync(string tokenId, string accessToken, string expiresIn, string refreshToken);
    }
}
