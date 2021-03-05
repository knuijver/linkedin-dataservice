using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Models
{
    public class Created
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("actor~")]
        public Actor Actor { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset Time { get; set; }
    }
}
