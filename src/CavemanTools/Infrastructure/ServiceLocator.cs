using System;

namespace CavemanTools.Infrastructure
{
    public static class ServiceLocator
    {
        private static IContainerScope _inst;
        public static IContainerScope Instance
        {
            get
            {
                if (_inst==null) throw new InvalidOperationException("No container was set");
                return _inst;
            }
        }

        public static void Register(IContainerScope container)
        {
            container.MustNotBeNull();
            _inst = container;
        }
    }
}