using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using LinkedInApiClient.Types;

namespace LinkedInApiClient
{
    public static class ResultExtensions
    {
        public static Result<LinkedInError, T> ConvertFromJson<T>(this Result<LinkedInError, string> result)
        {
            if (result.IsSuccess)
            {
                var model = JsonSerializer.Deserialize<T>(result.Data);
                return Result.Success(model);
            }
            else
            {
                return Result.Fail(result.Error);
            }
        }

        public static Result<R, T> ConvertFromJson<R, T>(this Result<R, string> result)
        {
            if (result.IsSuccess)
            {
                var model = JsonSerializer.Deserialize<T>(result.Data);
                return Result.Success(model);
            }
            else
            {
                return Result.Fail(result.Error);
            }
        }

        public static TData GetResultOrDefault<TError, TData>(this Result<TError, TData> result)
        {
            return result.IsSuccess
                ? result.Data
                : default;
        }

        public static Option<TData> GetOptionalResult<TError, TData>(this Result<TError, TData> result)
        {
            return result.IsSuccess
                ? Option.Some(result.Data)
                : Option.None;
        }
    }
}
