using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Shares
{
    [Obsolete("This API is deprecated. Access is no longer being granted.")]
    public class ActivityFeedNetworkShares : ILinkedInRequest
    {
        public ActivityFeedNetworkShares(string tokenId, LinkedInURN before, LinkedInURN after, int? count)
        {
            TokenId = tokenId;
            Url = "activityFeeds";
            QueryParameters = new Parameters
            {
                ["q"] = "networkShares"
            };

            if (before.HasValue) QueryParameters.Add("before", before);
            if (after.HasValue) QueryParameters.Add("after", after);
            if (count.HasValue) QueryParameters.Add("count", count.ToString());
        }

        public string TokenId { get; }
        public string Url { get; }
        public Parameters QueryParameters { get; }
    }
}
