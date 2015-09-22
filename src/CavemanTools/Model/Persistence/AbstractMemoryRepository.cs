using System;
using System.Collections.Generic;
using System.Linq;

namespace CavemanTools.Model.Persistence
{
    public abstract class AbstractMemoryRepository<T> : IStore<T> where T : class,IEntity
    {
        protected IEnumerable<T> GetItems()
        {
            return _data.GetItems<T>();
        }

        protected readonly MemoryCollection _data;

        protected AbstractMemoryRepository(MemoryCollection data)
        {
            _data = data;
        }

        public void Add(T entity)
        {
            if (_data.OfType<MemoryCollection.VersionedItem<T>>().Any(d=>d.Id==entity.Id)) return;
            
            _data.Add(new MemoryCollection.VersionedItem<T>(entity));
        }

        public void Store(T entity)
        {
           
            int ver=0;

            _versions.TryGetValue(entity.Id, out ver);

            var item = _data.OfType<MemoryCollection.VersionedItem<T>>().FirstOrDefault(d=>d.Id==entity.Id);
            item.MustNotBeNull(msg: "Entity doesn't exits. Maybe you want to add it");
            if ( ver>0 && item.Version > ver)
            {
                throw new NewerVersionExistsException();
            }
            item.Value = entity;
            item.Version++;
        }

        Dictionary<Guid,int> _versions=new Dictionary<Guid, int>();

        public T Get(Guid id)
        {
            var item= _data.OfType<MemoryCollection.VersionedItem<T>>().FirstOrDefault(d => d.Id == id);
            if (item == null) return null;
            _versions[id] = item.Version;
            return item.Value;
        }

        public void Delete(Guid id)
        {
            _data.RemoveAll(d =>(d is MemoryCollection.VersionedItem<T>) && (d as MemoryCollection.VersionedItem<T>).Id == id);
        }
    }
}