using LinkedInApiClient.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases
{
    public class RetrieveLifetimeOrganizationPageStatistics : ILinkedInRequest
    {
        public RetrieveLifetimeOrganizationPageStatistics(LinkedInURN organizationId, TimeInterval timeInterval)
        {
            Url = UrlHelper.Combine(LinkedInConstants.DefaultBaseUrl, "organizationPageStatistics");
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "organization",
                ["organization"] = organizationId.UrlEncode()
            } + timeInterval.AsQueryParameters();
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }
    }
}
