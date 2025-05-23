﻿using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SAWebHost.Data.Dto
{
    public class LinkedInProvider
    {
        public string Id { get; set; }

        [Required]
        public string ApplicationName { get; set; }

        [Required]
        public string ClientId { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string ClientSecret { get; set; }

        public bool IsActive { get; set; }

        public string Scope { get; set; }

        [Required]
        public string AuthorizationEndpoint { get; set; }

        [Required]
        public string TokenEndpoint { get; set; }
    }

    public class Organization
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }

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

    public class IdGenerator
    {
        public static LinkedInURN GetOne(string name, Guid id)
        {
            return new LinkedInURN("fan", name, id.ToString().GetHashCode().ToString("x"));
        }
    }
}
