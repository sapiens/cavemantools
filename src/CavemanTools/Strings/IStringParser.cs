namespace System.Text
{
	
	/// <summary>
	/// Provides functionality to parse a string into object
	/// </summary>
	/// <typeparam name="T">Type</typeparam>
	public interface IStringParser<T>
	{
		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="InvalidCastException"></exception>
		/// <param name="value"></param>
		/// <returns></returns>
		T Parse(string value);

		/// <summary>
		/// Tries to parse text into type. Returns true if successful
		/// </summary>
		/// <param name="text">Text to parse</param>
		/// <param name="value">Value or default of type</param>
		/// <returns></returns>
		bool TryParse(string text, out T value);
	}
}