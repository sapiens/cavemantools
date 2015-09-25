
namespace CavemanTools.Infrastructure
{
    public interface IHandleQueryAsync<TInput, TOutput> :IHandleRequestAsync<TInput,TOutput> where TOutput : class where TInput : class
    {
      
    }
}