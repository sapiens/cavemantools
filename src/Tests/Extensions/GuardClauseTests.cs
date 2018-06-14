using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace Tests.Extensions
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
            2.Must(i=>i<3," argument must be <3");
        }

        [Fact]
        public void argument_doesnt_comply_with_condition()
        {
            Assert.Throws<ArgumentException>(() => 2.Must(i => i > 3, "Argument must > 3"));
        }

        [Fact]
        public void must_be_at_least_3()
        {
            5.MustBeAtLeast(1);
            (-1).Invoking(d => d.MustBeAtLeast(3)).Should().Throw<ArgumentException>();
        }

        [Fact]
        public void must_be_greater_than_0()
        {
            4.MustBeGreaterThan0();
            0.Invoking(d => d.MustBeGreaterThan0()).Should().Throw<ArgumentException>();
        }
    }
}