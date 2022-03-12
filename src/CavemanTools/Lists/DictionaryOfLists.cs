namespace System.Collections.Generic
{
	public class DictionaryOfLists<TKey,TValue>:Dictionary<TKey,List<TValue>>
	{
		public DictionaryOfLists()
		{
			
		}

		public void AddValue(TKey key,TValue value)
		{
			var items = this.GetValueOrCreate(key, () => new List<TValue>());
			items.Add(value);
		}

		public void AddMany(TKey key,IEnumerable<TValue> values)
		{
			var items = this.GetValueOrCreate(key, () => new List<TValue>());
			items.AddRange(values);
		}
	}
}