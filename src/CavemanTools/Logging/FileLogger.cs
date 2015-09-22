using System;
using System.IO;

namespace CavemanTools.Logging
{
    
    public class FileLogger:LogWriterBase
    {
        private readonly string _filename;

        public FileLogger(string filename)
        {
            _filename = filename;
        }


        /// <summary>
        /// Should return the real logger implementation
        /// </summary>
        /// <typeparam name="T">Logger type</typeparam>
        /// <returns/>
        public override T GetLogger<T>()
        {
            throw new NotImplementedException();
        }


        public override void LogException(string source, LogLevel level, Exception ex, string context, params object[] args)
        {
            throw new NotImplementedException();
        }

        public override void Log(string source, LogLevel level, string message, params object[] args)
        {
            var data = (DateTime.Now.ToString() + " - " + source + level.ToString() + ": " + message.ToFormat(args));
            File.AppendAllText(_filename, data);  
        }
    }
}