using System.Linq;
using CavemanTools.Model.Validation;
using CavemanTools.Model.Validation.Attributes;
using Xunit;

namespace XTests.Validation
{
	
	public class ValidationHelperTests
	{
		public ValidationHelperTests()
		{

		}

		[Fact]
		public void INvalid_Validator_Value_Shouldnt_Pass()
		{
			var svc = new ValidationService<User>();
			svc.AddValidation(u=>u.Name,new StringLengthAttribute(5));
			var us = new User(){Name = "123456"};
			var v = new DefaultValidationWrapper();
			svc.ValidateObject(us,v);
			Assert.False(v.IsValid);
		}
		[Fact]
		public void Valid_Validator_Value_Passes()
		{
			var svc = new ValidationService<User>();
			svc.AddValidation(u => u.Name, new StringLengthAttribute(5));
			var us = new User() { Name = "1456",Address = "f"};
			var v = new DefaultValidationWrapper();
			svc.ValidateObject(us,v);
			Assert.True(v.IsValid);
		}

		[Fact]
		public void Class_Has_Two_Attributes()
		{
			var svc = new ValidationService<User>();
			Assert.Equal(2, svc.GetClassValidators.Count);
		}

		[Fact]
		public void Class_Has_Email_Validator()
		{
			var svc = new ValidationService<User>();
			var em = svc.GetClassValidator<EmailAttribute>();
			Assert.Equal(1, em.Count());
			var email = em.First();
			Assert.Equal("test",email.ErrorMessage);
		}

		[Fact]
		public void Property_Has_Required_Validator()
		{
			var svc = new ValidationService<User>();
			var r=svc.GetValidators<RequiredAttribute>(d => d.Name);
			Assert.Equal(1,r.Count());
		}

		[Fact]
		public void Property_Has_2Validators()
		{
			var svc = new ValidationService<User>();
			Assert.Equal(2, svc.GetValidators(d => d.Name).Count());
		}

		[Fact]
		public void Change_Validator_message()
		{
			var svc = new ValidationService<User>();
			var req = svc.GetValidators<RequiredAttribute>(d => d.Name).First();
			req.ErrorMessage = "*";
			var bag = new DefaultValidationWrapper();
			svc.ValidateValueFor(d => d.Name, "", bag);
			Assert.Equal("*", bag["Name"]);
		}

		[Fact]
		public void Class_Validation_For_Value()
		{
			var svc = new ValidationService<User>();
			Assert.False(svc.ValidateValueForClass("bula", null));
		}

		[Fact]
		public void Validate_Invalid_T()
		{
			var u = new User() {Name = "2"};
			Assert.False(u.Validate(null));
		}

		[Fact]
		public void Valid_T()
		{
			var u = new User() { Name = "fr",Address = "f"};
			Assert.True(u.Validate(null));
		}


		[Fact]
		public void Service_Validate_T_Invalid()
		{
			var svc = new ValidationService<User>();
			var u = new User() { Name = "2" ,Address = "f"};
			Assert.False(svc.ValidateObject(u));
		}

		[Required]
		[Email(ErrorMessage = "test")]
		public class User
		{
			public int Id { get; set; }
			[Required]
			[MinimumLength(2)]
			public string Name { get; set; }

			[Required]
			public string Address {get; set; }
		}

	}
}