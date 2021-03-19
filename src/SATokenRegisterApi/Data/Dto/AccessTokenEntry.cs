using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SATokenRegisterApi.Data.Dto
{
    public class AccessTokenEntry
    {
        public string Id { get; set; }

        public string ProviderId { get; set; }

        public string OrganizationId { get; set; }

        [Display(Name = "Access Token")]
        public string AccessToken { get; set; }

        [Display(Name = "Expires In")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Your refresh token for the application. This token must be kept secure.
        /// </summary>
        [Display(Name = "Refresh Token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds remaining until the refresh token expires. 
        /// Refresh tokens usually have a longer lifespan than access tokens.
        /// </summary>
        [Display(Name = "Expires In")]
        public int RefreshTokenExpiresIn { get; set; }

        [Display(Name = "Created On")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeOffset.Now;

        public TimeSpan TokenDaysLeft() => CreatedOn.AddSeconds(ExpiresIn).Subtract(DateTimeOffset.Now);

        public TimeSpan RefreshTokenDaysLeft() => CreatedOn.AddSeconds(ExpiresIn).Subtract(DateTimeOffset.Now);
    }
}
