using System.Collections.Generic;

namespace CavemanTools.Model
{
	/// <summary>
	/// Paginated result set with the total number of items from a query.
	/// Used for pagination.
	/// </summary>
	/// <typeparam name="T">Type of item</typeparam>
	public interface IPagedResult<T>
	{
		/// <summary>
		/// Gets the total number of items
		/// </summary>
		int Count { get;}

        /// <summary>
        /// Gets total number of items
        /// </summary>
        long LongCount { get; }

		/// <summary>
		/// Gets result items
		/// </summary>
		IEnumerable<T> Items{ get;}	
	}
}