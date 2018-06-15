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
            _writer($"{level} | {DateTime.Now} | {source}: {message}");
        }

        public void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
           Log(source,level,context +"\n"+ex.ToString(),args);
        }
    }

    //public class StructuralLogger : IWriteToLog
    //{
    //    public StructuralLogger(Action<string,object[]> writer)
    //    {
    //    }

    //    public void Log(string source, LogLevel level, string message, params object[] args)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}