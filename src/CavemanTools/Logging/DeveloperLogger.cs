using System;

namespace CavemanTools.Logging
{
    public class DeveloperLogger:LogWriterBase
    {
        private Action<string> _writer;

        public override T GetLogger<T>()
        {
            throw new System.NotImplementedException();
        }

        public DeveloperLogger(Action<string> writer)
        {
            _writer = writer;
        }

       
        public override void Log(string source, LogLevel level, string message, params object[] args)
        {
            _writer(level + " | " + DateTime.Now.ToString() + " | " +source+" " +(args.Length==0?message:string.Format(message, args)));
        }

        public override void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
           Log(source,level,context +"\n"+ex.ToString(),args);
        }
    }
}