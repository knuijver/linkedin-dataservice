using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient.UseCases.Models
{
    public class PagedResponse<TElement> : ILinkedInResponse<PagedResponse<TElement>>
    {
        public ICollection<TElement> Elements { get; set; }
        public Paging Paging { get; set; }
    }
}
