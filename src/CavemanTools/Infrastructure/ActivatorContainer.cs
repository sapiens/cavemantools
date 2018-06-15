using System;
using System.Reflection;

namespace CavemanTools.Infrastructure
{
    public class ActivatorContainer:DependencyContainerWrapper
    {
        static IContainerScope _inst= new ActivatorContainer();
        public static IContainerScope Instance => _inst;

        private ActivatorContainer() : base(t =>
            {


                var ti = t.GetTypeInfo();
                if (ti.IsInterface || ti.IsAbstract) return null;

                var res = Activator.CreateInstance(t);
                return res;
            })
        {
        }
    }
}