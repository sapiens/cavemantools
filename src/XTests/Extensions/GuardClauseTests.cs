using Xunit;
using System;
using System.Diagnostics;
using FluentAssertions;

namespace XTests.Extensions
{
    public class GuardClauseTests
    {
        private Stopwatch _t = new Stopwatch();

        public GuardClauseTests()
        {

        }

        [Fact]
        public void argument_complies_with_condition()
        {
            2.MustComplyWith(i=>i<3," argument must be <3");
        }

        [Fact]
        public void argument_doesnt_comply_with_condition()
        {
            Assert.Throws<ArgumentException>(() => 2.MustComplyWith(i => i > 3, "Argument must > 3"));
        }

        protected void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}