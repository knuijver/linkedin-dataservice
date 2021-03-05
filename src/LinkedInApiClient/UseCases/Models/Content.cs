using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class Content
    {
        [JsonPropertyName("contentEntities")]
        public ICollection<ContentEntity> ContentEntities { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("shareMediaCategory")]
        public string ShareMediaCategory { get; set; }
    }
}
