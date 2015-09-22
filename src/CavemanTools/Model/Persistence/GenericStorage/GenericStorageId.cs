namespace CavemanTools.Model.Persistence.GenericStorage
{
    public class GenericStorageId
    {

        public GenericStorageId(string entityId):this(DefaultPartition,entityId)
        {
            
        }

        public GenericStorageId(string partitionId, string entityId)
        {
            EntityId = entityId;
            PartitionId = partitionId;
        }

        public const string DefaultPartition = "_";
        public String250 PartitionId { get; set; }

        public String250 EntityId { get; set; }
        public IdempotencyId Idempotency { get; set; }=IdempotencyId.Empty;
       
    }
}