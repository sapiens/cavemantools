namespace CavemanTools.Model.Persistence.GenericStorage
{
    public interface IStoreEntities
    {
        void Add<T>(T data) where T : IGenericStorageId;
        /// <summary>
        /// 
        /// </summary>
        /// <exception cref="NewerVersionExistsException"></exception>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        void Save<T>(T data,bool ignoreConcurrency=false) where T : IGenericStorageId;
      
        T Get<T>(GenericStorageId id) where T : class, IGenericStorageId;

        void Delete(GenericStorageId id);
        void DeletePartition(String250 partitionId);
    }
}