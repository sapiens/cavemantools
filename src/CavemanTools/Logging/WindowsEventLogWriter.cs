using System;
using System.Diagnostics;

namespace CavemanTools.Logging
{
    //public class WindowsEventLogWriter:LogWriterBase
    //{
    //    private readonly EventLog log;

    //    public WindowsEventLogWriter(string src ):this("Application",src)
    //    {

    //    }
    //    public WindowsEventLogWriter(string logname, string src)
    //    {
    //        log = new EventLog();
    //        log.Log = logname;
    //        if (string.IsNullOrEmpty(src)) src = "Application";
    //        log.Source = src;
    //    }

    //    public override T GetLogger<T>()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override void Log(LogLevel level, string text)
    //    {
    //        Log(level,text,null);
    //    }

    //    public override void Log(LogLevel level, string message, params object[] args)
    //    {
    //        message = args == null ? message : string.Format(message, args);
    //        log.WriteEntry(message,Translate(level));
    //    }

    //    EventLogEntryType Translate(LogLevel level)
    //    {
    //        switch (level)
    //        {
    //            case LogLevel.Trace:
    //            case LogLevel.Debug:
    //            case LogLevel.Info:return EventLogEntryType.Information;
    //            case LogLevel.Warn:return EventLogEntryType.Warning;
    //            case LogLevel.Fatal:
    //            case LogLevel.Error:return EventLogEntryType.Error;
    //        }

    //        throw new NotSupportedException();
    //    }
    //}
}
