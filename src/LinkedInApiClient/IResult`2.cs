using System;
using System.Linq;

namespace LinkedInApiClient
{
    public interface IResult<out TError, out TResult>
    {
        bool IsSuccess { get; }
        TError Error();
        TResult Result();
    }
}
