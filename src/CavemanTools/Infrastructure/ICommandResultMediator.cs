using System;

namespace CavemanTools.Infrastructure
{
    public interface ICommandResultMediator
    {
        void AddResult<T>(Guid cmdId, T result) where T :class;
        /// <summary>
        /// Used to await a command result
        /// </summary>
        /// <param name="cmdId"></param>
        /// <param name="timeout">Default is 5s</param>
        /// <returns></returns>
        IResultListener GetListener(Guid cmdId,TimeSpan? timeout=null);
    }
}