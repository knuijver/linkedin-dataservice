using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Social
{
    /// <summary>
    /// https://docs.microsoft.com/nl-nl/linkedin/marketing/integrations/community-management/shares/network-update-social-actions#retrieve-likes-on-shares
    /// </summary>
    public class RetrieveLikesOnShares : ILinkedInRequest<PagedResponse<LikesOnShares>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tokenId"></param>
        /// <param name="urn">shareUrn|ugcPostUrn|commentUrn</param>
        public RetrieveLikesOnShares(string tokenId, LinkedInURN urn)
        {
            if (!new[] { "share", "ugcPost", "comment" }.Contains(urn.EntityType))
                throw new ArgumentException($"{nameof(urn)} has an invalid URN Type (shareUrn|ugcPostUrn|commentUrn)", nameof(urn));

            TokenId = tokenId;
            Url = $"socialActions/{urn}/likes";
            QueryParameters = new QueryParameterCollection
            {
                ["projection"] = "(*,elements*(*,actor~(localizedLastName,localizedFirstName,vanityName,localizedHeadline)))"
            };
        }

        public string TokenId { get; }
        public string Url { get; }
        public QueryParameterCollection QueryParameters { get; }
    }


    public class LikesOnShares
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("actor~")]
        public Actor Actor { get; set; }

        [JsonPropertyName("created")]
        public Created Created { get; set; }

        [JsonPropertyName("lastModified")]
        public Lastmodified LastModified { get; set; }

        [JsonPropertyName("$URN")]
        public LinkedInURN LikeUrn { get; set; }

        [JsonPropertyName("object")]
        public LinkedInURN ObjectUrn { get; set; }
    }

    public class Created
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }

    public class Lastmodified
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("time")]
        public long Time { get; set; }
    }

}
