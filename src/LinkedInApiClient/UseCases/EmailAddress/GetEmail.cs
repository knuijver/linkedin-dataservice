using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.EmailAddress
{
    public class GetEmail : ILinkedInRequest<string>
    {
        public GetEmail(string tokenId)
        {
            this.TokenId = tokenId;
        }

        public string Url { get; } = "emailAddress";

        public QueryParameterCollection QueryParameters { get; } = new QueryParameterCollection
        {
            ["q"] = "members",
            ["projection"] = "(elements*(handle~))"
        };

        public string TokenId { get; }
    }
}
