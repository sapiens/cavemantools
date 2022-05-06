namespace System.Collections.Generic
{
	public class DictionaryOfLists<TKey,TValue>:Dictionary<TKey,List<TValue>> where TKey:notnull
	{
		public DictionaryOfLists()
		{
			
		}

		public DictionaryOfLists(IEnumerable<KeyValuePair<TKey,TValue[]>> data)
		{
			foreach(var item in data)
			{
				var values = new List<TValue>(item.Value);
				this[item.Key]= values;
			}
		}

		/// <summary>
		/// Adds value to the list specified by the key
		/// </summary>
		/// <param name="key"></param>
		/// <param name="value"></param>
		public void AddValue(TKey key,TValue value)
		{
			var items = this.GetValueOrCreate(key, () => new List<TValue>());
			items.Add(value);
		}

		/// <summary>
		/// Adds multiple values to the list specified by the key
		/// </summary>
		/// <param name="key"></param>
		/// <param name="values"></param>
		public void AddMany(TKey key,IEnumerable<TValue> values)
		{
			var items = this.GetValueOrCreate(key, () => new List<TValue>());
			items.AddRange(values);
		}
	}
}