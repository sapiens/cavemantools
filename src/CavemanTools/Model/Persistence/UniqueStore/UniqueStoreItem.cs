using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
   


    public class UniqueStoreItem
    {
        public UniqueStoreItem(Guid entityId, string uniqueName, Guid operationId)
        {
            EntityId = entityId;
            UniqueName = uniqueName;
            OperationId = operationId;
        }

        public Guid EntityId { get; set; }
        public String250 UniqueName { get; set; }
      
        public Guid OperationId { get; set; }

        public IdempotencyId ToIdempotencyId()
        {
            return new IdempotencyId(OperationId,"ustore"+(EntityId+UniqueName).MurmurHash().ToBase64());
        }
         
    }
}