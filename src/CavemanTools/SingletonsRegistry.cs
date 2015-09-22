using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace CavemanTools
{
    public static class SingletonsRegistry
    {
       static Dictionary<Type,object>  _singletons= new Dictionary<Type, object>();

        static object SyncRoot
        {
            get { return ((IDictionary) _singletons).SyncRoot; }
        }

        /// <summary>
        /// Registers a singleton
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <exception cref="InvalidOperationException">If an instance already exists</exception>
        /// <param name="instance"></param>
        public static void Register<T>(T instance) where T:class
        {
            lock (SyncRoot)
            {
            if (_singletons.ContainsKey(typeof(T))) throw new InvalidOperationException(string.Format("An instance of type {0} is already registered",typeof(T).Name));            
                _singletons.Add(typeof(T), instance);    
            }
            
        }
        
        /// <summary>
        /// Returns the instance of type or null
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <returns></returns>
        public static T Get<T>() where T:class
        {
            if (_singletons.ContainsKey(typeof(T))) return _singletons[typeof (T)] as T;
            return null;
        }

        /// <summary>
        /// Unregisters the singleton for the provided type.
        /// </summary>
        /// <typeparam name="T">Reference type</typeparam>
        /// <param name="dispose">true to dispose if the type implements IDisposable</param>
        public static void Unregister<T>(bool dispose=false) where T:class
        {
            var tp = typeof (T);
            lock (SyncRoot)
            {
                if (!_singletons.ContainsKey(tp)) return;
                var obj = _singletons[tp];
                if (dispose && tp.Implements<IDisposable>())
                {
                    (obj as IDisposable).Dispose();
                }
                _singletons.Remove(tp);
            }
        }
    }
}