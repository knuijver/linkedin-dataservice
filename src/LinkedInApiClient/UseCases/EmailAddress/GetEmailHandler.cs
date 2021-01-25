using System;
using LinkedInApiClient.Types;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.EmailAddress
{
    public class GetEmailHandler : LinkedInRequestHandler<GetEmail, Option<string>>
    {
        readonly LinkedInHttpClient handler;
        readonly IAccessTokenRegistry tokenRegistry;

        public GetEmailHandler(LinkedInHttpClient handler, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry ?? throw new ArgumentNullException(nameof(tokenRegistry), $"{nameof(tokenRegistry)} is null.");
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler), $"{nameof(handler)} is null.");
        }

        protected override async Task<Option<string>> Handle(GetEmail request, CancellationToken cancellationToken)
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
