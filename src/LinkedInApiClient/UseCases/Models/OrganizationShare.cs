using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class OrganizationShare
    {
        [JsonPropertyName("owner")]
        public LinkedInURN OwnerUrn { get; set; }

        [JsonPropertyName("activity")]
        public LinkedInURN ActivityUrn { get; set; }

        [JsonPropertyName("edited")]
        public bool Edited { get; set; }

        [JsonPropertyName("created")]
        public Created Created { get; set; }

        [JsonPropertyName("text")]
        public TextEntry Text { get; set; }

        [JsonPropertyName("lastModified")]
        public LastModified LastModified { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("distribution")]
        public Distribution Distribution { get; set; }

        [JsonPropertyName("content")]
        public Content Content { get; set; }

        [JsonPropertyName("clientApp")]
        public string ClientApp { get; set; }
    }
}
