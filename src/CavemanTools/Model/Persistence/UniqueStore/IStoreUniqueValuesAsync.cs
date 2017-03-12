using System;
using System.Threading.Tasks;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public interface IStoreUniqueValuesAsync
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <param name="item"></param>
        /// <returns></returns>
        Task AddAsync(UniqueStoreItem item);
        /// <summary>
        /// Deletes all unique values associated with the entity
        /// </summary>
        /// <param name="entityId"></param>
        Task DeleteAsync(Guid entityId);
       
        Task DeleteAsync(string bucketId);
        /// <summary>
        /// Delete values associated with the entity and an aspect
        /// </summary>
        /// <param name="item"></param>
        Task DeleteAsync(UniqueStoreDeleteItem item);
        /// <summary>
        /// Updates an unique value of an aspect for an entity
        /// </summary>
        /// <param name="item"></param>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <returns></returns>
        Task UpdateAsync(UniqueStoreUpdateItem item);
    }
}