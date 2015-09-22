using System;
using System.Collections.Generic;
using System.Linq;

namespace CavemanTools.Model.Persistence
{
    public class MemoryCollection : List<object>
    {
        public class VersionedItem<T> where T:IEntity
        {
            public Guid Id { get; set; }
            public T Value { get; set; }
            public long Timestamp { get; set; }
            public int Version { get; set; }

            public VersionedItem(T value)
            {
                Id = value.Id;
                Value = value;
                Version = 1;
                Timestamp = DateTime.UtcNow.Ticks;
            }
        }

        public IEnumerable<T> GetItems<T>() where T : IEntity
        {
            return this.OfType<VersionedItem<T>>().Select(d => d.Value).Union(this.OfType<T>());
        }

        public void Remove<T>(Func<T, bool> match) where T:class
        {
            this.RemoveAll(o =>
            {
                var c = o as T;
                if (c == null) return false;
                return match(c);
            });
        }
    }
}