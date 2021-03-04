using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.Types
{
    public enum LifecycleStates
    {
        // Represents content that is accessible only to the author and is not yet published.
        DRAFT,
        // Represents content that is accessible to all entities. This is the only accepted field during creation.
        PUBLISHED,
        // Represents content that has been submitted for publish but is not yet ready for rendering.
        // The content will be published asynchronously once the processing has successfully completed.
        PROCESSING,
        // Represents content that has been submitted for publish but a processing failure caused the publish to fail.
        // An edit is required in order to re-attempt the publish.
        PROCESSING_FAILED,
        // Represents content that was once in another state, but has been deleted.
        DELETED,
        // Represents content that was published and later edited.
        PUBLISHED_EDITED,
    }

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

    public enum CommentsState
    {
        OPEN, // Thread is open to comments.
        CLOSED, // Thread is closed to comments.
        PROCESSING, // Thread is in the process of being deleted.
        DELETED, // Thread is deleted.
    }

    public enum ReactionType
    {
        LIKE, // "Like" in the UI
        PRAISE, // "Celebrate" in the UI
        MAYBE, // "Curious" in the UI
        EMPATHY, // "Love" in the UI
        INTEREST, // "Insightful" in the UI
        APPRECIATION, // "Support" in the UI
    }
}
