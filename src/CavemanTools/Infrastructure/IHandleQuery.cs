namespace CavemanTools.Infrastructure
{
    public interface IHandleQuery<TInput, TOutput> : IHandleRequest<TInput, TOutput> where TInput:class where TOutput:class
    {
    }
}