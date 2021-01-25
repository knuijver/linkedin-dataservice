using System;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatistics : ILinkedInRequest
    {
        public RetrieveOrganizationBrandPageStatistics(LinkedInURN organizationBrand, TimeInterval timeInterval)
        {
            Url = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, "brandPageStatistics");
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "brand",
                ["brand"] = organizationBrand.UrlEncode()
            };
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
        public string TokenId => throw new NotImplementedException();
    }
}
