using System;

namespace CavemanTools.Logging
{
    public class NullLogger : IWriteToLog
    {
        public static readonly NullLogger Instance=new NullLogger();
        private NullLogger()
        {
            
        }

        public void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
          
        }

        public void Log(string source, LogLevel level, string message, params object[] args)
        {
            
        }
    }
}