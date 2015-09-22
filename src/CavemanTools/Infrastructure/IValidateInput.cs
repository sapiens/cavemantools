namespace CavemanTools.Infrastructure
{
    public interface IValidateInput<TInput> where TInput:class
    {
        ValidatorResult Validate(TInput input);
    }
}