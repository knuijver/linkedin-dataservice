using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class GetOrganization : ILinkedInRequest
    {
        public GetOrganization(LinkedInURN organizationId)
        {
            Url = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, $"organizations/{organizationId.UrlEncode()}");
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; } = QueryParameterCollection.EmptyParameters;
        public string TokenId => throw new NotImplementedException();
    }
}
