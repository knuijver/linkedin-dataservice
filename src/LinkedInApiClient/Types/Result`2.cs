using System;
using System.Linq;

namespace LinkedInApiClient
{
    public readonly struct Result<TError, TData>
    {
        private Result(bool isSuccess, TError error, TData data)
        {
            IsSuccess = isSuccess;
            Error = error;
            Data = data;
        }
        public Result(TError error)
            : this(false, error, default)
        {
        }
        public Result(TData value)
            : this(true, default, value)
        {
        }

        public bool IsSuccess { get; }
        public TError Error { get; }
        public TData Data { get; }

        public static implicit operator Result<TError, TData>(DelayedData<TData> temp) =>
            new Result<TError, TData>(temp.Value);

        public static implicit operator Result<TError, TData>(DelayedError<TError> temp) =>
            new Result<TError, TData>(temp.Value);
    }

    public readonly struct DelayedData<T>
    {
        public DelayedData(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
    public readonly struct DelayedError<T>
    {
        public DelayedError(T value)
        {
            Value = value;
        }

        public T Value { get; }
    }
}
