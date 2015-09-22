using System.Collections.Generic;

namespace System.Collections
{
    /// <summary>
    ///  A simple bag object, where a string key can hold a list of string values
    /// </summary>
    public class KeyValueDataStore:IEnumerable<KeyValuePair<string,ICollection<string>>>
    {
        Dictionary<string,List<string>> _items= new Dictionary<string, List<string>>();
        public ICollection<string> this[string key]
        {
            get
            {
                if (string.IsNullOrEmpty(key)) throw new ArgumentNullException("key");
                if (!_items.ContainsKey(key))
                {
                    _items[key]=new List<string>();
                }
                return _items[key];
            }            
        }

        public void Clear()
        {
            _items.Clear();
        }

        public int Count
        {
            get
            {
                return _items.Count;
            }
        }

        public IEnumerable<string> Keys
        {
            get { return _items.Keys; }
        }

        public IEnumerator<KeyValuePair<string, ICollection<string>>> GetEnumerator()
        {
            foreach(var kv in _items)
            {
                yield return new KeyValuePair<string, ICollection<string>>(kv.Key,kv.Value);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}