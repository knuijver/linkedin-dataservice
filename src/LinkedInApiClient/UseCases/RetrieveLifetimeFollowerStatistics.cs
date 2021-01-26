using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeFollowerStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeFollowerStatistics(LinkedInURN organizationId, TimeInterval timeInterval, string tokenId)
        {
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId.UrlEncode()
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }
        public string Url { get; } = "organizationalEntityFollowerStatistics";

        public QueryParameterCollection QueryParameters { get; }

        public string TokenId { get; }
    }
}
