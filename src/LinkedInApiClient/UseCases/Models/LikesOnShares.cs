using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class LikesOnShares
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("actor~")]
        public Actor Actor { get; set; }

        [JsonPropertyName("created")]
        public Created Created { get; set; }

        [JsonPropertyName("lastModified")]
        public LastModified LastModified { get; set; }

        [JsonPropertyName("$URN")]
        public LinkedInURN LikeUrn { get; set; }

        [JsonPropertyName("object")]
        public LinkedInURN ObjectUrn { get; set; }
    }
}
