using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class PagedResponse<TElement>
    {
        [JsonPropertyName("elements")]
        public ICollection<TElement> Elements { get; set; }

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }
    }
}
