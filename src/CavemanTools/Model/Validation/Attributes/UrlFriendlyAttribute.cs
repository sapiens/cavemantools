using System;
using System.ComponentModel.DataAnnotations;

namespace CavemanTools.Model.Validation.Attributes
{
	/// <summary>
	/// Checks if value can be used "as is" without escaping in a url
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field|AttributeTargets.Class,Inherited = true)]
	public class UrlFriendlyAttribute:ValidationAttribute
	{
		private string _defaultError = "Field '{0}' is not url friendly";

		
		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrWhiteSpace(t)) return true;
			var s = t.MakeSlug();
			return s.Equals(t,StringComparison.InvariantCultureIgnoreCase);
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