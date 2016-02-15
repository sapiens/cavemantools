using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace CavemanTools.Model.Persistence
{
    public enum OnExceptionAction
    {
        IgnoreAndContinue,
        IgnoreAndExit,
        Throw
    }

    public class ModelTools
    {


        /// <summary>
        /// Executes the action which should update and store an entity.
        /// If action throws <see cref="NewerVersionExistsException"/> it's retried for the specified number of times.
        /// Returns a result if the action was executed successfully(regardless of the number of tries).
        /// </summary>
        /// <param name="update"></param>
        /// <param name="triesCount"></param>
        /// <param name="wait">How many ms to wait before retrying</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static bool TryUpdateEntity(Action update, int triesCount=10,int wait=100)
        {
             RetryOnException<NewerVersionExistsException>(update,null, triesCount, wait);
            return true;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T">Exception type</typeparam>
        /// <param name="update"></param>
        /// <param name="excHandler">what happens on exception</param>
        /// <param name="triesCount"></param>
        /// <param name="wait"></param>
        /// <param name="throttle">If true after 1/4 of the tries it will increase the wait period before tries</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static async Task RetryOnException<T>(Func<Task> update,Func<T,OnExceptionAction> excHandler=null, int triesCount = 10, int wait = 150,bool throttle=true) where T : Exception
        {
           
            if (excHandler == null)
            {
                excHandler = x => OnExceptionAction.IgnoreAndContinue;
            }

            for (var i = 0; i < triesCount; i++)
            {
                if (i > 0)
                {
                    if (throttle && i > triesCount/4)
                    {
                        wait *= (int) 1.5;
                    }
                    await Task.Delay(wait).ConfigureAwait(false);
                }
                try
                {
                    await update().ConfigureAwait(false);
                    break;
                }
                catch (T ex)
                {
                    switch (excHandler(ex))
                    {
                        case OnExceptionAction.IgnoreAndExit:
                            return;
                        case OnExceptionAction.Throw:
                            throw;
                  
                        default:
                            i++;
                            break;
                    }
                  
                    if (i == triesCount) throw;
                }  
            }
            
         
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="update"></param>
        /// <param name="excHandler">what happens on exception</param>
        /// <param name="triesCount"></param>
        /// <param name="wait"></param>
        /// <param name="throttle">If true after 1/4 of the tries it will increase the wait period before tries</param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static void RetryOnException<T>(Action update, Func<T, OnExceptionAction> excHandler=null,int triesCount = 10, int wait = 100, bool throttle = true) where T : Exception
        {
          
            if (excHandler == null)
            {
                excHandler = x => OnExceptionAction.IgnoreAndContinue;
            }

            for (var i = 0; i < triesCount; i++)
            {
                if (i > 0)
                {
                    if (throttle && i > triesCount / 4)
                    {
                        wait *= (int)1.5;
                    }
                    Task.Delay(wait).Wait();
                }
                try
                {
                    update();
                    break;
                }
                catch (T ex)
                {
                    switch (excHandler(ex))
                    {
                        case OnExceptionAction.IgnoreAndExit:
                            return;
                        case OnExceptionAction.Throw:
                            throw;
                       
                        default:
                            i++;
                            break;
                    }

                    if (i == triesCount) throw;
                }
            }
            
        }

          [DebuggerStepThrough]
        public static T GetBlocking<T>(Func<T> factory,int retries=10,int sleep=100) where T:class 
        {
            T data;
            var i = 0;
            do
            {
                data = factory();
                if (data != null) break;
                i++;
               TasksUtils.Sleep(null,TimeSpan.FromMilliseconds(100));

            } while (i < retries);
            return data;
        }


    }
}