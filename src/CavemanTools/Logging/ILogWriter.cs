using System;

namespace CavemanTools.Logging
{
	/// <summary>
	/// Use to abstract the usage of an explicit logger (log4net, nlog etc)
	/// Implement it or extend the LogWriteBase.
	/// </summary>
	[Obsolete("Use IWriteToLog")]
    public interface ILogWriter
	{
	    /// <summary>
	    /// Should return the real logger implementation
	    /// </summary>
	    /// <typeparam name="T">Logger type</typeparam>
	    /// <returns></returns>
	    T GetLogger<T>() where T : class;
        void Info(string text);
		void Info(string format, params object[] args);
		/// <summary>
		/// Writes a log entry with the specified logging level
		/// </summary>
		/// <param name="level">Status</param>
		/// <param name="text">Entry Text</param>
		void Log(LogLevel level,string text);

		/// <summary>
		/// Writes a formatted log entry with the specified logging level
		/// </summary>
		/// <param name="level">Status</param>
		/// <param name="message">Entry Text</param>
		/// <param name="args">List of arguments</param>
		void Log(LogLevel level,string message, params object[] args);

	    void Trace(string message);
	    void Trace(string format,params object[] args);

	    void Error(string text);
	    void Error(string format, params object[] args);

	    void Warning(string text);
	    void Warning(string format, params object[] args);

	    void Debug(string mesage);
	    void Debug(string format, params object[] args);

	    
	}
}