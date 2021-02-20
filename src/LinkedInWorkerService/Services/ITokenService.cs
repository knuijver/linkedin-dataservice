using IdentityModel.Client;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInWorkerService.Services
{
    public interface ITokenService
    {
        Task<TokenResponse> GetTokenAsync(string scope, CancellationToken cancellationToken = default);
    }
}