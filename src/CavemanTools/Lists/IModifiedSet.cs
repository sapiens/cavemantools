namespace System.Collections.Generic
{
	/// <summary>
	/// INterface for the result of 2 sequences comparison
	/// </summary>
	/// <typeparam name="T">Implements IEquatable</typeparam>
	public interface IModifiedSet<T>
	{
		/// <summary>
		/// Gets the sequence of items added
		/// </summary>
		IEnumerable<T> Added { get; }

		/// <summary>
		/// Gets the sequence of items removed
		/// </summary>
		IEnumerable<T> Removed { get; }

		/// <summary>
		/// Gets the sequence of items modified
		/// </summary>
		IEnumerable<ModifiedItem<T>> Modified { get; }
		
		/// <summary>
		/// Nothing has been added,removed or modified.
		/// </summary>
		bool IsEmpty { get;}
	}
}