using System;
using System.Linq;

namespace LinkedInApiClient.UseCases.Models
{
    public enum ShareMediaStatus
    {
        PROCESSING, // This ShareMedia is processing and not yet available.
        READY, // This ShareMedia is immediately available.
        FAILED, // This ShareMedia is not available and no further processing is being done.
    }
}
