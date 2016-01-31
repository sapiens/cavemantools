using System;
using System.Diagnostics;
using Xunit;

namespace Tests
{

    class Test
    {
        public void Do(string t)
        {
           Console.Write(t);
        } 
    }
    public class General
    {
        private Stopwatch _t = new Stopwatch();
      
        public General()
        {
            
        }

     

    
        [Fact]
        public void exception_log()
        {
            LogManager.OutputToConsole();
            this.LogError(new InvalidOperationException("something"));
        }

        private void Write(object format, params object[] param)
        {
            Console.WriteLine(format.ToString(), param);
        }
    }  
}