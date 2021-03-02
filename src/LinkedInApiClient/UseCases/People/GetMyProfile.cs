using System;
using LinkedInApiClient.Messages;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.People
{
    public class GetMyProfile : LinkedInRequest
    {
        public GetMyProfile(string accessToken)
        {
            AccessToken = accessToken;
            Address = "me";
            QueryParameters = new Parameters
            {
                ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
            };
        }
    }
}
