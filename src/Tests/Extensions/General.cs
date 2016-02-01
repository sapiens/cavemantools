using System;
using System.Diagnostics;
using System.Linq;
using CavemanTools.Logging;
using FluentAssertions;
using Xunit;

namespace Tests.Extensions
{
    public class General
    {
        private Stopwatch _t = new Stopwatch();

        public General()
        {

        }

        public class BlaController { }

        [Fact]
        public void substring_until()
        {
            "ItemsRow".SubstringUntil("Row").Should().Be("Items");
        }

        [Fact]
        public void date_enumeration()
        {
            var dt1 = new DateTime(2015, 2, 1);
            var dt2 = new DateTime(2015, 3, 1);
            var all = dt1.EnumerateTo(dt2).ToArray();
            all[0].Should().Be(dt1);
            all[all.Length - 1].Should().Be(dt2);
            all.ForEach(d=>this.LogDebug(d.ToString()));
            var inverted = dt2.EnumerateTo(dt1).ToArray();
            inverted[0].Should().Be(dt2);
            inverted[all.Length - 1].Should().Be(dt1);

        }

        [Fact]
        public void date_to_first_day()
        {
            var rez = new DateTime(2014, 12, 28).ToFirstDay();
            rez.Should().Be(new DateTime(2014, 12, 1));
        }

        [Fact]
        public void hmac()
        {
            var key = "key";
            var data = "{bla}";

            var h1 = data.Hmac256(key);
            this.LogDebug(h1);

            var h2 = "{bla".Hmac256(key);
            h2.Should().NotBe(h1);

            data.Hmac256("keY").Should().NotBe(h1);

            data.Hmac256(key).Should().Be(h1);

        }

    }
}