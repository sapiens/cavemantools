using System;
using System.Diagnostics;
using CavemanTools.Model;
using Xunit;

namespace Tests
{
    public class PaginationTests
    {
        private Stopwatch _t = new Stopwatch();

        public PaginationTests()
        {

        }

        [Theory]
        [InlineData(1,0)]
        [InlineData(2,10)]
        public void skips_from_page(int page,long skip)
        {
            var p = new Pagination(page,10);
            Assert.Equal(skip,p.Skip);
        }

        [Fact]
        public void page_from_skips()
        {
            var p = Pagination.Create(12, 5);
            Assert.Equal(3,p.Page);
        }

        protected void Write(string format, params object[] param)
        {
            Console.WriteLine(format, param);
        }
    }
}