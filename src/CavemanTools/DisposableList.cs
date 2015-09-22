using System;
using System.Collections.Generic;

namespace CavemanTools
{
    public class DisposableList:List<IDisposable>,IDisposable
    {
        public void DisposeAll()
        {
            ForEach(d=>d.Dispose());
            Clear();
        }

        public void Dispose()
        {
            if (Count>0) DisposeAll();
        }
    }
}