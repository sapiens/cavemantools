using Xunit;

namespace Tests.Model
{
	public class ValidationTests
	{
		public ValidationTests()
		{

		}

        [Fact]
		public void ValidationBag()
		{
			var vb = new ValidationBag();
			Assert.False(vb.IsValid);
			vb.Add(false);
			vb.Add(true);
			Assert.False(vb.IsValid);

			vb= new ValidationBag(true);
			Assert.True(vb.IsValid);
			vb.Add(true);
			vb.Add(false);
			Assert.False(vb.IsValid);

			vb = new ValidationBag();
			vb.Add(true);
			Assert.True(vb.IsValid);

		}

	}

	
	
}