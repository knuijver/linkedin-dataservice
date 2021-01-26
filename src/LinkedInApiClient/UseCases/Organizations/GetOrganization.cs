using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class GetOrganization : ILinkedInRequest
    {
        public GetOrganization(LinkedInURN organizationId, string tokenId)
        {
            Url = $"organizations/{organizationId.UrlEncode()}";
            TokenId = tokenId;
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; } = QueryParameterCollection.EmptyParameters;

        public string TokenId { get; }
    }
}
