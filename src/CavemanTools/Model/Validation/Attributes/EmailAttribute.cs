using System;
using System.ComponentModel.DataAnnotations;
using CavemanTools.Strings;

namespace CavemanTools.Model.Validation.Attributes
{
	/// <summary>
	/// Validates a string as an email
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field | AttributeTargets.Class,Inherited = true)]
	public class EmailAttribute:ValidationAttribute
	{
		private string _defaultError = "Invalid email format for the field '{0}'";
		
		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrEmpty(t)) return true;
			return t.IsEmail();
		}

		public override string FormatErrorMessage(string name)
		{
			if (string.IsNullOrEmpty(ErrorMessage) && string.IsNullOrEmpty(ErrorMessageResourceName))
			{
				return string.Format(_defaultError, name);
			}
			return base.FormatErrorMessage(name);
		}
	}
}