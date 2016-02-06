using System;

namespace CavemanTools.Logging
{
    public class NullLogger : LogWriterBase
    {
        public static readonly NullLogger Instance=new NullLogger();
        private NullLogger()
        {
            
        }

        public override T GetLogger<T>()
        {
            throw new NotImplementedException();
        }


        public override void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
          
        }

        public override void Log(string source, LogLevel level, string message, params object[] args)
        {
            
        }
    }
}