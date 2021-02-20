using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Models
{
    public class Paged<TElement>
    {
        [JsonPropertyName("elements")]
        public IReadOnlyList<TElement> Elements { get; set; }

        [JsonPropertyName("paging")]
        public Paging Paging { get; set; }
    }

    public static class PaginationExtensions
    {
        public static ILinkedInRequest<TElement> NextPageRequest<TElement>(this Paged<TElement> model, ILinkedInRequest<TElement> request)
        {
            var next = model.Paging.Start + model.Paging.Count;
            request.QueryParameters["start"] = next.ToString();
            return request;
        }
    }
}
