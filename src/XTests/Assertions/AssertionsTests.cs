using Xunit;
using System;
using System.Diagnostics;

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

        [Fact]
        public void require_null_doesnt_throw_when_arg_is_not_null()
        {
            var f = "hey";
            Assert.DoesNotThrow(()=>f.MustNotBeNull());
        }

        [Fact]
        public void require_not_empty_throws_on_empty_or_whitespace_string()
        {
            var f = "     ";
            Assert.Throws<FormatException>(() => f.MustNotBeEmpty());

            Assert.DoesNotThrow(()=>"hey".MustNotBeEmpty());
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}