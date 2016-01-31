//using System;
//using System.Runtime.Caching;

//namespace CavemanTools.Infrastructure
//{
//    public class LocalMemoryCache:ICacheData
//    {
//        private readonly MemoryCache _cache;

//        public LocalMemoryCache():this(MemoryCache.Default)
//        {
            
//        }

//        public LocalMemoryCache(MemoryCache cache)
//        {
//            cache.MustNotBeNull();
//            _cache = cache;
//        }

//        public bool Contains(string key)
//        {
//            return _cache.Contains(key);
//        }

//        public object Get(string key, DateTimeOffset? newExpiration = null)
//        {
//            var item = _cache.Get(key);
//            if (item!=null)
//            {
//                if (newExpiration.HasValue)
//            {
//                Set(key,item,newExpiration.Value);
//            }}
//            return item;
//        }

//        /// <summary>
//        /// Gets typed object from cache or the supplied default value if the value doesn't exist.
//        /// Optionally set a new expiration date.
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="defaultValue">Value to return if the object doesn't exist in cache</param>
//        /// <returns></returns>
//        public T Get<T>(string key, T defaultValue = default(T), DateTimeOffset? newExpiration = null)
//        {
//            var data = Get(key, newExpiration);
//            if (data != null)
//            {
//                return (T) data;
//            }
//            return defaultValue;
//        }

//        /// <summary>
//        /// Tries to add value to cache.
//        /// Returns the existing value if any
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="slidingExpiration"></param>
//        /// <param name="monitor"></param>
//        /// <returns></returns>
//        public object Add(string key, object value, TimeSpan slidingExpiration, ChangeMonitor monitor = null)
//        {
//            var pol = new CacheItemPolicy();
//            pol.SlidingExpiration = slidingExpiration;
//            if (monitor != null)
//            {
//                pol.ChangeMonitors.Add(monitor);
//            }
//            return _cache.AddOrGetExisting(key, value, pol);            
//        }

//        /// <summary>
//        /// Tries to add value to cache.
//        /// Returns the existing value if any
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="absoluteExpiration"></param>
//        /// <param name="monitor"></param>
//        public object Add(string key, object value, DateTimeOffset absoluteExpiration, ChangeMonitor monitor = null)
//        {
//            var pol = new CacheItemPolicy();
//            pol.AbsoluteExpiration = absoluteExpiration;
//            if (monitor != null)
//            {
//                pol.ChangeMonitors.Add(monitor);
//            }
//            return _cache.AddOrGetExisting(key, value, pol);            
//        }

//        /// <summary>
//        /// Adds or updates a key with the provided value.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="slidingExpiration"></param>
//        /// <param name="monitor"></param>
//        /// <returns></returns>
//        public void Set(string key, object value, TimeSpan slidingExpiration, ChangeMonitor monitor = null)
//        {
//            var pol = new CacheItemPolicy();
//            pol.SlidingExpiration = slidingExpiration;
//            if (monitor != null)
//            {
//                pol.ChangeMonitors.Add(monitor);
//            }
//            _cache.Set(key, value, pol);      
//        }

//        /// <summary>
//        /// Adds or updates a key with the provided value.
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="absoluteExpiration"></param>
//        /// <param name="monitor"></param>
//        public void Set(string key, object value, DateTimeOffset absoluteExpiration, ChangeMonitor monitor = null)
//        {
//            var pol = new CacheItemPolicy();
//            pol.AbsoluteExpiration = absoluteExpiration;
//            if (monitor != null)
//            {
//                pol.ChangeMonitors.Add(monitor);
//            }
//            _cache.Set(key, value, pol);      
//        }

//        public object Remove(string key)
//        {
//            return _cache.Remove(key);
//        }

//        public void Refresh(string key, DateTimeOffset absoluteExpiration)
//        {
//            Get(key, absoluteExpiration);
//        }

//        /// <summary>
//        /// Returns the underlying cache (the real caching object) used by this adapter.
//        ///  </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <exception cref="NotSupportedException"></exception>
//        /// <exception cref="InvalidCastException"></exception>
//        /// <returns></returns>
//        public T GetUnderlyingCacheAs<T>() where T : class
//        {
//            var rez = _cache as T;
//            if (rez==null) throw new InvalidCastException();
//            return rez;
//        }
//    }
//}