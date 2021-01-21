using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class GetEmail : ILinkedInRequest
    {
        public GetEmail()
        {
        }

        public string Url { get; } = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "emailAddress");

        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; } = new Dictionary<string, string>
        {
            ["q"] = "members",
            ["projection"] = "(elements*(handle~))"
        };
    }
}
