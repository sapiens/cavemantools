using System.Runtime.CompilerServices;
using CavemanTools.Logging;

namespace System.Threading.Tasks
{
    public static class TasksUtils
    {
        public static void FireAndForget(this Task task,string taskName,Action<Task> errorHandler=null)
        {
            if (errorHandler == null)
            {
                errorHandler = t => taskName.LogError(t.Exception);
            }
            task.ContinueWith(errorHandler, TaskContinuationOptions.OnlyOnFaulted);
        }

        /// <summary>
        /// Thread.Sleep alternative, using Task.Delay
        /// </summary>
        /// <param name="item"></param>
        /// <param name="duration"></param>
        public static void Sleep(this object item, TimeSpan duration)
        {
            Task.Delay(duration).Wait();
        }

        public static Task EmptyTask()
        {
            return Task.WhenAll();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<T> ToTask<T>(this T data) => Task.FromResult(data);
      
        public static ConfiguredTaskAwaitable<T> ConfigureFalse<T>(this Task<T> task) => task.ConfigureAwait(false);
        public static ConfiguredTaskAwaitable ConfigureFalse(this Task task) => task.ConfigureAwait(false);

    }
}