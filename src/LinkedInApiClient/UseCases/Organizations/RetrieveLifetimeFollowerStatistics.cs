using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    public class RetrieveLifetimeFollowerStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeFollowerStatistics(string tokenId, LinkedInURN organizationId, TimeInterval timeInterval)
        {
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }
        public string Url { get; } = "organizationalEntityFollowerStatistics";

        public QueryParameterCollection QueryParameters { get; }

        public string TokenId { get; }
    }
}
