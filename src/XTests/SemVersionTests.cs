using System.Data;
using CavemanTools;
using FluentAssertions;
using Xunit;
using System;
using System.Diagnostics;
using Xunit.Extensions;

namespace XTests
{
    public class SemVersionTests
    {
        private Stopwatch _t = new Stopwatch();

        public SemVersionTests()
        {

        }

        [Theory]
        [InlineData("alpha","alpha.1",-1)]
        [InlineData("alpha.2","alpha.1",1)]
        [InlineData("alpha.2.1","alpha.1",1)]
        [InlineData("alpha.2.2","alpha.2.1",1)]
        [InlineData("alpha","alpha",0)]
        [InlineData("alpha","beta",-1)]
        [InlineData("build","build.b8f12",-1)]
        public void semantic_token_comparison(string first,string second,int result)
        {
            var tk = new SemanticToken(first);
            var tk2 = new SemanticToken(second);
            Assert.Equal(result,tk.CompareTo(tk2));
        }

        [Theory]
        [InlineData("1.0.0-rc.1", "1.0.0-rc.1+build.1", -1)]
        [InlineData("1.0.0-alpha", "1.0.0-alpha.1",-1)]
        [InlineData("0.0.0-alpha", "0.0.0",-1)]
        [InlineData("1.0.0", "1.0.0",0)]
        [InlineData("1.1.0", "1.1.0",0)]
        [InlineData("1.0.0-beta.11", "1.0.0-rc.1", -1)]
        [InlineData("1.0.0-rc.1+build.1", "1.0.0 ", -1)]
        [InlineData("1.0.0", "1.0.0+0.3.7", -1)]
        [InlineData("1.3.7+build.2.b8f12d7", "1.3.7+build.11.e0f985a", -1)]
        public void semantic_version_comparison(string first,string second,int result)
        {
            Write("Comparing {0} to {1}. Expecting {2}",first,second,result);
            var v1 = new SemanticVersion(first);
            var v2 = new SemanticVersion(second);
            var rez = v1.CompareTo(v2);
            Write(rez.ToString());
            Assert.Equal(result,rez);
        }

        [Fact]
        public void parsing_1_1_0_is_correct()
        {
            var version = new SemanticVersion("1.1.0");
            version.ToString().Should().Be("1.1.0");
        }

        [Fact]
        public void semantic_from_version()
        {
            var version = new Version("1.0.0.23");
            var sem = new SemanticVersion(version);
            var sem2 = new SemanticVersion("1.0.0");
            Assert.Equal(sem2,sem);
        }

        private void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}