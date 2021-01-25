namespace LinkedInApiClient
{
    internal static class LinkedInConstants
    {
        public static readonly string DefaultBaseUrl = "https://api.linkedin.com/v2";
        public static readonly string DefaultBaseUrlV1 = "https://api.linkedin.com/v1";

        public static readonly string DefaultOAuthBaseUrl = "https://www.linkedin.com/uas/oauth2/";
        public static readonly string DefaultAuthorizationEndpoint = "https://www.linkedin.com/oauth/v2/authorization";
        public static readonly string DefaultTokenEndpoint = "https://www.linkedin.com/oauth/v2/accessToken";

        public const string AuthenticationScheme = "LinkedIn";
        public static readonly string DisplayName = AuthenticationScheme;
        public static readonly string UserInformationEndpoint;
    }
}
