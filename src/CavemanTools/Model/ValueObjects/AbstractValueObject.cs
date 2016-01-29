using System;

namespace CavemanTools.Model.ValueObjects
{
    public abstract class AbstractValueObject<T>
    {
        protected T _value;

        protected AbstractValueObject()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        /// <param name="value"></param>
        protected AbstractValueObject(T value)
        {
            if (!Validate(value)) throw new ArgumentException();
            _value = value;
        }

        /// <summary>
        /// Is automatically invoked by the constructor
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected abstract bool Validate(T value);

        public T Value => _value;


        public override string ToString() => $"[{GetType().Name}]{_value}";


        public override int GetHashCode() => _value.GetHashCode();

        public override bool Equals(object obj)
        {
            var d = obj as AbstractValueObject<T>;
            return d != null && d._value.Equals(_value);
        }
    }
}