using System;
using System.Diagnostics;
using Xunit;

namespace XTests.Assertions
{
    public class AssertionsTests
    {
        private Stopwatch _t = new Stopwatch();

        public AssertionsTests()
        {

        }

        [Fact]
        public void require_null_throws_when_arg_is_null()
        {
            string f = null;
            Assert.Throws<ArgumentNullException>(() => f.MustNotBeNull("f"));            
        }

      
        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}