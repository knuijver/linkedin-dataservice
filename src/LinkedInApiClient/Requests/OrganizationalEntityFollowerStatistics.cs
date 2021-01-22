using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public class OrganizationalEntityFollowerStatistics : ILinkedInRequest
    {
        public OrganizationalEntityFollowerStatistics(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organizationalEntity",
                ["organizationalEntity"] = organizationId.UrlEncode()
            }.AddRange(timeInterval.AsQueryParameters());
        }
        public string Url { get; } = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "organizationalEntityFollowerStatistics");

        public QueryParameterCollection QueryParameters { get; }
    }
}
