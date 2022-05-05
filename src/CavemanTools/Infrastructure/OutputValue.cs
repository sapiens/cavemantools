namespace CavemanTools.Infrastructure
{
    /// <summary>
    /// Type encapsulating a value type. Used with query handlers
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class OutputValue<T>
    {
        public T Value { get; set; }       

        public OutputValue(T value)
        {
            Value = value;
        }
    }
}