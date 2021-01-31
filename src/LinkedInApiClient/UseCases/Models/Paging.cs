using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class Paging
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("links")]
        public ICollection<PageLink> Links { get; set; } = new HashSet<PageLink>();

        [JsonPropertyName("start")]
        public int Start { get; set; }
    }


    public class PageLink
    {
        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("href")]
        public string HRef { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }
    }

}
