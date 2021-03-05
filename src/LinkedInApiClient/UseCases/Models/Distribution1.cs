using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/ugc-post-api#distribution
    /// </summary>
    public class Distribution
    {
        [JsonPropertyName("externalDistributionChannels")]
        public ExternalDistributionChannelType[] ExternalDistributionChannels { get; set; }

        [JsonPropertyName("distributedViaFollowFeed")]
        public bool DistributedViaFollowFeed { get; set; }

        [JsonPropertyName("feedDistribution")]
        public string FeedDistribution { get; set; }
    }
}
