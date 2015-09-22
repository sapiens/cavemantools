using System;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace CavemanTools.Model.Validation.Attributes
{
	/// <summary>
	/// Validates value according to regular expression.
	/// </summary>
	[AttributeUsage(AttributeTargets.Property|AttributeTargets.Field | AttributeTargets.Class,Inherited = true)]
	public class RegularExpressionAttribute:ValidationAttribute
	{
		private string _defaultError = "Invalid format for the field '{0}'";

		/// <summary>
		/// 
		/// </summary>
		/// <exception cref="ArgumentNullException"></exception>
		/// <param name="pattern">Regex</param>
		public RegularExpressionAttribute(string pattern)
		{
			if (string.IsNullOrEmpty(pattern)) throw new ArgumentNullException("pattern");
			Pattern = pattern;
		}

		/// <summary>
		/// Gets the pattern for validation. 
		/// </summary>
		public string Pattern { get;private set; }


		public override bool IsValid(object value)
		{
			var t = value as string;
			if (string.IsNullOrEmpty(t)) return true;
			t = t.Trim();
			return Regex.IsMatch(t,Pattern);
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