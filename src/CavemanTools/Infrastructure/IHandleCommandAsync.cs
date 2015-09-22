namespace CavemanTools.Infrastructure
{
    public interface IHandleCommandAsync<TInput, TResult> : IHandleActionAsync<TInput, TResult> where TResult : class
        where TInput : class
    {
        
    }
}