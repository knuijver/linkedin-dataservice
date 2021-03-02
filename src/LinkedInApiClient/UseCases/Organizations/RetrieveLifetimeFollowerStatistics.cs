using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// 
    /// </summary>
    public class RetrieveLifetimeFollowerStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeFollowerStatistics(string tokenId, LinkedInURN organizationId, TimeInterval timeInterval)
        {
            QueryParameters = new Parameters
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }
        public string Url { get; } = "organizationalEntityFollowerStatistics";

        public Parameters QueryParameters { get; }

        public string TokenId { get; }
    }
}
