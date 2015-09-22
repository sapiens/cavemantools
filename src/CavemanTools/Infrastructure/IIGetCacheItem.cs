using System;

namespace CavemanTools.Infrastructure
{
    public interface IIGetCacheItem
    {
        bool Contains(string key);
        object Get(string key,DateTimeOffset? newExpiration=null);

        /// <summary>
        /// Gets typed object from cache or the supplied default value if the value doesn't exist.
        /// Optionally set a new expiration date.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="defaultValue">Value to return if the object doesn't exist in cache</param>
        /// <returns></returns>
        T Get<T>(string key,T defaultValue=default(T),DateTimeOffset? newExpiration=null);
    }
}