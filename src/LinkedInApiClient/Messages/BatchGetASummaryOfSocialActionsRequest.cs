using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Messages
{
    /// <summary>
    /// GET "https://api.linkedin.com/v2/socialMetadata?ids=List({shareUrn|ugcPostUrn|commentUrn}, comma-delimited)
    /// <see cref="https://docs.microsoft.com/en-us/linkedin/marketing/integrations/community-management/shares/reactions-and-social-metadata#batch-get-a-summary-of-social-actions"/>
    /// </summary>
    public class BatchGetASummaryOfSocialActionsRequest : LinkedInRequest
    {
        public BatchGetASummaryOfSocialActionsRequest(params LinkedInURN[] actionUrn)
        {
            var invalid = actionUrn
               .Where(urn => !urn.IsEntityTypeOf("share", "ugcPost", "comment"))
               .Select(urn => new ArgumentException($"{urn} is an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(actionUrn)))
               .ToArray();

            if (invalid.Length > 0) throw new AggregateException($"{nameof(actionUrn)} contains invalid arguments", invalid);

            var urnList = string.Join(",", actionUrn.Select(s => s.UrlEncode()));

            Address = "socialMetadata";
            QueryParameters = new Types.Parameters
            {
                ["ids"] = $"List({urnList})"
            };
        }
    }
}
