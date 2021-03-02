using System;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Social.Models
{
    public class SummaryOfSocialAction
    {
        [JsonPropertyName("commentsSummary")]
        public CommentSummary CommentsSummary { get; set; }

        [JsonPropertyName("$URN")]
        public LinkedInURN URN { get; set; }

        [JsonPropertyName("likesSummary")]
        public LikeSummary LikesSummary { get; set; }
    }
}
