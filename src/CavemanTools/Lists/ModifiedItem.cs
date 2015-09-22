namespace System.Collections.Generic
{
	/// <summary>
	/// Pair of old and new objects. Used by list comparator
	/// </summary>
	/// <typeparam name="T"></typeparam>
	public struct ModifiedItem<T>
	{
		public ModifiedItem(T old, T @new)
		{
			_old = old;
			_new = @new;
		}

		private T _old;
		public T Old
		{
			get { return _old; }			
		}

		private T _new;
		public T New
		{
			get { return _new; }
		}
	}
}