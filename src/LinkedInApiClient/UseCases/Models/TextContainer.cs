using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class TextContainer
    {
        [JsonPropertyName("text")]
        public string Text { get; set; }
    }
}
