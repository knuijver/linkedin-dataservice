using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class GetOrganization : ILinkedInRequest
    {
        public GetOrganization(LinkedInURN organizationId)
        {
            Url = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, $"organizations/{organizationId.UrlEncode()}");
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; } = QueryParameterCollection.EmptyParameters;
    }
}
