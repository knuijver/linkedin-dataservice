using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases
{
    public class GetProfile : ILinkedInRequest
    {
        public GetProfile(string tokenId)
        {
            TokenId = tokenId;
        }

        public string Url { get; } = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, "me");

        public QueryParameterCollection QueryParameters { get; } = new QueryParameterCollection
        {
            ["projection"] = "(id,firstName,lastName,profilePicture(displayImage~:playableStreams))"
        };

        public string TokenId { get; }
    }
}
