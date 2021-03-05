using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases
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
}
