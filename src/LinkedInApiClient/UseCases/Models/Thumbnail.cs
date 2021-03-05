using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.UseCases.Models;

namespace LinkedInApiClient.UseCases.Models
{
    public class Thumbnail
    {
        [JsonPropertyName("imageSpecificContent")]
        public ImageSpecificContent ImageSpecificContent { get; set; }

        [JsonPropertyName("resolvedUrl")]
        public string ResolvedUrl { get; set; }
    }
}
