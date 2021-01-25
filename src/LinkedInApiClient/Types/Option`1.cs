using System;

namespace LinkedInApiClient.Types
{
    public readonly struct Option<T>
    {
        private readonly T value;
        private readonly bool hasValue;

        private Option(T value, bool hasValue)
        {
            this.value = value;
            this.hasValue = hasValue;
        }

        public Option(T value)
            : this(value, true)
        {
        }

        public static implicit operator Option<T>(NoneOption none) => new Option<T>();

        public TOut Match<TOut>(Func<T, TOut> some, Func<TOut> none) =>
            hasValue ? some(value) : none();

        public void Match(Action<T> some, Action none)
        {
            if (hasValue)
                some(value);
            else
                none();
        }

        public Option<TOut> Select<TOut>(Func<T, TOut> map) =>
            hasValue ? new Option<TOut>(map(value)) : new Option<TOut>();

        public Option<TOut> Bind<TOut>(Func<T, Option<TOut>> bind) =>
            hasValue ? bind(value) : new Option<TOut>();

        public void Tee(Action<T> action)
        {
            if (hasValue)
            {
                action(value);
            }
        }
    }

    public readonly struct NoneOption
    {
    }

    public static class Option
    {
        public static Option<T> Some<T>(T value) => new Option<T>(value);

        public static NoneOption None { get; } = new NoneOption();
    }
}
