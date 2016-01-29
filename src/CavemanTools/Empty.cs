using System;

namespace CavemanTools
{
    public static class Empty
    {
        public static readonly Action Action = () => { };

        public static Action<T> ActionOf<T>() => t => { };

        public static Func<T> Func<T>() => () => default(T);

        public static Func<T, T> FuncOf<T>() => t => t;

        public static Action GetOrEmpty(this Action act) => act ?? Action;

        public static Action<T> GetOrEmpty<T>(this Action<T> act) => act ?? ActionOf<T>();
    }
}