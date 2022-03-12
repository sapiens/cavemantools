namespace CavemanTools.Infrastructure
{
	public class ServiceActionResult<T>:ServiceActionResult
	{
		internal ServiceActionResult():base(false)
		{

		} 
		public ServiceActionResult(T result):base(true)
		{
            Value = result;
		}

		public T Value { get; }
	}

}