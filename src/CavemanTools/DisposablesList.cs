using System;
using System.Collections.Generic;

namespace CavemanTools
{
	public class DisposablesList:List<IDisposable>,IDisposable
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