using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreDeleteItem
    {
   
        public UniqueStoreDeleteItem(Guid entityId,string aspect=UniqueValue.DefaultAspect)
        {
            EntityId = entityId;
            Aspect = aspect;
        }

        public Guid EntityId { get;  }
        public string Aspect { get;  }

    }
}