using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
   


    public class UniqueStoreItem
    {
        public UniqueStoreItem(Guid entityId, UniqueValue uniqueValue, Guid operationId)
        {
            entityId.MustNotBeDefault();
            operationId.MustNotBeDefault();
            uniqueValue.MustNotBeNull();

            EntityId = entityId;
            Unique = uniqueValue;
            OperationId = operationId;
        }

        protected UniqueStoreItem()
        {
            
        }

        public Guid EntityId { get; protected set; }

        public UniqueValue Unique { get; protected set; }
      
        public Guid OperationId { get; protected set; }

        public IdempotencyId ToIdempotencyId()
        {
            return new IdempotencyId(OperationId,"ustore"+(EntityId+Unique.Aspect+Unique.Value).MurmurHash().ToBase64());
        }
         
    }

    public class UniqueValue
    {
        public const string DefaultAspect = "name";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value">Value that needs to be unique</param>
        /// <param name="aspect">What aspect of the concept the value refers to. Eg: name, email</param>
        /// <param name="scope">Scope where the value should be unique</param>
        public UniqueValue(string value,string aspect= DefaultAspect, string scope="[none]")
        {
            aspect.MustNotBeEmpty();
            value.MustNotBeEmpty();
            scope.MustNotBeEmpty();
            Scope = scope;
            Value = value;
            Aspect = aspect;
        }

        public string Aspect { get;  }
        public string Scope { get;  }
        public string Value { get; }
    }
}