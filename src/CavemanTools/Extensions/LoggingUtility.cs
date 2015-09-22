namespace CavemanTools.Logging
{
	/// <summary>
	/// Contains extensions methods for log writing
	/// </summary>
	public static class LoggingUtility
	{
		/// <summary>
		/// Writes DEBUG entry in log if logger is not null.
		/// If null it does nothing
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="text">Log Entry Text</param>
		public static void Write(this ILogWriter logger, string text)
		{
			Write(logger,LogLevel.Debug,text);
		}

		/// <summary>
		/// Writes and formats DEBUG entry in log if logger is not null.
		/// If null it does nothing
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="text">Log Entry Format Text</param>
		/// <param name="args">Format params</param>
		public static void Write(this ILogWriter logger, string text, params object[] args)
		{
			Write(logger,LogLevel.Debug,text,args);
		}
		
		/// <summary>
		/// Writes entry in log if logger is not null.
		/// If null it does nothing
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="text">Log Entry Text</param>
		/// <param name="level">Log Level</param>
		public static void Write(this ILogWriter logger, LogLevel level, string text)
		{
			if (logger==null) return;
			logger.Log(level,text);
		}

		/// <summary>
		/// Writes and formats entry in log if logger is not null.
		/// If null it does nothing
		/// </summary>
		/// <param name="logger"></param>
		/// <param name="text">Log Entry Format Text</param>
		/// <param name="level">Log Level</param>
		/// <param name="args">Format params</param>
		public static void Write(this ILogWriter logger, LogLevel level, string text,params object[] args)
		{
			if (logger == null) return;
			logger.Log(level, text,args);
		}

		#region Old
		///// <summary>
		///// Writes debug message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Debug(this ILogWriter logger,string message)
		//{
		//    if (logger==null) return;
		//    logger.WriteDebug(message);
		//}

		///// <summary>
		///// Writes debug message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Debug(this ILogWriter logger, string message,params object[] args)
		//{
		//    if (logger == null) return;
		//    logger.WriteDebug(message,args);
		//}

		///// <summary>
		///// Writes warn message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Warn(this ILogWriter logger, string message)
		//{
		//    if (logger == null) return;
		//    logger.WriteWarning(message);
		//}

		///// <summary>
		///// Writes warn message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Warn(this ILogWriter logger, string message, params object[] args)
		//{
		//    if (logger == null) return;
		//    logger.WriteWarning(message, args);
		//}

		///// <summary>
		///// Writes info message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Info(this ILogWriter logger, string message)
		//{
		//    if (logger == null) return;
		//    logger.WriteInfo(message);
		//}



		///// <summary>
		///// Writes warn message in log if logger is not null.
		///// If null it ignores the message
		///// </summary>
		///// <param name="logger"></param>
		///// <param name="message"></param>
		//public static void Info(this ILogWriter logger, string message, params object[] args)
		//{
		//    if (logger == null) return;
		//    logger.WriteInfo(message, args);
		//} 
		#endregion
	}
}