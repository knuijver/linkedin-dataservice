using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class ShareCommentary
    {
        [JsonPropertyName("inferredLocale")]
        public string InferredLocale { get; set; }

        [JsonPropertyName("attributes")]
        public ICollection<CommentaryAttribute> Attributes { get; set; } = new HashSet<CommentaryAttribute>();

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
