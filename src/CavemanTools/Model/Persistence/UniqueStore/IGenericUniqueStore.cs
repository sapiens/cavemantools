using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public interface IGenericUniqueStore
    {
        bool Add(UniqueStoreItem item);
        void Delete(Guid entityId);
        void Delete(String250 uniqueHash);
        bool Update(UniqueStoreUpdateItem item);
    }
}