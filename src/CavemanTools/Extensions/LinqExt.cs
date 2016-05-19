using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;


namespace System.Linq
{
	public static class LinqExt
	{
	
		/// <summary>
		/// Executes function for each sequence item
		/// </summary>
		/// <typeparam name="TSource">Sequence</typeparam>
		/// <param name="source">Function to execute</param>
		/// <param name="action"></param>
		/// <returns></returns>
         [DebuggerStepThrough]
    	public static void ForEach<TSource>(this IEnumerable<TSource> source,Action<TSource> action) 
		{
			if (source == null) throw new ArgumentNullException("source");
			if (action == null) throw new ArgumentNullException("action");
			foreach (var b in source)
			{
				action(b);
			}
		}
        [DebuggerStepThrough]
         public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<int,TSource> action)
         {
             if (source == null) throw new ArgumentNullException("source");
             if (action == null) throw new ArgumentNullException("action");
             var i = 0;
             foreach (var b in source)
             {
                 action(i,b);
                 i++;
             }
         }



        /// <summary>
        /// Tries to cast each item to the specified type. If it fails,  it just ignores the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        [DebuggerStepThrough]
        public static IEnumerable<T> CastSilentlyTo<T>(this IEnumerable source) where T:class
        {
            T res = null;
            foreach(var item in source)
            {
                res = item as T;
                if (res == null) continue;
                yield return res;
            }            
        }
        [DebuggerStepThrough]
	    public static IEnumerable<T> FilterNulls<T>(this IEnumerable<T> src) where T : class
	    {
	        return src.Where(d => d != null);
	    }
	}
}