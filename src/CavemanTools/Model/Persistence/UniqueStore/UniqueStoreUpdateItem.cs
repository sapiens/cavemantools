using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreUpdateItem
    {
        public UniqueStoreUpdateItem(Guid entityId, Guid operationId, params UniqueValueChange[] changes)
        {
            entityId.MustNotBeDefault();
            operationId.MustNotBeDefault();
            changes.MustNotBeEmpty();

            EntityId = entityId;
            OperationId = operationId;
            Changes = changes;
        }

        public Guid OperationId { get; }
        public UniqueValueChange[] Changes { get; }

        public Guid EntityId { get; }

      

        public IdempotencyId ToIdempotencyId() => new IdempotencyId(OperationId, "ustore" + EntityId);
    }
}