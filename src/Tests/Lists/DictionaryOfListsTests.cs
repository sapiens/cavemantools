using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Lists
{
	public class DictionaryOfListsTests
	{
		private const string TestValue = "test1";
		private readonly DictionaryOfLists<int, string> _sut;

		public DictionaryOfListsTests()
		{
			_sut = new DictionaryOfLists<int, string>();
		}
		[Fact]
		public void add_get_item()
		{
			_sut.AddValue(2, TestValue);
			var res = _sut[2];
			res.Count.Should().Be(1);
			res[0].Should().Be(TestValue);
		}
				  
		[Fact]
		public void add_many_items_get
			()
		{
			_sut.AddValue(2, TestValue);
			var res = _sut[2];
			res.Count.Should().Be(1);
			res[0].Should().Be(TestValue);
		}


		[Fact]
		public void init_with_collection()
		{			
			var sut = new DictionaryOfLists<int, string>(new[] { KeyValuePair.Create(2, new[] {TestValue}) });
			var res = sut[2];
			res.Count.Should().Be(1);
			res[0].Should().Be(TestValue);
		}
	}
}
