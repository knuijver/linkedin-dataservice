using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.Models
{
    public class ShareMedia
    {
        /// <summary>
        /// he thumbnail saved from the ingestion of this article.
        /// If provided, only one thumbnail is used and any thumbnails beyond the first will be removed.
        /// Video thumbnails are not retrievable.
        /// </summary>
        [JsonPropertyName("thumbnails")]
        public ICollection<ShareMediaThumbnail> Thumbnails { get; set; } = new HashSet<ShareMediaThumbnail>();

        /// <summary>
        /// The status of the availability of this media.
        /// </summary>
        [JsonPropertyName("status")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public ShareMediaStatus Status { get; set; }

        /// <summary>
        /// The URN of the media shared.
        /// Can be  IngestedVideoMetadata Urn,
        ///         IngestedImageMetadata Urn,
        ///         IngestedArticleMetadata Urn,
        ///         IngestedContent Urn,
        ///         IngestedRichMedia Metadata Urn,
        ///         DigitalmediaAsset Urn,
        ///         or Content Urn.
        /// </summary>
        [JsonPropertyName("media")]
        public LinkedInURN MediaUrn { get; set; }

        /// <summary>
        /// URL whose content is summarized; content may not have a corresponding url for some entities.
        /// </summary>
        [JsonPropertyName("originalUrl")]
        public string OriginalUrl { get; set; }

        [JsonPropertyName("nativeMediaSource")]
        public string NativeMediaSource { get; set; }

        [JsonPropertyName("overlayMetadata")]
        public OverlayMetadata OverlayMetadata { get; set; }

        [JsonPropertyName("landingPage")]
        public LandingPage LandingPage { get; set; }

        [JsonPropertyName("description")]
        public TextContainer Description { get; set; }

        /// <summary>
        /// The title of this media.
        /// </summary>
        [JsonPropertyName("title")]
        public TextContainer Title { get; set; }

        [JsonPropertyName("recipes")]
        public ICollection<string> Recipes { get; set; } = new HashSet<string>();
    }
}
