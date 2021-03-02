using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.People
{
    public class GetEmail : ILinkedInRequest
    {
        public GetEmail(string tokenId)
        {
            this.TokenId = tokenId;
        }

        public string Url { get; } = "emailAddress";

        public Parameters QueryParameters { get; } = new Parameters
        {
            ["q"] = "members",
            ["projection"] = "(*,elements*(handle~))"
        };

        public string TokenId { get; }
    }
}
