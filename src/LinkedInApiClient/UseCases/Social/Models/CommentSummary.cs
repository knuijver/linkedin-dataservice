using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Social
{
    public class CommentSummary
    {
        [JsonPropertyName("totalFirstLevelComments")]
        public int TotalFirstLevelComments { get; set; }

        [JsonPropertyName("aggregatedTotalComments")]
        public int AggregatedTotalComments { get; set; }
    }
}
