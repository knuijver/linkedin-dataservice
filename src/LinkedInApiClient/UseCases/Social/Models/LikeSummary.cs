using System;
using System.Linq;
using System.Text.Json.Serialization;

namespace LinkedInApiClient.UseCases.Social.Models
{
    public class LikeSummary
    {
        [JsonPropertyName("likedByCurrentUser")]
        public bool LikedByCurrentUser { get; set; }

        [JsonPropertyName("totalLikes")]
        public int TotalLikes { get; set; }
    }
}
