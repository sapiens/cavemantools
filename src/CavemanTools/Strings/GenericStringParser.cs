namespace System.Text
{
	/// <summary>
	/// Generic string parser class
	/// </summary>
	/// <typeparam name="T">Type to parse string to</typeparam>
	public class GenericStringParser<T> : IStringParser<T>
	{
		public T Parse(string value)
		{
			return value.ConvertTo<T>();
		}

		public bool TryParse(string text, out T value)
		{
			value = default(T);
			try
			{
				value = text.ConvertTo<T>();
				return true;
			}
			catch(InvalidCastException)
			{
				return false;
			}

			catch(FormatException)
			{
				return false;
			}
		}
	}
}