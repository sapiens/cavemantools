namespace CavemanTools
{
    public interface ISupportMemento<T> where T:class,new()
    {
        T GetMemento();
    }
}