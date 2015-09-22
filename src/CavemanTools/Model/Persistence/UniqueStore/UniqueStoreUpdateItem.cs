using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreUpdateItem:UniqueStoreItem
    {
        public UniqueStoreUpdateItem(String uniqueName, Guid operationId, string oldUniqueName) : base(Guid.Empty, uniqueName, operationId)
        {
            OldUniqueName = oldUniqueName;
        }

        public String250 OldUniqueName { get; set; }

       
    }
}