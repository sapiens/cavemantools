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
        public string PartitionId { get; set; }

        public string EntityId { get; set; }
        public IdempotencyId Idempotency { get; set; }=IdempotencyId.Empty;
       
    }
}