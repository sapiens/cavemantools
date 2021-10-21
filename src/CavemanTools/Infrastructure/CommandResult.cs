using System.Collections.Generic;

namespace CavemanTools.Infrastructure
{
    public class CommandResult
    {
        public bool HasErrors
        {
            get { return Errors.Count != 0; }
        }

        public IDictionary<string,string> Errors { get; }

        public Dictionary<string, object> Data => _data;

        public CommandResult()
        {
            Errors=new Dictionary<string, string>();
        }

        public void AddError(string key, string msg)
        {
            Errors.Add(key,msg);
        }

        Dictionary<string,object> _data=new Dictionary<string, object>();

        public object this[string key]
        {
            get { return _data.GetValueOrDefault(key); }
            set { _data[key] = value; }
        }

        public T Get<T>(string key, T defValue = default(T)) => (T)_data.GetValue(key,defValue);

    }
}