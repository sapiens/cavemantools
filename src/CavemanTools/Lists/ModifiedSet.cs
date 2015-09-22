namespace System.Collections.Generic
{
	/// <summary>
	/// Default implementation of IModifiedSet.
	/// The result of 2 sequences comparison
	/// </summary>
	/// <typeparam name="T">Implements IEquatable</typeparam>
	public class ModifiedSet<T>:IModifiedSet<T>
	{
		List<T> _add = new List<T>();
		List<T> _remove = new List<T>();
		List<ModifiedItem<T>>_mods= new List<ModifiedItem<T>>();

		public IEnumerable<T> Added
		{
			get
			{
				return _add;
			}
		}

		public IEnumerable<T> Removed
		{
			get
			{
				return _remove;
			}
		}

		public IEnumerable<ModifiedItem<T>> Modified
		{
			get { return _mods; }
		}

		public bool IsEmpty
		{
			get { return _add.Count == 0 && _remove.Count == 0 && _mods.Count==0; }
		}

		public void ModifiedItem(T old,T @new)
		{
			_mods.Add(new ModifiedItem<T>(old,@new));
		}

		public void AddedItem(T tag)
		{
			_add.Add(tag);
		}

		public void RemovedItem(T tag)
		{
			_remove.Add(tag);
		}
	}
}