using System.Collections.Generic;

namespace CavemanTools.Infrastructure
{

	/// <summary>
	/// Used to return a result when invoking an application service. Only for actions that change data.
	/// </summary>
	public class CommandResult
    {       
        public bool HasErrors
        {
            get { return Errors.Count != 0; }
        }

        public IDictionary<string,string> Errors { get; }
   

        public CommandResult()
        {
            Errors=new Dictionary<string, string>();
        }

        public void AddError(string key, string msg)
        {
            Errors.Add(key,msg);
        }                      

    }


    /// <summary>
    /// Used to return a result when invoking an application service. Only for actions that change data.
    /// </summary>
    public class CommandResult<T> : CommandResult
	{
		public CommandResult()
		{

		}
        public CommandResult(T value)
		{
			Value = value;
		}
		public T? Value { get; init; }
	}

   

}