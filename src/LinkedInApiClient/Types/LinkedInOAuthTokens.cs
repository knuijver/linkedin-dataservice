namespace LinkedInApiClient.Types
{
    /// <summary>
    /// LinkedIn OAuth tokens.
    /// </summary>
    public class LinkedInOAuthTokens
    {
        /// <summary>
        /// Gets or sets clientId.
        /// </summary>
        public string ClientId { get; set; }

        /// <summary>
        /// Gets or sets clientSecret.
        /// </summary>
        public string ClientSecret { get; set; }

        /// <summary>
        /// Gets or sets callback Uri.
        /// </summary>
        public string CallbackUri { get; set; }

        /// <summary>
        /// Gets or sets access token.
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Gets or sets access token Secret.
        /// </summary>
        public string AccessTokenSecret { get; set; }
    }


    public class AccessTokenResponse
    {
        //[Newtonsoft.Json.JsonProperty("access_token")]
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        //[Newtonsoft.Json.JsonProperty("expires_in")]
        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }
    }


    public class RefreshAccessToken
    {
        //[Newtonsoft.Json.JsonProperty("access_token")]
        [System.Text.Json.Serialization.JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        //[Newtonsoft.Json.JsonProperty("expires_in")]
        [System.Text.Json.Serialization.JsonPropertyName("expires_in")]
        public int ExpiresIn { get; set; }

        /// <summary>
        /// Your refresh token for the application. This token must be kept secure.
        /// </summary>
        //[Newtonsoft.Json.JsonProperty("refresh_token")]
        [System.Text.Json.Serialization.JsonPropertyName("refresh_token")]
        public string RefreshToken { get; set; }

        /// <summary>
        /// The number of seconds remaining until the refresh token expires. 
        /// Refresh tokens usually have a longer lifespan than access tokens.
        /// </summary>
        //[Newtonsoft.Json.JsonProperty("refresh_token_expires_in")]
        [System.Text.Json.Serialization.JsonPropertyName("refresh_token_expires_in")]
        public int RefreshTokenExpiresIn { get; set; }
    }

}
