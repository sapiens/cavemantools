//using System;
//using System.Runtime.Caching;

//namespace CavemanTools.Infrastructure
//{
//    public class NullCache : ICacheData
//    {
//        public static readonly NullCache Instance=new NullCache();
        
//        private NullCache()
//        {
           
//        }
      
//        public bool Contains(string key)
//        {
//            return false;
//        }

//        public object Get(string key, DateTimeOffset? newExpiration = null)
//        {
//            return null;
//        }

//        /// <summary>
//        /// Gets typed object from cache or the supplied default value if the value doesn't exist.
//        /// Optionally set a new expiration date
//        /// </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <param name="key"></param>
//        /// <param name="defaultValue">Value to return if the object doesn't exist in cache</param>
//        /// <returns></returns>
//        public T Get<T>(string key, T defaultValue, DateTimeOffset? newExpiration = null)
//        {
//            return defaultValue;
//        }


//        /// <summary>
//        /// Tries to add value to cache.
//        /// Returns false if the value already exists
//        /// </summary>
//        /// <param name="key"></param>
//        /// <param name="value"></param>
//        /// <param name="slidingExpiration"></param>
//        /// <param name="monitor"></param>
//        /// <returns></returns>
//        public object Add(string key, object value, TimeSpan slidingExpiration, ChangeMonitor monitor = null)
//        {
//            return false;
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
//            return null;
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
           
//        }

//        public object Remove(string key)
//        {
//            return null;
//        }

//        public void Refresh(string key, DateTimeOffset absoluteExpiration)
//        {
           
//        }

//        /// <summary>
//        /// Returns the implementor object of this interface.
//        ///  </summary>
//        /// <typeparam name="T"></typeparam>
//        /// <exception cref="NotSupportedException"></exception>
//        /// <exception cref="InvalidCastException"></exception>
//        /// <returns></returns>
//        public T GetUnderlyingCacheAs<T>() where T : class
//        {
//            throw new NotSupportedException();
//        }
//    }
//}