namespace CavemanTools.Infrastructure
{
    public interface IHandleQuery<TInput, TOutput> : IHandleAction<TInput, TOutput> where TInput:class where TOutput:class
    {
    }
}