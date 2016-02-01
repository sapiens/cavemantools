using System;
using System.Linq;
using CavemanTools.Logging;
using CavemanTools.Testing;
using Xunit;

namespace Tests
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
            b.GetResults.ForEach(d=>this.LogDebug(d.ToString()));
          
            //b.Execute();
            //b.GetResults.ForEach(Console.WriteLine);
        }

    }
}