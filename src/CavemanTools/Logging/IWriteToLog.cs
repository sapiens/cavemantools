using System;


namespace CavemanTools.Logging
{
    public interface IWriteToLog
    {
      
        void Log(string source,LogLevel level, string message, params object[] args);
        
        void LogException(string source, LogLevel level, Exception ex,string context,params object[] args);
                
    }

    //public interface ILogDetail
    //{
    //    void Write(string message);
    //    void Write<T>(string message,T arg);
    //    void Write<T,T1>(string message,T arg1,T1 arg2);
    //    void Write(Exception ex,string message);
    //    void Write<T>(Exception ex,string message,T arg);
    //    void Write<T,T1>(Exception ex,string message,T arg1,T1 arg2);
    //}
    

    //public interface ILogForContext
    //{
    //    ILogDetail Debug { get; }
    //    ILogDetail Info { get; }
    //    ILogDetail Warn { get; }
    //    ILogDetail Error { get; }
    //    ILogDetail Fatal { get; }
    //}
    
    //public class Serilog : LogForContext
    //{
    //    public Serilog(ILogger log)
    //    {
            
    //        Debug=new ActualLogWriter(s=>
    //                log.Debug(s)
    //            ,(s,a)=>log.Debug(s,a)                
    //            ,(s,a,b)=>log.Debug(s,a,b)
    //            ,(e,s)=> log.Debug(e,s)
    //            ,(e,s,a)=>log.Debug(e,s,a)
    //            ,(e,s,a,b)=>log.Debug(e,s,a,b)
                
    //            );
    //        Info=new ActualLogWriter(s=>
    //                log.Information(s)
    //            ,(s,a)=>log.Information(s,a)                
    //            ,(s,a,b)=>log.Information(s,a,b)
    //            ,(e,s)=> log.Information(e,s)
    //            ,(e,s,a)=>log.Information(e,s,a)
    //            ,(e,s,a,b)=>log.Information(e,s,a,b)
                
    //            );
    //        Warn=new ActualLogWriter(s=>
    //                log.Warning(s)
    //            ,(s,a)=>log.Warning(s,a)                
    //            ,(s,a,b)=>log.Warning(s,a,b)
    //            ,(e,s)=> log.Warning(e,s)
    //            ,(e,s,a)=>log.Warning(e,s,a)
    //            ,(e,s,a,b)=>log.Warning(e,s,a,b)
                
    //            );
    //        Error=new ActualLogWriter(s=>
    //                log.Error(s)
    //            ,(s,a)=>log.Error(s,a)                
    //            ,(s,a,b)=>log.Error(s,a,b)
    //            ,(e,s)=> log.Error(e,s)
    //            ,(e,s,a)=>log.Error(e,s,a)
    //            ,(e,s,a,b)=>log.Error(e,s,a,b)
                
    //            );
    //     Fatal=new ActualLogWriter(s=>
    //                log.Fatal(s)
    //            ,(s,a)=>log.Fatal(s,a)                
    //            ,(s,a,b)=>log.Fatal(s,a,b)
    //            ,(e,s)=> log.Fatal(e,s)
    //            ,(e,s,a)=>log.Fatal(e,s,a)
    //            ,(e,s,a,b)=>log.Fatal(e,s,a,b)
                
    //            );

    //    }
    //}

    //public class ActualLogWriter : ILogDetail
    //{
    //    private Action<string> _write0;
    //    private readonly Action<string, dynamic> _write1;
    //    private readonly Action<string, dynamic, dynamic> _write2;
    //    private readonly Action<Exception, string> _writex0;
    //    private readonly Action<Exception, string, dynamic> _writex1;
    //    private readonly Action<Exception, string, dynamic, dynamic> _writex2;

    //    public ActualLogWriter(Action<string> write0
    //        ,Action<string,dynamic> write1
    //        ,Action<string,dynamic,dynamic> write2
    //        ,Action<Exception,string> writex0
    //        ,Action<Exception,string,dynamic> writex1
    //        ,Action<Exception,string,dynamic,dynamic> writex2
    //        )
    //    {
    //        write0.MustNotBeNull();
    //        writex0.MustNotBeNull();
    //        write1.MustNotBeNull();
    //        writex1.MustNotBeNull();
    //        write2.MustNotBeNull();
    //        writex2.MustNotBeNull();
    //        _write0 = write0;
    //        _write1 = write1;
    //        _write2 = write2;
    //        _writex0 = writex0;
    //        _writex1 = writex1;
    //        _writex2 = writex2;
    //    }

    //    public void Write(string message) => _write0(message);


    //    public void Write<T>(string message, T arg) => _write1(message, arg);


    //    public void Write<T, T1>(string message, T arg1, T1 arg2) => _write2(message, arg1, arg2);
    //    public void Write(Exception ex, string message) => _writex0(ex, message);


    //    public void Write<T>(Exception ex, string message, T arg) => _writex1(ex, message, arg);


    //    public void Write<T, T1>(Exception ex, string message, T arg1, T1 arg2) => _writex2(ex, message, arg1, arg2);


    //}

    //public class LogForContext : ILogForContext
    //{
    //    public LogForContext()
    //    {
            
    //    }
    //    public ILogDetail Debug { get; protected set; }
    //    public ILogDetail Info { get; protected set; }
    //    public ILogDetail Warn { get; protected set; }
    //    public ILogDetail Error { get; protected set; }
    //    public ILogDetail Fatal { get; protected set; }
    //}

    //public class DeveloperContextLog : LogForContext
    //{
    //    public DeveloperContextLog(string source,Action<string> write)
    //    {
    //        var format=$"{DateTime.Now}|{}{source}|"
    //        Debug=new ActualLogWriter(
    //            s=>write(source+"")
    //            );
    //    }
    //}

}