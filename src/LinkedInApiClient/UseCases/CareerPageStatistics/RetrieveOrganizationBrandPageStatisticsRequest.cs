using System;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatisticsRequest : LinkedInRequest
    {
        public RetrieveOrganizationBrandPageStatisticsRequest(LinkedInURN organizationBrand, TimeInterval timeInterval)
        {
            if (organizationBrand.EntityType != "organizationBrand")
                throw new ArgumentException($"{nameof(organizationBrand)} has an invalid URN Type", nameof(organizationBrand));

            Address = "brandPageStatistics";
            QueryParameters = new Parameters
            {
                ["q"] = "brand",
                ["brand"] = organizationBrand
            } + timeInterval.AsRestLiParametersV1();
        }
    }
}