using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreDuplicateException : Exception
    {
        public UniqueStoreDuplicateException()
        {
        }

        public UniqueStoreDuplicateException(string message) : base(message)
        {
        }
    }
}