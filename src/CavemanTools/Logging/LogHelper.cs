using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CavemanTools.Logging
{
     /// <summary>
    /// Not thread safe
    /// </summary>
    [Obsolete("Use LogManager")]
	public static class LogHelper
    {
        /// <summary>
        /// Register a log, not thread safe
        /// </summary>
        /// <param name="log"></param>
        /// <param name="name"></param>
        /// <param name="isDefault"></param>
        public static void Register(ILogWriter log,string name,bool isDefault=false)
         {
             if (log == null) throw new ArgumentNullException("log");
             if (string.IsNullOrWhiteSpace(name)) throw new ArgumentNullException("name");
             var t = new Tuple<string, ILogWriter, bool>(name, log, isDefault);
             _logs.Add(t);
         }

        public static void Remove(string name)
        {
            _logs.RemoveAll(l => l.Item1 == name);
        }

        static List<Tuple<string,ILogWriter,bool>> _logs= new List<Tuple<string, ILogWriter, bool>>(2);
        /// <summary>
        /// Returns the log matching the name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static ILogWriter GetLogger(string name)
        {
            var t= _logs.Find(d => d.Item1 == name);
            if (t == null) throw new KeyNotFoundException("There isn't a log registered with this name");
            return t.Item2;
        }

        /// <summary>
        /// Gets the default registered log or the first registered log.
        /// If no logger is defined, a null logger is returned
        /// </summary>
        public static ILogWriter DefaultLogger
        {
            get
            {
                if (_logs.Count==1)
                {
                    return _logs[0].Item2;
                }
                var t = _logs.Find(d => d.Item3);
                if (t==null)
                {
                    if (_logs.Count>0)
                    {
                        return _logs[0].Item2;
                    }

                }
                return NullLogger.Instance;
            }
        }


        /// <summary>
        /// Sets the default logger to be consoler. 
        /// Logger name is "console"
        /// </summary>
        public static void OutputToConsole()
        {
            Register(new ConsoleLogger(), "console",true);
        }
      
        /// <summary>
        /// Sends all the logging to the writer.
        /// Logger name is "devel"
        /// </summary>
        /// <param name="writer"></param>
        public static void OutputTo(Action<string> writer)
        {
            Register(new DeveloperLogger(writer),"devel"); 
        }
    }
}