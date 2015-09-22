namespace CavemanTools.Infrastructure
{
    public class YesNoResult
    {
        public bool Value { get; private set; }

        public YesNoResult(bool value)
        {
            Value = value;
        }
    }
}