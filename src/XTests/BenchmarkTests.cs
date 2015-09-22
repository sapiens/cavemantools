using System;
using System.Linq;
using System.Threading;
using CavemanTools.Testing;
using FluentAssertions;
using Xunit;

namespace XTests
{
    public class BenchmarkTests
    {
        public BenchmarkTests()
        {

        }

        [Fact]
        public void results_should_be_very_similar()
        {
            var b = new BenchmarksContainer();
           // b.Iterations = 2;
            b.Add(p => {  }, "nothing");
            b.Add(p => { }, "nothing 2");
            //b.Add(p =>
            //{

            //}, "caveman create");
            //b.Add((p) =>
            //{

            //}, "create");
           b.ExecuteWarmup();
            b.Execute();
            b.GetResults.ForEach(Console.WriteLine);
          
            //b.Execute();
            //b.GetResults.ForEach(Console.WriteLine);
        }

    }
}