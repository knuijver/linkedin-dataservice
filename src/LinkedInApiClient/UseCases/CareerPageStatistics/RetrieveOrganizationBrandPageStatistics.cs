using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatistics : ILinkedInRequest
    {
        public RetrieveOrganizationBrandPageStatistics(LinkedInURN organizationBrand, TimeInterval timeInterval)
        {
            Url = LinkedInWebApiHandler.Combine(LinkedInConstants.DefaultBaseUrl, "brandPageStatistics");
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "brand",
                ["brand"] = organizationBrand.UrlEncode()
            };
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
    }
}
