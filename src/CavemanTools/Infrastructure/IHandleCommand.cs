namespace CavemanTools.Infrastructure
{
    public interface IHandleCommand<TInput, TResult> : IHandleAction<TInput, TResult> where TResult : class
        where TInput : class
    {
        
    }
}