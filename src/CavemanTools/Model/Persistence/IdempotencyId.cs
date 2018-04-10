using System;

namespace CavemanTools.Model.Persistence
{
    /// <summary>
    /// When implementing a storage, the combination [OperationId,ModelIdentifier] should be unique.
    /// You can use GetStorableHash() to persist it.
    /// </summary>
    public class IdempotencyId
    {
        public static readonly IdempotencyId Empty = new IdempotencyId();
        public bool IsEmpty() => OperationId == Guid.Empty && ModelIdentifier == null;

        public Guid OperationId { get; set; }

        /// <summary>
        /// Its actually an identifier of the entity/model/event involved
        /// </summary>
        public string ModelIdentifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operationId"></param>
        /// <param name="modelIdentifier">Identifier of the entity/model/event involved</param>
        public IdempotencyId(Guid operationId, string modelIdentifier)
        {
            modelIdentifier.MustNotBeEmpty();
            OperationId = operationId;
            ModelIdentifier = modelIdentifier;
        }

        private IdempotencyId()
        {

        }

        /// <summary>
        /// Generates a hash (murmur3) that can be persisted. It should have a unique constraint
        /// </summary>
        /// <returns></returns>
        public string GetStorableHash() => (OperationId + ModelIdentifier).MurmurHash().ToBase64();

    }

    public class ConcurrencyException : Exception
    {
        public ConcurrencyException()
        {

        }

        public ConcurrencyException(string msg) : base(msg)
        {

        }
    }
}
    
    
    
    