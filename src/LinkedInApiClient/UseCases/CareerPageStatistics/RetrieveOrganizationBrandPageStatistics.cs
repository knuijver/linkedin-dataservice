using System;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatistics : ILinkedInRequest<string>
    {
        public RetrieveOrganizationBrandPageStatistics(LinkedInURN organizationBrand, TimeInterval timeInterval, string tokenId)
        {
            if (organizationBrand.EntityType != "organizationBrand") 
                throw new ArgumentException($"{nameof(organizationBrand)} has an invalid URN Type", nameof(organizationBrand));

            TokenId = tokenId;
            Url = "brandPageStatistics";
            QueryParameters = new QueryParameterCollection
            {
                ["q"] = "brand",
                ["brand"] = organizationBrand
            } + timeInterval.AsQueryParameters();
        }

        public string Url { get; }

        public QueryParameterCollection QueryParameters { get; }

        public string TokenId { get; }
    }
}