using System;
using System.Diagnostics;
using FluentAssertions;
using Xunit;

namespace XTests.Extensions
{
    public class ObjectExtensionsTests
    {
        private Stopwatch _t = new Stopwatch();

        public ObjectExtensionsTests()
        {

        }

        [Fact]
        public void convert_datetime_to_datetimeoffset()
        {
            var dt = new DateTime(2000, 1, 1);
            dt.ConvertTo<DateTimeOffset>().Should().Be(new DateTimeOffset(dt));
        }


        [Fact]
        public void convert_datetimeoffset_todatetime()
        {
            var dtoff = new DateTimeOffset(new DateTime(100, 1, 1));
            dtoff.ConvertTo<DateTime>().Should().Be(new DateTime(100, 1, 1));
        }

        [Fact]
        public void convert_string_to_datetimeoffset()
        {
            "200/1/1".ConvertTo<DateTimeOffset>().Year.Should().Be(200);
        }

        protected void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}