using System.Threading;
using System.Threading.Tasks;

namespace CavemanTools.Infrastructure
{
    public interface IHandleRequestAsync<TInput, TOutput> where TInput : class where TOutput : class
    {
        Task<TOutput> HandleAsync(TInput input,CancellationToken cancel);
    }
}