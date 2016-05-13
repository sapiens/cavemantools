using System;
using System.Linq;
using System.Reflection;

namespace CavemanTools
{
    public class AnyOf<T, V>
    {
        private Type[] _types ;
        private dynamic _value;
        public static readonly AnyOf<T,V> Empty=new AnyOf<T, V>();
        protected AnyOf()
        {
            _types = GetType().GetGenericArguments();
        }

        public bool IsEmpty { get; private set; } = true;
        
        public AnyOf(T value):this()
        {
            Value = value;
        }

        public dynamic Value
        {
            get { return _value; }
            protected set
            {
                _value = value;
                IsEmpty = false;
            }
        }

        public AnyOf(V value):this()
        {
            Value = value;
        }

        public bool Is<TValue>() => !IsEmpty && (Value is TValue);

        public TResult As<TResult>() => (TResult) Value;

        public void When<TValue>(Action<TValue> action)
        {
            var type = typeof(TValue);
            if (!_types.Any(t=>t==type)) throw new InvalidOperationException();
           
            if (Value==null && !(type.IsClass() || type.IsNullable())) throw new InvalidOperationException("You're trying to use a null value as a non-nullable type");

            if (Is<TValue>()) action(As<TValue>());
        }

     
    }

    public class AnyOf<T, V, U> : AnyOf<T, V>
    {
        public AnyOf(U value)
        {
            Value = value;
        }
        public AnyOf(T value) : base(value)
        {
        }

        public AnyOf(V value) : base(value)
        {
        }
    }
}