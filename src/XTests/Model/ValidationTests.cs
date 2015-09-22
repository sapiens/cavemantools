using CavemanTools.Model.Validation;
using CavemanTools.Model.Validation.Attributes;
using Xunit;

namespace XTests.Model
{
	public class ValidationTests
	{
		public ValidationTests()
		{

		}
		[Fact]
		public void ValidateForModel()
		{
			var value = "";
			var state = new DefaultValidationWrapper();
			Assert.True(state.IsValid);
			
			value.ValidateFor<MyTest>(d=>d.Body,state);
			Assert.False(state.IsValid);
			Assert.Equal("Req", state["Body"]);
			
		}

		[Fact]
		public void ValidateForModelWithoutAttributes()
		{
			var value = 2;
			var state = new DefaultValidationWrapper();
			Assert.True(state.IsValid);

			value.ValidateFor<MyTest>(d => d.Id, state);
			Assert.True(state.IsValid);
			Assert.NotEqual("Req", state["Body"]);

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

	public class MyTest
	{
		[Required(ErrorMessage = "Req")]
		public string Body { get; set; }
		
		public int Id { get; set;}
	}

	
}