using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class GetProfile : ILinkedInRequest
    {
        public GetProfile()
        {
        }

        public string Url { get; } = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "me");

        public QueryParameterCollection QueryParameters { get; } = new QueryParameterCollection
        {
            ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
        };
    }
}
