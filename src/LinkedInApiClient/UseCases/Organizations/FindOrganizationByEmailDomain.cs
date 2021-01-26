using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class FindOrganizationByEmailDomain : ILinkedInRequest
    {
        public FindOrganizationByEmailDomain(string tokenId, string emailDomain)
        {
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "emailDomain",
                ["emailDomain"] = emailDomain
            };
            TokenId = tokenId;
        }
        public string TokenId { get; }

        public string Url { get; } = "organizations";

        public QueryParameterCollection QueryParameters { get; }
    }
}
