
namespace CavemanTools.Infrastructure
{
    public interface IHandleQueryAsync<TInput, TOutput> :IHandleActionAsync<TInput,TOutput> where TOutput : class where TInput : class
    {
      
    }
}