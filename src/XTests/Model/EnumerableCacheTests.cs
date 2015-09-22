using System;
using System.Diagnostics;
using CavemanTools;
using FluentAssertions;
using Xunit;

namespace XTests.Model
{
    public class EnumerableCacheTests
    {
        private Stopwatch _t = new Stopwatch();
        private LimitedList<int> _sut;

        public EnumerableCacheTests()
        {
            _sut = new LimitedList<int>(2);
        }

        [Fact]
        public void cache_has_item()
        {
            _sut.Add(23);
            _sut.Contains(23).Should().BeTrue();
            _sut.Contains(2).Should().BeFalse();
        }

        [Fact]
        public void only_last_2_items_are_remembered()
        {
            _sut.Add(1);
            _sut.Add(2);
            _sut.Add(3);
            _sut.Add(4);
            _sut.Contains(1).Should().BeFalse();
            _sut.Contains(2).Should().BeFalse();
            _sut.Contains(3).Should().BeTrue();
            _sut.Contains(4).Should().BeTrue();
        }

        protected void Write(object format, params object[] param)
        {
            Console.WriteLine(format.ToString(), param);
        }
    }
}