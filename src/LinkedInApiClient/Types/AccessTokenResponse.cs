using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    /// <summary>
    /// 
    /// </summary>
    public class AccessTokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public string ExpiresIn { get; set; }
    }
}
