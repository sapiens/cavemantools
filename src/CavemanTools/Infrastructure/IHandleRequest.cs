namespace CavemanTools.Infrastructure
{
    public interface IHandleRequest<in TInput, out TOutput> where TInput : class where TOutput : class
    {
        TOutput Handle(TInput input);
    }
}