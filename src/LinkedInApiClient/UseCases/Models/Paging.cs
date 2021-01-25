using System.Collections.Generic;

namespace LinkedInApiClient.UseCases.Models
{
    public class Paging
    {
        public int Count { get; set; }
        public ICollection<string> Links { get; set; }
        public int Start { get; set; }
    }
}
