using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class ResponseContext
    {
        [JsonPropertyName("parent")]
        public LinkedInURN Parent { get; set; }

        [JsonPropertyName("root")]
        public LinkedInURN Root { get; set; }
    }
}
