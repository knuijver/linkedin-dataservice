using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.Types
{
    public class LocaleString
    {
        [JsonPropertyName("locale")]
        public Locale Locale { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }


        public static implicit operator string(LocaleString localeString) => localeString.Value;
    }
}
