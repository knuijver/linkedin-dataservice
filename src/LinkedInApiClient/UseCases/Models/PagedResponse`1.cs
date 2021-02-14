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

    public static class PaginationExtensions
    {
        public static ILinkedInRequest<TElement> NextPageRequest<TElement>(this PagedResponse<TElement> model, ILinkedInRequest<TElement> request)
        {
            var next = model.Paging.Start + model.Paging.Count;
            request.QueryParameters["start"] = next.ToString();
            return request;
        }
    }
}
