using System;

namespace CavemanTools.Model.Persistence
{
    public class IdempotencyId
    {
        public static IdempotencyId Empty=new IdempotencyId() ;
        public bool IsEmpty() => OperationId == Guid.Empty && Hash==null;

        public Guid OperationId { get; set; }
        /// <summary>
        /// Its actually an identifier of the entity/model/event involved
        /// </summary>
        public String250 Hash { get; set; }

        public IdempotencyId(Guid operationId,string hash)
        {
            hash.MustNotBeEmpty();
            OperationId = operationId;
            Hash = hash;
        }

        private IdempotencyId()
        {
            
        }
    }
}