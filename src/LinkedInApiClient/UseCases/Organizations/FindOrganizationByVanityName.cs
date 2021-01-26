using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class FindOrganizationByVanityName : ILinkedInRequest
    {
        public FindOrganizationByVanityName(string tokenId, string vanityName)
        {
            TokenId = tokenId;
            Url = "organizations";
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "vanityName",
                ["vanityName"] = vanityName
            };
        }

        public string TokenId { get; }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
    }
}
