using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations.Models
{
    public class Annotation
    {
        [JsonPropertyName("length")]
        public int Length { get; set; }

        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("entity")]
        public LinkedInURN Entity { get; set; }
    }
}
