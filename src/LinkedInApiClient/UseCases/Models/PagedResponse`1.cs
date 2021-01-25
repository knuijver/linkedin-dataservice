using System.Collections.Generic;

namespace LinkedInApiClient.UseCases.Models
{
    public class PagedResponse<TElement>
    {
        public ICollection<TElement> Elements { get; set; }
        public Paging Paging { get; set; }
    }
}
