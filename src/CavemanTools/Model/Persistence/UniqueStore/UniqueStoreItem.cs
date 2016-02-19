using System;
using System.Linq;

namespace CavemanTools.Model.Persistence.UniqueStore
{
   


    public class UniqueStoreItem
    {
        public UniqueStoreItem(Guid entityId, Guid operationId, params UniqueValue[] uniqueValues)
        {
            entityId.MustNotBeDefault();
            operationId.MustNotBeDefault();
            uniqueValues.MustNotBeEmpty();

            AspectsMustHaveSameValue(uniqueValues);

            EntityId = entityId;
            Uniques = uniqueValues;
            OperationId = operationId;
        
        }

        /// <summary>
        /// We can't have different values for the same aspect
        /// </summary>
        /// <param name="uniqueValues"></param>
        private static void AspectsMustHaveSameValue(UniqueValue[] uniqueValues) 
            => uniqueValues.GroupBy(d=>d.Aspect)
            .ForEach(g=>g
                        .Select(d=>d.Value).Distinct().Count().MustBe(1,""));

        public Guid EntityId { get; protected set; }

        public UniqueValue[] Uniques { get; protected set; }
        public const string DefaultBucket = "_";

        /// <summary>
        /// For multi tenant support
        /// </summary>
        public string Bucket { get; set; } = DefaultBucket;

        public Guid OperationId { get; protected set; }

        public IdempotencyId ToIdempotencyId() => new IdempotencyId(OperationId,"ustore"+EntityId);
    }   

}