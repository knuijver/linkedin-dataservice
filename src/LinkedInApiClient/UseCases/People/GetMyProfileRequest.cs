using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.People
{
    public class GetMyProfileRequest : LinkedInRequest
    {
        public GetMyProfileRequest()
        {
            Address = "me";
            QueryParameters = new Parameters
            {
                ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
            };
        }
    }
}
