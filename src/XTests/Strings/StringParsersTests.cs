using System;
using System.Text;

using Xunit;

namespace XTests.Strings
{
	public class StringParsersTests
	{
		public StringParsersTests()
		{

		}

		

		[Fact]
		public void TimeSpan_Try_Parser()
		{
			var g = new GenericStringParser<TimeSpan>();
			TimeSpan tm;
			Assert.False(g.TryParse("2#12:00",out tm));			
		}
	}
}