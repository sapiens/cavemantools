using System;

namespace CavemanTools
{
    public static class Empty
    {
        public static readonly Action Action = () => { };

        public static Action<T> ActionOf<T>()
        {
            return t => { };
        }

        public static Func<T> Func<T>()
        {
            return () => default(T);
        }

        public static Func<T, T> FuncOf<T>()
        {
            return t => t;  
        }

        public static Action GetOrEmpty(this Action act)
        {
            return act ?? Action;
        }

        public static Action<T> GetOrEmpty<T>(this Action<T> act)
        {
            return act ?? ActionOf<T>();
        }
    }
}