using System;

namespace CavemanTools.Logging
{
    public static class LogManager
    {
        public static IWriteToLog Writer=NullLogger.Instance;

        public static void SetWriter(IWriteToLog writer)
        {
            writer.MustNotBeNull();
            Writer = writer;
        }

        /// <summary>
        /// Sets the default logger to be consoler. 
        /// Logger name is "console"
        /// </summary>
        public static void OutputToConsole()
        {
            Writer=new ConsoleLogger();
        }

        public static void OutputToTrace()
        {
            OutputTo(s=>System.Diagnostics.Trace.WriteLine(s));
        }

        /// <summary>
        /// Sends all the logging to the writer.
        /// Logger name is "devel"
        /// </summary>
        /// <param name="writer"></param>
        public static void OutputTo(Action<string> writer)
        {
            Writer=new DeveloperLogger(writer);
        }

        #region Extensions

        public static void Log<T>(this T source, LogLevel level, string message, params object[] args)
        {
            Writer.Log(LogWriterBase.SourceToString(source),level,message,args);
        }

        public static void LogDebug<T>(this T source, string message, params object[] args)
        {
            source.Log(LogLevel.Debug, message,args);
        }
        
        public static void LogInfo<T>(this T source, string message, params object[] args)
        {
            source.Log(LogLevel.Info, message,args);
        }
        
        public static void LogWarn<T>(this T source, string message, params object[] args)
        {
            source.Log(LogLevel.Warn, message,args);
        }
        
        public static void LogError<T>(this T source, string message, params object[] args)
        {
            source.Log(LogLevel.Error, message,args);
        }

        public static void LogError<T>(this T source, Exception ex)
        {
           Writer.LogException(LogWriterBase.SourceToString(source),LogLevel.Error, ex,"");
        }
        
        //public static void LogError<T>(this T source, Exception ex,string context)
        //{
        //   Writer.LogException(source,LogLevel.Error, ex);
        //}
    
        public static void LogError<T>(this T source, Exception ex,string context,params object[] args)
        {
           Writer.LogException(LogWriterBase.SourceToString(source),LogLevel.Error, ex,context,args);
        }

        //public static void LogError<T>(this T source, Exception ex,string msg,params object[] args)
        //{
        //    LogError(source,"Exception thrown: "+ex.GetType().Name+"\n"+msg, args);
        //    Writer.LogException(source,LogLevel.Error, ex);
        //}

        #endregion
    }
}