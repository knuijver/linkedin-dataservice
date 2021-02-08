using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    public class RefreshAccessToken
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Your refresh token for the application. This token must be kept secure.
        /// </summary>
        [JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds remaining until the refresh token expires. 
        /// Refresh tokens usually have a longer lifespan than access tokens.
        /// </summary>
        [JsonPropertyName("refresh_token_expires_in")]
        public int RefreshTokenExpiresIn { get; set; }
    }
}
