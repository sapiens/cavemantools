using System;

namespace CavemanTools.Model.Persistence
{
    public interface IStore<T> where T:class,IEntity
    {
        void Add(T entity);
        void Store(T entity);
        T Get(Guid id);
        void Delete(Guid id);
    }
}