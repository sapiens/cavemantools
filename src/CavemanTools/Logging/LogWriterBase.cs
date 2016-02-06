using System;

namespace CavemanTools.Logging
{
    public interface IHaveRealLogger
    {
        T GetLogger<T>() where T:class;
    }   
}