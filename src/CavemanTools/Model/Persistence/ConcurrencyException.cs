using System;

namespace CavemanTools.Model.Persistence
{
	public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
        {

        }

        public ConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
    
    
    
    