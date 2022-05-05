namespace CavemanTools.Infrastructure;

/// <summary>
/// Used to return a result from an external off-site service
/// </summary>
public class ServiceResult
{
        public static readonly ServiceResult Ok = new ServiceResult() { IsSuccessful = true };
        public static readonly ServiceResult Failed = new ServiceResult() { IsSuccessful = false };
        
        public bool IsSuccessful { get; init; }
}

/// <summary>
/// Used to return a result from an external service
/// </summary>
public class ServiceResult<T> : ServiceResult
{
	public static readonly ServiceResult<T> Fail = new ServiceResult<T>();
	private ServiceResult()
	{
		IsSuccessful = false;
	}

	public ServiceResult(T data)
	{
		Data = data;
		IsSuccessful = true;
	}
	public T? Data { get; init; }
}