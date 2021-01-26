using LinkedInApiClient.Types;
using System.Threading;
using System.Threading.Tasks;

namespace LinkedInApiClient
{
    public interface ILinkedInRequest : ILinkedInRequest<string>
    {
    }

    public interface ILinkedInRequest<TResponse> : IBaseApiRequest
    {
        string TokenId { get; }
    }

    public interface IBaseApiRequest
    {
        string Url { get; }

        QueryParameterCollection QueryParameters { get; }
    }

    public interface ILinkedInRequestHandler<in TRequest, TResponse>
        where TRequest : ILinkedInRequest<TResponse>
    {
        Task<Result<LinkedInError, TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }

    public abstract class LinkedInRequestHandler<TRequest, TResponse> : ILinkedInRequestHandler<TRequest, TResponse>
        where TRequest : ILinkedInRequest<TResponse>
    {
        Task<Result<LinkedInError, TResponse>> ILinkedInRequestHandler<TRequest, TResponse>.Handle(TRequest request, CancellationToken cancellationToken)
        {
            request.Validate();
            return Handle(request, cancellationToken);
        }

        protected abstract Task<Result<LinkedInError, TResponse>> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
