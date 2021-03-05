using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class UGCPost
    {
        [JsonPropertyName("lifecycleState")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public LifecycleStates LifecycleState { get; set; }

        [JsonPropertyName("specificContent")]
        public SpecificContent SpecificContent { get; set; }

        [JsonPropertyName("visibility")]
        public Visibility Visibility { get; set; }

        [JsonPropertyName("created")]
        public Created created { get; set; }

        [JsonPropertyName("author")]
        public LinkedInURN AuthorUrn { get; set; }

        [JsonPropertyName("clientApplication")]
        public LinkedInURN ClientApplicationUrn { get; set; }

        [JsonPropertyName("versionTag")]
        public string VersionTag { get; set; }

        [JsonPropertyName("id")]
        public LinkedInURN Id { get; set; }

        [JsonPropertyName("firstPublishedAt")]
        [JsonConverter(typeof(UnixTimeConverter))]
        public DateTimeOffset FirstPublishedAt { get; set; }

        [JsonPropertyName("lastModified")]
        public LastModified LastModified { get; set; }

        [JsonPropertyName("distribution")]
        public Distribution Distribution { get; set; }

        [JsonPropertyName("contentCertificationRecord")]
        public string ContentCertificationRecord { get; set; }

        [JsonPropertyName("containerEntity")]
        public LinkedInURN ContainerEntity { get; set; }

        [JsonPropertyName("responseContext")]
        public ResponseContext ResponseContext { get; set; }
    }
}
