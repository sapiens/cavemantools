using System;

namespace CavemanTools.Infrastructure
{
    public interface IContainerScope:IResolveDependencies,IDisposable
    {
        IContainerScope BeginLifetimeScope();
    }
}