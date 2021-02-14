using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Social
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
        public Lastmodified LastModified { get; set; }

        [JsonPropertyName("$URN")]
        public LinkedInURN LikeUrn { get; set; }

        [JsonPropertyName("object")]
        public LinkedInURN ObjectUrn { get; set; }
    }
}
