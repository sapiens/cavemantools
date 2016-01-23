using System.Linq;
using CavemanTools.Special;
using Xunit;

namespace XTests
{
	public class OverridableOptionsTests
	{

		public enum Sources
		{
			Default = 1,
			UserPrefs = 2,
			Request = 3
		}
		private OverridableOption<byte,string> _theme;
		public OverridableOptionsTests()
		{
			_theme = new OverridableOption<byte, string>(0, "default", v => v[v.Keys.Max()]);
			var t = new OverridableOption<Sources,string>(Sources.Default, "default", v => v[v.Keys.Max()]);
			t.FromSource(Sources.Request, "fr");
			t.FromSource(Sources.UserPrefs, "de");
			var b = t.Value;
		}

		[Fact]
		public void Null_Values_Return_Default()
		{
			_theme.FromSource(3,null);
			Assert.Equal("default",_theme.Value);
		}
		
		[Fact]
		public void Higher_Is_Priority()
		{
			_theme.FromSource(3,"lore");
			_theme.FromSource(1,"port");
			Assert.Equal("lore",_theme.Value);
			//theme: url,cookie,project,portfolio,default
			//language: url,cookie,project,portfolio,default
		}

	}
}