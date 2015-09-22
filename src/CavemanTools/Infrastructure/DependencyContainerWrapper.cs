using System;
using System.Collections.Generic;

namespace CavemanTools.Infrastructure
{
    public class DependencyContainerWrapper:IContainerScope
    {
        private readonly Func<Type, object> _resolver;

        public DependencyContainerWrapper(Func<Type,object> resolver)
        {
            _resolver = resolver;
        }

        public object Resolve(Type type)
        {
            return _resolver(type);
        }

        public T Resolve<T>()
        {
            return (T) _resolver(typeof (T));
        }

        /// <summary>
        /// If type is not registered return null
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public object ResolveOptional(Type type)
        {
            return Resolve(type);
        }

        public T ResolveOptional<T>() where T : class
        {
            return (T)ResolveOptional(typeof (T));
        }

        public IEnumerable<T> GetServices<T>()
        {
            yield return Resolve<T>();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            
        }

        public IContainerScope BeginLifetimeScope()
        {
            return this;
        }
    }
}