namespace CavemanTools.Model.Persistence
{
    public interface ICreateStorage
    {
        void Create();
    }

    public interface ICreateRelationStorage : ICreateStorage
    {
        void Create(string schema);
    }
}