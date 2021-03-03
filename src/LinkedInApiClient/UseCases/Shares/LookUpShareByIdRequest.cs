using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Shares
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/marketing/integrations/community-management/shares/share-api#look-up-share-by-id
    /// </summary>
    public class LookUpShareByIdRequest : LinkedInRequest
    {
        public LookUpShareByIdRequest(LinkedInURN shareId)
        {
            if (!new[] { "share", "ugcPost", "comment" }.Contains(shareId.EntityType))
                throw new ArgumentException($"{nameof(shareId)} has an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(shareId));

            Address = $"shares/{shareId.Id}";
            QueryParameters = new Parameters
            {
                ["projections"] = "(*,created(*,actor~(localizedLastName,localizedFirstName,vanityName,localizedHeadline)))"
            };
        }
    }
}
