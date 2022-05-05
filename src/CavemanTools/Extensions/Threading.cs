using System.Runtime.CompilerServices;


namespace System.Threading.Tasks
{
    public static class TasksUtils
    {
        public static void FireAndForget(this Task task,string taskName,Action<Task> errorHandler=null)
        {
            if (errorHandler == null)
            {
                errorHandler = t => { };
            }
            task.ContinueWith(errorHandler, TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<T> ToTask<T>(this T data) => Task.FromResult(data);
      
       

    }
}