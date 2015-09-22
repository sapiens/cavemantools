using System;

namespace CavemanTools
{
    public interface IUnitOfWork:IDisposable
    {
        void Commit();
        string Tag { get; set; }
    }
}