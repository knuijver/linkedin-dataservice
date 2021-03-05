using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.Models
{
    public class LastModified
    {
        [JsonPropertyName("actor")]
        public LinkedInURN ActorUrn { get; set; }

        [JsonPropertyName("time")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset Time { get; set; }
    }
}
