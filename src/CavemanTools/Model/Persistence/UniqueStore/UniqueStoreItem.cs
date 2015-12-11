using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
   


    public class UniqueStoreItem
    {
        public UniqueStoreItem(Guid entityId, UniqueValue uniqueValue, Guid operationId,string bucket=DefaultBucket)
        {
            entityId.MustNotBeDefault();
            operationId.MustNotBeDefault();
            uniqueValue.MustNotBeNull();
            bucket.MustNotBeEmpty();

            EntityId = entityId;
            Unique = uniqueValue;
            OperationId = operationId;
            Bucket = bucket;
        }

        protected UniqueStoreItem()
        {
            
        }

        public Guid EntityId { get; protected set; }

        public UniqueValue Unique { get; protected set; }
        public const string DefaultBucket = "_";
        /// <summary>
        /// For multi tenant support
        /// </summary>
        public string Bucket { get; set; } 

        public Guid OperationId { get; protected set; }

        public IdempotencyId ToIdempotencyId()
        {
            return new IdempotencyId(OperationId,"ustore"+(EntityId+Unique.Aspect+Unique.Value).MurmurHash().ToBase64());
        }
         
    }
}