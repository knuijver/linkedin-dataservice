using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Shares
{
    [Obsolete("This API is deprecated. Access is no longer being granted.")]
    public class ActivityFeedNetworkSharesRequest : LinkedInRequest
    {
        public ActivityFeedNetworkSharesRequest(LinkedInURN before, LinkedInURN after, int? count)
        {
            Address = "activityFeeds";
            QueryParameters = new Parameters
            {
                ["q"] = "networkShares"
            };

            if (before.HasValue) QueryParameters.Add("before", before);
            if (after.HasValue) QueryParameters.Add("after", after);
            if (count.HasValue) QueryParameters.Add("count", count.ToString());
        }
    }
}
