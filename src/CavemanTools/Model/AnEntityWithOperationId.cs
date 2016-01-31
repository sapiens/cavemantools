using System;

namespace CavemanTools.Model
{
    public abstract class AnEntityWithOperationId : IOperationId
    {
        protected Guid? _operationId;
        public virtual void SetOperationId(Guid id)
        {
            _operationId = id;
        }

        public Guid GetOperationId()
        {
            if (_operationId == null)
            {
                throw new OperationIdNotSetException();
            }
            return _operationId.Value;
        }

    }
}