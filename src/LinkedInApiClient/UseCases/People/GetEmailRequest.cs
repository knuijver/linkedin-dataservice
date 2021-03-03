using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.People
{
    public class GetEmailRequest : LinkedInRequest
    {
        public GetEmailRequest()
        {
            Address = "emailAddress";
            QueryParameters = new Parameters
            {
                ["q"] = "members",
                ["projection"] = "(*,elements*(handle~))"
            };
        }
    }
}
