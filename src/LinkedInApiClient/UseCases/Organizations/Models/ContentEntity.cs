using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Organizations.Models
{
    public class ContentEntity
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("entityLocation")]
        public string EntityLocation { get; set; }

        [JsonPropertyName("thumbnails")]
        public ICollection<Thumbnail> Thumbnails { get; set; }

        [JsonPropertyName("entity")]
        public LinkedInURN Entity { get; set; }
    }
}
