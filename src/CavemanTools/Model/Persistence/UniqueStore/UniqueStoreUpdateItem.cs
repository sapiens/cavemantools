using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreUpdateItem:UniqueStoreItem
    {
        public UniqueStoreUpdateItem(Guid entityId, string oldValue, string value, Guid operationId, string aspect=UniqueValue.DefaultAspect)
        {
            oldValue.MustNotBeEmpty();
            entityId.MustNotBeDefault();
            operationId.MustNotBeDefault();

            EntityId = entityId;
            OperationId = operationId;
            Unique=new UniqueValue(value,aspect);
            OldValue = oldValue;
        }

        public string OldValue { get;  }

       
    }
}