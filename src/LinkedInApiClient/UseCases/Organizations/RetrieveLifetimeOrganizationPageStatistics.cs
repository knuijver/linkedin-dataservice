using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics
    /// There is a difference in response when using Time Intervals.
    /// Without: https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics#sample-response
    /// TimeBound: https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics#retrieve-time-bound-organization-page-statistics
    /// </summary>
    public class RetrieveLifetimeOrganizationPageStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatistics(string tokenId, LinkedInURN organizationId, TimeInterval timeInterval)
        {
            if (!organizationId.HasValue)
            {
                throw new ArgumentException(nameof(organizationId));
            }

            Url = "organizationPageStatistics";
            QueryParameters = new Parameters
            {
                ["q"] = "organization",
                ["organization"] = organizationId
            } + timeInterval.AsQueryParameters();
            TokenId = tokenId;
        }

        public string TokenId { get; }

        public string Url { get; }

        public Parameters QueryParameters { get; }
    }
}
