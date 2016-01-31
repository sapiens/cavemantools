using System;
using System.Diagnostics;
using Xunit;

namespace Tests.Model
{
    public class TagTests
    {
        private Stopwatch _t = new Stopwatch();

        public TagTests()
        {

        }

        [Fact]
        public void create_3_tags_from_string()
        {
            var tags = "test1, fe 322, hah-1 ,f?";
            var all = Tag.CreateFrom(tags).ToArray();
            all[0].Value.Should().Be("test1");
            all[1].Value.Should().Be("fe 322");
            all[2].Value.Should().Be("hah-1");
            all.Count().Should().Be(3);
        }

        protected void Write(object format, params object[] param)
        {
            Console.WriteLine(format.ToString(), param);
        }
    }
}