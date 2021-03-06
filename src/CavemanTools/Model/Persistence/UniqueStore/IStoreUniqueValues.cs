﻿using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public interface IStoreUniqueValues
    {
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="UniqueStoreDuplicateException"></exception>
        /// <param name="item"></param>
        /// <returns></returns>
        void Add(UniqueStoreItem item);
        /// <summary>
        /// Deletes all unique values associated with the entity
        /// </summary>
        /// <param name="entityId"></param>
        void Delete(Guid entityId);
       
        void Delete(string bucketId);
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
    }
}