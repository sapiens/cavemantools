using System;
#if COREFX
using System.Reflection;
#endif

namespace CavemanTools.Infrastructure
{
    public class ActivatorContainer:DependencyContainerWrapper
    {
        static IContainerScope _inst= new ActivatorContainer();
        public static IContainerScope Instance => _inst;

        private ActivatorContainer() : base(t =>
            {

#if COREFX
                var ti = t.GetTypeInfo();
                if (ti.IsInterface || ti.IsAbstract) return null;
#else
                if (t.IsInterface || t.IsAbstract) return null;
#endif
                var res = Activator.CreateInstance(t);
                return res;
            })
        {
        }
    }
}