using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class TextEntry
    {
        [JsonPropertyName("annotations")]
        public ICollection<Annotation> Annotations { get; set; }

        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
