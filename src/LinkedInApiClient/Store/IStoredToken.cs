using System;
using System.Linq;

namespace LinkedInApiClient.Store
{
    public interface IStoredToken
    {
        string AccessToken { get; set; }
        DateTime CreatedOn { get; set; }
        int ExpiresIn { get; set; }
        string Id { get; set; }
        string RefreshToken { get; set; }
        int RefreshTokenExpiresIn { get; set; }
    }
}
