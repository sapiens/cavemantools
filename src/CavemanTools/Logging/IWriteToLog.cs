using System;

namespace CavemanTools.Logging
{
    public interface IWriteToLog
    {
      
        void Log(string source,LogLevel level, string message, params object[] args);
        
        void LogException(string source, LogLevel level, Exception ex,string context,params object[] args);
                
    }
}