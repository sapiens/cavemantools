using System;

namespace CavemanTools.Model.Persistence
{
    
    /// <summary>
    /// Used to wrap a specific storage exception
    /// </summary>
    public class PersistenceException:Exception
    {
        public PersistenceException(Exception inner,string msg=""):base(msg,inner)
        {
            
        }
    }
}