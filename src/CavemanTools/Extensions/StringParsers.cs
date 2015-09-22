using System.Globalization;
using System.Text;

namespace System
{
	public static class StringParsers
	{
		/// <summary>
		/// Parse string into object of type. Returns the default value if not successful.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="value">String</param>
		/// <returns></returns>
		public static T Parse<T>(this string value)
		{
			return Parse(value, default(T));
		}

		/// <summary>
		/// Parse string into object of type using the provided parser
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="value">String</param>
		/// <param name="parser">Parser for the object</param>
		/// <returns></returns>
		public static T Parse<T>(this string value,IStringParser<T> parser)
		{
			if (parser == null) throw new ArgumentNullException("parser");
			return parser.Parse(value);
		}


		/// <summary>
		/// Parse string into object of type. Returns the provided value if not successful.
		/// </summary>
		/// <typeparam name="T">Type</typeparam>
		/// <param name="value">String</param>
		/// <param name="defaultValue">Value to return if conversion fails</param>
		/// <returns></returns>
		public static T Parse<T>(this string value, T defaultValue)
		{
			if (string.IsNullOrEmpty(value)) return defaultValue;
			try
			{
				return (T)Convert.ChangeType(value, typeof(T), CultureInfo.InvariantCulture);
			}
			catch
			{
				return defaultValue;
			}
		}

	}
}