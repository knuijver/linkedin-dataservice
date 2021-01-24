using System;
using System.Collections.Generic;
using System.Linq;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class GetEmail : ILinkedInRequest
    {
        public GetEmail()
        {
        }

        public string Url { get; } = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "emailAddress");

        public QueryParameterCollection QueryParameters { get; } = new QueryParameterCollection
        {
            ["q"] = "members",
            ["projection"] = "(elements*(handle~))"
        };
    }
}
