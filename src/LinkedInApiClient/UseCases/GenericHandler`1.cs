using System;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases
{
    public class GenericHandler<TResponse> : LinkedInRequestHandler<GenericApiQuery<TResponse>, TResponse>
    {
        Func<GenericApiQuery<TResponse>, CancellationToken, Task<Result<LinkedInError, TResponse>>> handle;

        public GenericHandler(Func<GenericApiQuery<TResponse>, CancellationToken, Task<Result<LinkedInError, TResponse>>> handle)
        {
            this.handle = handle;
        }

        protected override Task<Result<LinkedInError, TResponse>> Handle(GenericApiQuery<TResponse> request, CancellationToken cancellationToken)
            => handle(request, cancellationToken);

    }
}
