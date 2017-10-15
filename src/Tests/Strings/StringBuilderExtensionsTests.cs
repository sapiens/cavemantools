﻿using System;
using System.Diagnostics;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Tests.Strings
{
    public class StringBuilderExtensionsTests
    {
        private Stopwatch _t = new Stopwatch();
        private StringBuilder _sb;

        public StringBuilderExtensionsTests()
        {
            _sb = new StringBuilder();
        }

        [Theory]
        [InlineData("abcd","cd","ab")]
        [InlineData("ab","abc","ab")]
        [InlineData("add bla, add","add","add bla, ")]
        [InlineData("","add","")]
        public void remove_last_if_equals_value(string init,string remove,string result)
        {
            _sb.Append(init);
            _sb.RemoveLastIfEquals(remove);
            Assert.Equal(result,_sb.ToString());
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }

    public class StringExtTests
    {
        [Fact]
        public void strip_string()
        {
            "12-vb22  ;".Strip('-', ' ', ';').Should().Be("12vb22");
        }
    }
}