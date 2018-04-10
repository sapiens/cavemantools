using System;

namespace CavemanTools.Model
{
    public interface IOperationId
    {
        /// <summary>
        /// Used for idempotency purposes
        /// </summary>
        /// <param name="id"></param>
        void SetOperationId(Guid id);

        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="OperationIdNotSetException"></exception>
        /// <returns></returns>
        Guid GetOperationId();

    }
    
    
}