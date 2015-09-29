using System;

namespace CavemanTools.Model.ValueObjects
{
    public abstract class AReadOnlyValue<T> : Tuple<T>
    {
        protected AReadOnlyValue(T item1) : base(item1)
        {
        }

        public T Value => Item1;
    }
}