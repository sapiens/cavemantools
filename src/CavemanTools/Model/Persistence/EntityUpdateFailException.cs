using System;

namespace CavemanTools.Model.Persistence
{
    public class EntityUpdateFailException : Exception
    {
        public EntityUpdateFailException(string message = "Couldn't update the entity, it changes too fast. Concurrency is too high")
            : base(message)
        {
            
        }
    }
}