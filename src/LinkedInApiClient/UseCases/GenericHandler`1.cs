using System;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient.UseCases
{
    public class GenericHandler<TResponse> : LinkedInRequestHandler<GenericApiQuery<TResponse>, TResponse>
    {
        Func<GenericApiQuery<TResponse>, CancellationToken, Task<TResponse>> handle;

        public GenericHandler(Func<GenericApiQuery<TResponse>, CancellationToken, Task<TResponse>> handle)
        {
            this.handle = handle;
        }

        protected override Task<TResponse> Handle(GenericApiQuery<TResponse> request, CancellationToken cancellationToken)
            => handle(request, cancellationToken);

    }
}
