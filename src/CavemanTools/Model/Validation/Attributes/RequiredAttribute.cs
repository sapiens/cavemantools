using System;
using System.ComponentModel.DataAnnotations;


namespace CavemanTools.Model.Validation.Attributes
{
	///<summary>
	/// Specify that a (non-empty) or blanc value is provided.
	///</summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field|AttributeTargets.Class,Inherited = true)]
	public class RequiredAttribute:ValidationAttribute
	{
		private string _defaultError = "Field '{0}' is required";

		
		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrWhiteSpace(t)) return false;
			return true;
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