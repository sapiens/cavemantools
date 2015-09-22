using System;

namespace CavemanTools.Infrastructure
{
    public class ActivatorContainer:DependencyContainerWrapper
    {
        static IContainerScope _inst= new ActivatorContainer();
        public static IContainerScope Instance
        {
            get { return _inst; }
        }
        private ActivatorContainer() : base(t =>
            {
                if (t.IsInterface || t.IsAbstract) return null;
                var res = Activator.CreateInstance(t);
                return res;
            })
        {
        }
    }
}