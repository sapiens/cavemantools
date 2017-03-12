using System;

namespace CavemanTools.Model.Persistence.UniqueStore
{
    public class UniqueStoreDeleteItem
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entityId"></param>
        /// <param name="aspect">aspect of the data the value refers to. Eg: name, email</param>
        public UniqueStoreDeleteItem(Guid entityId,string aspect=UniqueValue.DefaultAspect)
        {
            EntityId = entityId;
            Aspect = aspect;
        }

        public Guid EntityId { get;  }
        /// <summary>
        ///  Gets aspect of the data the value refers to. Eg: name, email
        /// </summary>
        public string Aspect { get;  }

    }
}