namespace System
{
	/// <summary>
	/// Generic event args
	/// </summary>
	/// <typeparam name="T"></typeparam>
	
	public class EventArgs<T> : EventArgs
		{
			public EventArgs(T value)
			{
				m_value = value;
			}

			private T m_value;

			public T Value
			{
				get { return m_value; }
			}
		}
	
}
