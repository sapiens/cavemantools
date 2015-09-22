namespace CavemanTools.Infrastructure
{
    public interface IHandleAction<TInput, TOutput> where TInput : class where TOutput : class
    {
        TOutput Handle(TInput input);
    }
}