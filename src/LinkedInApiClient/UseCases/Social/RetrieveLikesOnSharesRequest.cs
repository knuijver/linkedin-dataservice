using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Social
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/marketing/integrations/community-management/shares/network-update-social-actions#retrieve-likes-on-shares
    /// </summary>
    public class RetrieveLikesOnSharesRequest : LinkedInRequest //Paged<LikesOnShares>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="urn">shareUrn|ugcPostUrn|commentUrn</param>
        public RetrieveLikesOnSharesRequest(LinkedInURN urn)
        {
            if (!new[] { "share", "ugcPost", "comment" }.Contains(urn.EntityType))
                throw new ArgumentException($"{nameof(urn)} has an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(urn));

            Address = $"socialActions/{urn}/likes";
            QueryParameters = new Parameters
            {
                ["projection"] = "(*,elements*(*,actor~(localizedLastName,localizedFirstName,vanityName,localizedHeadline)))"
            };
        }
    }
}
