using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public interface IGenericUniqueStore
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <param name="item"></param>
        /// <returns></returns>
        void Add(UniqueStoreItem item);
        /// <summary>
        /// Deletes or unique values associated with the entity
        /// </summary>
        /// <param name="entityId"></param>
        void Delete(Guid entityId);
        /// <summary>
        /// Delete values associated with the entity and an aspect
        /// </summary>
        /// <param name="item"></param>
        void Delete(UniqueStoreDeleteItem item);
        /// <summary>
        /// Updates an unique value of an aspect for an entity
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <returns></returns>
        void Update(UniqueStoreUpdateItem item);

        IUnitOfWork StartUnitOfWork();
    }

    public class UniqueStoreDuplicateException : Exception
    {
        public UniqueStoreDuplicateException()
        {
        }

        public UniqueStoreDuplicateException(string message) : base(message)
        {
        }
    }
}