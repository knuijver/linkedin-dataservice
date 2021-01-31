using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;

namespace LinkedInApiClient.Authentication
{
    public class LinkedInOptions : OAuthOptions
    {
        public LinkedInOptions()
        {
            CallbackPath = new PathString("/signin-linkedin");
            AuthorizationEndpoint = LinkedInConstants.DefaultAuthorizationEndpoint;
            TokenEndpoint = LinkedInConstants.DefaultTokenEndpoint;
            UserInformationEndpoint = LinkedInConstants.UserInformationEndpoint;
            Scope.Add("r_organization_social");
            Scope.Add("r_1st_connections_size");
            Scope.Add("r_ads_reporting");
            Scope.Add("rw_organization_admin");
            Scope.Add("r_basicprofile");
            Scope.Add("r_ads");
        }

    }
}
