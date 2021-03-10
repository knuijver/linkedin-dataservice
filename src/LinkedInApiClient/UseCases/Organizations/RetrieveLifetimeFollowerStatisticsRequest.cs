using System;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Organizations
{
    /// <summary>
    /// Retrieve Lifetime Follower Statistics <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/follower-statistics#retrieve-lifetime-follower-statistics"/>
    /// Retrieve Time-Bound Follower Statistics <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/organizations/follower-statistics#retrieve-time-bound-follower-statistics"/>
    /// Time-bound follower counts are aggregated and not segmented by facet.
    /// </summary>
    public class RetrieveLifetimeFollowerStatisticsRequest : LinkedInRequest
    {
        public RetrieveLifetimeFollowerStatisticsRequest(LinkedInURN organizationId, TimeInterval timeInterval = default)
        {
            Address = "organizationalEntityFollowerStatistics";
            if (timeInterval == default)
            {
                QueryParameters = new Parameters
                {
                    ["q"] = "organizationalEntity",
                    ["organizationalEntity"] = organizationId
                };
            }
            else
            {
                ProtocolVersion = RestLiProtocolVersion.V2;
                QueryParameters = new Parameters
                {
                    DisableValueEcoding = true,
                    ["q"] = "organizationalEntity",
                    ["organizationalEntity"] = organizationId.UrlEncode()
                };
                QueryParameters.AddRange(timeInterval.AsRestLiParametersV2());
            }
        }
    }

    public class RetrieveLifetimeFollowerStatisticsResponse : LinkedInResponse
    {
        public RetrieveLifetimeFollowerStatisticsResponse()
        {
        }

        public Paged<FollowerStatistics> ToFollowerStatistics()
        {
            return System.Text.Json.JsonSerializer.Deserialize<Paged<FollowerStatistics>>(this.Raw);
        }

        public class FollowerStatistics
        {
            [JsonPropertyName("followerGains")]
            public FollowerGains FollowerGains { get; set; }

            [JsonPropertyName("organizationalEntity")]
            public LinkedInURN OrganizationUrn { get; set; }

            [JsonPropertyName("timeRange")]
            public TimeRange TimeRange { get; set; }
        }

        public class FollowerGains
        {
            [JsonPropertyName("organicFollowerGain")]
            public int OrganicFollowerGain { get; set; }

            [JsonPropertyName("paidFollowerGain")]
            public int PaidFollowerGain { get; set; }
        }
    }
}
