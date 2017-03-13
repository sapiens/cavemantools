using System;
using System.Threading;
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
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task AddAsync(UniqueStoreItem item,CancellationToken cancel);

        /// <summary>
        /// Deletes all unique values associated with the entity
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="cancel"></param>
        Task DeleteAsync(Guid entityId, CancellationToken cancel);

        Task DeleteAsync(string bucketId, CancellationToken cancel);

        /// <summary>
        /// Delete values associated with the entity and an aspect
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        Task DeleteAsync(UniqueStoreDeleteItem item, CancellationToken cancel);

        /// <summary>
        /// Updates an unique value of an aspect for an entity
        /// </summary>
        /// <param name="item"></param>
        /// <param name="cancel"></param>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <returns></returns>
        Task UpdateAsync(UniqueStoreUpdateItem item, CancellationToken cancel);
    }
}