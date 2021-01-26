using System;
using System.Threading;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

namespace LinkedInApiClient.UseCases.CareerPageStatistics
{
    public class RetrieveOrganizationBrandPageStatisticsHandler : LinkedInRequestHandler<RetrieveOrganizationBrandPageStatistics, Option<string>>
    {
        readonly LinkedInHttpClient handler;
        readonly IAccessTokenRegistry tokenRegistry;

        public RetrieveOrganizationBrandPageStatisticsHandler(LinkedInHttpClient handler, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry ?? throw new ArgumentNullException(nameof(tokenRegistry), $"{nameof(tokenRegistry)} is null.");
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler), $"{nameof(handler)} is null.");
        }

        protected override async Task<Option<string>> Handle(RetrieveOrganizationBrandPageStatistics request, CancellationToken cancellationToken)
        {
            var toke = await tokenRegistry.AccessTokenAsync(request.TokenId);
            if (toke.IsSuccess)
            {
                var result = await handler.GetAsync(request.TokenId, request).ConfigureAwait(false);
                return result.GetOptionalResult();
            }
            else
            {
                return Option.None;
            }
        }
    }
}
