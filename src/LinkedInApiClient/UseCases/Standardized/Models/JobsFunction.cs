using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Standardized.Models
{
    public class JobsFunction
    {
        [JsonPropertyName("$URN")]
        public string FunctionURN { get; set; }

        [JsonPropertyName("name")]
        public Localized Name { get; set; }

        [JsonPropertyName("id")]
        public int id { get; set; }
    }
}
