using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public interface IOrganizationAccessTokenRegistry
    {
        Task<Result<string, string>> LinkedInAccessTokenAsync(string organizationId);
        Task<Result<string, string>> UpdateLinkedInAccessTokenAsync(string organizationId, string accessToken, string expiresIn, string refreshToken);
    }
}
