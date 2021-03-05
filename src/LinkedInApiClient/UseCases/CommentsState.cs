using System;
using System.Linq;

namespace LinkedInApiClient.UseCases
{
    public enum CommentsState
    {
        OPEN, // Thread is open to comments.
        CLOSED, // Thread is closed to comments.
        PROCESSING, // Thread is in the process of being deleted.
        DELETED, // Thread is deleted.
    }
}
