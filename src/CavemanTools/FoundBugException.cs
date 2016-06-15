namespace System
{
    public class FoundBugException:Exception
    {
        public FoundBugException()
        {
        }

        public FoundBugException(string message) : base(message)
        {
        }

        public FoundBugException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}