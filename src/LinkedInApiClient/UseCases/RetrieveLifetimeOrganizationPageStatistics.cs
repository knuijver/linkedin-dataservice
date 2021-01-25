using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeOrganizationPageStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatistics(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            Url = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, "organizationPageStatistics");
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organization",
                ["organization"] = organizationId.UrlEncode()
            } + timeInterval.AsQueryParameters();
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
        public string TokenId => throw new NotImplementedException();
    }
}
