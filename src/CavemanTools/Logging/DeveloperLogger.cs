using System;

namespace CavemanTools.Logging
{
    public class DeveloperLogger:IWriteToLog
    {
        private Action<string> _writer;

        public DeveloperLogger(Action<string> writer)
        {
            _writer = writer;
        }

       
        public void Log(string source, LogLevel level, string message, params object[] args)
        {
            _writer(level + " | " + DateTime.Now.ToString() + " | " +source+" " +(args.Length==0?message:string.Format(message, args)));
        }

        public void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
           Log(source,level,context +"\n"+ex.ToString(),args);
        }
    }
}