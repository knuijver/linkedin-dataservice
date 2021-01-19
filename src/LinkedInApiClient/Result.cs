using System;
using System.Linq;

namespace LinkedInApiClient
{
    public static class Result
    {
        public static IResult<TError, TResult> Ok<TError, TResult>(TResult result)
        {
            return new OkResult<TError, TResult>(result);
        }
        public static IResult<TError, TResult> Error<TError, TResult>(TError error)
        {
            return new ErrorResult<TError, TResult>(error);
        }

        internal class OkResult<TError, TResult> : IResult<TError, TResult>
        {
            private TResult result;

            public OkResult(TResult result)
            {
                this.result = result;
            }

            public bool IsSuccess => true;
            public TError Error() => default;
            public TResult Result() => result;
        }
        internal class ErrorResult<TError, TResult> : IResult<TError, TResult>
        {
            private readonly TError error;

            public ErrorResult(TError error)
            {
                this.error = error;
            }

            public bool IsSuccess => false;
            public TError Error() => error;
            public TResult Result() => default;
        }
    }
}
