namespace CavemanTools.Infrastructure
{
	public class ServiceActionResult
	{
        public static ServiceActionResult Success = new ServiceActionResult(true);
        public static ServiceActionResult Failed = new ServiceActionResult(false);
        public static ServiceActionResult<T> CreateFailed<T>() => new ServiceActionResult<T>();
		public ServiceActionResult(bool isSuccess)
		{
            HasFailed = !isSuccess;
            WasSuccessful = isSuccess;
		}
        public bool HasFailed { get; }
		public bool WasSuccessful { get; }
	}

}