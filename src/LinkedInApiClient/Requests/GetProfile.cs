using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class GetProfile : ILinkedInRequest
    {
        public GetProfile()
        {
        }

        public string Url { get; } = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "me");

        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; } = new Dictionary<string, string>
        {
            ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
        };
    }
}
