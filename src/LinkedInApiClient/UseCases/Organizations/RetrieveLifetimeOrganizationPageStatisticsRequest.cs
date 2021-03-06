using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics"/>
    /// There is a difference in response when using Time Intervals.
    /// Without: <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics#sample-response"/>
    /// TimeBound: <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/page-statistics#retrieve-time-bound-organization-page-statistics"/>
    /// </summary>
    public class RetrieveLifetimeOrganizationPageStatisticsRequest : LinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatisticsRequest(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            if (!organizationId.HasValue)
            {
                throw new ArgumentException(nameof(organizationId));
            }

            Address = "organizationPageStatistics";
            if (timeInterval == default)
            {
                QueryParameters = new Parameters
                {
                    ["q"] = "organization",
                    ["organization"] = organizationId
                };
            }
            else
            {
                ProtocolVersion = RestLiProtocolVersion.V2;
                QueryParameters = new Parameters
                {
                    DisableValueEcoding = true,
                    ["q"] = "organization",
                    ["organization"] = organizationId.UrlEncode()
                };
                QueryParameters.AddRange(timeInterval.AsRestLiParametersV2());
            }
        }
    }
}
