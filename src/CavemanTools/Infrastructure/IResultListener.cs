using System;
using System.Threading;
using System.Threading.Tasks;

namespace CavemanTools.Infrastructure
{
    public interface IResultListener
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <exception cref="TimeoutException"></exception>
        /// <exception cref="OperationCanceledException"></exception>
        /// <param name="cancel"></param>
        /// <returns></returns>
        Task<T> GetResult<T>(CancellationToken cancel=default(CancellationToken)) where T:class;
    }
}