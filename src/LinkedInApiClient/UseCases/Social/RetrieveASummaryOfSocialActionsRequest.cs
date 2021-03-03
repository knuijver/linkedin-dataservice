using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Social.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Social
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/marketing/integrations/community-management/shares/network-update-social-actions#retrieve-a-summary-of-social-actions
    /// </summary>
    public class RetrieveASummaryOfSocialActionsRequest : LinkedInRequest //SummaryOfSocialAction
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urn">shareUrn|ugcPostUrn|commentUrn</param>
        public RetrieveASummaryOfSocialActionsRequest(LinkedInURN urn)
        {
            if (!new[] { "share", "ugcPost", "comment" }.Contains(urn.EntityType))
                throw new ArgumentException($"{nameof(urn)} has an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(urn));

            Address= $"socialActions/{urn}";
            QueryParameters = Parameters.EmptyParameters;
        }
    }
}
