using System;
using System.Diagnostics;

using Xunit;
using Xunit.Abstractions;

namespace Tests
{

  
    public class General
    {
        private readonly ITestOutputHelper _h;
        private Stopwatch _t = new Stopwatch();
      
        public General(ITestOutputHelper h)
        {
            _h = h;
        }

     

    
        [Fact]
        public void exception_log()
        {
            _h.WriteLine("testing");
          
        }

       
    }  
}