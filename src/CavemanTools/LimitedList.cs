using System.Linq;


namespace CavemanTools
{
    /// <summary>
    /// Used to maintain a fixed sized list for history purposes.
    /// Intended to support idempotency (by storing last n message ids)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedList<T>
    {
        private readonly int _size;
        private int _i;

        public LimitedList():this(5)
        {
            
        }

        public LimitedList(T[] data)
        {
            Data = data;
            _size = data.Length;
            _i = data.Length;
        }

        public T[] Data { get; private set; }

        public LimitedList(int size)
        {
            _size = size;
            Data = new T[size];
            _i = 0;
        }

        public void Add(T item)
        {
            if (_i == _size)
            {
                _i = 0;
            }
            Data[_i] = item;
            _i++;
        }

        public bool Contains(T item)
        {
            return Data.Contains(item);
        }
    }
}