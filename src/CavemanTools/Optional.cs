using System.Diagnostics;

namespace System
{
    public struct Optional<T> where T:class 
    {
        public static readonly Optional<T> Empty=new Optional<T>();

        public T Value { [DebuggerStepThrough]get; }
        
        public bool HasValue => !IsEmpty;

        public bool IsEmpty => Value == null;

        /// <summary>
        /// Returns the value or the specified argument if it's empty
        /// </summary>
        /// <param name="other">Value to return if option is empty</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public T ValueOr(T other) => Value??other;

        public Optional<V> Map<V>(Func<T, V> mapper) where V : class
        {
            if (Value==null) return Optional<V>.Empty;
            return new Optional<V>(mapper(Value));
        }

        public V? MapValue<V>(Func<T, V> mapper) where V : struct
        {
            if (Value == null) return null;
            return mapper(Value);
        }

        public void Execute(Action<T> action) => action?.Invoke(Value);
        
        public Optional(T value)
        {
            Value = value;
        }
        public static implicit operator Optional<T>(T d)  => new Optional<T>(d);
        public static implicit operator T(Optional<T> d)
        {
            if (d.IsEmpty) throw new InvalidOperationException("Optional value is empty!");
            return d.Value;
        }
    }
    
}