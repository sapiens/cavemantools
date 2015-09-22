using System.Collections.Generic;

namespace CavemanTools.Infrastructure
{
    public class CommandResult
    {
        public bool HasErrors
        {
            get { return Errors.Count != 0; }
        }

        public IDictionary<string,string> Errors { get; private set; }

        public CommandResult()
        {
            Errors=new Dictionary<string, string>();
        }

        public void AddError(string key, string msg)
        {
            Errors.Add(key,msg);
        }
    }
}