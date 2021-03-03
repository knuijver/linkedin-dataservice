using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// 
    /// </summary>
    public class RetrieveLifetimeFollowerStatisticsRequest : LinkedInRequest
    {
        public RetrieveLifetimeFollowerStatisticsRequest(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            Address = "organizationalEntityFollowerStatistics";
            QueryParameters = new Parameters
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId
            } + timeInterval.AsQueryParameters();
        }
    }
}
