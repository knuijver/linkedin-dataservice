using System;
using System.Linq;

namespace LinkedInApiClient.Types
{
    public enum ShareMediaCategory
    {
        ARTICLE, // Represents shared content that only contains articles.
        IMAGE, // Represents shared content that only contains images.
        NONE, // Represents shared content that does not contain any media.
        RICH, // Represents shared content that only cotains rich media.
        VIDEO, // Represents shared content that only contains videos.
        LEARNING_COURSE, // Represents shared content that only contains learning courses.
        JOB, // Represents shared content that only contains jobs.
        QUESTION, // Represents shared content that only contains questa questions.
        ANSWER, // Represents shared content that only contains questa answers.
        CAROUSEL, // Represents shared content of various types that should be rendered in carousel format.
        TOPIC, // Represents shared content that only contains topics.
        NATIVE_DOCUMENT, // Represents shared content of document file types that are uploaded natively.
        URN_REFERENCE, // Refer to the media urn for the category of the content, except when urn type is digital media asset because this urn type does not expose the content type. Use mediaType in ShareMedia when disambiguation is required.
        LIVE_VIDEO, // Represents shared content of a video that is streamed live. This means that the ugcPost may be consumed during recording. The resource serving the media is the source of truth for whether the video is currently live.
    }
}
