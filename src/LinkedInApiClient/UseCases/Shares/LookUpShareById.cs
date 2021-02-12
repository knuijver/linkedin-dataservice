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
    public class LookUpShareById : ILinkedInRequest
    {
        public LookUpShareById(string tokenId, LinkedInURN shareId)
        {
            if (!new[] { "share", "ugcPost", "comment" }.Contains(shareId.EntityType))
                throw new ArgumentException($"{nameof(shareId)} has an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(shareId));

            TokenId = tokenId;
            Url = $"shares/{shareId.Id}";
            QueryParameters = new QueryParameterCollection
            {
                ["projections"] = "(*,created(*,actor~(localizedLastName,localizedFirstName,vanityName,localizedHeadline)))"
            };
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }
}
