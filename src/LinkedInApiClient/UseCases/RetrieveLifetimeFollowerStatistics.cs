using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeFollowerStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeFollowerStatistics(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId.UrlEncode()
            } + timeInterval.AsQueryParameters();
        }
        public string Url { get; } = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, "organizationalEntityFollowerStatistics");

        public QueryParameterCollection QueryParameters { get; }
        public string TokenId => throw new NotImplementedException();
    }
}
