using System;
using System.Collections.Generic;
using System.Linq;

namespace LinkedInApiClient
{
    public class GetOrganization : ILinkedInRequest
    {
        public GetOrganization(string organizationId)
        {
            Url = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, $"organizations/{organizationId}");
        }

        public string Url { get; }

        public IEnumerable<KeyValuePair<string, string>> QueryParameters { get; } = LinkedInWebApiHandler.EmptyParameters;
    }
}
