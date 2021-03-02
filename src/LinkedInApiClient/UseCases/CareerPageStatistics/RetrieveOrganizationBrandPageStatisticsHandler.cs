using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatisticsHandler : LinkedInRequestHandler<RetrieveOrganizationBrandPageStatistics, JsonElement>
    {
        readonly LinkedInHttpClient handler;
        readonly IAccessTokenRegistry tokenRegistry;

        public RetrieveOrganizationBrandPageStatisticsHandler(LinkedInHttpClient handler, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry ?? throw new ArgumentNullException(nameof(tokenRegistry), $"{nameof(tokenRegistry)} is null.");
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler), $"{nameof(handler)} is null.");
        }

        protected override Task<Result<LinkedInError, JsonElement>> Handle(RetrieveOrganizationBrandPageStatistics request, CancellationToken cancellationToken)
        {
            return request.HandleAsync(tokenRegistry, handler, cancellationToken);
        }
    }
}
