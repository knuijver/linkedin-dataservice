using System;
using LinkedInApiClient.Types;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases.EmailAddress
{
    public class GetEmailHandler : LinkedInRequestHandler<GetEmail, string>
    {
        readonly LinkedInHttpClient handler;
        readonly IAccessTokenRegistry tokenRegistry;

        public GetEmailHandler(LinkedInHttpClient handler, IAccessTokenRegistry tokenRegistry)
        {
            this.tokenRegistry = tokenRegistry ?? throw new ArgumentNullException(nameof(tokenRegistry), $"{nameof(tokenRegistry)} is null.");
            this.handler = handler ?? throw new ArgumentNullException(nameof(handler), $"{nameof(handler)} is null.");
        }

        protected override async Task<Result<LinkedInError, string>> Handle(GetEmail request, CancellationToken cancellationToken)
        {
            var token = await tokenRegistry.AccessTokenAsync(request.TokenId, cancellationToken);
            if (token.IsSuccess)
            {
                return await handler.GetAsync(request.TokenId, request, cancellationToken).ConfigureAwait(false);
            }
            else
            {
                return Result.Fail(new LinkedInError(token.Error));
            }
        }
    }
}
